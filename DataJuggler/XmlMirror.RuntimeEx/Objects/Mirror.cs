

#region using statements

using System;
using System.Linq;
using System.Text;
using XmlMirror.Runtime.Enumerations;
using System.Collections.Generic;

#endregion

namespace XmlMirror.Runtime.Objects
{

    #region class Mirror
    /// <summary>
    /// This class is used to save an Xml Mirror mapping file.
    /// </summary>
    [Serializable]
    public class Mirror
    {
        
        #region Private Variables
        private string sourceXmlFilePath;
        private string targetObjectFilePath;
        private string targetClassName;
        private string className;
        private string outputFolderPath;
        private List<FieldLink> fieldLinks;
        private List<FieldValuePair> fields;
        private string parserNamespace;
        private string newObjectNodeName;
        private string collectionNodeName;
        private string name;
        private MirrorTypeEnum mirrorType;
        private OutputTypeEnum outputType;
        #endregion
        
        #region Constructor
        /// <summary>
        /// Create a new instance of a 'Mirror' object.
        /// </summary>
        public Mirror()
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
                // create the FieldLinks collection
                this.FieldLinks = new List<FieldLink>();
                this.Fields = new List<FieldValuePair>();
            }
            #endregion

            #region ToString()
            /// <summary>
            /// This method is used to show information about this object
            /// </summary>
            /// <returns></returns>
            public override string ToString()
            {
                // Show some information about this object 
                return this.Name + Environment.NewLine + "FieldLinks: " + FieldLinks.Count + Environment.NewLine + "Fields: " + Fields.Count;
            }
            #endregion

        #endregion

        #region Properties

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
            
            #region CollectionNodeName
            /// <summary>
            /// This property gets or sets the value for 'CollectionNodeName'.
            /// </summary>
            public string CollectionNodeName
            {
                get { return collectionNodeName; }
                set { collectionNodeName = value; }
            }
            #endregion
            
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
            
            #region Fields
            /// <summary>
            /// This property gets or sets the value for 'Fields'.
            /// </summary>
            public List<FieldValuePair> Fields
            {
                get { return fields; }
                set { fields = value; }
            }
            #endregion
            
            #region HasClassName
            /// <summary>
            /// This property returns true if the 'ClassName' exists.
            /// </summary>
            public bool HasClassName
            {
                get
                {
                    // initial value
                    bool hasClassName = (!String.IsNullOrEmpty(this.ClassName));
                    
                    // return value
                    return hasClassName;
                }
            }
            #endregion
            
            #region HasCollectionNodeName
            /// <summary>
            /// This property returns true if the 'CollectionNodeName' exists.
            /// </summary>
            public bool HasCollectionNodeName
            {
                get
                {
                    // initial value
                    bool hasCollectionNodeName = (!String.IsNullOrEmpty(this.CollectionNodeName));
                    
                    // return value
                    return hasCollectionNodeName;
                }
            }
            #endregion
            
