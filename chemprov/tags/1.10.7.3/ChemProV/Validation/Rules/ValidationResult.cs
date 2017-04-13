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

namespace ChemProV.Validation.Rules
{
    /// <summary>
    /// This class stores the infomation need for a validation result, that is
    /// the target and the associated message
    /// </summary>
    public class ValidationResult
    {
        private object target;
        private string message;
        private static ValidationResult empty = new ValidationResult();

        /// <summary>
        /// This constructer takes a target and a message and sets them to its properties target and message
        /// </summary>
        /// <param name="target">the rule breaker</param>
        /// <param name="message">the feedback message of why the rule was broken</param>
        public ValidationResult(object target, string message)
        {
            Target = target;
            Message = message;
        }

        /// <summary>
        /// Private constructor used in the static ValidationResult.Empty property
        /// </summary>
        private ValidationResult()
        {
            target = null;
            message = null;
        }

        /// <summary>
        /// Returns an empty validation result
        /// </summary>
        public static ValidationResult Empty
        {
            get
            {
                return empty;
            }
        }

        /// <summary>
        /// Returns whether or not the object is empty.
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                if (target == null && message == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Gets or sets the validation result's target
        /// </summary>
        public Object Target
        {
            get
            {
                return target;
            }
            set
            {
                target = value;
            }
        }

        /// <summary>
        /// Gets or sets the message associated with the validation result
        /// </summary>
        public string Message
        {
            get
            {
                return message;
            }
            set
            {
                message = value;
            }
        }

    }
}
