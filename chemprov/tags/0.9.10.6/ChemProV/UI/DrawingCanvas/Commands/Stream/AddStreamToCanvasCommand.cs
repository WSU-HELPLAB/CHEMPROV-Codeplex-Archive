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

using ChemProV.Validation;
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
    /// This class is for adding a stream to the canvas.
    /// </summary>
    public class AddStreamToCanvasCommand : ICommand
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
        private IStream newIStream;

        public IStream NewIStream
        {
            get { return newIStream; }
            set { newIStream = value; }
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
                instance = new AddStreamToCanvasCommand();
            }
            return instance;
        }

        /// <summary>
        /// Constructor method
        /// </summary>
        private AddStreamToCanvasCommand()
        {
        }

        /// <summary>
        /// This sets source and destination to the same spot and creates the table too.
        /// </summary>
        /// <returns></returns>
        public bool Execute()
        {

            //At this point we do not know if we are making a source or destionation doesnt matter
            IProcessUnit source;
            while (newIStream.Source == null || newIStream.Destination == null)
            {
                if (newIStream.Source == null && (canvas as DrawingCanvas).HoveringOver is IProcessUnit && (!((canvas as DrawingCanvas).HoveringOver is TemporaryProcessUnit)))
                {
                    source = (canvas as DrawingCanvas).HoveringOver as IProcessUnit;
                    (newIStream as AbstractStream).SourceRectangleVisbility = true;
                }
                else
                {
                    if (newIStream.Source == null)
                    {
                        source = ProcessUnitFactory.ProcessUnitFromUnitType(ProcessUnitType.Source);
                        (newIStream as AbstractStream).SourceRectangleVisbility = false;
                    }
                    else
                    {
                        source = ProcessUnitFactory.ProcessUnitFromUnitType(ProcessUnitType.Sink);
                        (newIStream as AbstractStream).DestinationArrorVisbility = false;
                    }

                    CommandFactory.CreateCommand(CanvasCommands.AddToCanvas, source, canvas, location).Execute();

                }

                //now check to see if source is null then we made a source if not then we made a destination
                if (newIStream.Source == null)
                {
                    //attach source to stream
                    newIStream.Source = source;

                    //attach stream to source
                    source.AttachOutgoingStream(newIStream);
                }
                else
                {
                    //attach source to stream
                    newIStream.Destination = source;

                    //attach stream to source
                    source.AttachIncomingStream(newIStream);
                }
            }

            //if the stream's destination is a TemporaryProcessUnit, then we need to
            //turn off the stream's "head" icon (currently an arrow)
            if (newIStream.Destination is TemporaryProcessUnit)
            {
                (newIStream as AbstractStream).DestinationArrorVisbility = false;
            }

            //the same goes for the stream's source
            if (newIStream.Source is TemporaryProcessUnit)
            {
                (newIStream as AbstractStream).SourceRectangleVisbility = false;
            }

            canvas.Children.Add(newIStream as UIElement);
            newIStream.Arrow_MouseButtonLeftDown += new MouseButtonEventHandler((canvas as DrawingCanvas).HeadMouseLeftButtonDownHandler);
            newIStream.Tail_MouseButtonLeftDown += new MouseButtonEventHandler((canvas as DrawingCanvas).TailMouseLeftButtonDownHandler);

            Point locationOfSource = new Point();
            locationOfSource.X = ((double)(newIStream.Source as UIElement).GetValue(System.Windows.Controls.Canvas.LeftProperty));
            locationOfSource.Y = ((double)(newIStream.Source as UIElement).GetValue(System.Windows.Controls.Canvas.TopProperty));

            Point locationOfDestination = new Point();
            locationOfDestination.X = ((double)(newIStream.Destination as UIElement).GetValue(System.Windows.Controls.Canvas.LeftProperty));
            locationOfDestination.Y = ((double)(newIStream.Destination as UIElement).GetValue(System.Windows.Controls.Canvas.TopProperty));

            //sometimes we'll be passed in an IStream that already has a pre-existing table.  This
            //occurs when we load PFD documents from file.
            Point tableLocation = new Point(-1.0, -1.0);
            if (newIStream.Table == null)
            {
                newIStream.Table = PropertiesTableFactory.TableFromStreamObject(newIStream);
                tableLocation = CalculateTablePositon(locationOfSource, locationOfDestination);
                
                //this is so it gets more away from the source
                tableLocation.Y = tableLocation.Y + 50;
            }
            (newIStream.Table as UIElement).MouseLeftButtonDown += new MouseButtonEventHandler((canvas as DrawingCanvas).MouseLeftButtonDownHandler);
            (newIStream.Table as UIElement).MouseLeftButtonUp += new MouseButtonEventHandler((canvas as DrawingCanvas).MouseLeftButtonUpHandler);

            //This gets an instance of the TableManager Class
            TableManager tm = TableManager.GetInstance();

            //Let TableManger know we just added this table.
            tm.AddTableToList(newIStream.Table);

            //placing the ChemicalPropertiesTable on canvas at the point calcualted by CalculateTablePoistion
            CommandFactory.CreateCommand(CanvasCommands.AddToCanvas, newIStream.Table, canvas, tableLocation).Execute();
            (canvas as DrawingCanvas).SelectedElement = new StreamDestinationIcon(newIStream, (newIStream as AbstractStream).Arrow);
            return true;
        }

        /// <summary>
        /// Calculates where the table should be placed given the Source and Destination as points
        /// </summary>
        /// <param name="Source"></param>
        /// <param name="Destination"></param>
        /// <returns>Top Left Point of where table should be placed</returns>
        Point CalculateTablePositon(Point Source, Point Destination)
        {
            Point distance = new Point((Source.X - Destination.X), (Source.Y - Destination.Y));
            Point TableLocation = new Point();
            if (distance.X > 0 && distance.Y > 0)
            {
                distance.X = Math.Abs(distance.X) / 2;
                distance.Y = Math.Abs(distance.Y) / 2;
                TableLocation.X = Source.X - distance.X;
                TableLocation.Y = Source.Y - distance.Y;
            }
            else if (distance.X > 0 && distance.Y < 0)
            {
                distance.X = Math.Abs(distance.X) / 2;
                distance.Y = Math.Abs(distance.Y) / 2;
                TableLocation.X = Source.X - distance.X;
                TableLocation.Y = Source.Y + distance.Y;
            }
            else if (distance.X < 0 && distance.Y > 0)
            {
                distance.X = Math.Abs(distance.X) / 2;
                distance.Y = Math.Abs(distance.Y) / 2;
                TableLocation.X = Source.X + distance.X;
                TableLocation.Y = Source.Y - distance.Y;
            }
            else if (distance.X < 0 && distance.Y < 0)
            {
                distance.X = Math.Abs(distance.X) / 2;
                distance.Y = Math.Abs(distance.Y) / 2;
                TableLocation.X = Source.X + distance.X;
                TableLocation.Y = Source.Y + distance.Y;
            }
            else if (distance.X == 0 && distance.Y == 0)
            {
                TableLocation.X = Source.X;
                TableLocation.Y = Source.Y;
            }
            TableLocation.Y = TableLocation.Y + 10;
            return (TableLocation);
        }
    }
}
