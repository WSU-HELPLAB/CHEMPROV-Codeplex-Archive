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

using ChemProV.UI.DrawingCanvas;
using ChemProV.PFD;
using ChemProV.PFD.ProcessUnits;
using ChemProV.PFD.Streams;
using ChemProV.PFD.Streams.PropertiesTable;
using ChemProV.UI.DrawingCanvas.Commands.ProcessUnit;
using ChemProV.UI.DrawingCanvas.Commands;
using ChemProV;

namespace ChemProV.UI.DrawingCanvas.Commands.Stream
{
    /// <summary>
    /// This is our command for deleting streams.
    /// </summary>
    public class DeleteStreamFromCanvas : ICommand
    {
        /// <summary>
        /// Private reference to our canvas.  Needed to add the new object to the drawing space
        /// </summary>
        private Panel canvas;
        public Panel Canvas
        {
            get { return canvas; }
            set { canvas = value; }
        }

        /// <summary>
        /// Reference to the process unit to add the the canvas.
        /// </summary>
        private IStream removingIStream;

        public IStream RemovingiStream
        {
            get { return removingIStream; }
            set { removingIStream = value; }
        }

        /// <summary>
        /// Reference to the target location where we'd like to add the process unit
        /// </summary>
        private Point location;

        public Point Location
        {
            get { return location; }
            set { location = value; }
        }

        private static ICommand instance;

        /// <summary>
        /// Used to get at the single instance of this object
        /// </summary>
        /// <returns></returns>
        public static ICommand GetInstance()
        {
            if (instance == null)
            {
                instance = new DeleteStreamFromCanvas();
            }
            return instance;
        }

        /// <summary>
        /// Constructor method
        /// </summary>
        private DeleteStreamFromCanvas()
        {
        }

        /// <summary>
        /// For deleteing a stream we must get rid of the table and any temporary process units it is connect too.
        /// We also need to let any non temporary process unit know we are dettaching from it.
        /// </summary>
        /// <returns></returns>
        public bool Execute()
        {
            if(removingIStream.Source == null)
            {
            }
            else if (removingIStream.Source is TemporaryProcessUnit)
            {
                   CommandFactory.CreateCommand(CanvasCommands.RemoveFromCanvas, removingIStream.Source, canvas, new Point()).Execute();
            }   
            else
            {
                (removingIStream.Source as IProcessUnit).DettachOutgoingStream(removingIStream);
            }
            if(removingIStream.Destination == null)
            {
            }
            else if (removingIStream.Destination is TemporaryProcessUnit)
            {
                   CommandFactory.CreateCommand(CanvasCommands.RemoveFromCanvas, removingIStream.Destination, canvas, new Point()).Execute();
            }   
            else
            {
                (removingIStream.Destination as IProcessUnit).DettachIncomingStream(removingIStream);
            }
            CommandFactory.CreateCommand(CanvasCommands.RemoveFromCanvas, removingIStream.Table, canvas, new Point()).Execute();

            removingIStream.Arrow_MouseButtonLeftDown -= new MouseButtonEventHandler((canvas as DrawingCanvas).HeadMouseLeftButtonDownHandler);
            removingIStream.Tail_MouseButtonLeftDown -= new MouseButtonEventHandler((canvas as DrawingCanvas).TailMouseLeftButtonDownHandler);

            (removingIStream.Table as UIElement).MouseLeftButtonDown -= new MouseButtonEventHandler((canvas as DrawingCanvas).MouseLeftButtonDownHandler);
            (removingIStream.Table as UIElement).MouseLeftButtonUp -= new MouseButtonEventHandler((canvas as DrawingCanvas).MouseLeftButtonUpHandler);

            canvas.Children.Remove(removingIStream as UIElement);

            return true;                
        }
    }
}