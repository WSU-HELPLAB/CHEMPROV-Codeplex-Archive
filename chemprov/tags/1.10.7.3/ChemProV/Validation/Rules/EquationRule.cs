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
using System.Collections.ObjectModel;
using System.Collections.Generic;

using ChemProV.PFD.EquationEditor.Tokens;
using ChemProV.PFD.EquationEditor;
using ChemProV.PFD.Streams.PropertiesTable;
using ChemProV.PFD.Streams.PropertiesTable.Chemical;

namespace ChemProV.Validation.Rules
{
    /// <summary>
    /// This class checks all the ruels for all the equations at once and then for solvability
    /// 
    /// </summary>
    public class EquationRule : IRule
    {
        /// <summary>
        /// This list is all of our ChemicalEquations
        /// </summary>
        private List<Tuple<object, ObservableCollection<IEquationToken>>> ChemicalEquations = new List<Tuple<object,ObservableCollection<IEquationToken>>>();

        /// <summary>
        /// This list is all of our HeatEquations
        /// </summary>
        private List<Tuple<object, ObservableCollection<IEquationToken>>> HeatEquations = new List<Tuple<object,ObservableCollection<IEquationToken>>>();

        /// <summary>
        /// This checks all the rules for all the equations, and then solvabilty
        /// </summary>
        public void CheckRule()
        {
            //clear any prevous messages
            results.Clear();

            ValidationResult vr;

            //minus 1 because the last equation doesn't count
            if (listOfEquations != null || listOfEquations.Count - 1 <= 0)
            {
                //So in this tuple item1 is a reference to the Equation class, item2 is the parsed tokens from the equation
                foreach (Tuple<object, ObservableCollection<IEquationToken>> tuple in listOfEquations)
                {
                    ObservableCollection<IEquationToken> equation = tuple.Item2;
                    List<string> variablesNames = BuildVariableList(equation);
                    VariableNamesPerEquation.Add(variablesNames);
                    EquationSemanticsRule esr = new EquationSemanticsRule(equation, variablesNames, tuple.Item1);
                    esr.DictionaryOfTableData = DictionaryOfTableData;
                    esr.ChemicalEquations = ChemicalEquations;
                    esr.HeatEquations = HeatEquations;
                    vr = esr.CheckRule();
                    if(!vr.IsEmpty)
                    {
                        ValidationResults.Add(vr);
                    }
                }
                    vr = UseAllUnknowns();
                    if(!vr.IsEmpty)
                    {
                        ValidationResults.Add(vr);
                    }

                if (ValidationResults.Count == 0)
                {
                    ValidationResults.Add(Solvability());
                }

            }
        }

