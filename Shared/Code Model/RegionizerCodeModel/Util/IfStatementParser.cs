

#region using statements

using DataJuggler.Core.UltimateHelper.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataJuggler.Regionizer.CodeModel.Objects;
using DataJuggler.Regionizer.CodeModel.Enumerations;

#endregion

namespace DataJuggler.Regionizer.CodeModel.Util
{

    #region class IfStatementParser
    /// <summary>
    /// This class is sued to parse IfStatements and CompountIfStatements out of the sourceCode given.
    /// </summary>
    public class IfStatementParser
    {

        #region private variables
        public const string OpenParenString = "(";
        public const string CloseParenString = ")";
        #endregion
        
        #region Methods

            #region GetComparisonType(string op)
            /// <summary>
            /// This method returns the Comparison Type
            /// </summary>
            private static ComparisonTypeEnum GetComparisonType(string op)
            {
                // initial value
                ComparisonTypeEnum comparisonType = ComparisonTypeEnum.NotSet;

                // determine the operator
                switch (op)
                {
                    case ">":

                        // set the return value
                        comparisonType = ComparisonTypeEnum.GreaterThan;

                        // required
                        break;

                    case ">=":

                        // set the return value
                        comparisonType = ComparisonTypeEnum.GreaterThanOrEqual;

                        // required
                        break;

                    case "==":

                        // set the return value
                        comparisonType = ComparisonTypeEnum.Equal;

                        // required
                        break;

                    case "<=":

                        // set the return value
                        comparisonType = ComparisonTypeEnum.LessThanOrEqual;

                        // required
                        break;

                    case "<":

                        // set the return value
                        comparisonType = ComparisonTypeEnum.LessThan;

                        // required
                        break;
                }

                // return value
                return comparisonType;
            }
            #endregion
            
            #region GetOccurrencesOfChar(string sourceString, char sourceChar)
            /// <summary>
            /// This method returns the Occurrences Of Char
            /// </summary>
            public static int GetOccurrencesOfChar(string sourceString, char sourceChar)
            {
                // initial value
                int count = 0;

                // if the sourceString exists
                if (!String.IsNullOrEmpty(sourceString))
                {
                    // iterate the count
                    foreach (char c in sourceString)
                    {
                        // if this is the sourceChar
                        if (c == sourceChar)
                        {
                            // increment count
                            count++;
                        }
                    }
                }

                // return value
                return count;
            }
            #endregion

            #region IsIfStatement(string sourceCode, bool isCompoundIfStatement)
            /// <summary>
            /// This method returns true if the sourceCode given is an If Statement
            /// </summary>
            public static bool IsIfStatement(string sourceCode, ref bool isCompoundIfStatement)
            {
                // initial value
                bool isIfStatement = false;

                // local
                int openParenCount = 0;
                int closeParenCount = 0;
                string openParenString = "(";
                string closeParenString = ")";
                char openParen = openParenString[0];
                char closeParen = closeParenString[0];

                // if the string exists
                if (!String.IsNullOrEmpty(sourceCode))
                {
                    // trim the sourceCode
                    sourceCode = sourceCode.Trim();

                    // if this is an ifStatement
                    if (sourceCode.StartsWith("if"))
                    {
                        // set the count of each character to make sure we have a valid ifStatement
                        openParenCount = GetOccurrencesOfChar(sourceCode, openParen);
                        closeParenCount = GetOccurrencesOfChar(sourceCode, closeParen);

                        // if there is atleast one set of parens
                        if ((openParenCount == closeParenCount) && (openParenCount >= 1))
                        {
                            // set to true
                            isIfStatement = true;

                            // if there are more than one set of ()'s than this is a compound if statement
                            if (openParenCount > 1)
                            {
                                // this is a compount if statement
                                isCompoundIfStatement = true;
                            }
                        }
                    }
                }

                // return value
                return isIfStatement;
            }
            #endregion
            
