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
using ChemProV.UI.DrawingCanvas.States;

namespace ChemProV.UI.DrawingCanvas.Commands.Stream.PropertiesTable
{
    public class MovePropertiesTableCommand : ICommand
    {

    private IPropertiesTable tableToMove;

    public IPropertiesTable TableToMove
        {
            get { return tableToMove; }
            set { tableToMove = value; }
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

    private DrawingCanvas drawingcanvas;

    public DrawingCanvas Drawingcanvas
    {
        get { return drawingcanvas; }
        set { drawingcanvas = value; }
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
                instance = new MovePropertiesTableCommand();
            }
            return instance;
        }

        /// <summary>
        /// Constructor method
        /// </summary>
        private MovePropertiesTableCommand()
        {
        }

        /// <summary>
        /// Moves the propertiesTable
        /// </summary>
        public bool Execute()
        {
            //first, we need to turn the table into something we can actually use, again
            //not a safe cast
            UserControl puAsUiElement = tableToMove as UserControl;

            //width and height needed to calculate position
            double width = puAsUiElement.ActualWidth;
            double height = puAsUiElement.ActualHeight;

            //basically if coming from placing state
            if (previousMouseLocation.X == -1 && previousMouseLocation.Y == -1)
            {
                puAsUiElement.SetValue(Canvas.LeftProperty, currentMouseLocation.X - (width / 2));
                puAsUiElement.SetValue(Canvas.TopProperty, currentMouseLocation.Y - 10);
                (drawingcanvas.MovingState as MovingState).previousLocation = currentMouseLocation;
                return true;
            }

                //is this our first moving state?
            else if (previousMouseLocation.X == -2 && previousMouseLocation.Y == -2)
            {
            return true;
        }

            //otherwise we are in moving it around

            //this gets a percent of how far right we are.  Left-hand side is 0 percent right-hand side is 100%. This is 
            //represented in decimal form so mouseOffSet must be between 0 and 1.
            double mouseOffSetX = (previousMouseLocation.X - (double)puAsUiElement.GetValue(Canvas.LeftProperty)) / width;

            //this can happen if width is zero or if the LeftProperty of the element has not been set yet.
            if (double.IsNaN(mouseOffSetX))
            {
                return true;
    }
            else if (mouseOffSetX < 0)
            {
                mouseOffSetX = 0;
            }
            else if (mouseOffSetX > 1)
            {
                mouseOffSetX = 1;
            }

            

            Point Position = new Point();

            //this uses the percent to calcualate how far over we should push the drawing_canvas.leftProperty
            Position.X = (currentMouseLocation.X - (width * mouseOffSetX));
            Position.Y = currentMouseLocation.Y - 10;

            //NOTE: Doesn't check if bigger than drawing_canvas on purpose, drawing_canvas should  grow.
            if(Position.X < 0)
            {
                Position.X = 0;
            }

            if(Position.Y < 0)
            {
                Position.Y = 0;
            }

            puAsUiElement.SetValue(Canvas.LeftProperty, Position.X);
            puAsUiElement.SetValue(Canvas.TopProperty, Position.Y);

            return true;
        }
    }
}
