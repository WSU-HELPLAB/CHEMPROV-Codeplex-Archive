/*
Copyright 2010 HELP Lab @ Washington State University

This file is part of ChemProV (http://helplab.org/chemprov).

ChemProV is distributed under the Open Software License ("OSL") v3.0.
Consult "LICENSE.txt" included in this package for the complete OSL license.
*/
using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

using ChemProV.UI.DrawingCanvas.Commands;
using ChemProV.PFD;
using ChemProV.PFD.ProcessUnits;
using ChemProV.PFD.Streams;

namespace ChemProV.UI.DrawingCanvas.States
{
    /// <summary>
    /// This state is the default state for the drawing drawing_canvas.
    /// </summary>
    public class NullState : IState
    {
        private DrawingCanvas canvas;
        private CommandFactory commandFactory;

        /// <summary>
        /// This is the constructor for the NullState
        /// </summary>
        /// <param name="c">Refernce to the drawing Canvas</param>
        /// <param name="cf">Optional: Reference to a CommandFactory</param>
        public NullState(DrawingCanvas c, CommandFactory cf = null)
        {
            canvas = c;
            if (cf == null)
            {
                cf = new CommandFactory();
            }
            commandFactory = cf;
        }

        #region IState Members

        /// <summary>
        /// Does Nothing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void MouseEnter(object sender, MouseEventArgs e)
        {
        }

        /// <summary>
        /// Does Nothing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void MouseLeave(object sender, MouseEventArgs e)
        {
        }

        /// <summary>
        /// Does Nothing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void MouseMove(object sender, MouseEventArgs e)
        {
        }

        /// <summary>
        /// The user selected something and until mouse left up we are dragging it.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            canvas.SelectedElement = sender as IPfdElement;

            //if null then we didn't select anything so don't do nothing
            if (canvas.SelectedElement != null)
            {
                canvas.CurrentState = canvas.MovingState;
            }
        }

        public void MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
        }

        /// <summary>
        /// This makes the right click menu with out delete since we are in null state nothing is
        /// selected so do not need delete since we would not know what to delete.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            CommandFactory.CreateCommand(CanvasCommands.AddToCanvas, new ContextMenu(), canvas, e.GetPosition(canvas)).Execute();

            canvas.CurrentState = canvas.MenuState;
            e.Handled = true;
        }

        public void MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
        }

        public void MouseWheel(object sender, MouseEventArgs e)
        {
        }

        #endregion
    }
}
