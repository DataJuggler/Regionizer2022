

#region using statements

using System;

#endregion

namespace DataJuggler.Regionizer.CodeModel.Objects
{

    #region class DictionaryInfo
    /// <summary>
    /// This class is used to keep all the dictionary settings from the Registry
    /// </summary>
    [Serializable]
    public class DictionaryInfo
    {
        
        #region Private Variables
        private string dictionaryPath;
        private double installedVersion;
        private bool useCustomDictionary;
        private string customDictionaryPath;
        private bool tryCustomDictionaryFirst;
        #endregion

        #region Properties

            #region CustomDictionaryPath
            /// <summary>
            /// This property gets or sets the value for 'CustomDictionaryPath'.
            /// </summary>
            public string CustomDictionaryPath
            {
                get { return customDictionaryPath; }
                set { customDictionaryPath = value; }
            }
            #endregion
            
            #region DictionaryPath
            /// <summary>
            /// This property gets or sets the value for 'DictionaryPath'.
            /// </summary>
            public string DictionaryPath
            {
                get { return dictionaryPath; }
                set { dictionaryPath = value; }
            }
            #endregion
            
            #region HasCustomDictionaryPath
            /// <summary>
            /// This property returns true if the 'CustomDictionaryPath' exists.
            /// </summary>
            public bool HasCustomDictionaryPath
            {
                get
                {
                    // initial value
                    bool hasCustomDictionaryPath = (!String.IsNullOrEmpty(this.CustomDictionaryPath));
                    
                    // return value
                    return hasCustomDictionaryPath;
                }
            }
            #endregion
            
            #region HasDictionaryPath
            /// <summary>
            /// This property returns true if the 'DictionaryPath' exists.
            /// </summary>
            public bool HasDictionaryPath
            {
                get
                {
                    // initial value
                    bool hasDictionaryPath = (!String.IsNullOrEmpty(this.DictionaryPath));
                    
                    // return value
                    return hasDictionaryPath;
                }
            }
            #endregion
            
            #region InstalledVersion
            /// <summary>
            /// This property gets or sets the value for 'InstalledVersion'.
            /// </summary>
            public double InstalledVersion
            {
                get { return installedVersion; }
                set { installedVersion = value; }
            }
            #endregion
            
            #region TryCustomDictionaryFirst
            /// <summary>
            /// This property gets or sets the value for 'TryCustomDictionaryFirst'.
            /// </summary>
            public bool TryCustomDictionaryFirst
            {
                get { return tryCustomDictionaryFirst; }
                set { tryCustomDictionaryFirst = value; }
            }
            #endregion
            
            #region UseCustomDictionary
            /// <summary>
            /// This property gets or sets the value for 'UseCustomDictionary'.
            /// </summary>
            public bool UseCustomDictionary
            {
                get { return useCustomDictionary; }
                set { useCustomDictionary = value; }
            }
            #endregion
            
        #endregion
        
    }
    #endregion

}
