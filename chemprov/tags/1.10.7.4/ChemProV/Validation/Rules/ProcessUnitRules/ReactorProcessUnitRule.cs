/*
Copyright 2010 HELP Lab @ Washington State University

This file is part of ChemProV (http://helplab.org/chemprov).

ChemProV is distributed under the Open Software License ("OSL") v3.0.
Consult "LICENSE.txt" included in this package for the complete OSL license.
*/
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

using ChemProV.PFD;
using ChemProV.PFD.ProcessUnits;
using ChemProV.PFD.Streams;
using ChemProV.PFD.Streams.PropertiesTable;
using ChemProV.PFD.Streams.PropertiesTable.Chemical;

using ChemProV.Validation;
using ChemProV.Validation.Rules.Adapters.Table;

namespace ChemProV.Validation.Rules.ProcessUnitRules
{
    /// <summary>
    /// This class inherients from GenericProcessUnitRule and overrides the TallyCompounds function and extends the CheckRule
    /// function so it also checks to make sure the units are in moles.
    /// </summary>
    public class ReactorProcessUnitRule : GenericProcessUnitRule
    {
        protected override ValidationResult CheckOverallFlowRate()
        {
            return ValidationResult.Empty;
        }

        /// <summary>
        /// This overrides the GenericProcessUnitRules tally function as this tallies the elements not the compounds themselves
        /// </summary>
        /// <param name="streams"></param>
        /// <returns></returns>
        protected override Dictionary<string, StreamComponent> TallyCompounds(IList<IStream> streams)
        {
            Dictionary<string, StreamComponent> compounds = new Dictionary<string, StreamComponent>(5);

            //tally up flow rates for each compound
            foreach (IStream stream in streams)
            {
                ITableAdapter tableAdapter = TableAdapterFactory.CreateTableAdapter(stream.Table);

                //start at index value = 1 as we're assuming that 1 is the header row, which we don't
                //check in this particular rule (see CheckOverallFlowRate())
                for (int i = 1; i < tableAdapter.GetRowCount(); i++)
                {
                    string compound = tableAdapter.GetCompoundAtRow(i);
                    string quantity = tableAdapter.GetQuantityAtRow(i);
                    string units = tableAdapter.GetUnitAtRow(i);

                    if (compound != null)
                    {
                        List<KeyValuePair<Element, int>> elements = CompoundFactory.GetElementsOfCompound(compound).elements;

                        //For the keyValuePair Element stores the name of the element and the int is the number of that
                        //perticular element in the compound example compound = water
                        //elements = <Hydrogren, 2>, <Oxygen, 1>
                        foreach (KeyValuePair<Element, int> element in elements)
                        {
                            if (!compounds.ContainsKey(element.Key.Name))
                            {
                                compounds[element.Key.Name] = new StreamComponent();
                                compounds[element.Key.Name].Name = element.Key.Name;
                            }

                            double numMoles;
                            if (units != "?")
                            {
                                try
                                {
                                    numMoles = double.Parse(quantity);

                                    compounds[element.Key.Name].AddValue(numMoles * element.Value, units);
                                }
                                catch
                                {
                                    compounds[element.Key.Name].AddValue(quantity, units);
                                }
                            }
                            else
                            {
                                //so dealing with percents
                                try
                                {
                                    double overalQuantity = double.Parse(tableAdapter.GetQuantityAtRow(0));
                                    double rowQuantity = double.Parse(quantity);

                                    compounds[element.Key.Name].AddValue((rowQuantity/100 * overalQuantity) * element.Value, units);

                                }
                                catch
                                {
                                    //if overal is question dunno what the rowQ is so set as ?.
                                    //if rowQuantity is ? then still dunno what is so set to ?
                                    compounds[element.Key.Name].AddValue("?", units);
                                }
                            }
                        }
                    }
                }
            }
            return compounds;
        }

