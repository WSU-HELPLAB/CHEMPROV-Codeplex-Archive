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

using ChemProV;

namespace ChemProV.UI
{
    public partial class OptionWindow : ChildWindow
    {
        public OptionDifficultySetting OptionSelection
        {
            get
            {
                if (Simplest.IsChecked == true)
                {
                    return OptionDifficultySetting.MaterialBalance;
                }
                else if (Medium.IsChecked == true)
                {
                    return OptionDifficultySetting.MaterialBalanceWithReactors;
                }
                else
                {
                    return OptionDifficultySetting.MaterialAndEnergyBalance;
                }
            }
        }


        public OptionWindow()
        {
            InitializeComponent();
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}

