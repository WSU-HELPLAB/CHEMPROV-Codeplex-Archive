/*
Copyright 2010 HELP Lab @ Washington State University

This file is part of ChemProV (http://helplab.org/chemprov).

ChemProV is distributed under the Open Software License ("OSL") v3.0.
Consult "LICENSE.txt" included in this package for the complete OSL license.
*/
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

using ChemProV.PFD.Streams.PropertiesWindow;
using ChemProV.PFD;
using ChemProV.Validation;
using ChemProV.Validation.Rules.Adapters.Table;

namespace ChemProV.UI
{
    public partial class CompoundTable : UserControl
    {

        public CompoundTable()
        {
            InitializeComponent();
        }

        public event EventHandler ConstantClicked = delegate { };

        /// <summary>
        /// This is what updates the datagrid when the compound selection is changed
        /// </summary>
        /// <param name="sender">not used</param>
        /// <param name="e">not used</param>
        private void Compound_ComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            List<CompoundTableData> elements = new List<CompoundTableData>();
            List<ConstantsTableData> constants = new List<ConstantsTableData>();
            Compound compound;

            if (Compound_ComboBox.SelectedItem != null)
            {
                compound = CompoundFactory.GetElementsOfCompound((Compound_ComboBox.SelectedItem as string).ToLower());

                foreach (KeyValuePair<Element, int> element in compound.elements)
                {
                    elements.Add(new CompoundTableData(element.Key.Name, element.Value));
                }

                constants.Add(new ConstantsTableData("Cp (J/mol-C)", "Cp" + compound.Abbr));
                constants.Add(new ConstantsTableData("Hf (kJ/mol)", "Hf" + compound.Abbr));
                constants.Add(new ConstantsTableData("Hv (kJ/mol)", "Hv" + compound.Abbr));
                constants.Add(new ConstantsTableData("Tb (C)", "Tb" + compound.Abbr));
                constants.Add(new ConstantsTableData("Tm (C)", "Tm" + compound.Abbr));

                Compound_DataGrid.ItemsSource = elements;
                Constants_DataGrid.ItemsSource = constants;
            }

        }

        /// <summary>
        /// This is called when the pfd is changed not just when a compound is changed.  This gets a lit of ipfdElements
        /// then it pulls out of those the tables and then makes a list of the compounds which it then sets as the elements source
        /// to our combo_box
        /// </summary>
        /// <param name="ipfdElements"></param>
        public void UpdateCompounds(IEnumerable<IPfdElement> ipfdElements)
        {
            HashSet<string> usingCompounds = new HashSet<string>();
            int currentSelected = Compound_ComboBox.SelectedIndex;

            ITableAdapter tableAdapter;

            foreach (IPfdElement ipfd in ipfdElements)
            {
                if (ipfd is IPropertiesWindow)
                {
                    tableAdapter = TableAdapterFactory.CreateTableAdapter(ipfd as IPropertiesWindow);
                    int i = 0;
                    while (i < tableAdapter.GetRowCount())
                    {
                        string compound = tableAdapter.GetCompoundAtRow(i);
                        if (compound != "Select" && compound != "Overall")
                        {
                            if (!usingCompounds.Contains(compound))
                            {
                                usingCompounds.Add(compound);
                            }
                        }
                        i++;
                    }
                }
            }
            Compound_ComboBox.ItemsSource = new ObservableCollection<string>(usingCompounds);
            if (usingCompounds.Count > 0)
            {
                if (currentSelected > -1)
                {
                    Compound_ComboBox.SelectedIndex = currentSelected;
                }
                else
                {
                    Compound_ComboBox.SelectedIndex = 0;
                }
            }
            Compound_ComboBox_SelectionChanged(this, EventArgs.Empty as SelectionChangedEventArgs);


        }

        private void Constants_DataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            string constantColumnContents = ((Constants_DataGrid.ItemsSource as List<ConstantsTableData>)[e.Row.GetIndex()].Constant);
            string toolTipMessage = "";
            TextBlock cell = new TextBlock();
            switch (constantColumnContents)
            {
                case "Cp (J/mol-C)":
                    {
                        DataGridColumn elementColumn = Constants_DataGrid.Columns[0];
                        cell = elementColumn.GetCellContent(e.Row) as TextBlock;
                        toolTipMessage = String.Format("This represents the heat capacity for {0}.", Compound_ComboBox.SelectedItem.ToString()); ;
                    }
                    break;
                case "Hf (kJ/mol)":
                    {
                        DataGridColumn elementColumn = Constants_DataGrid.Columns[0];
                        cell = elementColumn.GetCellContent(e.Row) as TextBlock;
                        toolTipMessage = String.Format("This represents the heat of formation for {0}.", Compound_ComboBox.SelectedItem.ToString()); ;
                    }
                    break;
                case "Hv (kJ/mol)":
                    {
                        DataGridColumn elementColumn = Constants_DataGrid.Columns[0];
                        cell = elementColumn.GetCellContent(e.Row) as TextBlock;
                        toolTipMessage = String.Format("This represents the heat of vaporization for {0}.", Compound_ComboBox.SelectedItem.ToString()); ;
                    }
                    break;
                case "Tb (C)":
                    {
                        DataGridColumn elementColumn = Constants_DataGrid.Columns[0];
                        cell = elementColumn.GetCellContent(e.Row) as TextBlock;
                        toolTipMessage = String.Format("This represents the boiling point for {0}.", Compound_ComboBox.SelectedItem.ToString()); ;
                    }
                    break;
                case "Tm (C)":
                    {
                        DataGridColumn elementColumn = Constants_DataGrid.Columns[0];
                        cell = elementColumn.GetCellContent(e.Row) as TextBlock;
                        toolTipMessage = String.Format("This represents the melting point for {0}.", Compound_ComboBox.SelectedItem.ToString()); ;
                    }
                    break;
            }
            ToolTipService.SetToolTip(cell, toolTipMessage);
        }

        private void Constant_Symbol_Button_Click(object sender, RoutedEventArgs e)
        {
            ConstantClicked(sender, e);
        }
    }
}
