﻿using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using ChemProV.PFD.Streams;
namespace ChemProV.UI
{
    /// <summary>
    /// This is used in the Palette for all streams
    /// </summary>
    public class StreamPaletteItem : GenericPaletteItem
    {

        //Dependency region declarations for setting palette item options.
        //Coolness.
        #region dependency properties

        /// <summary>
        /// used to set the stream type attached to the palette item
        /// </summary>
        public static readonly DependencyProperty StreamProperty;

        #endregion

        //where traditional C# properties (GET/SET) go
        #region properties

        /// <summary>
        /// This gets or sets the StreamType
        /// </summary>
        public StreamType Stream
        {
            get
            {
                return (StreamType)GetValue(StreamProperty);
            }
            set
            {
                SetValue(StreamProperty, value);
            }
        }

        /// <summary>
        /// This gets or sets a reference to an IStream
        /// </summary>
        public IStream LocalStream
        {
            get
            {
                return (IStream)data;
            }
            set
            {
                data = value;
            }
        }

        #endregion

        //where we put event listeners
        #region event listeneres

        /// <summary>
        /// Will be called whenever someone makes a change to the "ProcessUnit" property
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void OnStreamPropertyChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //cast the new value as a ProcessUnitType enum
            StreamType type = (StreamType)e.NewValue;

            //turn the DependencyObject into a ProcessUnitPaletteItem
            StreamPaletteItem item = (StreamPaletteItem)d;

            //get the associated process unit type
            IStream stream = StreamFactory.StreamFromStreamType(type);
            item.LocalStream = stream;

            //should use data binding, but meh
            item.IconSource = StreamFactory.IconFromStreamType(type);
            item.Description = item.Description;
        }

        #endregion

        /// <summary>
        /// Static constructor that sets up dependency properties and other goodies.
        /// </summary>
        static StreamPaletteItem()
        {
            //initialize the class' dependency properties
            StreamProperty = DependencyProperty.Register(
                                                     "Stream",
                                                     typeof(StreamType),
                                                     typeof(StreamPaletteItem),
                                                     new PropertyMetadata(
                                                         StreamType.Generic,
                                                         new PropertyChangedCallback(OnStreamPropertyChange)
                                                         )
                                                     );


        }

        /// <summary>
        /// This is an empty contructor and just returns a reference to itself
        /// </summary>
        public StreamPaletteItem()
        {
        }
    }
}
