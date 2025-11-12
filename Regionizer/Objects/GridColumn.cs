

#region using statements

using DataJuggler.Regionizer.CodeModel.Enumerations;
using XmlMirror.Runtime.Objects;
using System.Collections.Generic;

#endregion

namespace DataJuggler.Regionizer.Objects
{

    #region class GridColumn
    /// <summary>
    /// This class is used by the Grid to define columns
    /// </summary>
    public class GridColumn
    {
        
        #region Private Variables
        private string caption;
        private string className;
        private int columnNumber;
        private DataTypeEnum dataType;
        private string fieldName;
        private string fieldValue;
        private int height;
        private int index;
        private string name;        
        private bool readOnly;
        private int width;
        private bool lastColumn;
        private bool visible;
        private List<FieldValuePair> attributes;
        #endregion
        
        #region Events
            
        #endregion

        #region Methods

            #region ParseAttributes()
            /// <summary>
            /// method Parse Attributes
            /// </summary>
            public void ParseAttributes()
            {
                // if the value for HasAttributes is true
                if (HasAttributes)
                {
                    // Iterate the collection of FieldValuePair objects
                    foreach (FieldValuePair attribute in Attributes)
                    {
                        switch (attribute.FieldName)
                        {
                            case "Caption":

                                // Set the Caption
                                Caption = (string) attribute.FieldValue;

                                // required
                                break;

                            case "ClassName":

                                // Set ClassName
                                ClassName = (string) attribute.FieldValue;

                                // required
                                break;

                            case "ColumnNumber":

                                // Set ColumnNumber
                                ColumnNumber = (int) attribute.FieldValue;

                                // required
                                break;

                            case "DataType":

                                // Set DataType
                                DataType = (DataTypeEnum) attribute.FieldValue;

                                // required
                                break;

                            case "FieldName":

                                // Set FieldName
                                FieldName = (string) attribute.FieldValue;

                                // required
                                break;

                            case "FieldValue":

                                // Set FieldValue
                                FieldValue = (string) attribute.FieldValue;

                                // required
                                break;

                            case "Height":

                                // Set Height
                                Height = (int) attribute.FieldValue;

                                // required
                                break;

                            case "Index":

                                // Set Index
                                Index = (int) attribute.FieldValue;

                                // required
                                break;

                            case "LastColumn":

                                // Set LastColumn
                                LastColumn = (bool) attribute.FieldValue;

                                // required
                                break;

                            case "Name":

                                // Set the Name
                                Name = (string) attribute.FieldValue;

                                // required
                                break;

                            case "ReadOnly":

                                // Set ReadOnly
                                ReadOnly = (bool) attribute.FieldValue;

                                // required
                                break;

                            case "Visible":

                                // Set Visible
                                Visible = (bool) attribute.FieldValue;

                                // required
                                break;

                            case "Width":

                                // Set Width
                                Width = (int) attribute.FieldValue;

                                // required
                                break;
                        }
                    }
                }
            }
            #endregion
            
        #endregion
        
        #region Properties
            
            #region Attributes
            /// <summary>
            /// This property gets or sets the value for 'Attributes'.
            /// </summary>
            public List<FieldValuePair> Attributes
            {
                get { return attributes; }
                set { attributes = value; }
            }
            #endregion
            
            #region Caption
            /// <summary>
            /// This property gets or sets the value for 'Caption'.
            /// </summary>            
            public string Caption
            {
                get { return caption; }
                set { caption = value; }
            }
            #endregion
            
            #region ClassName
            /// <summary>
            /// This property gets or sets the value for 'ClassName'.
            /// </summary>            
            public string ClassName
            {
                get { return className; }
                set { className = value; }
            }
            #endregion
            
            #region ColumnNumber
            /// <summary>
            /// This property gets or sets the value for 'ColumnNumber'.
            /// </summary>
            public int ColumnNumber
            {
                get { return columnNumber; }
                set { columnNumber = value; }
            }
            #endregion
            
            #region DataType
            /// <summary>
            /// This property gets or sets the value for 'DataType'.
            /// </summary>            
            public DataTypeEnum DataType
            {
                get { return dataType; }
                set { dataType = value; }
            }
            #endregion
            
            #region FieldName
            /// <summary>
            /// This property gets or sets the value for 'FieldName'.
            /// </summary>
            public string FieldName
            {
                get { return fieldName; }
                set { fieldName = value; }
            }
            #endregion
            
            #region FieldValue
            /// <summary>
            /// This property gets or sets the value for 'FieldValue'.
            /// </summary>
            public string FieldValue
            {
                get { return fieldValue; }
                set { fieldValue = value; }
            }
            #endregion
            
            #region HasAttributes
            /// <summary>
            /// This property returns true if this object has an 'Attributes'.
            /// </summary>
            public bool HasAttributes
            {
                get
                {
                    // initial value
                    bool hasAttributes = (Attributes != null);

                    // return value
                    return hasAttributes;
                }
            }
            #endregion
            
            #region Height
            /// <summary>
            /// This property gets or sets the value for 'Height'.
            /// </summary>
            public int Height
            {
                get { return height; }
                set { height = value; }
            }
            #endregion
            
            #region Index
            /// <summary>
            /// This property gets or sets the value for 'Index'.
            /// </summary>
            public int Index
            {
                get { return index; }
                set { index = value; }
            }
            #endregion
            
            #region LastColumn
            /// <summary>
            /// This property gets or sets the value for 'LastColumn'.
            /// </summary>
            public bool LastColumn
            {
                get { return lastColumn; }
                set { lastColumn = value; }
            }
            #endregion
            
            #region Name
            /// <summary>
            /// This property gets or sets the value for Name
            /// </summary>
            public string Name
            {
                get
                {
                    return name;
                }
                set
                {
                    name = value;
                }
            }
            #endregion
            
            #region ReadOnly
            /// <summary>
            /// This property gets or sets the value for 'ReadOnly'.
            /// </summary>
            public bool ReadOnly
            {
                get { return readOnly; }
                set { readOnly = value; }
            }
            #endregion
            
            #region Visible
            /// <summary>
            /// This property gets or sets the value for 'Visible'.
            /// </summary>
            public bool Visible
            {
                get { return visible; }
                set { visible = value; }
            }
            #endregion
            
            #region Width
            /// <summary>
            /// This property gets or sets the value for 'Width'.
            /// </summary>
            public int Width
            {
                get { return width; }
                set { width = value; }
            }
            #endregion
            
        #endregion
        
    }
    #endregion

}
