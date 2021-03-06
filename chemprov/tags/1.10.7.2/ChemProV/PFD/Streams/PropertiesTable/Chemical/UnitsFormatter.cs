﻿using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace ChemProV.PFD.Streams.PropertiesTable.Chemical
{
    public class UnitsFormatter : IValueConverter
    {
        /// <summary>
        /// This converts an index value from the unit dropdown box into a string that it 
        /// puts in the table for when the dropdown box is not shown
        /// </summary>
        /// <param name="value">index of currently selected dropdown menu item</param>
        /// <param name="targetType">not used</param>
        /// <param name="parameter">not used</param>
        /// <param name="culture">not used</param>
        /// <returns>a string for the text box to use when dropdown is not visble</returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return ConvertFromIntToString((int)value);
        }

        /// <summary>
        /// This converts an index value from the unit dropdown box into a string that it puts in the table for when the dropdown box is not shown
        /// </summary>
        /// <param name="value">index of currently selected dropdown menu item</param>
        /// <returns>a string for the text box to use when dropdown is not visble</returns>
        public string ConvertFromIntToString(int value)
        {
            switch (value)
            {
                //case -1 is for when row is first created nothing is set yet
                case -1:
                    return "";
                case 0:
                    return "%";
                case 1:
                    return "grams";
                case 2:
                    return "grams per minute";
                case 3:
                    return "kilograms";
                case 4:
                    return "kilograms per minute";
                case 5:
                    return "moles";
                case 6:
                    return "moles per minute";
            }
            return "";
        }

        /// <summary>
        /// This function is never called but required to be implimented because it is 
        /// virtual in IValueConverter
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            switch ((string)value)
            {
                case "%":
                    return 0;
                case "grams":
                    return 1;
                case "grams per minute":
                    return 2;
                case "kilograms":
                    return 3;
                case "kilograms per minute":
                    return 4;
            }
            return 0;
        }
    }
}