        /// <summary>
        /// This checks to see if the set of equations is solveable
        /// </summary>
        /// <returns></returns>
    ValidationResult Solvability()
    {
                        //This is our dictionary that will hold all the variables we find on the left hand side of one equation
                Dictionary<IEquationToken, float> Dict_lhs = new Dictionary<IEquationToken, float>();

                //This is our dictionary that will hold all the variables we find on the right hand side of one equation
                Dictionary<IEquationToken, float> Dict_rhs = new Dictionary<IEquationToken, float>();

                //This is our list which holds all the dictionarys for each equation for the lhs
                //So you can think of each dictionary as a row in our matrix and each dictionary pair as an cell in the matrix
                List<Dictionary<IEquationToken, float>> listOfDictionaries = new List<Dictionary<IEquationToken, float>>();

                foreach (Tuple<object, ObservableCollection<IEquationToken>> tuple in listOfEquations)
                {
                    if (tuple.Item2.Count != 0)
                    {
                        Dict_lhs.Clear();
                        Dict_rhs.Clear();
                        int equalSignLocation = findEqualsSign(tuple.Item2);
                        ObservableCollection<IEquationToken> lhs = new ObservableCollection<IEquationToken>(tuple.Item2);
                        ObservableCollection<IEquationToken> rhs = new ObservableCollection<IEquationToken>(tuple.Item2);

                        if (equalSignLocation != 0)
                        {

                            //Now for lhs remove everything at and past the location of the equalSign
                            for (int j = equalSignLocation; j < lhs.Count; lhs.RemoveAt(j))
                            {
                            }

                            //Reomve everything from 0 to equalSignLocation + 1 to get rid of the left hind side and the equalSign .
                            for (int j = 0; j < equalSignLocation + 1; rhs.RemoveAt(0))
                            {
                                j++;
                            }

                            if (DictionaryBuilder(Dict_lhs, lhs) == 0)
                            {
                                return new ValidationResult(listOfEquations[0].Item1, ErrorMessageGenerator.GenerateMesssage(ErrorMessages.Quadratic_Equation));
                            }
                            if (DictionaryBuilder(Dict_rhs, rhs) == 0)
                            {
                                return new ValidationResult(listOfEquations[0].Item1, ErrorMessageGenerator.GenerateMesssage(ErrorMessages.Quadratic_Equation));
                            }

                            //Makes lhs contain all
                            MergeDictionaries(Dict_lhs, Dict_rhs);

                            //cannot just be refernce needs to be a deep copy
                            listOfDictionaries.Add(new Dictionary<IEquationToken, float>(Dict_lhs));
                            //need to check here? if number of unknowns is equal to number of equations

                        }
                    }
                }
                if (listOfDictionaries.Count > 0)
                {
                    float[,] matrix = buildMatrix(listOfDictionaries);

                    float determinate;

                    if (matrix.GetLength(0) != matrix.GetLength(1))
                    {
                        return new ValidationResult(listOfEquations[0].Item1, ErrorMessageGenerator.GenerateMesssage(ErrorMessages.Equations_and_Unknowns, new object[2] {matrix.GetLength(0).ToString(), matrix.GetLength(1).ToString()}));
                    }

                    if (matrix.GetLength(0) <= 2)
                    {
                        determinate = findDeterminant2by2(matrix);
                    }
                    else
                    {
                        determinate = findDeterminantNbyN(matrix);
                    }

                    if (determinate == 0)
                    {
                        return new ValidationResult(listOfEquations[0].Item1, ErrorMessageGenerator.GenerateMesssage(ErrorMessages.Not_Independent));
                    }
                    else
                    {
                        return new ValidationResult(listOfEquations[0].Item1, ErrorMessageGenerator.GenerateMesssage(ErrorMessages.Solvable));
                    }
                }
                else
                {
                    return new ValidationResult(listOfEquations[0].Item1, ErrorMessageGenerator.GenerateMesssage(ErrorMessages.Insuffcient_infomation));
                }
            }

        float findDeterminantNbyN(float[,] matrix)
        {
            float determinant = 0;
            int column = 0;
            int numOfColumns = matrix.GetLength(1); //= NUM OF COLUMNS
            int numOfRows = matrix.GetLength(0); //= NUM OF COLUMNS
            float smallerDet = 0;
            float[,] smallerMatrix = new float[numOfRows - 1, numOfColumns - 1];

            while (column < numOfColumns)
            {
                //index is rows and we always through out the first 1 so start it at 1 and not 0
                int i = 1;

                //j is columns need to ignore the row when j is equal the current column
                int j = 0;

                //when we skip the column we need to subtract 1 from j when inserting into smaller matrix
                int skippedColumn = 0;
                while (i < numOfRows)
                {
                    j = 0;
                    skippedColumn = 0;
                    while (j < numOfColumns)
                    {
                        if (j != column)
                        {
                            smallerMatrix[i - 1, j - skippedColumn] = matrix[i, j];
                        }
                        else
                        {
                            //instead of using a bool use int and only matters when skippedColumn is 1
                            skippedColumn = 1;
                        }
                        j++;
                    }
                    i++;
                }
                if (numOfColumns - 1 == 2)
                {
                    smallerDet = findDeterminant2by2(smallerMatrix);
                }
                else
                {
                    smallerDet = findDeterminantNbyN(smallerMatrix);
                }
                determinant += smallerDet;
                column++;
            }

            return determinant;
        }

        float findDeterminant2by2(float[,] matrix)
        {
            if (matrix.GetLength(1) < 2)
            {
                //assume solvability :)
                return 1;
            }
            else if (matrix.GetLength(1) >= 2 && matrix.GetLength(0) < 2)
            {
                //NOT SOLVABLE
                return 0;
            }
            else
            {
                return (matrix[0, 0] * matrix[1, 1] - matrix[0, 1] * matrix[1, 0]);
            }
        }

