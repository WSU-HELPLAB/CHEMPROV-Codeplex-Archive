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

using ChemProV.PFD.Streams;
using ChemProV.PFD.ProcessUnits;

namespace ChemProV.UI.DrawingCanvas.Commands.Stream
{
    /// <summary>
    /// This is the move the head of the stream that is the destination side.
    /// </summary>
    public class MoveStreamHeadCommand : ICommand
    {

        /// <summary>
        /// Reference to the stream we are manipulating.
        /// </summary>
        private IStream streamToMove;
        
        public IStream StreamToMove
        {
            get { return streamToMove; }
            set { streamToMove = value; }
        }

        /// <summary>
        /// Reference to the target location where we'd like to add the process unit
        /// </summary>
        private Point currentMouseLocation;

        public Point CurrentMouseLocation
        {
            get { return currentMouseLocation; }
            set { currentMouseLocation = value; }
        }

        private Point previousMouseLocation;

        public Point PreviousMouseLocation
        {
            get { return previousMouseLocation; }
            set { previousMouseLocation = value; }
        }

        /// <summary>
        /// Private reference to our drawing_canvas.  Needed to add the new object to the drawing space
        /// </summary>
        private Panel canvas;

        public Panel Canvas
        {
            get { return canvas; }
            set { canvas = value; }
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
                instance = new MoveStreamHeadCommand();
            }
            return instance;
        }

        /// <summary>
        /// Constructor method
        /// </summary>
        private MoveStreamHeadCommand()
        {
        }

        /// <summary>
        /// This checks to see if we are connected to a temp process unit if we are we delete it if not then we just let
        /// the process unit know we are dettaching from it. Then we check to see if we are over a process unit if we are
        /// we attach to it, if we can, if we are not over a process unit we make a new source at the location given.
        /// </summary>
        public bool Execute()
        {
            if (streamToMove.Destination is IProcessUnit && !(streamToMove.Destination is TemporaryProcessUnit))
            {
                (streamToMove.Destination as IProcessUnit).DettachIncomingStream(streamToMove);
                streamToMove.Destination = null;

            }

            IProcessUnit hoveringOver = ((canvas as DrawingCanvas).HoveringOver as IProcessUnit);
            if (hoveringOver != null)
            {
                if (streamToMove.Destination is TemporaryProcessUnit || streamToMove.Destination == null)
                {
                    CommandFactory.CreateCommand(CanvasCommands.RemoveFromCanvas, streamToMove.Destination, canvas, currentMouseLocation).Execute();
                    streamToMove.Destination = null;
                }
                    if (hoveringOver.IsAcceptingIncomingStreams(streamToMove))
                {
                    streamToMove.Destination = (canvas as DrawingCanvas).HoveringOver as IProcessUnit;
                    ((canvas as DrawingCanvas).HoveringOver as IProcessUnit).AttachIncomingStream(streamToMove);
                    (hoveringOver as GenericProcessUnit).ProcessUnitBorder.BorderBrush = new SolidColorBrush(Colors.Green);
                    (streamToMove as AbstractStream).DestinationArrorVisbility = true;
                    (canvas as DrawingCanvas).ChildrenModified();
                    return true;
                }
                else
                {
                    (hoveringOver as GenericProcessUnit).ProcessUnitBorder.BorderBrush = new SolidColorBrush(Colors.Red);
                    return false;
                }
            }
            if (streamToMove.Destination == null)
            {
                IProcessUnit Destination = ProcessUnitFactory.ProcessUnitFromUnitType(ProcessUnitType.Sink);
                CommandFactory.CreateCommand(CanvasCommands.AddToCanvas, Destination, canvas, currentMouseLocation).Execute();
                streamToMove.Destination = Destination;
                (streamToMove as AbstractStream).DestinationArrorVisbility = false;
                Destination.AttachIncomingStream(streamToMove);
            }
            else if (streamToMove.Destination is TemporaryProcessUnit)
            {
                //use IProcessUnitMove to move the stream destination
                CommandFactory.CreateCommand(CanvasCommands.MoveHead, streamToMove.Destination, canvas, currentMouseLocation, previousMouseLocation).Execute();
            }
            return true;
        }
    }
}
