/*
Copyright 2010 HELP Lab @ Washington State University

This file is part of ChemProV (http://helplab.org/chemprov).

ChemProV is distributed under the Open Software License ("OSL") v3.0.
Consult "LICENSE.txt" included in this package for the complete OSL license.
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Controls.Primitives;
using System.Windows.Threading;

namespace ChemProV.UI.PalletItems
{
    public partial class ProcessUnitPalette : UserControl
    {
        /// <summary>
        /// This is fired whenever the selection has been changed.
        /// </summary>
        public event EventHandler SelectionChanged;
        private IPaletteItem selectedItem = null;
        struct HoveringOver
        {
            public bool OverCategorySelector;
            public bool OverPopUp;
        }
        private HoveringOver SelectingProcessUnit;
        private HoveringOver SelectingStreams;
        DispatcherTimer timer = new DispatcherTimer();

        public void StartPopUpTimer()
        {
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Tick += new EventHandler(PopUpTimerClick);
            timer.Start();
        }

        void PopUpTimerClick(object sender, EventArgs e)
        {
            timer.Stop();
            timer.Tick -= new EventHandler(PopUpTimerClick);
            if (SelectingProcessUnit.OverCategorySelector == false && SelectingProcessUnit.OverPopUp == false && ProcessUnitFlyOut.IsOpen == true)
            {
                FlyOut_Close(ProcessUnitFlyOut);
            }
            if (SelectingStreams.OverCategorySelector == false && SelectingStreams.OverPopUp == false && StreamsFlyOut.IsOpen == true)
            {
                FlyOut_Close(StreamsFlyOut);
            }
        }

        /// <summary>
        /// This is the currently selectedItem in the Palette
        /// </summary>
        public IPaletteItem SelectedItem
        {
            get 
            {
                return selectedItem; 
            }
            set 
            {
                selectedItem = value; 

                //fire a selection changed event
                SelectionChanged(this, new EventArgs());
            }
        }

        /// <summary>
        /// This is the constructor for Palette item, it initializes itself and then attaches necessary mouse listeners
        /// </summary>
        public ProcessUnitPalette()
        {
            InitializeComponent();

            //attach mouse listeners to the child objects (palette elements)
            AttachMouseListeners();

        }

        /// <summary>
        /// Attaches mouse listeners for each of the stack's children
        /// </summary>
        private void AttachMouseListeners()
        {
                    foreach (UIElement ui in ((ProcessUnitFlyOut.Child as Border).Child as StackPanel).Children)
                    {
                        ui.MouseLeftButtonDown += new MouseButtonEventHandler(OnMouseLeftButtonDown);
                        ui.MouseEnter +=new MouseEventHandler(OnMouseEnter);
                        ui.MouseLeave += new MouseEventHandler(OnMouseLeave);
                    }
                    foreach (UIElement ui in ((StreamsFlyOut.Child as Border).Child as StackPanel).Children)
                    {
                        ui.MouseLeftButtonDown += new MouseButtonEventHandler(OnMouseLeftButtonDown);
                        ui.MouseEnter += new MouseEventHandler(OnMouseEnter);
                        ui.MouseLeave += new MouseEventHandler(OnMouseLeave);
                    }
                    foreach (UIElement ui in LayoutRoot.Children)
                    {
                        if (ui is GenericPaletteItem)
                        {
                            ui.MouseLeftButtonDown += new MouseButtonEventHandler(OnMouseLeftButtonDown);
                        }
                    }
        }

        private void OnMouseEnter(object sender, MouseEventArgs e)
        {
            (sender as GenericPaletteItem).LayoutRoot.Background = new SolidColorBrush(Colors.LightGray);
        }
        private void OnMouseLeave(object sender, MouseEventArgs e)
        {
            (sender as GenericPaletteItem).LayoutRoot.Background = new SolidColorBrush(Colors.White);
        }

        /// <summary>
        /// Resets the current selection back to the default choice
        /// </summary>
        public void ResetSelection()
        {
            HighlightItem(DefaultSelection);

        }

        private void HighlightItem(IPaletteItem item)
        {
            if (item.CompareTo(selectedItem) != 0)
            {
                if (selectedItem != null)
                {
                    selectedItem.Selected = false;
                }
                else
                {
                    DefaultSelection.Selected = false;
                }

                //set the new selected item
                SelectedItem = item;

                //highlight current selection
                item.Selected = true;

                if (item != DefaultSelection && item.Description != "Sticky Note")
                {
                    GenericPaletteItem gItem = (item as GenericPaletteItem);

                    gItem.blink_Storyboard.Completed += new EventHandler(blink_Storyboard_Completed);
                    gItem.blink_Storyboard.Begin();
                }
            }
        }

        void blink_Storyboard_Completed(object sender, EventArgs e)
        {
            if (StreamsFlyOut.IsOpen == true)
            {
                FlyOut_Close(StreamsFlyOut);
            }
            else if (ProcessUnitFlyOut.IsOpen == true)
            {
                FlyOut_Close(ProcessUnitFlyOut);
            }
        }

        /// <summary>
        /// Handles palette item clicks
        /// </summary>
        /// <param name="sender">The object that was clicked</param>
        /// <param name="e">Some mouse event args?</param>
        private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            HighlightItem((IPaletteItem)sender);
            e.Handled = true;
        }

        private void FlyOut_MouseEnter(object sender, MouseEventArgs e)
        {
            if(sender == this.ProcessUnitFlyOut.Child)
            {
                SelectingProcessUnit.OverPopUp = true;
            }
            else if (sender == this.StreamsFlyOut.Child)
            {
                SelectingStreams.OverPopUp = true;
            }
        }

        private void FlyOut_MouseLeave(object sender, MouseEventArgs e)
        {
                SelectingProcessUnit.OverPopUp = false;
                SelectingStreams.OverPopUp = false;
             /*   if (StreamsFlyOut.IsOpen == true)
                {
                    FlyOut_Close(StreamsFlyOut);
                }
                else if (ProcessUnitFlyOut.IsOpen == true)
                {
                    FlyOut_Close(ProcessUnitFlyOut);
                }*/
        }

        private void CategorySelector_MouseEnter(object sender, MouseEventArgs e)
        {
            if (sender == this.ProcessUnitCategory)
            {
                ProcessUnitFlyOut.IsOpen = true;
                StreamsFlyOut.IsOpen = false;
                SelectingProcessUnit.OverCategorySelector = true;
            }
            else if (sender == this.StreamsCategory)
            {
                StreamsFlyOut.IsOpen = true;
                ProcessUnitFlyOut.IsOpen = false;
                SelectingStreams.OverCategorySelector = true;
            }
        }

        private void CategorySelector_MouseLeave(object sender, MouseEventArgs e)
        {
            if (sender == this.ProcessUnitCategory)
            {
                SelectingProcessUnit.OverCategorySelector = false;
                StartPopUpTimer();
            }
            else if (sender == this.StreamsCategory)
            { 
                SelectingStreams.OverCategorySelector = false;
                StartPopUpTimer();
            }
        }

        private void FlyOut_Opened(object sender, EventArgs e)
        {
            Storyboard OpenPopUp_Storyboard = new Storyboard();
            DoubleAnimation da = new DoubleAnimation();
            Duration duration = new Duration(TimeSpan.FromSeconds(.25));
            da.Duration = duration;
            OpenPopUp_Storyboard.Duration = duration;
            OpenPopUp_Storyboard.Children.Add(da);

            Storyboard.SetTarget(da, (sender as Popup).Child);

            Storyboard.SetTargetProperty(da, new PropertyPath(Border.OpacityProperty));

            da.From = 0;
            da.To = 1;

            OpenPopUp_Storyboard.Begin();
        }

        private bool closingFlyOutProcessUnit = false;
        private bool closingFlyOutStreams = false;

        private void FlyOut_Close(Popup sender)
        {
            Storyboard ClosePopUp_Storyboard = new Storyboard();
            DoubleAnimation da = new DoubleAnimation();
            Duration duration = new Duration(TimeSpan.FromSeconds(.25));
            da.Duration = duration;
            ClosePopUp_Storyboard.Duration = duration;
            ClosePopUp_Storyboard.Children.Add(da);

            Storyboard.SetTarget(da, sender.Child);

            Storyboard.SetTargetProperty(da, new PropertyPath(Border.OpacityProperty));

            da.From = 1;
            da.To = 0;
            ClosePopUp_Storyboard.Completed += new EventHandler(ClosePopUp_Storyboard_Completed);
            if(sender == ProcessUnitFlyOut)
            {
                if (closingFlyOutProcessUnit == false)
                {
                    closingFlyOutProcessUnit = true;
                    closing = sender;
                    ClosePopUp_Storyboard.Begin();
                }
            }
            else if(sender == StreamsFlyOut)
            {
                if (closingFlyOutStreams == false)
                {
                    closingFlyOutStreams = true;
                    Closing = sender;
                    ClosePopUp_Storyboard.Begin();
                }
            }
                
        }

        private Popup closing;

        public Popup Closing
        {
            get { return closing; }
            set {
                if (closing != null)
                    closing.IsOpen = false;
                closing = value; }
        }


        void ClosePopUp_Storyboard_Completed(object sender, EventArgs e)
        {
            if (closing != null)
            {
                Closing.IsOpen = false;

                if (closing == ProcessUnitFlyOut)
                {
                    closingFlyOutProcessUnit = false;
                }
                else if (closing == StreamsFlyOut)
                {
                    closingFlyOutStreams = false;
                }
                closing = null;
            }
        }
    }
}
