/*
Copyright 2010 HELP Lab @ Washington State University

This file is part of ChemProV (http://helplab.org/chemprov).

ChemProV is distributed under the Open Software License ("OSL") v3.0.
Consult "LICENSE.txt" included in this package for the complete OSL license.
*/

using System.Windows;
using System.Windows.Controls.Primitives;

using Behaviors;

namespace ChemProV.UI.Behaviors
{
    public class MovePopupHeader : FrameworkElementMoveBehavior
    {
        private Popup parent = null;
        private UIElement TopMostContainer = null;

        public MovePopupHeader()
            : base()
        {
        }

        protected override Point GetObjectLocation()
        {
            GetObjectToMove();
            return new Point(parent.HorizontalOffset, parent.VerticalOffset);
        }

        protected override FrameworkElement GetObjectToMove()
        {
            if (parent == null)
            {
                FrameworkElement p = this.AssociatedObject.Parent as FrameworkElement;
                while (!(p is Popup))
                {
                    p = p.Parent as FrameworkElement;
                }
                parent = p as Popup;
            }
            return parent;
        }

        protected override UIElement GetTopMostContainer()
        {
            GetObjectToMove();
            if (TopMostContainer == null)
            {
                TopMostContainer = (parent.Parent as FrameworkElement).Parent as UIElement;
            }
            return TopMostContainer;
        }

        protected override void SetLeftPosition(double postion)
        {
            GetObjectToMove();
            parent.HorizontalOffset = postion;
        }

        protected override void SetTopPosition(double postion)
        {
            GetObjectToMove();
            parent.VerticalOffset = postion;
        }
    }
}