﻿using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.Generic;

namespace ChemProV.Validation.Rules
{
    /// <summary>
    /// This is our interface for any rule.
    /// </summary>
    public interface IRule
    {
        /// <summary>
        /// Called to perform the underlying rule validation logic
        /// </summary>
        void CheckRule();

        /// <summary>
        /// A list of validation results associated with a particular rule
        /// </summary>
        ObservableCollection<ValidationResult> ValidationResults
        {
            get;
        }

        /// <summary>
        /// The target that the rule will be checking.
        /// </summary>
        Object Target
        {
            get;
            set;
        }
    }
}
