/*
Copyright 2010 HELP Lab @ Washington State University

This file is part of ChemProV (http://helplab.org/chemprov).

ChemProV is distributed under the Open Software License ("OSL") v3.0.
Consult "LICENSE.txt" included in this package for the complete OSL license.
*/
using System;
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
    public class ConstantsTableData
    {
        private string constant;

        public string Constant
        {
            get { return constant; }
            set { constant = value; }
        }

        private string symbol;

        public string Symbol
        {
            get { return symbol; }
            set { symbol = value; }
        }

        public ConstantsTableData(string constant, string symbol)
        {
            this.constant = constant;
            this.symbol = symbol;
        }
    }
}
