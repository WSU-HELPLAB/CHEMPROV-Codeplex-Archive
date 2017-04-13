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

using ChemProV.PFD.Streams.PropertiesTable;
using ChemProV.PFD.Streams.PropertiesTable.Chemical;

namespace ChemProV.Validation.Rules.Adapters.Table
{
    /// <summary>
    /// This class creates a TablesAdapter based upon the table passed in.
    /// </summary>
    public class TableAdapterFactory
    {
        /// <summary>
        /// This creates a TableAdapter for the table passed.
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public static ITableAdapter CreateTableAdapter(IPropertiesTable table)
        {
            if (table is ChemicalStreamPropertiesTable)
            {
                return new ChemicalTableAdapter(table as ChemicalStreamPropertiesTable);
            }
            return new NullTableAdapter();
        }
        
    }
}
