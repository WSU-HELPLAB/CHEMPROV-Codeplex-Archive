/*
Copyright 2010 - 2012 HELP Lab @ Washington State University

This file is part of ChemProV (http://helplab.org/chemprov).

ChemProV is distributed under the Microsoft Reciprocal License (Ms-RL).
Consult "LICENSE.txt" included in this package for the complete Ms-RL license.
*/

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using ChemProV.Core;
using ChemProV.Logic;
using ChemProV.PFD.Streams;
using ChemProV.PFD.Undos;
using ChemProV.UI;
using ChemProV.Logic.Undos;

namespace ChemProV.UI.DrawingCanvasStates
{
    /// <summary>
    /// The menu state is set as the drawing canvas's current state whenever a right-click occurs. This 
    /// state is responsible for creating and showing an appropriate popup menu and setting up click 
    /// events for the items in the menu.
    /// </summary>
    public class MenuState : IState
    {
        private DrawingCanvas m_canvas;

        private ContextMenu m_contextMenu;

        private Workspace m_workspace;

        public MenuState(DrawingCanvas c, Workspace workspace)
        {
            m_canvas = c;
            m_workspace = workspace;

            // Create the context menu
            m_contextMenu = new ContextMenu();

            // Undo menu item
            MenuItem menuItem = new MenuItem();
            menuItem.Header = m_workspace.UndoTitle;
            menuItem.IsEnabled = (m_workspace.UndoCount > 0);
            m_contextMenu.Items.Add(menuItem);
            menuItem.Click += delegate(object sender, RoutedEventArgs e)
            {
                m_workspace.Undo();

                // Flip back to the default state for the canvas (null)
                m_canvas.CurrentState = null;
            };

            // Redo menu item
            menuItem = new MenuItem();
            menuItem.Header = m_workspace.RedoTitle;
            menuItem.IsEnabled = (m_workspace.RedoCount > 0);
            m_contextMenu.Items.Add(menuItem);
            menuItem.Click += delegate(object sender, RoutedEventArgs e)
            {
                m_workspace.Redo();

                // Flip back to the default state for the canvas (null)
                m_canvas.CurrentState = null;
            };

            // Delete (selected object) menu item
            menuItem = new MenuItem();
            menuItem.Header = "Delete";
            menuItem.IsEnabled = !m_canvas.IsReadOnly;
            m_contextMenu.Items.Add(menuItem);
            menuItem.Click += new RoutedEventHandler(this.Delete);
            
            // Hide all sticky notes menu item
            menuItem = new MenuItem();
            menuItem.Header = "Hide all comments";
            m_contextMenu.Items.Add(menuItem);
            menuItem.Click += new RoutedEventHandler(this.HideStickyNotes);
            
            // Show all sticky notes menu item
            menuItem = new MenuItem();
            menuItem.Header = "Show all comments";
            m_contextMenu.Items.Add(menuItem);
            menuItem.Click += delegate(object sender, RoutedEventArgs e)
            {
                foreach (UIElement uie in m_canvas.Children)
                {
                    // Change visibility if it's a sticky note
                    if (uie is StickyNoteControl)
                    {
                        (uie as StickyNoteControl).Show();
                    }
                }

                // Flip back to the default state for the canvas (null)
                m_canvas.CurrentState = null;
            };

            // ----- Now we start into stuff that's dependent on the selected item -----

            if (m_canvas.SelectedElement is StreamControl)
            {
                AddCommentCollectionMenuOptions(m_contextMenu,
                    "Stream #" + (m_canvas.SelectedElement as StreamControl).Stream.Id.ToString());                

                // Show an option to change the stream ID
                menuItem = new MenuItem();
                menuItem.Header = "Change stream number...";
                m_contextMenu.Items.Add(menuItem);
                menuItem.Click += delegate(object sender, RoutedEventArgs e)
                {                    
                    UI.ChangeStreamNumberWindow win = new ChangeStreamNumberWindow(
                        (m_canvas.SelectedElement as StreamControl).Stream, workspace);
                    Core.App.ControlPalette.SwitchToSelect();
                    win.Show();
                };
            }

            // If the user has right-clicked on a process unit then we want to add subprocess options
            if (m_canvas.SelectedElement is ProcessUnitControl)
            {
                // Show comment options
                if (m_canvas.SelectedElement is PFD.Streams.StreamControl ||
                    m_canvas.SelectedElement is ProcessUnitControl)
                {
                    AddCommentCollectionMenuOptions(m_contextMenu);
                }
                
                AddSubprocessMenuOptions(m_contextMenu,
                    m_canvas.SelectedElement as ProcessUnitControl);
            }
        }

        #region IState Members

