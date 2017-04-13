/*
Copyright 2010 HELP Lab @ Washington State University

This file is part of ChemProV (http://helplab.org/chemprov).

ChemProV is distributed under the Open Software License ("OSL") v3.0.
Consult "LICENSE.txt" included in this package for the complete OSL license.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml.Linq;
using ChemProV.PFD;
using ChemProV.PFD.EquationEditor;
using ChemProV.UI.DrawingCanvas;
using ChemProV.Validation.Feedback;
using ChemProV.Validation.Rules;

namespace ChemProV.UI
{
    public partial class WorkSpace : UserControl
    {
        public event EventHandler ToolPlaced = delegate { };
        public event EventHandler UpdateCompounds = delegate { };
        public event EventHandler ValidationChecked = delegate { };

        private bool isLoadingFile = false;

        private bool isReadOnly = false;

        public bool IsReadOnly
        {
            get { return isReadOnly; }
            set
            {
                isReadOnly = value;
                DrawingCanvas.IsReadOnly = value;
            }
        }

        private List<Tuple<string, Equation>> userDefinedVaraibles = new List<Tuple<string, Equation>>();

        private RuleManager ruleManager = RuleManager.GetInstance();

        private bool checkRules = true;

        private OptionDifficultySetting currentDifficultySetting;

        public OptionDifficultySetting CurrentDifficultySetting
        {
            get { return currentDifficultySetting; }
            set
            {
                if (value != currentDifficultySetting)
                {
                    DifficultySettingChanged(currentDifficultySetting, value);
                    currentDifficultySetting = value;
                }
            }
        }

        public bool CheckRules
        {
            get { return checkRules; }
            set { checkRules = value; }
        }

        public void GotKeyDown(object sender, KeyEventArgs e)
        {
            DrawingCanvas.GotKeyDown(sender, e);
        }

        /// <summary>
        /// gets a reference to the DrawingCanvas used by WorkSpace
        /// </summary>
        public DrawingCanvas.DrawingCanvas DrawingCanvasReference
        {
            get
            {
                return DrawingCanvas;
            }
        }

        /// <summary>
        /// gets a reference to the EquationEditor used by WorkSpace
        /// </summary>
        public EquationEditor EquationEditorReference
        {
            get
            {
                return EquationEditor;
            }
        }

        /// <summary>
        /// gets a reference to the FeedbackWindow used by WorkSpace
        /// </summary>
        public FeedbackWindow FeedbackWindowReference
        {
            get
            {
                return FeedbackWindow;
            }
        }

        public void Redo()
        {
            //pass it on down
            DrawingCanvas.Redo();
        }

        public void Undo()
        {
            //pass it on down
            DrawingCanvas.Undo();
        }

        public void ClearWorkSpace()
        {
            //now, clear the drawing drawing_canvas
            DrawingCanvas.ClearDrawingCanvas();
            EquationEditor.ClearEquations();

            //clear any existing messages in the feedback window and rerun the error checker
            CheckRulesForPFD(this, EventArgs.Empty);
        }

        public WorkSpace(bool isReadOnly)
        {
            InitializeComponent();
            IsReadOnly = isReadOnly;
            DrawingCanvas.PfdChanging += new EventHandler(DrawingCanvas_PfdChanging);
            DrawingCanvas.ToolPlaced += new EventHandler(DrawingCanvas_ToolPlaced);
            DrawingCanvas.PfdUpdated += new PfdUpdatedEventHandler(CheckRulesForPFD);
            GridSplitter.MouseMove += new MouseEventHandler(GridSplitter_MouseMove);
            EquationEditor.EquationTokensChanged += new EventHandler(CheckRulesForPFD);
            SizeChanged += new SizeChangedEventHandler(WorkSpace_SizeChanged);
        }

        private void DrawingCanvas_PfdChanging(object sender, EventArgs e)
        {
            FeedbackWindow.FeedbackStatusChanged(FeedbackStatus.ChangedButNotChecked);
        }

        public WorkSpace()
        {
            InitializeComponent();

            //this will make the workspace and everything in it readonly
            //IsReadOnly = true;
            EquationEditor.IsReadOnly = isReadOnly;
            if (isReadOnly)
            {
            }
            else
            {
                DrawingCanvas.ToolPlaced += new EventHandler(DrawingCanvas_ToolPlaced);
                DrawingCanvas.PfdUpdated += new PfdUpdatedEventHandler(CheckRulesForPFD);
                EquationEditor.EquationTokensChanged += new EventHandler(CheckRulesForPFD);
            }
            GridSplitter.MouseMove += new MouseEventHandler(GridSplitter_MouseMove);
            SizeChanged += new SizeChangedEventHandler(WorkSpace_SizeChanged);
        }

        private void WorkSpace_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            FixSizeOfComponents();
        }

        public void RemoveScrollViewerFromDrawingCanvas()
        {
            this.WorkspaceGrid.Children.Remove(this.DrawingCanvasScollViewer);
            this.DrawingCanvasScollViewer.Content = null;
            this.WorkspaceGrid.Children.Add(this.DrawingCanvas);
        }

        public void DifficultySettingChanged(OptionDifficultySetting oldValue, OptionDifficultySetting newValue)
        {
            DrawingCanvas.CurrentDifficultySetting = newValue;
        }

        private void DrawingCanvas_ToolPlaced(object sender, EventArgs e)
        {
            ToolPlaced(this, EventArgs.Empty);
        }

        /// <summary>
        /// Since Canvas object don't auto-resize, this method needs to be called
        /// whenever the main window gets resized so that we can resize our drawing
        /// drawing_canvas appropriately.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GridSplitter_MouseMove(object sender, MouseEventArgs e)
        {
            FixSizeOfComponents();
        }

        private void FixSizeOfComponents()
        {
            //set the drawing_canvas' scroll viewer's size
            double height = WorkspaceGrid.RowDefinitions[0].ActualHeight;
            double width = WorkspaceGrid.ColumnDefinitions[0].ActualWidth;
            DrawingCanvasScollViewer.Width = width;
            DrawingCanvasScollViewer.Height = height;

            DrawingCanvas.MinHeight = height;
            DrawingCanvas.MinWidth = width;

            //set the feedback and equation window height
            height = WorkspaceGrid.RowDefinitions[2].ActualHeight;

            if (height < 33)
            {
                height = 33;
            }
            FeedbackWindow.Height = height;
            EquationEditor.Height = height;
        }

        public void UserDefinedVariablesUpdated(List<Tuple<string, Equation>> newVariables)
        {
            userDefinedVaraibles = newVariables;
            CheckRulesForPFD(null, EventArgs.Empty);
        }

        /// <summary>
        /// This fires when an equation is changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void CheckRulesForPFD(object sender, EventArgs e)
        {
            if (!isLoadingFile)
            {
                var pfdElements = from c in DrawingCanvas.Children
                                  where c is IPfdElement
                                  select c as IPfdElement;

                foreach (IPfdElement pfdElement in pfdElements)
                {
                    pfdElement.RemoveFeedback();
                }
                ruleManager.Validate(pfdElements, EquationEditor.EquationTokens, userDefinedVaraibles);
                FeedbackWindow.updateFeedbackWindow(ruleManager.ErrorMessages);
                ValidationChecked(this, EventArgs.Empty);
            }
        }

        public void LoadXmlElements(XDocument doc)
        {
            isLoadingFile = true;
            //clear out previous daat
            DrawingCanvas.ClearDrawingCanvas();
            EquationEditor.ClearEquations();

            //tell the drawing drawing_canvas to load its new children
            DrawingCanvas.LoadXmlElements(doc.Descendants("DrawingCanvas").ElementAt(0));

            //load the equations
            EquationEditor.LoadXmlElements(doc.Descendants("EquationEditor").ElementAt(0));

            FeedbackWindow.LoadXmlElements(doc.Descendants("FeedbackWindow").ElementAt(0));

            //done loading the file so set isLoadingFile to false and call the CheckRulesForPFD to check the rules
            isLoadingFile = false;

            //clear any existing messages in the feedback window and rerun the error checker
            CheckRulesForPFD(this, EventArgs.Empty);
        }

        public object GetobjectFromId(string id)
        {
            return null;
        }
    }
}