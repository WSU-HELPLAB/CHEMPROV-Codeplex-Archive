﻿using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace ChemProV.UI
{
    public class CompoundTableData
    {
        private string elementName;

        public string ElementName
        {
            get { return elementName; }
        }
        private double quantity;

        public double Quantity
        {
            get { return quantity; }
        }

        public CompoundTableData(string elementName, double quantity)
        {
            this.elementName = elementName;
            this.quantity = quantity;
        }
    }
}
