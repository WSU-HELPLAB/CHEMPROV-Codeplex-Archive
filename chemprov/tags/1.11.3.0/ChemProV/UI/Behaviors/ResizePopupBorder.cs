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
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

using Behaviors;

namespace ChemProV.UI.Behaviors
{
    public class ResizePopupBorder : BorderSizeableBehavior
    {
        private Popup mainObject;

        public ResizePopupBorder()
            : base()
        {
        }

        protected override Point GetObjectLocation()
        {
            GetObjectToMove();
            return new Point(mainObject.HorizontalOffset, mainObject.VerticalOffset);
        }

        protected override FrameworkElement GetObjectToMove()
        {
            if (mainObject == null)
            {
                mainObject = this.AssociatedObject.Parent as Popup;
            }
            return mainObject;
        }

        protected override UIElement GetTopMostContainer()
        {
            GetObjectToMove();
            return ((mainObject.Parent as FrameworkElement).Parent as UIElement);
        }

        protected override void SetLeftPosition(double postion)
        {
            GetObjectToMove();
            mainObject.HorizontalOffset = postion;
        }

        protected override void SetTopPosition(double postion)
        {
            GetObjectToMove();
            mainObject.VerticalOffset = postion;
        }
    }
}
