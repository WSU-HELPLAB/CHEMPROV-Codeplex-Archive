﻿using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

using ChemProV.PFD.EquationEditor.Tokens;
using ChemProV.Validation.Rules;
using ChemProV.Validation;

namespace ChemProV.PFD.EquationEditor
{
    public partial class EquationEditor : UserControl, IXmlSerializable
    {

        public object SelectedTool;

        /// <summary>
        /// A refernce, to a list of references, which themselves are a list of references of strings represeting the variable names used
        /// </summary>
        public ObservableCollection<Tuple<object, ObservableCollection<IEquationToken>>> EquationTokens = new ObservableCollection<Tuple<object, ObservableCollection<IEquationToken>>>();

        //private EquationValidationChecker variableNameExisitanceRule = EquationValidationChecker.GetInstance();

        public event EventHandler EquationTokensChanged = delegate { };

        public EquationEditor()
        {
            Equation eq = new Equation();
            InitializeComponent();

            //variableNameExisitanceRule.ListofEquationsFromEquations = EquationTokens;
            EquationTokens.CollectionChanged += new NotifyCollectionChangedEventHandler(EquationTokens_CollectionChanged);
            
            //this makes the first textbox listen for when it has been changed and when it is it makes a new textbox and sets that one to listen for the same thing
            RegisterEquationListeners(eq);
            eq.TextInputStart += new TextCompositionEventHandler(eq_TextInputStart);
            EquationTokens.Add(eq.EquationTokens);
            EquationStackPanel.Children.Add(eq);
            EquationScrollViewer.DataContext = this;
        }

        /// <summary>
        /// Will remove all existing equations currently listed in the equation editor.
        /// </summary>
        public void ClearEquations()
        {
            //remove event handlers
            foreach (UIElement element in EquationStackPanel.Children)
            {
                if (element is Equation)
                {
                    Equation eq = element as Equation;
                    UnregisterEquationListneres(eq);
                    
                }
            }

            //clear existing children
            EquationTokens.CollectionChanged -= new NotifyCollectionChangedEventHandler(EquationTokens_CollectionChanged);
            EquationTokens.Clear();
            EquationTokens.CollectionChanged += new NotifyCollectionChangedEventHandler(EquationTokens_CollectionChanged);
            this.EquationStackPanel.Children.Clear();

            //add new child
            Equation newEq = new Equation();

            //attach event listeners
            RegisterEquationListeners(newEq);
            newEq.TextInputStart += new TextCompositionEventHandler(eq_TextInputStart);

            //add to the list of equations
            EquationStackPanel.Children.Add(newEq);
            EquationTokens.Add(newEq.EquationTokens);
        }

        void EquationTokens_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            EquationTokensChanged(sender, new EventArgs());
        }

        /// <summary>
        /// Called whenever a user navigates away from an equation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void eq_LostFocus(object sender, RoutedEventArgs e)
        {
            Equation senderEq = sender as Equation;

            //if the sender is not the last equation in the list  and it empty, remove it
            if (senderEq.CompareTo(EquationStackPanel.Children.ElementAt(EquationStackPanel.Children.Count - 1)) != 0)
            {
                if (senderEq.EquationTextBox.Text.Trim().Length == 0)
                {
                    //remove from the stack panel
                    EquationStackPanel.Children.Remove(senderEq);
                    
                    //remove event hanlders
                    UnregisterEquationListneres(senderEq);

                    //remove from set of equation tokesn
                    EquationTokens.Remove(senderEq.EquationTokens);
                }
            }
        }

        void eq_EquationTokensChagned(object sender, EventArgs e)
        {
            EquationTokensChanged(sender, e);
        }

        /// <summary>
        /// This fires whenever a textbox has been edited
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void eq_TextInputStart(object sender, TextCompositionEventArgs e)
        {   
            Equation eq = new Equation();

            //this makes the textbox that just fired stop listenning since it is no longer the last one
            ((Equation)sender).TextInputStart -= new TextCompositionEventHandler(eq_TextInputStart);

            //this makes the new textbox start listening whenever it has been fired
            eq.TextInputStart += new TextCompositionEventHandler(eq_TextInputStart);
            RegisterEquationListeners(eq);
            EquationStackPanel.Children.Add(eq);
            EquationTokens.Add(eq.EquationTokens);
        }

        /// <summary>
        /// Created to consolidate all event listener attachments into a single location
        /// rather than having it spread all over the file.
        /// </summary>
        /// <param name="eq">The equation that we'd like to attach events to.</param>
        private void RegisterEquationListeners(Equation eq)
        {
            eq.EquationTokensChagned += new EventHandler(eq_EquationTokensChagned);
            eq.LostFocus += new RoutedEventHandler(eq_LostFocus);
        }

        /// <summary>
        /// The inverse of RegisterEquationListeners: unregisters all event listeners from
        /// a given equation
        /// </summary>
        /// <param name="eq"></param>
        private void UnregisterEquationListneres(Equation eq)
        {
            eq.LostFocus -= new RoutedEventHandler(eq_LostFocus);
            eq.EquationTokensChagned -= new EventHandler(eq_EquationTokensChagned);
            eq.TextInputStart -= new TextCompositionEventHandler(eq_TextInputStart);
        }

        #region IXmlSerializable Members

        public System.Xml.Schema.XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            //not responsible for reading of XML data.  Handled in LoadXmlElements.
        }

        public void WriteXml(XmlWriter writer)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Equation));

            writer.WriteStartElement("Equations");

            //loop through our list of equations
            foreach (Tuple<object, ObservableCollection<IEquationToken>> equationTuple in EquationTokens)
            {
                serializer.Serialize(writer, (equationTuple.Item1 as Equation));
            }
            writer.WriteEndElement();
        }

        #endregion

        public void LoadXmlElements(XElement doc)
        {
            //pull out the equations
            XElement equations = doc.Descendants("Equations").ElementAt(0);
            foreach (XElement xmlEquation in equations.Elements())
            {
                //create the equation
                Equation eq = new Equation();

                //attach event listeners
                RegisterEquationListeners(eq);

                //add to the list of equations
                EquationStackPanel.Children.Add(eq);
                EquationTokens.Add(eq.EquationTokens);

                //set the equation's value
                eq.EquationTextBox.Text = xmlEquation.Value.ToString();

                //tell object that an equation's text has changed
                eq_EquationTokensChagned(eq, EventArgs.Empty);
            }

            //the last equation added needs to have a special event listener attached
            (EquationStackPanel.Children.ElementAt(EquationStackPanel.Children.Count - 1) as Equation).TextInputStart += new TextCompositionEventHandler(eq_TextInputStart);
        }
    }
}