        float[,] buildMatrix(List<Dictionary<IEquationToken, float>> listOfDictionaries)
        {
            //We will use this to see how many different columns in our matrix we need and keep track of what column goes with what variable
            Dictionary<string, int> keyToMatrix = new Dictionary<string, int>();

            //This is the number of variables we have found so far and is also the 
            int i = 0;
            foreach (Dictionary<IEquationToken, float> dict in listOfDictionaries)
            {
                List<Tuple<string, int>> row = new List<Tuple<string, int>>();
                foreach (IEquationToken key in dict.Keys)
                {
                    try
                    {
                        float.Parse(key.Value);
                    }
                    catch
                    {
                        if (!(keyToMatrix.ContainsKey(key.Value)))
                        {
                            keyToMatrix.Add(key.Value, i);
                            i++;
                        }
                    }
                }
            }
            //row, column
            //listOfDictionaries.Count gives me the number of equations we have
            float[,] matrix = new float[listOfDictionaries.Count, i];
            int equationNumber = 0;
            foreach (Dictionary<IEquationToken, float> dict in listOfDictionaries)
            {
                foreach (IEquationToken key in dict.Keys)
                {
                    float coefficent;
                    int column;
                    dict.TryGetValue(key, out coefficent);
                    keyToMatrix.TryGetValue(key.Value, out column);
                    matrix[equationNumber, column] += coefficent;
                }
                equationNumber++;
            }
            return matrix;
        }

        void MergeDictionaries(Dictionary<IEquationToken, float> lhs, Dictionary<IEquationToken, float> rhs)
        {
            foreach (IEquationToken key in rhs.Keys)
            {
                if (lhs.ContainsKey(key))
                {
                    float lhsValue;
                    float rhsValue;
                    lhs.TryGetValue(key, out lhsValue);
                    rhs.TryGetValue(key, out rhsValue);
                    lhs.Remove(key);
                    lhs.Add(key, rhsValue + lhsValue);
                }
                else
                {
                    float value;
                    rhs.TryGetValue(key, out value);
                    lhs.Add(key, value);
                }
                
            }
        }

        int findEqualsSign(ObservableCollection<IEquationToken> equation)
        {
            int i = 0;
            foreach (IEquationToken token in equation)
            {
                if (token.Value == "=")
                {
                    return i;
                }
                i++;
            }
            return (0);
        }

