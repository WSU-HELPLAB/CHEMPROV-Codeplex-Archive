﻿using System;
using System.Collections.ObjectModel;
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
using System.Xml.Serialization;

using ChemProV.PFD;

namespace ChemProV.PFD.Streams.PropertiesTable
{
    public delegate void TableDataEventHandler(object sender, TableDataChangedEventArgs e);

    public interface IPropertiesTable : IPfdElement, IXmlSerializable, IComparable
    {
        event TableDataEventHandler TableDataChanged; 

        /// <summary>
        /// Gets the underlying DataGrid for the properties table
        /// </summary>
        DataGrid PropertiesTable
        {
            get;
        }

        /// <summary>
        /// Gets or sets the parent stream that the table is attached to
        /// </summary>
        IStream ParentStream
        {
            get;
            set;
        }


    }
}
