

#region using statements

using System;
using System.Collections.Generic;
using System.IO;
using CM = DataJuggler.Regionizer.CodeModel.Objects;
using EnvDTE;

#endregion

namespace DataJuggler.Regionizer.CodeModel.Util
{

    #region FileCodeModelHelper
    /// <summary>
    /// This class is designed to aide in getting items from the EnvDTE.FileCodeModel object
    /// </summary>
    public class FileCodeModelHelper
    {
        
        #region Static Methods

            #region GetClasses(CodeElement namespaceCodeElement)
            /// <summary>
            /// This method gets the class for the parent codeElement
            /// </summary>
            /// <param name="namespaceCodeElement"></param>
            /// <returns></returns>
            internal static List<CodeElement> GetClasses(CodeElement namespaceCodeElement)
            {
                // initial value
                List<CodeElement> classes = new List<CodeElement>();

                try
                {
                    // verify the objects exist
                    if ((namespaceCodeElement != null) && (namespaceCodeElement.Children != null))
                    {
                        // iterate the code elements
                        foreach (CodeElement codeElement in namespaceCodeElement.Children)
                        {
                            // if this is a namespace
                            if (codeElement.Kind == vsCMElement.vsCMElementClass)
                            {
                                // create a class
                                CodeElement classElement = codeElement;

                                // add this item
                                classes.Add(classElement);

                                // break out of the loop
                                break;
                            }
                        }
                    }
                }
                catch (Exception error)
                {
                    // for debugging only
                    string err = error.ToString();
                }

                // return value
                return classes;
            } 
            #endregion

            #region GetMethods(CodeElement codeClass, bool getConstructors)
            /// <summary>
            /// This method gets the Method 
            /// </summary>
            /// <param name="className"></param>
            /// <returns></returns>
            internal static List<CodeElement> GetMethods(CodeElement codeClass, bool getConstructors)
            {
                // initial value
                List<CodeElement> methods = new List<CodeElement>();

                // if the codeClass exists
                if  ((codeClass != null) && (codeClass.Children != null))
                {
                    // iterate the code elements in the class
                    foreach(CodeElement codeElement in codeClass.Children)
                    {
                        // if this is a Function
                        if (codeElement.Kind == vsCMElement.vsCMElementFunction)
                        {
                            // if this is a Constructor and we are getting constructors
                            if  ((getConstructors) && (codeElement.Name == codeClass.Name))
                            {   
                                // add this item
                                methods.Add(codeElement);
                            }
                            else if ((!getConstructors) && (codeElement.Name != codeClass.Name))
                            {
                                // add this item
                                methods.Add(codeElement);
                            }
                        }
                    }
                }

                // return value
                return methods;
            }
            #endregion

            #region GetNamespace(FileCodeModel fileCodeModel)
            /// <summary>
            /// This method finds the first Namespace CodeElement in the fileCodeModel
            /// </summary>
            /// <param name="fileCodeModel"></param>
            /// <returns></returns>
            public static CodeElement GetNamespace(FileCodeModel fileCodeModel)
            {
                // initial value
                CodeElement codeNamespace = null;

                try
                {
                    // verify the objects exist
                    if ((fileCodeModel != null) && (fileCodeModel.CodeElements != null))
                    {
                        // iterate the code elements
                        foreach (CodeElement codeElement in fileCodeModel.CodeElements)
                        {
                            // if this is a namespace
                            if (codeElement.Kind == vsCMElement.vsCMElementNamespace)
                            {
                                // set the return value
                                codeNamespace = codeElement;

                                // break out of the loop
                                break;
                            }
                        }
                    }
                }
                catch (Exception error)
                {
                    // for debugging only
                    string err = error.ToString();
                }

                // return value
                return codeNamespace;
            } 
            #endregion

            #region GetProperties(CodeElement codeClass)
            /// <summary>
            /// This method gest the Properties for the 
            /// </summary>
            /// <param name="codeElement"></param>
            /// <returns></returns>
            internal static List<CodeElement> GetProperties(CodeElement codeClass)
            {
                // initial value
                List<CodeElement> properties = new List<CodeElement>();

                // if the codeClass exists
                if ((codeClass != null) && (codeClass.Children != null))
                {
                    // iterate the code elements in the class
                    foreach (CodeElement codeElement in codeClass.Children)
                    {
                        // if this is a Function
                        if (codeElement.Kind == vsCMElement.vsCMElementProperty)
                        {
                            // add this property
                            properties.Add(codeElement);
                        }
                    }
                }

                // return value
                return properties;
            } 
            #endregion

        #endregion

    } 
    #endregion

}