        /// <summary>
        /// The takes the simplified equation and builds the dictionary which will later be used to be translated into a matrix
        /// </summary>
        /// <param name="dict"></param>
        /// <param name="equation">The function MUST be in this form x*A + y*B + x / 100 *y... + C  (+ can be - or +)</param>
        /// <returns>1 for ok, 0 for not</returns>
        int DictionaryBuilder(Dictionary<IEquationToken, float> dict, ObservableCollection<IEquationToken> equation)
        {
            //index is used to keep track of where we are in the list
            int i = 0;
            float scalar = 1;

            substitution(equation);

          //  combineLikeTerms(equation);

            while (i + 1 < equation.Count)
            {
                IEquationToken token = equation[i];
                if (token is VariableToken)
                {
                    if (equation[i + 1].Value == "/")
                    {
                        if (i + 4 < equation.Count)
                        {
                            if (equation[i + 2].Value == "100" && equation[i + 3].Value == "*")
                            {
                                float firstValue = -1;
                                float secondValue = -1;
                                float currentScalar;
                                try
                                {
                                    firstValue = float.Parse(equation[i].Value);
                                    try
                                    {
                                        secondValue = float.Parse(equation[i + 4].Value);

                                        //we know both so skip it

                                        i += 3;
                                    }
                                    catch
                                    {
                                        //know first variable but not second

                                        firstValue = firstValue / 100;
                                        if (dict.ContainsKey(equation[i+4]))
                                        {
                                            dict.TryGetValue(equation[i+4], out currentScalar);
                                            dict.Remove(equation[i+4]);
                                            dict.Add(equation[i+4], currentScalar + firstValue);
                                        }
                                        else
                                        {
                                            dict.Add(equation[i+4], firstValue);
                                        }
                                        i += 3;
                                    }
                                }
                                catch
                                {
                                    try
                                    {
                                        secondValue = float.Parse(equation[i + 4].Value);

                                        //didn't fail so know second variable but not first

                                        //multiple secondValue by 100 and then 1 over that answer to get in decimal form
                                        secondValue = 1 / (secondValue * 100);
                                        if (dict.ContainsKey(equation[i]))
                                        {
                                            dict.TryGetValue(equation[i], out currentScalar);
                                            dict.Remove(equation[i]);
                                            dict.Add(equation[i], currentScalar + secondValue);
                                        }
                                        else
                                        {
                                            dict.Add(equation[i], secondValue);
                                        }
                                        i += 3;
                                    }
                                    catch
                                    {
                                        //this means we have unkown times unkown so no longer linear so no longer solvable by this program
                                        return 0;
                                    }
                                }
                            }
                        }
                        else
                        {
                            //ERROR divided used but not like a percent
                        }
                    }
                    else if (equation[i + 1].Value == "*")
                    {
                        float currentScalar = 0;
                        try
                        {
                            scalar = float.Parse(equation[i + 2].Value);
                            if (dict.ContainsKey(equation[i]))
                            {
                                dict.TryGetValue(equation[i], out currentScalar);
                                dict.Remove(equation[i]);
                                dict.Add(equation[i], currentScalar + scalar);
                            }
                            else
                            {
                                try
                                {
                                    float.Parse(equation[i].Value);
                                }
                                catch
                                {
                                    dict.Add(equation[i], scalar);
                                }
                            }
                        }
                        catch
                        {
                            //two things are multiplied together and it isn't a percent we got a problem
                            //ERROR? ignore?
                        }
                    }
                    else if (equation[i + 1].Value == "+" || equation[i + 1].Value == "-")
                    {
                        //Scalor is 1

                        float currentScalar = 0;
                        if (dict.ContainsKey(equation[i]))
                        {
                            dict.TryGetValue(equation[i], out currentScalar);
                            dict.Remove(equation[i]);
                            if (equation[i + 1].Value == "+")
                            {
                                dict.Add(equation[i], currentScalar + 1);
                            }
                            else
                            {
                                dict.Add(equation[i], currentScalar - 1);
                            }
                        }
                        else
                        {
                            if (equation[i + 1].Value == "+")
                            {
                                dict.Add(equation[i], 1);
                            }
                            else
                            {
                                dict.Add(equation[i], -1);
                            }
                        }
                    }
                }
                i += 2;
            }
            //This is the last term
            if (equation.Count > 0 && i < equation.Count)
            {
                IEquationToken lastToken = equation[i];
                if (lastToken is OperatorToken)
                {
                    //FAIL because the last token is an operator
                }
                else
                {
                    float result = -1;
                    try
                    {
                        //if this does not fail it is a constant in which case we don't care about it
                        result = float.Parse(lastToken.Value);
                    }
                    catch
                    {
                        //otherwise it is a variable and we add it to our dict
                        if (dict.ContainsKey(lastToken))
                        {
                            float value;
                            dict.TryGetValue(lastToken, out value);
                            dict.Remove(lastToken);

                            //once it see's that index = 0 it will not bother to evaluate the other side so dont have to worry
                            //about going out of bounds
                            if (i == 0 || equation[i - 1].Value == "+")
                            {
                                dict.Add(lastToken, value + 1);
                            }
                            else
                            {
                                dict.Add(lastToken, value - 1);
                            }
                        }
                        //once it see's that index = 0 it will not bother to evaluate the other side so dont have to worry
                        //about going out of bounds
                        if (i == 0 || equation[i - 1].Value == "+")
                        {
                            dict.Add(lastToken, 1);
                        }
                        else
                        {
                            dict.Add(lastToken, 1);
                        }
                    }
                }
            }
            return 1;
        }

        void substitution(ObservableCollection<IEquationToken> equation)
        {
            GenericTableData data;
            foreach (IEquationToken token in equation)
            {
                if (token is VariableToken)
                {
                    if (DictionaryOfTableData.ContainsKey(token.Value))
                    {
                        DictionaryOfTableData.TryGetValue(token.Value, out data);
                        if (data.Quantity != "?")
                        {
                            token.Value = data.Quantity;
                        }
                    }
                }
            }
        }

