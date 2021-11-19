

#region using statements

using DataJuggler.Core.UltimateHelper;
using DataJuggler.Regionizer.CodeModel.Objects;
using Microsoft.Win32;
using System;

#endregion

namespace DataJuggler.Regionizer.Controls.Util
{

    #region RegistryHelper
    /// <summary>
    /// This class is used to put all the access to the registry in one place.
    /// </summary>
    public class RegistryHelper
    {

        #region Methods

            #region GetDictionaryInfo()
            /// <summary>
            /// This method returns the Dictionary Info
            /// </summary>
            public static DictionaryInfo GetDictionaryInfo()
            {
                // initial value
                DictionaryInfo dictionaryInfo = null;

                // open the key for Regionizer
                RegistryKey regionizerKey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Data Juggler\Regionizer");

                // if the regionizerKey exists
                if (regionizerKey != null)
                {
                    // Create the dictionaryInfo object
                    dictionaryInfo = new DictionaryInfo();

                    // get the path to the dictionairy
                    dictionaryInfo.DictionaryPath = (string) regionizerKey.GetValue("DictionaryPath");
                    dictionaryInfo.InstalledVersion = NumericHelper.ParseDouble((string) regionizerKey.GetValue("InstalledVersion"), 0, -1);

                    // if UseCustomDictionary is set in the registry
                    if (regionizerKey.GetValue("UseCustomDictionary") != null)
                    {
                        // Set the value
                        dictionaryInfo.UseCustomDictionary = Boolean.Parse(regionizerKey.GetValue("UseCustomDictionary").ToString());
                    }
                    else
                    {
                        // Set the value to false
                        dictionaryInfo.UseCustomDictionary = false;
                    }

                    // Set the value for CustomDictionaryPath
                    dictionaryInfo.CustomDictionaryPath = (string) regionizerKey.GetValue("CustomDictionaryPath");

                    // if TryCustomDictionaryFirst is set in the registry
                    if (regionizerKey.GetValue("TryCustomDictionaryFirst") != null)
                    {
                        // Set the value
                        dictionaryInfo.TryCustomDictionaryFirst = Boolean.Parse(regionizerKey.GetValue("TryCustomDictionaryFirst").ToString());
                    }
                    else
                    {
                        // Set the value to false
                        dictionaryInfo.TryCustomDictionaryFirst = false;
                    }
                }

                // return value
                return dictionaryInfo;
            }
            #endregion

            #region StoreDictionaryInfo(DictionaryInfo dictionaryInfo)
            /// <summary>
            /// This method updates the registry with the Dictionary Path and the version
            /// </summary>
            public static void StoreDictionaryInfo(DictionaryInfo dictionaryInfo)
            {
                try
                {
                    // if the dictionaryInfo object exists
                    if (dictionaryInfo != null)
                    {
                        // open the key for software
                        RegistryKey softwareKey = Registry.CurrentUser.OpenSubKey("Software", true);
                    
                        // open the key for Regionizer
                        softwareKey.CreateSubKey("Data Juggler");

                        // open the DataJugglerKey
                        RegistryKey dataJugglerKey = softwareKey.OpenSubKey("Data Juggler", true);

                        // Create the key for Regionizer
                        dataJugglerKey.CreateSubKey("Regionizer");

                        // Now create the key for Regionizer
                        RegistryKey regionizerKey = dataJugglerKey.OpenSubKey("Regionizer", true);

                        // Set the value for DictionaryPath
                        regionizerKey.SetValue("DictionaryPath", dictionaryInfo.DictionaryPath);

                        // Set the value for InstalledVersion
                        regionizerKey.SetValue("InstalledVersion", dictionaryInfo.InstalledVersion);

                        // Set the value for UseCustomDictionary
                        regionizerKey.SetValue("UseCustomDictionary", dictionaryInfo.UseCustomDictionary);

                        // Set the value for CustomDictionaryPath
                        regionizerKey.SetValue("CustomDictionaryPath", dictionaryInfo.CustomDictionaryPath);

                        // Set the value for TryCustomFirst
                        regionizerKey.SetValue("TryCustomDictionaryFirst", dictionaryInfo.TryCustomDictionaryFirst);

                        // Show the user a message
                        MessageBoxHelper.ShowMessage("Registry update completed successfully", "Registry Update Complete");
                    }
                }
                catch (Exception error)
                {
                    // for debugging only
                    string err = error.ToString();

                    // Show the user a message
                    MessageBoxHelper.ShowMessage("We were not able to upate the registry." + Environment.NewLine + "Running Visual Studio as an Administrator will usually fix this problem.", "Permission Denied");
                }
            }
            #endregion
            
        #endregion

    }
    #endregion

}
