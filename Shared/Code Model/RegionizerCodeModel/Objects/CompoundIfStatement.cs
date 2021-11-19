

#region using statements

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataJuggler.Regionizer.CodeModel.Enumerations;

#endregion

namespace DataJuggler.Regionizer.CodeModel.Objects
{

    #region class CompoundIfStatement
    /// <summary>
    /// This class is used to represent a collection of IfStatements
    /// </summary>
    public class CompoundIfStatement
    {
        
        #region Private Variables
        private List<IfStatement> ifStatements;
        private List<CompoundIfStatement> compoundIfStatements;
        private ComparisonTypeEnum leftComparisonType;
        private ComparisonTypeEnum rightComparisonType;
        private string innerText;
        private int level;
        #endregion
        
        #region Constructor
        /// <summary>
        /// Create a new instance of a 'CompoundIfStatement' object.
        /// </summary>
        public CompoundIfStatement()
        {
            // Perform initializations for this object
            Init();
        }
        #endregion

        #region Methods

            #region GetCommentText(bool addCommentStart, bool skipTheWordIf)
            /// <summary>
            /// This method returns the Comment Text
            /// </summary>
            internal string GetCommentText(bool addCommentStart, bool skipTheWordIf)
            {
                // initial value
                string commentText = "";

                // locals
                StringBuilder sb = new StringBuilder();
                string tempCommentText = "";
                int count = 0;
                int totalCount = 0;
                bool lastItem = false;
                string comparisonText = "";

                // if there are one or more ifStatements
                if (addCommentStart)
                {
                    // if the commentStart should be added
                    sb.Append("// ");
                }

                // if the wordIf should be added
                if (!skipTheWordIf)
                {
                    // append the word if
                    sb.Append(" if ");
                }

                // This item must be loaded by the IfStatementParser.
                // There are two scenarios possible:
                // 1. This object contains a collection of CompoundIfStatements
                // OR
                // 2. This object contains a collection of IfStatements

                // if there are one or more CompoundIfStatements
                if (this.HasOneOrMoreCompoundIfStatements)
                {
                    // set the totalCount
                    totalCount = this.CompoundIfStatements.Count;
                }
                // if there are one or more ifStatements
                else if (this.HasOneOrMoreIfStatements)
                {
                    // set the totalCount
                    totalCount = this.IfStatements.Count;

                    // iterate the ifStatements
                    foreach (IfStatement ifStatement in this.IfStatements)
                    {
                        // increment count
                        count++;

                        // set the value for lastItem
                        lastItem = (count == totalCount);

                        // get the commentText
                        tempCommentText = ifStatement.GetCommentText(false, false);

                        // append this comment
                        sb.Append(tempCommentText);

                        // if count is greater than 1 and this is not the lastItem and the ifStatement has a right comparisonType
                        if ((count > 1) && (!lastItem) && (ifStatement.HasRightComparisonType))
                        {
                            // set the comparisonText
                            comparisonText = ifStatement.GetComparisonText(ifStatement.RightComparisonType);

                            // append the comparisonText
                            sb.Append(comparisonText);

                            // add a space
                            sb.Append(" ");
                        }
                    }
                }

                // return value
                return commentText;
            }
            #endregion
            
            #region Init()
            /// <summary>
            /// This method performs initializations for this object.
            /// </summary>
            public void Init()
            {
                // Create the ifStatements and the CompountIfStatements
                this.IfStatements = new List<IfStatement>();
                this.CompoundIfStatements = new List<CompoundIfStatement>();
            }
            #endregion
            
        #endregion

        #region Properties
           
            #region CompoundIfStatements
            /// <summary>
            /// This property gets or sets the value for 'CompoundIfStatements'.
            /// </summary>
            public List<CompoundIfStatement> CompoundIfStatements
            {
                get { return compoundIfStatements; }
                set { compoundIfStatements = value; }
            }
            #endregion
            
