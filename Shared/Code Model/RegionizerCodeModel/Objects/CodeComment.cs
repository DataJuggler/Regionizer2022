

#region using statements

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataJuggler.Regionizer.CodeModel.Enumerations;

#endregion

namespace DataJuggler.Regionizer.CodeModel.Objects
{

    #region class Comment
    /// <summary>
    /// This class represents a code item that needs to be commented
    /// </summary>
    [Serializable]
    public class CodeComment
    {
        
        #region Private Variables
        private string comment;
        private int iD;
        private string name;
        private string pattern;
        private string targetPattern;
        private string targetPattern2;
        private bool hasReplacements;
        private List<Replacement> replacements;
        #endregion

        #region Methods

            #region ToString()
            /// <summary>
            /// This method returns the name of this Comment
            /// </summary>
            public override string ToString()
            {
                // return the name of this Comment
                return this.Name;
            }
            #endregion
            
        #endregion

        #region Properties

            #region Comment
            /// <summary>
            /// This property gets or sets the value for 'Comment'.
            /// </summary>
            public string Comment
            {
                get { return comment; }
                set { comment = value; }
            }
            #endregion
            
            #region HasComment
            /// <summary>
            /// This property returns true if the 'Comment' exists.
            /// </summary>
            public bool HasComment
            {
                get
                {
                    // initial value
                    bool hasComment = (!String.IsNullOrEmpty(this.Comment));
                    
                    // return value
                    return hasComment;
                }
            }
            #endregion
            
            #region HasCommentAndPattern
            /// <summary>
            /// This read only property returns the value for 'HasCommentAndPattern'.
            /// </summary>
            public bool HasCommentAndPattern
            {
                get
                {
                    // initial value
                    bool hasCommentAndPattern = ((this.HasComment) && (this.HasPattern));
                    
                    // return value
                    return hasCommentAndPattern;
                }
            }
            #endregion
            
            #region HasID
            /// <summary>
            /// This property returns true if the 'ID' is set.
            /// </summary>
            public bool HasID
            {
                get
                {
                    // initial value
                    bool hasID = (this.ID > 0);
                    
                    // return value
                    return hasID;
                }
            }
            #endregion
            
            #region HasPattern
            /// <summary>
            /// This property returns true if the 'Pattern' exists.
            /// </summary>
            public bool HasPattern
            {
                get
                {
                    // initial value
                    bool hasPattern = (!String.IsNullOrEmpty(this.Pattern));
                    
                    // return value
                    return hasPattern;
                }
            }
            #endregion
            
            #region HasReplacements
            /// <summary>
            /// This property gets or sets the value for 'HasReplacements'.
            /// </summary>
            public bool HasReplacements
            {
                get { return hasReplacements; }
                set { hasReplacements = value; }
            }
            #endregion
            
            #region HasTargetPattern
            /// <summary>
            /// This property returns true if the 'TargetPattern' exists.
            /// </summary>
            public bool HasTargetPattern
            {
                get
                {
                    // initial value
                    bool hasTargetPattern = (!String.IsNullOrEmpty(this.TargetPattern));
                    
                    // return value
                    return hasTargetPattern;
                }
            }
            #endregion
            
            #region HasTargetPattern2
            /// <summary>
            /// This property returns true if the 'TargetPattern2' exists.
            /// </summary>
            public bool HasTargetPattern2
            {
                get
                {
                    // initial value
                    bool hasTargetPattern2 = (!String.IsNullOrEmpty(this.TargetPattern2));
                    
                    // return value
                    return hasTargetPattern2;
                }
            }
            #endregion
            
            #region ID
            /// <summary>
            /// This property gets or sets the value for 'ID'.
            /// </summary>
            public int ID
            {
                get { return iD; }
                set { iD = value; }
            }
            #endregion
            
            #region IsValid
            /// <summary>
            /// This read only property returns the value for 'IsValid'.
            /// </summary>
            public bool IsValid
            {
                get
                {
                    // return the value for HasCommentAndPattern
                    return this.HasCommentAndPattern;
                }
            }
            #endregion
            
            #region Name
            /// <summary>
            /// This property gets or sets the value for 'Name'.
            /// </summary>
            public string Name
            {
                get { return name; }
                set { name = value; }
            }
            #endregion
            
            #region Pattern
            /// <summary>
            /// This property gets or sets the value for 'Pattern'.
            /// </summary>
            public string Pattern
            {
                get { return pattern; }
                set { pattern = value; }
            }
            #endregion
            
            #region Replacements
            /// <summary>
            /// This property gets or sets the value for 'Replacements'.
            /// </summary>
            public List<Replacement> Replacements
            {
                get { return replacements; }
                set { replacements = value; }
            }
            #endregion
            
            #region TargetPattern
            /// <summary>
            /// This property gets or sets the value for 'TargetPattern'.
            /// </summary>
            public string TargetPattern
            {
                get { return targetPattern; }
                set { targetPattern = value; }
            }
            #endregion
            
            #region TargetPattern2
            /// <summary>
            /// This property gets or sets the value for 'TargetPattern2'.
            /// </summary>
            public string TargetPattern2
            {
                get { return targetPattern2; }
                set { targetPattern2 = value; }
            }
            #endregion
            
        #endregion
        
    }
    #endregion

}