        void combineLikeTerms(ObservableCollection<IEquationToken> equation)
        {
            int i = 0;
            bool increment = true;
            while (i < equation.Count)
            {
                IEquationToken token = equation[i];
                if (i + 1 != equation.Count)
                {
                    if (token is VariableToken)
                    {
                        if (equation[i + 1].Value == "+" || equation[i + 1].Value == "-")
                        {
                            float value;
                            try
                            {
                                value = float.Parse(token.Value);

                                //is a constant so we dont care about it

                                equation.RemoveAt(i);

                                //did not forget to increment
                                equation.RemoveAt(i);

                                //We just removed to element that is like we we incremented 2 so we need to not 
                                //increment at the end of this loop
                                increment = false;

                            }
                            catch
                            {
                                //Not a number
                            }
                        }
                        else if (equation[i + 1].Value == "/")
                        {
                            i += 4;
                            increment = false;
                        }
                        else if (equation[i + 1].Value == "*")
                        {
                            //1 because we times digit by it if it was zero we would get zero
                            float scalar = 1;
                            if (i > 0 && equation[i - 1].Value != "/")
                            {
                                while (i + 1 < equation.Count && equation[i + 1].Value == "*")
                                {
                                    float digit = 1;
                                    try
                                    {
                                        digit = float.Parse(token.Value);
                                        scalar *= digit;

                                        //This also serves as incrementing so do not increament as well as removing
                                        equation.RemoveAt(i);
                                        equation.RemoveAt(i);
                                    }
                                    catch
                                    {
                                        //Not a number

                                        //Need to increment
                                        i += 2;
                                    }
                                }
                            }

                            //Do not want to complicate things so check to make sure we actually did something
                            if (scalar != 1)
                            {
                                equation.Insert(i, new OperatorToken("*"));
                                equation.Insert(i + 1, new VariableToken(scalar.ToString()));
                            }
                        }
                    }
                }
                //Want next variable already did the operation
                if (increment == true)
                {
                    i += 2;
                }
                else
                {
                    increment = true;
                }
            }
        }

        ValidationResult UseAllUnknowns()
        {
            List<string> UnknowVariables = new List<string>();
            HashSet<string> hashtable = new HashSet<string>();
            List<string> unusedVariables = new List<string>();

            foreach (string key in DictionaryOfTableData.Keys)
            {
                GenericTableData data;
                try
                {
                    DictionaryOfTableData.TryGetValue(key, out data);
                    if (data.Quantity == "?")
                    {
                        UnknowVariables.Add(key);
                    }
                    try
                    {
                        double.Parse(data.Temperature);
                    }
                    catch
                    {
                        if (data.Temperature != "")
                        {
                            UnknowVariables.Add(data.Temperature);
                        }
                    }
                }
                catch
                {
                }
            }
            

            foreach (List<string> list in VariableNamesPerEquation)
            {
                foreach (string s in list)
                {
                    if (!hashtable.Contains(s))
                    {
                        hashtable.Add(s);
                    }
                }
            }

            foreach(string s in UnknowVariables)
            {
                if (!hashtable.Contains(s))
                {
                    unusedVariables.Add(s);
                }
            }

            if (unusedVariables.Count > 0)
            {
                return new ValidationResult(listOfEquations[0].Item1, ErrorMessageGenerator.GenerateMesssage(ErrorMessages.Unused_Variables, unusedVariables.ToArray()));
            } 

            return ValidationResult.Empty;

        }

        List<string> BuildVariableList(ObservableCollection<IEquationToken> equation)
        {
            List<string> variables = new List<string>();

            foreach(IEquationToken token in equation)
            {
                if (token is VariableToken)
                {
                    try
                    {
                        float.Parse(token.Value);
                        //it is a number so dont add to our list
                    }
                    catch
                    {
                        //not a number so must be a variable
                        variables.Add(token.Value);
                    }

                }
            }

            return variables;
        }


        /// <summary>
        /// This gets updated from the RuleManger
        /// </summary>
        public Dictionary<string, GenericTableData> DictionaryOfTableData;

        private ObservableCollection<ValidationResult> results = new ObservableCollection<ValidationResult>();

        /// <summary>
        /// This is a list of all the errors CheckRule found
        /// </summary>
        public ObservableCollection<ValidationResult> ValidationResults
        {
            get {return results;}
        }

        private List<List<string>> VariableNamesPerEquation = new List<List<string>>();

        /// <summary>
        /// This gets updated by the RuleManger
        /// </summary>
        public ObservableCollection<Tuple<object, ObservableCollection<IEquationToken>>> listOfEquations;

        private object target;

        /// <summary>
        /// This is a reference to the Equation that this rule is checking
        /// </summary>
        public object Target
        {
            get {return target;}
            set {target = value;}
        }
    }
}
