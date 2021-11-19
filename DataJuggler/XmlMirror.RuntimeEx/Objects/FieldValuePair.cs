

#region using statements

using System;
using System.Collections.Generic;
using System.Text;
using XmlMirror.Runtime.Enumerations;

#endregion

namespace XmlMirror.Runtime.Objects
{

    #region class FieldValuePair
    /// <summary>
    /// This object represents one property
    /// in a BusinessObject.
    /// This is used to determine if an object
    /// has been modified since it was loaded.
    /// </summary>
    [Serializable]
    public class FieldValuePair
    {

        #region Private Variables
        private string fieldName;
        private object fieldValue;
        private DataTypeEnum fieldDataType;
        private string fieldDataTypeString;
        private bool isEnumeration;
        private string enumDataTypeName;
        private bool skip;
        #endregion

        #region Constructors

            #region Default Constructor
            /// <summary>
            /// Create a new instance of a FieldValuePair object.
            /// </summary>
            public FieldValuePair()
            {
            }
            #endregion

            #region Parameterized Constructor(string fieldNameArg, object fieldValueArg)
            /// <summary>
            /// Creates a 
            /// </summary>
            /// <param name="fieldNameArg"></param>
            /// <param name="fieldValueArg"></param>
            public FieldValuePair(string fieldNameArg, object fieldValueArg)
            {
                // set properties
                this.FieldName = fieldNameArg;
                this.fieldValue = fieldValueArg;
            }
            #endregion

            #region Parameterized Constructor(string fieldNameArg, object fieldValueArg, DataManager.DataTypeEnum fieldDataType)
            /// <summary>
            /// Creates a 
            /// </summary>
            /// <param name="fieldNameArg"></param>
            /// <param name="fieldValueArg"></param>
            public FieldValuePair(string fieldNameArg, object fieldValueArg, DataTypeEnum fieldDataType)
            {
                // set properties
                this.FieldName = fieldNameArg;
                this.fieldValue = fieldValueArg;
                this.FieldDataType = fieldDataType;
            }
            #endregion

        #endregion

        #region Methods

            #region ParseDataType(string fieldDataTypeString)
            /// <summary>
            /// This method returns the Data Type
            /// </summary>
            private DataTypeEnum ParseDataType(string fieldDataTypeString)
            {
                // initial value
                DataTypeEnum dataType = DataTypeEnum.NotSupported;

                // determine the DataTpe from the string given.
                switch(fieldDataTypeString)
                {
                    case "System.Guid":

                        // set to Integer
                        dataType = DataTypeEnum.Guid;

                        // required
                        break;

                    case "System.Double":

                        // set to Integer
                        dataType = DataTypeEnum.Double;

                        // required
                        break;

                    case "System.String":

                        // set to Integer
                        dataType = DataTypeEnum.String;

                        // required
                        break;

                    case "System.Int32":

                        // set to Integer
                        dataType = DataTypeEnum.Integer;

                        // required
                        break;

                    case "System.DateTime":

                        // set to date
                        dataType = DataTypeEnum.DateTime;

                        // required
                        break;

                    case "System.Boolean":

                        // set to Boolean
                        dataType = DataTypeEnum.Boolean;

                        // required
                        break;

                    default:
    
                        // if it is a GenericList
                        if (fieldDataTypeString.StartsWith("System.Collections.Generic.List"))
                        {
                            // set to GenericList
                            dataType = DataTypeEnum.GenericList;
                        }
                        else
                        {
                            // set to object
                            dataType = DataTypeEnum.Object;
                        }

                        // required
                        break;
                }

                // return value
                return dataType;
            }
            #endregion
            
        #endregion

        #region Properties

            #region EnumDataTypeName
            /// <summary>
            /// This property gets or sets the value for 'EnumDataTypeName'.
            /// </summary>
            public string EnumDataTypeName
            {
                get { return enumDataTypeName; }
                set 
                { 
                    // set the value
                    enumDataTypeName = value;

                    // if there is a plus sign
                    if (enumDataTypeName.Contains("+"))
                    {   
                        // Replace out the plus sign with a period
                        enumDataTypeName = enumDataTypeName.Replace("+", ".");
                    }
                }
            }
            #endregion
            
            #region FieldDataType
            /// <summary>
            /// This property gets or sets the value for 'FieldDataType'.
            /// </summary>
            public DataTypeEnum FieldDataType
            {
                get { return fieldDataType; }
                set { fieldDataType = value; }
            }
            #endregion
            
            #region FieldDataTypeString
            /// <summary>
            /// This property gets or sets the value for 'FieldDataTypeString'.
            /// </summary>
            public string FieldDataTypeString
            {
                get { return fieldDataTypeString; }
                set 
                { 
                    // set the value
                    fieldDataTypeString = value;

                    // now parse the string into the DataType
                    this.FieldDataType = ParseDataType(fieldDataTypeString);
                }
            }
            #endregion
            
            #region FieldName
            /// <summary>
            /// The name for this field.
            /// </summary>
            public string FieldName
            {
                get { return fieldName; }
                set { fieldName = value; }
            }
            #endregion

            #region FieldValue
            /// <summary>
            /// The value for this property
            /// </summary>
            public object FieldValue
            {
                get { return fieldValue; }
                set { fieldValue = value; }
            }
            #endregion

            #region HasEnumDataTypeName
            /// <summary>
            /// This property returns true if the 'EnumDataTypeName' exists.
            /// </summary>
            public bool HasEnumDataTypeName
            {
                get
                {
                    // initial value
                    bool hasEnumDataTypeName = (!String.IsNullOrEmpty(this.EnumDataTypeName));
                    
                    // return value
                    return hasEnumDataTypeName;
                }
            }
            #endregion
            
            #region IsEnumeration
            /// <summary>
            /// This property gets or sets the value for 'IsEnumeration'.
            /// </summary>
            public bool IsEnumeration
            {
                get { return isEnumeration; }
                set { isEnumeration = value; }
            }
            #endregion
            
            #region Skip
            /// <summary>
            /// This property gets or sets the value for 'Skip'.
            /// </summary>
            public bool Skip
            {
                get { return skip; }
                set { skip = value; }
            }
            #endregion
            
        #endregion

    }
    #endregion

}