            #region HasDateFieldLink
            /// <summary>
            /// This read only property returns true if this object contains a FieldLink with a TargetDataType of 'DateTime'.
            /// If this mirror has a DateTime field than the default DateTime and error DateTime must be written.
            /// </summary>
            public bool HasDateFieldLink
            {
                get
                {
                    // initial value
                    bool hasDateTimeFieldLink = false;

                    // if this object contains one or more FieldLinks
                    if (this.HasOneOrMoreFieldLinks)
                    {
                        // iterate the FieldLinks in this collection
                        foreach (FieldLink fieldLink in this.FieldLinks)
                        {
                            // if this is a date
                            if (fieldLink.TargetDataType == DataTypeEnum.DateTime)
                            {
                                // set the return value to true
                                hasDateTimeFieldLink = true;

                                // break out of the loop
                                break;
                            }
                        }
                    }

                    // return value
                    return hasDateTimeFieldLink;
                }
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
            
            #region HasName
            /// <summary>
            /// This property returns true if the 'Name' exists.
            /// </summary>
            public bool HasName
            {
                get
                {
                    // initial value
                    bool hasName = (!String.IsNullOrEmpty(this.Name));
                    
                    // return value
                    return hasName;
                }
            }
            #endregion
            
            #region HasNewObjectNodeName
            /// <summary>
            /// This property returns true if the 'NewObjectNodeName' exists.
            /// </summary>
            public bool HasNewObjectNodeName
            {
                get
                {
                    // initial value
                    bool hasNewObjectNodeName = (!String.IsNullOrEmpty(this.NewObjectNodeName));
                    
                    // return value
                    return hasNewObjectNodeName;
                }
            }
            #endregion            
            
            #region HasOutputFolderPath
            /// <summary>
            /// This property returns true if the 'OutputFolderPath' exists.
            /// </summary>
            public bool HasOutputFolderPath
            {
                get
                {
                    // initial value
                    bool hasOutputFolderPath = (!String.IsNullOrEmpty(this.OutputFolderPath));
                    
                    // return value
                    return hasOutputFolderPath;
                }
            }
            #endregion

            #region HasOneOrMoreFieldLinks
            /// <summary>
            /// This read only property returns true if this object has one or more FieldLinks.
            /// </summary>
            public bool HasOneOrMoreFieldLinks
            {
                get
                {
                    // initial value
                    bool hasOneOrMoreFieldLinks = ((this.HasFieldLinks) && (this.FieldLinks.Count > 0));

                    // return value
                    return hasOneOrMoreFieldLinks;
                }
            }
            #endregion
         
            #region HasParserNamespace
            /// <summary>
            /// This property returns true if the 'Namespace' exists.
            /// </summary>
            public bool HasParserNamespace
            {
                get
                {
                    // initial value
                    bool hasParserNamespace = (!String.IsNullOrEmpty(this.Namespace));
                    
                    // return value
                    return hasParserNamespace;
                }
            }
            #endregion
            
            #region HasSourceXmlFilePath
            /// <summary>
            /// This property returns true if the 'SourceXmlFilePath' exists.
            /// </summary>
            public bool HasSourceXmlFilePath
            {
                get
                {
                    // initial value
                    bool hasSourceXmlFilePath = (!String.IsNullOrEmpty(this.SourceXmlFilePath));
                    
                    // return value
                    return hasSourceXmlFilePath;
                }
            }
            #endregion
            
            #region HasTargetObjectFilePath
            /// <summary>
            /// This property returns true if the 'TargetObjectFilePath' exists.
            /// </summary>
            public bool HasTargetObjectFilePath
            {
                get
                {
                    // initial value
                    bool hasTargetObjectFilePath = (!String.IsNullOrEmpty(this.TargetObjectFilePath));
                    
                    // return value
                    return hasTargetObjectFilePath;
                }
            }
            #endregion
            
            #region MirrorType
            /// <summary>
            /// This property gets or sets the value for 'MirrorType'.
            /// </summary>
            public MirrorTypeEnum MirrorType
            {
                get { return mirrorType; }
                set { mirrorType = value; }
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
            
            #region NewObjectNodeName
            /// <summary>
            /// This property gets or sets the value for 'NewObjectNodeName'.
            /// </summary>
            public string NewObjectNodeName
            {
                get { return newObjectNodeName; }
                set { newObjectNodeName = value; }
            }
            #endregion
            
            #region OutputFolderPath
            /// <summary>
            /// This property gets or sets the value for 'OutputFolderPath'.
            /// </summary>
            public string OutputFolderPath
            {
                get { return outputFolderPath; }
                set { outputFolderPath = value; }
            }
            #endregion
            
            #region OutputType
            /// <summary>
            /// This property gets or sets the value for 'OutputType'.
            /// </summary>
            public OutputTypeEnum OutputType
            {
                get { return outputType; }
                set { outputType = value; }
            }
            #endregion
            
            #region ParserNamespace
            /// <summary>
            /// This property gets or sets the value for 'Namespace'.
            /// </summary>
            public string Namespace
            {
                get { return parserNamespace; }
                set { parserNamespace = value; }
            }
            #endregion
            
            #region SourceXmlFilePath
            /// <summary>
            /// This property gets or sets the value for 'SourceXmlFilePath'.
            /// </summary>
            public string SourceXmlFilePath
            {
                get { return sourceXmlFilePath; }
                set { sourceXmlFilePath = value; }
            }
            #endregion
            
            #region TargetClassName
            /// <summary>
            /// This property gets or sets the value for 'TargetClassName'.
            /// </summary>
            public string TargetClassName
            {
                get { return targetClassName; }
                set { targetClassName = value; }
            }
            #endregion
            
            #region TargetObjectFilePath
            /// <summary>
            /// This property gets or sets the value for 'TargetObjectFilePath'.
            /// </summary>
            public string TargetObjectFilePath
            {
                get { return targetObjectFilePath; }
                set 
                { 
                    // set the value
                    targetObjectFilePath = value;

                    // if the string exists
                    if (!String.IsNullOrEmpty(targetObjectFilePath))
                    {
                        // if the targetObjectFilePath exists
                        if (targetObjectFilePath.Length > 6)
                        {
                            // create a temp string of the first five characters
                            string temp = targetObjectFilePath.Substring(0, 5);
                            
                            // get the index of temp in temp2
                            int index = targetObjectFilePath.IndexOf(temp);
                            int lastIndex = targetObjectFilePath.LastIndexOf(temp);

                            // if the index was found
                            if ((index >= 0) && (lastIndex > index))
                            {
                                // reduce to a single copy
                                targetObjectFilePath = targetObjectFilePath.Substring(0, lastIndex);
                            }
                        }
                    }
                }
            }
            #endregion
        
        #endregion
        
    }
    #endregion

}
