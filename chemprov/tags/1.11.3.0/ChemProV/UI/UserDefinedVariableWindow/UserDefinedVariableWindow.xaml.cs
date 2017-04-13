/*
Copyright 2010 HELP Lab @ Washington State University

This file is part of ChemProV (http://helplab.org/chemprov).

ChemProV is distributed under the Open Software License ("OSL") v3.0.
Consult "LICENSE.txt" included in this package for the complete OSL license.
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Data;
using System.Text.RegularExpressions;
using System.Windows.Controls.Primitives;

using ChemProV.PFD.Streams.PropertiesWindow;
using ChemProV.PFD.EquationEditor;

namespace ChemProV.UI.UserDefinedVariableWindow
{
    public partial class UserDefinedVariableWindow : UserControl
    {
        private CustomDataGrid propertiesWindowGrid = new CustomDataGrid();

        private List<Tuple<string, Equation>> variableDictionary = new List<Tuple<string, Equation>>();

        public List<Tuple<string, Equation>> VariableDictionary
        {
            get { return variableDictionary; }
            set { variableDictionary = value; }
        }

        public UserDefinedVariableWindow()
        {
            InitializeComponent();

            GridHolder.Content = (propertiesWindowGrid.BaseGrid);

            propertiesWindowGrid.HideBordersForLastRow = false;
            propertiesWindowGrid.HideBordersForLastTwoColumns = false;
            UpdateTable();
        }

        public void UpdateTable()
        {
            propertiesWindowGrid.ClearAll();

            int i = 0;

            //this is the headers
            propertiesWindowGrid.PlaceUIElement(new TextBlock() {HorizontalAlignment=HorizontalAlignment.Center, Text = "Variable Name" }, 1, i);
            propertiesWindowGrid.PlaceUIElement(new TextBlock() {HorizontalAlignment=HorizontalAlignment.Center, Text = "Data" }, 2, i);
            i++;

            foreach (Tuple<string, Equation> keyPair in variableDictionary)
            {
                Button deleteRowButton = new Button() { Content = "-" };

                deleteRowButton.Width = 20;

                deleteRowButton.Click += new RoutedEventHandler(deleteRow_Click);

                propertiesWindowGrid.PlaceUIElement(deleteRowButton, 0, i);

                TextBox tb = new TextBox() { Text = keyPair.Item1 };

                tb.TextWrapping = TextWrapping.Wrap;
                tb.TextChanged += new TextChangedEventHandler(TextChanged);

                propertiesWindowGrid.PlaceUIElement(tb, 1, i);

                Equation eq = keyPair.Item2;

                propertiesWindowGrid.PlaceUIElement(eq, 2, i);
                i++;
            }

            Button addNewRowButton = new Button() { Content = "+"};

            addNewRowButton.Width = 20;

            addNewRowButton.Click += new RoutedEventHandler(addNewRow_Click);

            propertiesWindowGrid.PlaceUIElement(addNewRowButton, 0, i);

        }

        void TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tb = (sender as TextBox);

            string text = tb.Text;

            //minus 1 for header
            int index = (int)tb.Parent.GetValue(Grid.RowProperty) - 1;
            Regex forceAlphaAtStart = new Regex(@"^([a-z]|[A-Z])");
            Regex forceAlphaOrDigit = new Regex(@"^([a-z]|[A-Z]|[0-9])*$");

            if (forceAlphaAtStart.Match(text).Success == false)
            {
                if (text != "")
                {
                    MessageBox.Show("Variable Name Must Start With A Character");
                }
            }
            if (forceAlphaOrDigit.Match(text).Success == false)
            {
                MessageBox.Show("Variable Name Must Contain Only Characters Or Digits");
            }
            else
            {
                variableDictionary[index] = new Tuple<string, Equation>(tb.Text, variableDictionary[index].Item2);
            }
        }

        private void deleteRow_Click(object sender, RoutedEventArgs e)
        {
            Button tb = (sender as Button);

            //minus 1 for header
            int index = (int)tb.Parent.GetValue(Grid.RowProperty) - 1;

            variableDictionary.RemoveAt(index);

            UpdateTable();
        }

        private void addNewRow_Click(object sender, RoutedEventArgs e)
        {
            variableDictionary.Add(new Tuple<string, Equation>("", new Equation()));
            UpdateTable();
            Application.Current.RootVisual.SetValue(Control.IsEnabledProperty, true);
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            this.Window.IsOpen = false;
        }

        private void LayoutRoot_LayoutUpdated(object sender, EventArgs e)
        {
            if (LayoutRoot.RowDefinitions[0].ActualHeight != 0)
            {
                LayoutRoot.LayoutUpdated -= new EventHandler(LayoutRoot_LayoutUpdated);
                //we fix rows 0 and 2 and allow 1 to fill the space
                LayoutRoot.RowDefinitions[0].MinHeight = LayoutRoot.RowDefinitions[0].ActualHeight;
                LayoutRoot.RowDefinitions[0].MaxHeight = LayoutRoot.RowDefinitions[0].ActualHeight;
                LayoutRoot.RowDefinitions[2].MinHeight = LayoutRoot.RowDefinitions[2].ActualHeight;
                LayoutRoot.RowDefinitions[2].MaxHeight = LayoutRoot.RowDefinitions[2].ActualHeight;
                this.border.MinHeight = border.BorderThickness.Top + border.BorderThickness.Bottom + LayoutRoot.RowDefinitions[0].ActualHeight + LayoutRoot.RowDefinitions[2].ActualHeight;
                this.border.MinWidth = 150;
            }
        }
    }
}

