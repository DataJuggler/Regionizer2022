

#region using statements

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#endregion

namespace XmlMirror.Runtime.Objects
{

    #region class ParserMap
    /// <summary>
    /// This class is used to map an xml source file to the target object to allow a parser to be built.
    /// </summary>
    public class ParserMap
    {
        
        #region Private Variables
        private List<FieldLink> fieldLinks;
        #endregion

        #region Constructors
        /// <summary>
        /// Create a new instance of a ParserMap object.
        /// </summary>
        public ParserMap()
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
                // load the fieldLinks
                this.FieldLinks = new List<FieldLink>();
            }
            #endregion
            
        #endregion

        #region Properties

            #region FieldLinks
            /// <summary>
            /// This property gets or sets the value for 'FieldLinks'.
            /// </summary>
            public List<FieldLink> FieldLinks
            {
                get { return fieldLinks; }
                set { fieldLinks = value; }
            }
            #endregion
            
            #region HasFieldLinks
            /// <summary>
            /// This property returns true if this object has a 'FieldLinks'.
            /// </summary>
            public bool HasFieldLinks
            {
                get
                {
                    // initial value
                    bool hasFieldLinks = (this.FieldLinks != null);
                    
                    // return value
                    return hasFieldLinks;
                }
            }
            #endregion
            
        #endregion
        
    }
    #endregion

}