            #region HasCompoundIfStatements
            /// <summary>
            /// This property returns true if this object has a 'CompoundIfStatements'.
            /// </summary>
            public bool HasCompoundIfStatements
            {
                get
                {
                    // initial value
                    bool hasCompoundIfStatements = (this.CompoundIfStatements != null);
                    
                    // return value
                    return hasCompoundIfStatements;
                }
            }
            #endregion
            
            #region HasIfStatements
            /// <summary>
            /// This property returns true if this object has an 'IfStatements'.
            /// </summary>
            public bool HasIfStatements
            {
                get
                {
                    // initial value
                    bool hasIfStatements = (this.IfStatements != null);
                    
                    // return value
                    return hasIfStatements;
                }
            }
            #endregion

            #region HasInnerText
            /// <summary>
            /// This property returns true if the 'InnerText' exists.
            /// </summary>
            public bool HasInnerText
            {
                get
                {
                    // initial value
                    bool hasInnerText = (!String.IsNullOrEmpty(this.InnerText));
                    
                    // return value
                    return hasInnerText;
                }
            }
            #endregion
            
            #region HasLeftComparisonType
            /// <summary>
            /// This property returns true if this object has a 'LeftComparisonType'.
            /// </summary>
            public bool HasLeftComparisonType
            {
                get
                {
                    // initial value
                    bool hasLeftComparisonType = (this.LeftComparisonType != ComparisonTypeEnum.NotSet);

                    // return value
                    return hasLeftComparisonType;
                }
            }
            #endregion

            #region HasOneOrMoreCompoundIfStatements
            /// <summary>
            /// This property returns true if this object has a 'CompoundIfStatements' collections
            /// that contains at least one item.
            /// </summary>
            public bool HasOneOrMoreCompoundIfStatements
            {
                get
                {
                    // initial value
                    bool hasOneOrMoreCompoundIfStatements = ((this.HasCompoundIfStatements) && (this.CompoundIfStatements.Count > 0));

                    // return value
                    return hasOneOrMoreCompoundIfStatements;
                }
            }
            #endregion

            #region HasOneOrMoreIfStatements
            /// <summary>
            /// This property returns true if this object has an 'IfStatements' collections
            /// that contains at least one item.
            /// </summary>
            public bool HasOneOrMoreIfStatements
            {
                get
                {
                    // initial value
                    bool hasOneOrMoreIfStatements = ((this.HasIfStatements) && (this.IfStatements.Count > 0));

                    // return value
                    return hasOneOrMoreIfStatements;
                }
            }
            #endregion

            #region HasRightComparisonType
            /// <summary>
            /// This property returns true if this object has a 'RightComparisonType'.
            /// </summary>
            public bool HasRightComparisonType
            {
                get
                {
                    // initial value
                    bool hasRightComparisonType = (this.RightComparisonType != ComparisonTypeEnum.NotSet);

                    // return value
                    return hasRightComparisonType;
                }
            }
            #endregion
            
            #region IfStatements
            /// <summary>
            /// This property gets or sets the value for 'IfStatements'.
            /// </summary>
            public List<IfStatement> IfStatements
            {
                get { return ifStatements; }
                set { ifStatements = value; }
            }
            #endregion
            
            #region InnerText
            /// <summary>
            /// This property gets or sets the value for 'InnerText'.
            /// </summary>
            public string InnerText
            {
                get { return innerText; }
                set { innerText = value; }
            }
            #endregion
            
            #region LeftComparisonType
            /// <summary>
            /// This property gets or sets the value for 'LeftComparisonType'.
            /// </summary>
            public ComparisonTypeEnum LeftComparisonType
            {
                get { return leftComparisonType; }
                set { leftComparisonType = value; }
            }
            #endregion
            
            #region Level
            /// <summary>
            /// This property gets or sets the value for 'Level'.
            /// </summary>
            public int Level
            {
                get { return level; }
                set { level = value; }
            }
            #endregion
            
            #region RightComparisonType
            /// <summary>
            /// This property gets or sets the value for 'RightComparisonType'.
            /// </summary>
            public ComparisonTypeEnum RightComparisonType
            {
                get { return rightComparisonType; }
                set { rightComparisonType = value; }
            }
            #endregion
            
        #endregion

    }
    #endregion

}

