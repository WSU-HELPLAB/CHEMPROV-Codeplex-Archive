/*
Copyright 2010, 2011 HELP Lab @ Washington State University

This file is part of ChemProV (http://helplab.org/chemprov).

ChemProV is distributed under the Microsoft Reciprocal License (Ms-RL).
Consult "LICENSE.txt" included in this package for the complete Ms-RL license.
*/

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ChemProV.PFD;
using ChemProV.PFD.EquationEditor;
using ChemProV.UI;
using ChemProV.PFD.Streams.PropertiesWindow;
using ChemProV.Validation.Rules.Adapters.Table;
using ChemProV.Validation.Rules.ProcessUnitRules;

namespace ChemProV.Validation.Rules
{
    /// <summary>
    /// This class is responsible for checking all rules it needs to when passed a list of iprocessUnits or tables or equations. 
    /// And then reporting the errors in a list of strings
    /// </summary>
    public class RuleManager
    {
        public event EventHandler Solvable = delegate { };

        OptionDifficultySetting currentDifficultySetting = OptionDifficultySetting.MaterialBalance;

        public OptionDifficultySetting CurrentDifficultySetting
        {
            get { return currentDifficultySetting; }
            set { currentDifficultySetting = value; }
        }

        /// <summary>
        /// This class builds the ListOfFeedbackMessages so the MainPage has a list of the errors.
        /// </summary>
        public List<Tuple<object, string>> ListOfFeedbackMessages = new List<Tuple<object, string>>();

        private int ruleNumber = 1;

        /// <summary>
        /// This is built by buildFeedbackMessages this combines the equationDict, processUnit and TableDict.
        /// </summary>
        private Dictionary<object, List<string>> EveryoneDict = new Dictionary<object, List<string>>();

        private Dictionary<string, GenericTableData> tableDict = new Dictionary<string, GenericTableData>();

        private static RuleManager instance;

        /// <summary>
        /// This is called when we want to check the processUnits validity.  Then it calls buildFeedbackMessage so that,
        /// a a new EveryoneDict can be made with the new data.
        /// </summary>
        /// <param name="iProcessUnits">This is a list of all the iProcessUnits to be checked typicall all ProcessUnits</param>
        private void CheckProcessUnits(IEnumerable<ProcessUnitControl> iProcessUnits)
        {
            IRule rule;

            foreach (ProcessUnitControl ipu in iProcessUnits)
            {
                rule = ProcessUnitRuleFactory.GetProcessUnitRule(ipu);
                rule.Target = ipu;
                rule.CheckRule();
                foreach (ValidationResult vr in rule.ValidationResults)
                {
                    if (!EveryoneDict.ContainsKey(vr.Target))
                    {
                        EveryoneDict.Add(vr.Target, new List<string>());
                    }
                    if (!EveryoneDict[vr.Target].Contains(vr.Message))
                    {
                        EveryoneDict[vr.Target].Add("[" + ruleNumber + "]\n-" + vr.Message + "\n");
                        ruleNumber++;
                    }
                }
            }
        }

