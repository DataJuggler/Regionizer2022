

#region using statements

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#endregion

namespace DataJuggler.Regionizer.CodeModel.Objects
{

    #region class RowBuilderInfo
    /// <summary>
    /// This class is used to capture the info from the BlazorComponentsForm
    /// so it can be used to create a RowBuilder for a specific object type.
    /// </summary>
    public class RowBuilderInfo
    {
        
        #region Private Variables
        private string className;
        private string namespaceName;
        private string gridRazorPath;
        private string listName;
        private string objectName;
        private string outputFolder;
        private string outputFileName;        
        private string variableName;
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
            
            #region GridRazorPath
            /// <summary>
            /// This property gets or sets the value for 'GridRazorPath'.
            /// </summary>
            public string GridRazorPath
            {
                get { return gridRazorPath; }
                set { gridRazorPath = value; }
            }
            #endregion
            
            #region ListName
            /// <summary>
            /// This property gets or sets the value for 'ListName'.
            /// </summary>
            public string ListName
            {
                get { return listName; }
                set { listName = value; }
            }
            #endregion
            
            #region NamespaceName
            /// <summary>
            /// This property gets or sets the value for 'NamespaceName'.
            /// </summary>
            public string NamespaceName
            {
                get { return namespaceName; }
                set { namespaceName = value; }
            }
            #endregion
            
            #region ObjectName
            /// <summary>
            /// This property gets or sets the value for 'ObjectName'.
            /// </summary>
            public string ObjectName
            {
                get { return objectName; }
                set { objectName = value; }
            }
            #endregion
            
            #region OutputFileName
            /// <summary>
            /// This property gets or sets the value for 'OutputFileName'.
            /// </summary>
            public string OutputFileName
            {
                get { return outputFileName; }
                set { outputFileName = value; }
            }
            #endregion
            
            #region OutputFolder
            /// <summary>
            /// This property gets or sets the value for 'OutputFolder'.
            /// </summary>
            public string OutputFolder
            {
                get { return outputFolder; }
                set { outputFolder = value; }
            }
            #endregion
            
            #region VariableName
            /// <summary>
            /// This property gets or sets the value for 'VariableName'.
            /// </summary>
            public string VariableName
            {
                get { return variableName; }
                set { variableName = value; }
            }
            #endregion
            
        #endregion
        
    }
    #endregion

}
