

#region using statements

using DataJuggler.Regionizer.CodeModel.Enumerations;
using System;
using System.Text;

#endregion

namespace DataJuggler.Regionizer.CodeModel.Objects
{

    #region class IfStatement
    /// <summary>
    /// This method class is used to represent and 'If' statement in C#
    /// </summary>
    public class IfStatement
    {
        
        #region Private Variables
        private object source;
        private object target;
        private ComparisonTypeEnum comparisonType;
        private ComparisonTypeEnum leftComparisonType;
        private ComparisonTypeEnum rightComparisonType;
        private string innerText;
        private int level;
        #endregion

        #region Constructors

            #region Parameterized Constructor(int level)
            /// <summary>
            /// Create a new instance of an IfStatement object.
            /// </summary>
            public IfStatement(int level)
            {
                // store the level
                this.Level = level;

                // Perform initializations for this object
                Init();
            }
            #endregion

            #region Parameterized Constructor(string source, ComparisonTypeEnum comparisonType, string target, int level)
            /// <summary>
            /// Create a new instance of an IfStatement object and set the properties.
            /// </summary>
            /// <param name="source"></param>
            /// <param name="comparisonType"></param>
            /// <param name="target"></param>
            public IfStatement(string source, ComparisonTypeEnum comparisonType, string target, int level)
            {
                // Set the properties
                this.Source = source;
                this.ComparisonType = comparisonType;
                this.Target = target;
                this.Level = level;

                // Perform initializations for this object
                Init();
            }
            #endregion

            #region Parameterized Constructor(string source, ComparisonTypeEnum comparisonType, int level)
            /// <summary>
            /// Create a new instance of an IfStatement object and set the properties.
            /// This override is used for ifStatements that are a single word such as:
            /// if (true) or if (!true). The ComparisonType for this type of ifStatement can only be Equal or NotEqual.
            /// </summary>
            /// <param name="source"></param>
            /// <param name="isEqual"></param>
            public IfStatement(string source, bool isEqual, int level)
            {
                // Set the properties
                this.Source = source;
                
                // if isEqual is true
                if (isEqual)
                {
                    // Set the ComparisonType
                    this.ComparisonType = ComparisonTypeEnum.Equal;
                }
                else
                {
                    // Set the ComparisonType
                    this.ComparisonType = ComparisonTypeEnum.NotEqual;
                }
                
                // Perform initializations for this object
                Init();
            }
            #endregion

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

                // Create a StringBuilder
                StringBuilder sb = new StringBuilder();

                // locals
                string space = " ";
                string comparisonText = GetComparisonText(this.ComparisonType);

                // if the commentStart should be added
                if (addCommentStart)
                {
                    // add the commentStart chars
                    sb.Append("// ");
                }

                // if the word if should not be skipped
                if (!skipTheWordIf)
                {
                    // append the word if
                    sb.Append("if ");
                }

                // append the source, comparisonType and target
                sb.Append(source);
                sb.Append(space);
                sb.Append(comparisonText);
                sb.Append(space);

                // if this object has a target
                if (this.HasTarget)
                {
                    // append the target
                    sb.Append(target);
                }
                else if ((this.ComparisonType == ComparisonTypeEnum.Equal) || (this.ComparisonType == ComparisonTypeEnum.NotEqual))
                {
                    // append the target
                    sb.Append("true");
                }

                // set the return value
                commentText = sb.ToString();

                // return value
                return commentText;
            }
            #endregion
            
            #region GetComparisonText(ComparisonTypeEnum comparisonType)
            /// <summary>
            /// This method returns the Comparison Text for the ComparisonType given.
            /// </summary>
            public string GetComparisonText(ComparisonTypeEnum comparisonType)
            {
                // initial value
                string comparisonText = "";

                // determine the return value based upon a comparisonType
                switch (comparisonType)
                {
                    case ComparisonTypeEnum.LessThan:

                        // set the return value
                        comparisonText = "is less than";

                        // required
                        break;

                    case ComparisonTypeEnum.LessThanOrEqual:

                        // set the return value
                        comparisonText = "is less than or equal to";

                        // required
                        break;

                    case ComparisonTypeEnum.Equal:

                        // set the return value
                        comparisonText = "is equal to";

                        // required
                        break;

                    case ComparisonTypeEnum.GreaterThanOrEqual:

                        // set the return value
                        comparisonText = "is greater than or equal";

                        // required
                        break;

                    case ComparisonTypeEnum.GreaterThan:

                        // set the return value
                        comparisonText = "is greater than";

                        // required
                        break;

                    case ComparisonTypeEnum.NotEqual:

                        // set the return value
                        comparisonText = "is not equal to";

                        // required
                        break;
                }

                // return value
                return comparisonText;
            }
            #endregion
            
            #region Init()
            /// <summary>
            /// This method performs initializations for this object.
            /// </summary>
            public void Init()
            {
                // Create the innerText
                this.InnerText = "";
            }
            #endregion
            
        #endregion

        #region Properties

            #region ComparisonType
            /// <summary>
            /// This property gets or sets the value for 'ComparisonType'.
            /// </summary>
            public ComparisonTypeEnum ComparisonType
            {
                get { return comparisonType; }
                set { comparisonType = value; }
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
            
            #region HasSource
            /// <summary>
            /// This property returns true if this object has a 'Source'.
            /// </summary>
            public bool HasSource
            {
                get
                {
                    // initial value
                    bool hasSource = (this.Source != null);
                    
                    // return value
                    return hasSource;
                }
            }
            #endregion
            
            #region HasTarget
            /// <summary>
            /// This property returns true if this object has a 'Target'.
            /// </summary>
            public bool HasTarget
            {
                get
                {
                    // initial value
                    bool hasTarget = (this.Target != null);
                    
                    // return value
                    return hasTarget;
                }
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
            
            #region Source
            /// <summary>
            /// This property gets or sets the value for 'Source'.
            /// </summary>
            public object Source
            {
                get { return source; }
                set { source = value; }
            }
            #endregion
            
            #region Target
            /// <summary>
            /// This property gets or sets the value for 'Target'.
            /// </summary>
            public object Target
            {
                get { return target; }
                set { target = value; }
            }
            #endregion
            
        #endregion

    }
    #endregion

}
