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

namespace ChemProV.PFD.EquationEditor.Tokens
{
    public class OperatorToken : IEquationToken
    {
        public string Value
        {
            get;
            set;
        }

        public OperatorToken(string text)
        {
            Value = text;
        }
    }
}
