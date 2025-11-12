

#region using statements

using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

#endregion

namespace DataJuggler.Regionizer.CodeModel.Util
{

    #region class CommandRunner
    /// <summary>
    /// This class is used to run dotnet commands such as installing templates
    /// or creating new instances of templates.
    /// </summary>
    public class CommandRunner
    {
        
        #region Methods
            
            #region CreateInstance(string createCommand, string targetFolder)
            /// <summary>
            /// Creates a new instance of a dotnet template in the specified folder.
            /// Example: "dotnet new RowBuilder -n Customer"
            /// </summary>
            /// <param name="createCommand">The full dotnet new command.</param>
            /// <param name="targetFolder">The folder where the template should be created.</param>
            /// <returns>True if the creation succeeded; otherwise false.</returns>
            public static bool CreateInstance(string createCommand, string targetFolder)
            {
                // initial value
                bool success = false;

                try
                {  

                    ProcessStartInfo startInfo = new ProcessStartInfo
                    {
                        FileName = "cmd.exe",
                        Arguments = "/c " + createCommand,
                        WorkingDirectory = targetFolder,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    };

                    using (Process process = new Process())
                    {
                        process.StartInfo = startInfo;
                        process.Start();

                        // Start both reads concurrently
                        Task<string> outputTask = process.StandardOutput.ReadToEndAsync();
                        Task<string> errorTask  = process.StandardError.ReadToEndAsync();

                        // Block until the process exits
                        process.WaitForExit();

                        // Ensure both stream reads complete before checking ExitCode
                        Task.WaitAll(new Task[] { outputTask, errorTask });

                        string output = outputTask.Result;
                        string error  = errorTask.Result;

                        success = (process.ExitCode == 0);

                        if (!success && !string.IsNullOrEmpty(error))
                        {
                            Console.WriteLine("Template install error:\n" + error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.Message);
                }

                // return value
                return success;
            }
            #endregion
            
            #region InstallTemplate(string installCommand)
            /// <summary>
            /// Installs a dotnet template globally (no folder needed).
            /// Example: "dotnet new install DataJuggler.Templates.RowBuilder::9.20.0"
            /// </summary>
            /// <param name="installCommand">The full dotnet install command.</param>
            /// <returns>True if the installation succeeded; otherwise false.</returns>
            public static bool InstallTemplate(string installCommand)
            {
                // initial value
                bool success = false;

                try
                {
                    ProcessStartInfo startInfo = new ProcessStartInfo
                    {
                        FileName = "cmd.exe",
                        Arguments = "/c " + installCommand,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    };

                    using (Process process = new Process())
                    {
                        process.StartInfo = startInfo;
                        process.Start();

                        // Start both reads concurrently
                        Task<string> outputTask = process.StandardOutput.ReadToEndAsync();
                        Task<string> errorTask  = process.StandardError.ReadToEndAsync();

                        // Block until the process exits
                        process.WaitForExit();

                        // Ensure both stream reads complete before checking ExitCode
                        Task.WaitAll(new Task[] { outputTask, errorTask });

                        string output = outputTask.Result;
                        string error  = errorTask.Result;

                        success = (process.ExitCode == 0);

                        if (!success && !string.IsNullOrEmpty(error))
                        {
                            Console.WriteLine("Template install error:\n" + error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.Message);
                }

                // return value
                return success;
            }
            #endregion
            
        #endregion
        
    }
    #endregion

}