        /// <summary>
        /// This overrides the base class CheckRule function to extend it to do CheckMoles as well
        /// </summary>
        public override void CheckRule()
        {
            ValidationResult result = ValidationResult.Empty;

            result = CheckMoles();
            if (!result.IsEmpty)
            {
                base.ValidationResults.Add(result);
                return;
            }

            //This calls the CheckRule from GenericProcessUnit and it returns to here.
            base.CheckRule();

            result = energyConservation();
            if (!result.IsEmpty)
            {
                base.ValidationResults.Add(result);
                return;
            }

        }

        private ValidationResult energyConservation()
        {
            double q = 0;
            double enthropyIncoming = enthropy(target.IncomingStreams, ref q);
            double enthropyOutgoing = enthropy(target.OutgoingStreams, ref q);

            double changeInEnthropy = enthropyOutgoing - enthropyIncoming;

            if (double.IsNaN(changeInEnthropy) || double.IsNaN(q))
            {
                return ValidationResult.Empty;
            }

            //see q it accounts for the difference

            if (changeInEnthropy != q)
            {
                List<IStream> feedbacktarget = new List<IStream>(target.IncomingStreams);
                feedbacktarget.AddRange(target.OutgoingStreams);
                return new ValidationResult(feedbacktarget, ErrorMessageGenerator.GenerateMesssage(ErrorMessages.Unconserved_Energy));
            }


            return ValidationResult.Empty;
        }

        private double enthropy(IList<IStream> streams, ref double q)
    {
            double enthropy = 0;

            foreach(IStream stream in streams)
            {
                int i = 0;
                ITableAdapter ta = TableAdapterFactory.CreateTableAdapter(stream.Table);
                if (ta.GetTableType() == TableType.Chemcial)
                {
                    while (i < ta.GetRowCount())
                    {
                        Compound c = CompoundFactory.GetElementsOfCompound(ta.GetCompoundAtRow(i));
                        double temp;
                        try
                        {
                            temp = double.Parse(ta.GetTemperature());
                        }
                        catch
                        {
                            temp = double.NaN;
                        }
                        enthropy += c.HeatFormation + c.HeatCapacity * (temp - 298) * ta.GetActuallQuantityAtRow(i);
                        i++;
                    }
                }
                else if (ta.GetTableType() == TableType.Heat)
                {
                    try
                    {
                        q = double.Parse(ta.GetQuantityAtRow(0));
                    }
                    catch
                    {
                        q = double.NaN;
                    }
                }
            }


            return enthropy;
    }

        /// <summary>
        /// This checks to make sure each unit is moles or moles per minute uses target which is from the base class
        /// </summary>
        /// <returns></returns>
        private ValidationResult CheckMoles()
        {
            ValidationResult result = ValidationResult.Empty;

            result = CheckMoles(target.IncomingStreams);

            if (result != ValidationResult.Empty)
            {
                return result;
            }
            else
            {
                return CheckMoles(target.OutgoingStreams);
            }
        }

        /// <summary>
        /// This is our helper function to CheckMoles it takes a list of streams and checks to make sure every row in each table is moles or moles per minute
        /// </summary>
        /// <param name="streams"></param>
        /// <returns></returns>
        private ValidationResult CheckMoles(IList<IStream> streams)
        {
            List<IStream> ruleBreakers = new List<IStream>();
            if (streams.Count > 0)
            {
                foreach (IStream stream in streams)
                {
                    ITableAdapter tableAdapter = TableAdapterFactory.CreateTableAdapter(stream.Table);
                    if (tableAdapter.GetTableType() == TableType.Chemcial)
                    {
                        string units = tableAdapter.GetUnitAtRow(0);
                        if (units != "moles" && units != "moles per minute")
                        {
                            ruleBreakers.Add(stream);
                        }
                    }
                }
            }
            else
            {
                //if we don't have any incoming or outgoing streams, return with no errors as 
                //we don't have anything to check
                return ValidationResult.Empty;
            }
            if (ruleBreakers.Count > 0)
            {
                return new ValidationResult(ruleBreakers, ErrorMessageGenerator.GenerateMesssage(ErrorMessages.Not_In_Moles));
            }
            else
            {
                return ValidationResult.Empty;
            }
        }
    }
}