            #region ParseCompoundIfStatement(string sourceCode)
            /// <summary>
            /// This method returns the Compound If Statement
            /// </summary>
            internal static CompoundIfStatement ParseCompoundIfStatement(string sourceCode)
            {
                // initial value
                CompoundIfStatement compoundIfStatement = null;

                // locals
                int openParenCount = 0;
                char openParen = OpenParenString[0];
                char closeParen = CloseParenString[0];
                IfStatement ifStatement = null;

                // if the sourceCode exists
                if (!String.IsNullOrEmpty(sourceCode))
                {
                    // iterate the characters in the sourceCode
                    foreach (char c in sourceCode)
                    {
                        // if this is an openParen
                        if (c == openParen)
                        {
                            // increment openParenCount (openParenCount = Level)
                            openParenCount++;

                            // if this is the firstOpenParen
                            if (openParenCount == 1)
                            {
                                // create the return value
                                compoundIfStatement = new CompoundIfStatement();
                            }
                            else if (compoundIfStatement != null)
                            {
                                // create a tempCompoundIfStatement object
                                ifStatement = new IfStatement(openParenCount);

                                // add this ifStatement to the currentCompountIfStatement
                                compoundIfStatement.IfStatements.Add(ifStatement);
                            }
                        }
                        else if (c == closeParen)
                        {
                            // decrement openParenCount
                            openParenCount--;
                        }
                        else
                        {
                            // if the ifStatement exists
                            if (ifStatement != null)
                            {
                                // add this character to the innerText
                                ifStatement.InnerText += (c);
                            }
                        }
                    }
                }

                // return value
                return compoundIfStatement;
            }
            #endregion

            #region ParseIfStatement(string sourceCode, int level = 1)
            /// <summary>
            /// This method attempts to parse an IfStatement object out of the sourceCode given.
            /// </summary>
            public static IfStatement ParseIfStatement(string sourceCode, int level = 1)
            {
                // initial value
                IfStatement ifStatement = null;

                // local
                int openParenCount = 0;
                int closeParenCount = 0;
                int openParenIndex = 0;
                int closeParenIndex = 0;
                char openParen = OpenParenString[0];
                char closeParen = CloseParenString[0];
                int len = 0;
                string innerCode = "";
                string source = "";
                string op = "";
                string target = "";
                ComparisonTypeEnum comparisonType = ComparisonTypeEnum.NotSet;

                // if the sourceCode exists
                if (!String.IsNullOrEmpty(sourceCode))
                {
                    // trim the sourceCode
                    sourceCode = sourceCode.Trim();

                    // if this is an ifStatement
                    if (sourceCode.StartsWith("if"))
                    {
                        // set the count of each character to make sure we have a valid ifStatement
                        openParenCount = GetOccurrencesOfChar(sourceCode, openParen);
                        closeParenCount = GetOccurrencesOfChar(sourceCode, closeParen);

                        // if this is a 'Simple' IfStatement
                        if ((openParenCount == closeParenCount) && (openParenCount == 1))
                        {
                            // get the index of the openParen
                            openParenIndex = sourceCode.IndexOf(openParen);
                            closeParenIndex = sourceCode.IndexOf(closeParen);
                            len = closeParenIndex - openParenIndex - 1;

                            // set the inner code
                            innerCode = sourceCode.Substring(openParenIndex + 1, len);

                            // if the innerCode exists  
                            if (!String.IsNullOrEmpty(innerCode))
                            {
                                // get the words
                                List<Word> words = CSharpCodeParser.ParseWords(innerCode);

                                // if the words collection existsre are exactly 3 words
                                if (words != null)
                                {
                                    // if there is only one word
                                    if (words.Count == 1)
                                    {
                                        // set the sourceWord
                                        source = words[0].Text;

                                        // if this is a NotComparison
                                        bool isEqual = (!source.StartsWith("!"));
                                        
                                        // create the ifStatement
                                        ifStatement = new IfStatement(source, isEqual, level);
                                    }
                                    // if there are exactly 3 words
                                    else if (words.Count == 3)
                                    {
                                        // parse out the items that build the ifStatement
                                        source = words[0].Text;
                                        op = words[1].Text;
                                        target = words[2].Text;

                                        // Get the comparisonType for the operator
                                        comparisonType = IfStatementParser.GetComparisonType(op);

                                        // now create the ifStatement
                                        ifStatement = new IfStatement(source, comparisonType, target, level);
                                    }
                                }
                            }
                        }
                    }  
                }

                // return value
                return ifStatement;
            }
            #endregion
            
        #endregion

    }
    #endregion

}
