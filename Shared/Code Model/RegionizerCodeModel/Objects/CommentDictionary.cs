

#region using statements

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#endregion

namespace DataJuggler.Regionizer.CodeModel.Objects
{

    #region class CommentDictionary
    /// <summary>
    /// This class is used to hold a collection of CommentItems
    /// </summary>
    [Serializable]
    public class CommentDictionary
    {
        
        #region Private Variables
        private List<CodeComment> comments;
        private bool commentsLoaded;
        private List<CodeComment> customComments;
        #endregion
        
        #region Constructor
        /// <summary>
        /// Create a new instance of a CommentDictionairy
        /// </summary>
        public CommentDictionary()
        {
            // Perform initializations for this object
            Init();
        }
        #endregion

        #region Methods

            #region Init()
            /// <summary>
            /// This method performs initializations for this object.
            /// </summary>
            public void Init()
            {
                // create the Comments collection
                this.Comments = new List<CodeComment>();
            }
            #endregion
            
        #endregion

        #region Properties

            #region Comments
            /// <summary>
            /// This property gets or sets the value for 'Comments'.
            /// </summary>
            public List<CodeComment> Comments
            {
                get { return comments; }
                set { comments = value; }
            }
            #endregion
            
            #region CommentsLoaded
            /// <summary>
            /// This property gets or sets the value for 'CommentsLoaded'.
            /// </summary>
            public bool CommentsLoaded
            {
                get { return commentsLoaded; }
                set { commentsLoaded = value; }
            }
            #endregion
            
            #region CustomComments
            /// <summary>
            /// This property gets or sets the value for 'CustomComments'.
            /// </summary>
            public List<CodeComment> CustomComments
            {
                get { return customComments; }
                set { customComments = value; }
            }
            #endregion
            
            #region HasComments
            /// <summary>
            /// This property returns true if this object has a 'Comments'.
            /// </summary>
            public bool HasComments
            {
                get
                {
                    // initial value
                    bool hasComments = (this.Comments != null);
                    
                    // return value
                    return hasComments;
                }
            }
            #endregion
            
            #region HasCustomComments
            /// <summary>
            /// This property returns true if this object has a 'CustomComments'.
            /// </summary>
            public bool HasCustomComments
            {
                get
                {
                    // initial value
                    bool hasCustomComments = (this.CustomComments != null);
                    
                    // return value
                    return hasCustomComments;
                }
            }
            #endregion
            
        #endregion
        
    }
    #endregion

}
