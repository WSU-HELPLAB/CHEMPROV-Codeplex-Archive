﻿using System;
using System.Collections.Generic;
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

namespace ChemProV.UI.DrawingCanvas
{
    public class PfdUpdatedEventArgs : EventArgs
    {
        public readonly IEnumerable<IPfdElement> pfdElements;

        public PfdUpdatedEventArgs(IEnumerable<IPfdElement> pfdElements)
        {
            this.pfdElements = pfdElements;
        }
    }
}
