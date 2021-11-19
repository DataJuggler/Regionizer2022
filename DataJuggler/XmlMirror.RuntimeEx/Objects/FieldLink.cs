

#region using statements

using System;
using XmlMirror.Runtime.Enumerations;

#endregion

namespace XmlMirror.Runtime.Objects
{

    #region class FieldLink
    /// <summary>
    /// This class is used to map the link between the source xml field and the target property in the selected class.
    /// </summary>
    [Serializable]
    public class FieldLink
    {
        
        #region Private Variables
        private Guid id;
        private MapTypeEnum mapType;
        private InstanceTypeEnum relationshipType;
        private string sourceFieldName;
        private string targetFieldName;
        private DataTypeEnum targetDataType;
        private FieldValuePair fieldValuePair;
        #endregion
        
        #region Constructors

            #region Default Constructor
            /// <summary>
            /// Create a new instance of a FieldLink
            /// </summary>
            public FieldLink()
            {
                // Perform initializations for this object
                Init();
            }
            #endregion

            #region ParameterizedConstructor(string sourceFieldName, string targetFieldName, DataManager.DataTypeEnum targetDataType)
            /// <summary>
            /// Create a new instance of a 'FieldLink' object.
            /// </summary>
            /// <param name="sourceFieldName"></param>
            /// <param name="targetFieldName"></param>
            public FieldLink(string sourceFieldName, string targetFieldName, DataTypeEnum targetDataType)
            {
                // Store the values
                this.SourceFieldName = sourceFieldName;
                this.TargetFieldName = targetFieldName;
                this.TargetDataType = targetDataType;

                // set the default values
                this.MapType = MapTypeEnum.MapToElement;
                this.RelationshipType = InstanceTypeEnum.Singleton;
                
                // Perform initializations for this object
                Init();
            }
            #endregion

            #region Parameterized Constructor(string sourceFieldName, string targetFieldName, MapTypeEnum mapType, InstanceTypeEnum relationshipType)
            /// <summary>
            /// Create a new instance of a FieldLink object.
            /// </summary>
            /// <param name="sourceFieldName"></param>
            /// <param name="targetFieldName"></param>
            /// <param name="mapType"></param>
            /// <param name="relationshipType"></param>
            public FieldLink(string sourceFieldName, string targetFieldName, MapTypeEnum mapType, InstanceTypeEnum relationshipType)
            {
                // Store the values
                this.SourceFieldName = sourceFieldName;
                this.TargetFieldName = targetFieldName;
                this.MapType = mapType;
                this.RelationshipType = relationshipType;
                
                // Perform initializations for this object
                Init();
            }
            #endregion
            
        #endregion

        #region Methods

            #region Init()
            /// <summary>
            /// This method  This method performs initializations for this object.
            /// </summary>
            public void Init()
            {
                // Create a new Guid                
                ID = Guid.NewGuid();
            }
            #endregion
            
        #endregion
        
        #region Properties
            
            #region FieldValuePair
            /// <summary>
            /// This property gets or sets the value for 'FieldValuePair'.
            /// </summary>
            public FieldValuePair FieldValuePair
            {
                get { return fieldValuePair; }
                set { fieldValuePair = value; }
            }
            #endregion
            
            #region HasFieldValuePair
            /// <summary>
            /// This property returns true if this object has a 'FieldValuePair'.
            /// </summary>
            public bool HasFieldValuePair
            {
                get
                {
                    // initial value
                    bool hasFieldValuePair = (this.FieldValuePair != null);
                    
                    // return value
                    return hasFieldValuePair;
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
            
            #region HasTargetFieldName
            /// <summary>
            /// This property returns true if the 'TargetFieldName' exists.
            /// </summary>
            public bool HasTargetFieldName
            {
                get
                {
                    // initial value
                    bool hasTargetFieldName = (!String.IsNullOrEmpty(this.TargetFieldName));
                    
                    // return value
                    return hasTargetFieldName;
                }
            }
            #endregion
            
            #region ID
            /// <summary>
            /// This property gets or sets the value for 'ID'.
            /// </summary>
            public Guid ID
            {
                get { return id; }
                set { id = value; }
            }
            #endregion
            
            #region MapType
            /// <summary>
            /// This property gets or sets the value for 'MapType'.
            /// </summary>
            public MapTypeEnum MapType
            {
                get { return mapType; }
                set { mapType = value; }
            }
            #endregion

            #region Name
            /// <summary>
            /// This read only property returns the FieldName without
            /// the fullyqualified name.
            /// Example: Full Name: Mirror.ClassName
            ///                      Name: ClassName
            /// </summary>
            public string Name
            {
                get
                {
                    // initial value
                    string name = "";

                    // If the SourceFieldName object exists
                    if (HasSourceFieldName)
                    {
                        // get the index of the last period
                        int index = SourceFieldName.LastIndexOf(".");

                        // if the index was found
                        if (index >= 0)
                        {
                            // set the return value
                            name = SourceFieldName.Substring(index +1);
                        }
                        else
                        {
                            // return as is
                            name = SourceFieldName;
                        }
                    }

                    // return value
                    return name;
                }
            }
            #endregion
            
            #region RelationshipType
            /// <summary>
            /// This property gets or sets the value for 'RelationshipType'.
            /// </summary>
            public InstanceTypeEnum RelationshipType
            {
                get { return relationshipType; }
                set { relationshipType = value; }
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
            
            #region TargetDataType
            /// <summary>
            /// This property gets or sets the value for 'TargetDataType'.
            /// </summary>
            public DataTypeEnum TargetDataType
            {
                get { return targetDataType; }
                set { targetDataType = value; }
            }
            #endregion
            
            #region TargetFieldName
            /// <summary>
            /// This property gets or sets the value for 'TargetFieldName'.
            /// </summary>
            public string TargetFieldName
            {
                get { return targetFieldName; }
                set {  targetFieldName = value; }
            }
            #endregion
            
        #endregion
        
    }
    #endregion

}