        /// <summary>
        /// This is called when we want to check the tables validity.  Then it calls buildFeedbackMessage so that,
        /// a a new EveryoneDict can be made with the new data.
        /// </summary>
        /// <param name="tables">This is a list of PropertiesWindow to be checked typically all of them</param>
        private void CheckChemicalStreamPropertiesWindowFeedback(IEnumerable<IPfdElement> tables)
        {
            IRule rule = new TableRule();

            List<string> nonUniqueNames = new List<string>();
            List<IPropertiesWindow> listOfTables = new List<IPropertiesWindow>();

            foreach (IPropertiesWindow table in tables)
            {
                rule.Target = table;
                rule.CheckRule();

                // TODO: fix (eventually)
                throw new NotImplementedException("Rule manager is broken");
                ITableAdapter tableAdapter = null;
                //ITableAdapter tableAdapter = TableAdapterFactory.CreateTableAdapter(table);
                int i = 0;
                int items = tableAdapter.GetRowCount();
                TableType tableType;
                string label, units, quantity, compound, temp;

                while (i < items)
                {
                    tableType = tableAdapter.GetTableType();
                    label = tableAdapter.GetLabelAtRow(i);
                    units = tableAdapter.GetUnitAtRow(i);
                    quantity = tableAdapter.GetQuantityAtRow(i);
                    compound = tableAdapter.GetCompoundAtRow(i);

                    if (currentDifficultySetting == OptionDifficultySetting.MaterialAndEnergyBalance)
                    {
                        temp = tableAdapter.GetTemperature();
                    }
                    else
                    {
                        //we dont need temp to just zero it out
                        temp = "0";
                    }

                    if (!tableDict.Keys.Contains(label))
                    {
                        tableDict.Add(label, new GenericTableData(table, tableType, label, units, quantity, compound, temp));
                    }
                    else
                    {
                        if (!nonUniqueNames.Contains(label))
                        {
                            nonUniqueNames.Add(label);
                        }
                        listOfTables.Add(table);
                    }
                    i++;
                }

                foreach (ValidationResult vr in rule.ValidationResults)
                {
                    if (!EveryoneDict.ContainsKey(vr.Target))
                    {
                        EveryoneDict.Add(vr.Target, new List<string>());
                    }
                    EveryoneDict[vr.Target].Add("[" + ruleNumber + "]\n-" + vr.Message + "\n");
                    ruleNumber++;
                }
            }
            if (nonUniqueNames.Count > 0)
            {
                ValidationResult vr = (new ValidationResult(listOfTables, ErrorMessageGenerator.GenerateMesssage(Validation.ErrorMessages.NonUniqueNames, nonUniqueNames.ToArray())));
                if (!EveryoneDict.ContainsKey(vr.Target))
                {
                    EveryoneDict.Add(vr.Target, new List<string>());
                }
                EveryoneDict[vr.Target].Add("[" + ruleNumber + "]\n-" + vr.Message + "\n");
                ruleNumber++;
            }
        }

        private void Equations_Solvable(object sender, EventArgs e)
        {
            Solvable(this, e);
        }

        /// <summary>
        /// This checks all the rules for all IPfdElements in pfdElements all the equations in equations
        /// </summary>
        /// <param name="pfdElements">list of all pfdElements that need to be checked</param>
        /// <param name="equations">list of all equations that need to be checked</param>
        public void Validate(IEnumerable<IPfdElement> pfdElements, IList<Tuple<string, EquationRowControl>> userDefinedVariables)
        {
            //clear out the dictionary before we begin adding new stuff
            EveryoneDict.Clear();
            tableDict.Clear();

            if (pfdElements != null)
            {
                //pull out process units from the list of pfd elements
                var processUnits = from c in pfdElements
                                   where c is ProcessUnitControl
                                   select c as ProcessUnitControl;

                //and properties tables
                var tables = from c in pfdElements
                             where c is IPropertiesWindow
                             select c;

                //run the rule checker

                //start at 1
                ruleNumber = 1;

                CheckProcessUnits(processUnits);
                CheckChemicalStreamPropertiesWindowFeedback(tables);

                // AC: Removing checks for equation correctness as it currently doesn't work right
                //     and it's better to have no messages over a bunch of false positives.
                //     I'm not removing the code block below because I'm not yet familiar enough
                //     with the code and worry that I wouldn't be able to reproduce the line.
                /*
                if (EveryoneDict.Count == 0)
                {
                    CheckEquationSemantics(equations, userDefinedVariables, processUnits.ToList());
                }
                 * */
            }
        }

        /// <summary>
        /// This returns a Dictionary which as its key is an object that broke one or more rule.
        /// The data is a List of strings which are the messages associated with the rules it broke
        /// </summary>
        public Dictionary<object, List<string>> ErrorMessages
        {
            get
            {
                return EveryoneDict;
            }
        }

        private RuleManager()
        {
        }

        /// <summary>
        /// Used to get at the single instance of this object
        /// </summary>
        /// <returns></returns>
        public static RuleManager GetInstance()
        {
            if (instance == null)
            {
                instance = new RuleManager();
            }
            return instance;
        }
    }
}