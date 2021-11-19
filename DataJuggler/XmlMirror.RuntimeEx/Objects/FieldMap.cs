

#region using statements

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#endregion

namespace XmlMirror.Runtime.Objects
{

    #region class FieldMap
    /// <summary>
    /// This class is needed because more than one FieldLink may be mapped to 
    /// a source field. This class maps a SourceFieldName to a collection of 
    /// FieldLink objects
    /// </summary>
    public class FieldMap
    {
        
        #region Private Variables
        private string sourceFieldName;
        private List<FieldLink> fieldLinks;
        #endregion

        #region Constructor
        /// <summary>
        /// Create a new instance of a FieldMap object and set the SourceFieldName.
        /// </summary>
        /// <param name="sourceFieldName"></param>
        public FieldMap(string sourceFieldName)
        {
            // store the parameter
            this.SourceFieldName = sourceFieldName;

            // perform initializations for this object
            Init();
        }
        #endregion

        #region Methods

            #region AddFieldLink(FieldLink fieldLink)
            /// <summary>
            /// This method Adds FieldLink to this object's FieldLink collection.
            /// </summary>
            public void AddFieldLink(FieldLink fieldLink)
            {
                // if the FieldLinks
                if (this.HasFieldLinks)
                {
                    // add this fieldLink
                    this.FieldLinks.Add(fieldLink);
                }
            }
            #endregion
            
            #region Init()
            /// <summary>
            /// This method performs initializations for this object.
            /// </summary>
            public void Init()
            {
                // create the FieldLink collection.
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
            
            #region HasSourceFieldName
            /// <summary>
            /// This property returns true if the 'SourceFieldName' exists.
            /// </summary>
            public bool HasSourceFieldName
            {
                get
                {
                    // initial value
                    bool hasSourceFieldName = (!String.IsNullOrEmpty(this.SourceFieldName));
                    
                    // return value
                    return hasSourceFieldName;
                }
            }
            #endregion
            
            #region SourceFieldName
            /// <summary>
            /// This property gets or sets the value for 'SourceFieldName'.
            /// </summary>
            public string SourceFieldName
            {
                get { return sourceFieldName; }
                set { sourceFieldName = value; }
            }
            #endregion
            
        #endregion
        
    }
    #endregion

}
