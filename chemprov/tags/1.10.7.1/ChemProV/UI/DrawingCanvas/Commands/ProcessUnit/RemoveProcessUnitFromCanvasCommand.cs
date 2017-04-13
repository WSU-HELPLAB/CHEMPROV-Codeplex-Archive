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

using ChemProV.PFD;
using ChemProV.PFD.ProcessUnits;
using ChemProV.PFD.Streams;
using ChemProV.PFD.Streams.PropertiesTable;

namespace ChemProV.UI.DrawingCanvas.Commands.ProcessUnit
{
    public class RemoveProcessUnitFromCanvasCommand : ICommand
    {
        /// <summary>
        /// Private reference to our canvas.  Needed to remove the object from the drawing space
        /// </summary>
        private Panel canvas;
        public Panel Canvas
        {
            get { return canvas; }
            set { canvas = value; }
        }

        /// <summary>
        /// Reference to the process unit to remove the the canvas.
        /// </summary>
        private IProcessUnit newProcessUnit;

        public IProcessUnit NewProcessUnit
        {
            get { return newProcessUnit; }
            set { newProcessUnit = value; }
        }

        /// <summary>
        /// Used to get at the single instance of this object
        /// </summary>
        /// <returns></returns>
        public static ICommand GetInstance()
        {
            return new RemoveProcessUnitFromCanvasCommand();
        }

        /// <summary>
        /// Constructor method
        /// </summary>
        /// <param name="canvas">The canvas that we'd like to remove the process unit from</param>
        /// <param name="newProcessUnit">The process unit to be removed from the canvas</param>
        private RemoveProcessUnitFromCanvasCommand()
        {
        }

        /// <summary>
        /// Removes the process unit from the canvas... hoping grabbage collection will deal with delete the process unit itself.
        /// </summary>
        public bool Execute()
        {
            //first, we need to turn the IProcessUnit into something we can actually use, again
            //not a safe cast
            UserControl puAsUiElement = newProcessUnit as UserControl;

            //The steam will delete the temp process unit if needed not the other way around so the temp process unit just dies
            if (!(newProcessUnit is TemporaryProcessUnit))
            {

                //While it has streams either going in or out delete them.
                while (newProcessUnit.OutgoingStreams.Count > 0)
                {
                    //I am not forgetting to increment.  Each time this command is 
                    //called it automatically removes it from the list and so if we set 
                    //at 0 will keep deleteing the next one.
                    CommandFactory.CreateCommand(CanvasCommands.RemoveFromCanvas, newProcessUnit.OutgoingStreams[0], canvas, new Point()).Execute();
                }
                while (newProcessUnit.IncomingStreams.Count > 0)
                {
                    //Same thing as above
                    CommandFactory.CreateCommand(CanvasCommands.RemoveFromCanvas, newProcessUnit.IncomingStreams[0], canvas, new Point()).Execute();
                }
            }

            //unregister all event handlers
            if (newProcessUnit is TemporaryProcessUnit)
            {
                puAsUiElement.MouseLeftButtonDown -= new MouseButtonEventHandler((canvas as DrawingCanvas).TempProcessUnitMouseLeftButtonDownHandler);
            }
            else
            {
                puAsUiElement.MouseLeftButtonDown -= new MouseButtonEventHandler((canvas as DrawingCanvas).MouseLeftButtonDownHandler);
                puAsUiElement.MouseLeftButtonUp -= new MouseButtonEventHandler((canvas as DrawingCanvas).MouseLeftButtonUpHandler);
                puAsUiElement.MouseRightButtonDown -= new MouseButtonEventHandler((canvas as DrawingCanvas).MouseRightButtonDownHandler);
                puAsUiElement.MouseRightButtonUp -= new MouseButtonEventHandler((canvas as DrawingCanvas).MouseRightButtonUpHandler);
                puAsUiElement.MouseEnter -= new MouseEventHandler((canvas as DrawingCanvas).IProcessUnit_MouseEnter);
                puAsUiElement.MouseLeave -= new MouseEventHandler((canvas as DrawingCanvas).IProcessUnit_MouseLeave);
            }
            //remove the PU to the drawing canvas
            canvas.Children.Remove(puAsUiElement);
            return true;
        }
    }
}