        public void MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // This implies that the user clicked somewhere within the canvas that wasn't over 
            // the popup menu. So we need to remove the popup menu from the canvas and flip back 
            // to the default state for the canvas (null;
            m_canvas.Children.Remove(m_contextMenu);
            m_contextMenu = null;
            m_canvas.CurrentState = null;
        }

        #region Unused Mouse Events

        public void MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
        }

        public void MouseWheel(object sender, MouseEventArgs e)
        {
        }

        public void MouseEnter(object sender, MouseEventArgs e)
        {
        }

        public void MouseLeave(object sender, MouseEventArgs e)
        {
        }

        public void MouseMove(object sender, MouseEventArgs e)
        {
        }

        #endregion Unused Mouse Events

        private void HideStickyNotes(object sender, EventArgs e)
        {
            foreach (UIElement uie in m_canvas.Children)
            {
                if (uie is StickyNoteControl)
                {
                    (uie as StickyNoteControl).Hide();
                }
            }

            // Make sure to remove the popup menu from the canvas
            m_canvas.Children.Remove(m_contextMenu);
            m_contextMenu = null;

            // Flip back to the default state for the canvas (null)
            m_canvas.CurrentState = null;
        }

        /// <summary>
        /// This is called when the user selects the Delete from the right click menu
        /// </summary>
        /// <param name="sender">not used</param>
        /// <param name="e">not used</param>
        public void Delete(object sender, EventArgs e)
        {
            // Start by getting rid of the popup menu
            m_canvas.Children.Remove(m_contextMenu);
            m_contextMenu = null;

            // Use the DrawingCanvasCommands static class
            Core.DrawingCanvasCommands.DeleteSelectedElement(m_canvas);

            // Go back to the null state
            m_canvas.CurrentState = null;
        }

        public void LostMouseCapture(object sender, MouseEventArgs e)
        {
        }

        public void StateEnding()
        {
            // Ensure that the popup menu is hidden
            Core.App.ClosePopup();
        }

        #endregion IState Members

        private void AddCommentCollectionMenuOptions(ContextMenu newContextMenu)
        {
            AddCommentCollectionMenuOptions(newContextMenu, "Comment Options");
        }
        
        private void AddCommentCollectionMenuOptions(ContextMenu newContextMenu, string title)
        {
            // Make the header in the menu for the comment-specific options
            MenuItem menuItem = new MenuItem();
            menuItem.Header = title;
            newContextMenu.Items.Add(menuItem);
            // We're using this item as a label, so don't let the user click it
            menuItem.IsHitTestVisible = false;
            // Change the colors to signal to the user that it's a label
            menuItem.Background = new SolidColorBrush(Colors.LightGray);
            menuItem.Foreground = new SolidColorBrush(Color.FromArgb(255, 100, 100, 100));
            menuItem.FontWeight = FontWeights.Bold;

            menuItem = new MenuItem();
            menuItem.Header = "Add new comment";
            menuItem.Tag = m_canvas.SelectedElement;
            newContextMenu.Items.Add(menuItem);

            // Use an anonymous delegate to handle the click event
            menuItem.Click += delegate(object sender, RoutedEventArgs e)
            {
                MenuItem tempMI = sender as MenuItem;
                PFD.Streams.StreamControl stream = tempMI.Tag as PFD.Streams.StreamControl;
                ProcessUnitControl unit = tempMI.Tag as ProcessUnitControl;

                // Get a reference to the comment collection
                IList<StickyNote> comments;
                if (null != stream)
                {
                    comments = stream.Stream.Comments;
                }
                else
                {
                    comments = unit.ProcessUnit.Comments;
                }

                // Compute the location for the new comment
                MathCore.Vector location = StickyNoteControl.ComputeNewCommentNoteLocation(
                    m_canvas, tempMI.Tag, 100.0, 100.0);

                // Create the new sticky note and add it to the workspace. Event handlers will update 
                // the UI appropriately
                StickyNote sn = new StickyNote()
                {
                    Width = 100.0,
                    Height = 100.0,
                    LocationX = location.X,
                    LocationY = location.Y
                };
                comments.Add(sn);

                // Add an undo that will remove the comment
                m_workspace.AddUndo(new UndoRedoCollection("Undo creation of comment",
                    new RemoveComment(comments, comments.IndexOf(sn))));

                // Make sure to remove the popup menu from the canvas
                m_canvas.Children.Remove(m_contextMenu);
                m_contextMenu = null;

                // Flip back to the default state for the canvas (null)
                m_canvas.CurrentState = null;
            };

            string objName = "selected object";
            if (m_canvas.SelectedElement is ProcessUnitControl)
            {
                objName = (m_canvas.SelectedElement as ProcessUnitControl).ProcessUnit.Label;
            }
            else if (m_canvas.SelectedElement is ChemProV.PFD.Streams.StreamControl)
            {
                objName = "selected stream";
            }

            // Add a new menu item to hide all comments
            menuItem = new MenuItem();
            menuItem.Header = "Hide all comments for " + objName;
            menuItem.Tag = m_canvas.SelectedElement;
            newContextMenu.Items.Add(menuItem);
            menuItem.Click += delegate(object sender, RoutedEventArgs e)
            {
                MenuItem tempMI = sender as MenuItem;
                (tempMI.Tag as UI.ICommentControlManager).HideAllComments();

                // Make sure to remove the popup menu from the canvas
                m_canvas.Children.Remove(m_contextMenu);
                m_contextMenu = null;

                // Flip back to the default state for the canvas (null)
                m_canvas.CurrentState = null;
            };

            // Add one to show all comments too
            menuItem = new MenuItem();
            menuItem.Header = "Show all comments for " + objName;
            menuItem.Tag = m_canvas.SelectedElement;
            newContextMenu.Items.Add(menuItem);
            menuItem.Click += delegate(object sender, RoutedEventArgs e)
            {
                MenuItem tempMI = sender as MenuItem;
                (tempMI.Tag as UI.ICommentControlManager).ShowAllComments();

                // Make sure to remove the popup menu from the canvas
                m_canvas.Children.Remove(m_contextMenu);
                m_contextMenu = null;

                // Flip back to the default state for the canvas (null)
                m_canvas.CurrentState = null;
            };
        }

        /// <summary>
        /// Adds menu options for the subprocess color changing
        /// </summary>
        private void AddSubprocessMenuOptions(ContextMenu newContextMenu, ProcessUnitControl pu)
        {
            MenuItem parentMenuItem = new MenuItem();
            parentMenuItem.Header = "Subprocess";
            newContextMenu.Items.Add(parentMenuItem);
            // We're using this item as a label, so don't let the user click it
            parentMenuItem.IsHitTestVisible = false;
            // Change the colors to signal to the user that it's a label
            parentMenuItem.Background = new SolidColorBrush(Colors.LightGray);
            parentMenuItem.Foreground = new SolidColorBrush(Color.FromArgb(255, 100, 100, 100));
            parentMenuItem.FontWeight = FontWeights.Bold;

            if (false)
            {
                // Create a submenu for colors. See the declaration of the Core.NamedColors.All array for 
                // the list of colors that are being used. Make changes in that array (not in this code) 
                // to change the list of available colors.
                foreach (Core.NamedColor nc in Core.NamedColors.All)
                {
                    MenuItem menuItem = new MenuItem();
                    menuItem.Header = nc.Name;
                    menuItem.Tag = System.Tuple.Create<ProcessUnitControl, Color>(
                        pu, nc.Color);

                    // Show the menu item with a check next to it if it's the current color
                    if (nc.Color.Equals(pu.Subprocess))
                    {
                        menuItem.Icon = Core.App.CreateImageFromSource("check_16x16.png");
                    }

                    // Add it to the menu
                    newContextMenu.Items.Add(menuItem);

                    // Use an anonymous delegate to handle the click event
                    menuItem.Click += delegate(object sender, RoutedEventArgs e)
                    {
                        // Get the objects we need
                        MenuItem tempMI = sender as MenuItem;
                        System.Tuple<ProcessUnitControl, Color> t = tempMI.Tag as
                            System.Tuple<ProcessUnitControl, Color>;

                        // Create undo item before setting the new subgroup
                        m_workspace.AddUndo(new UndoRedoCollection("Undo subprocess change",
                            new Logic.Undos.SetSubprocess(t.Item1.ProcessUnit)));

                        // Set the subgroup
                        t.Item1.Subprocess = t.Item2;

                        // Make sure to remove the popup menu from the canvas
                        m_canvas.Children.Remove(m_contextMenu);
                        m_contextMenu = null;

                        // Flip back to the default state for the canvas (null)
                        m_canvas.CurrentState = null;
                    };
                }
            }

            MenuItem mi = new MenuItem();
            mi.Header = "Select subprocess...";
            mi.Click += delegate(object sender, RoutedEventArgs r)
            {
                SubprocessChooserWindow win = new SubprocessChooserWindow(
                    pu as ProcessUnitControl, m_workspace);
                win.Show();

                // Make sure to remove the popup menu from the canvas
                m_canvas.Children.Remove(m_contextMenu);
                m_contextMenu = null;

                // Flip back to the default state for the canvas (null)
                m_canvas.CurrentState = null;
            };
            newContextMenu.Items.Add(mi);
        }

        public void Show(MouseButtonEventArgs mbea)
        {
            if (null != m_contextMenu)
            {
                // Build a popup menu and show it
                System.Windows.Controls.Primitives.Popup pop = new System.Windows.Controls.Primitives.Popup();
                pop.Child = m_contextMenu;
                Core.App.LaunchPopup(pop, mbea);
            }
        }
    }
}