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
using System.Collections.Generic;

namespace ChemProV.PFD.Streams.PropertiesTable
{
    public interface ICompound
    {
        List<KeyValuePair<Element, int> > elements
        {
            get;
        }
    }
}
