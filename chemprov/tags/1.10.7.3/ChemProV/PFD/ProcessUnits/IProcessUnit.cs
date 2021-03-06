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
using System.Xml.Serialization;

using ChemProV.PFD.Streams;

namespace ChemProV.PFD.ProcessUnits
{
    /// <summary>
    /// Interface that must be implemented by any process unit.  This outlines all of the basic 
    /// functionality encapsulated by any process unit.
    /// </summary>
    public interface IProcessUnit : IPfdElement, IXmlSerializable
    {

        /// <summary>
        /// All process units need an icon so they can be represented in a drawing drawing_canvas
        /// </summary>
        Image Icon
        {
            get;
            set;
        }

        /// <summary>
        /// A short description of the process unit.  Not more than a few words in length.
        /// </summary>
        String Description
        {
            get;
            set;
        }

        /// <summary>
        /// Total number of incoming streams allowed.  A value of zero is taken to mean unlimited.
        /// A value of -1 indicates no incoming streams.
        /// </summary>
        int MaxIncomingStreams
        {
            get;
            set;
        }

        /// <summary>
        /// Total number of outgoing streams allowed.  A value of zero is taken to mean unlimited.
        /// A value of -1 indicates no outgoing streams.
        /// </summary>
        int MaxOutgoingStreams
        {
            get;
            set;
        }

        /// <summary>
        /// Total number of incoming streams allowed.  A value of zero is taken to mean unlimited.
        /// A value of -1 indicates no incoming streams.
        /// </summary>
        int MaxIncomingHeatStreams
        {
            get;
            set;
        }

        /// <summary>
        /// Total number of outgoing streams allowed.  A value of zero is taken to mean unlimited.
        /// A value of -1 indicates no outgoing streams.
        /// </summary>
        int MaxOutgoingHeatStreams
        {
            get;
            set;
        }

        /// <summary>
        /// Gets whether or not the IProcessUnit is accepting new incoming streams
        /// </summary>
        bool IsAcceptingIncomingStreams(IStream stream);

        /// <summary>
        /// Gets whether or not the IProcessUnit is accepting new outgoing streams
        /// </summary>
        bool IsAcceptingOutgoingStreams(IStream stream);

        /// <summary>
        /// Attaches a new incoming stream to the IProcessUnit
        /// </summary>
        /// <param name="stream">The IStream to attach</param>
        /// <returns>Whether or not the stream was successfully attached</returns>
        bool AttachIncomingStream(IStream stream);

        /// <summary>
        /// Attaches a new outgoing stream to the IProcessUnit
        /// </summary>
        /// <param name="stream">The IStream to attach</param>
        /// <returns>Whether or not the stream was successfully attached</returns>
        bool AttachOutgoingStream(IStream stream);

        /// <summary>
        /// Dettaches an incoming stream to the IProcessUnit
        /// </summary>
        /// <param name="stream">The IStream to dettach</param>
        void DettachIncomingStream(IStream stream);

        /// <summary>
        /// Dettaches an outgoing stream to the IProcessUnit
        /// </summary>
        /// <param name="stream">The IStream to dettach</param>
        void DettachOutgoingStream(IStream stream);

        /// <summary>
        /// List of incoming streams
        /// </summary>
        IList<IStream> IncomingStreams
        {
            get;
        }

        /// <summary>
        /// List of outgoing streams
        /// </summary>
        IList<IStream> OutgoingStreams
        {
            get;
        }

    }
}
