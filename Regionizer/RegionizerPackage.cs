

#region using statements

using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.ComponentModel.Design;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.Shell;

#endregion

namespace DataJuggler.Regionizer
{

    #region class RegionizerPackage
    /// <summary>
    /// This is the class that implements the package exposed by this assembly.
    ///
    /// The minimum requirement for a class to be considered a valid package for Visual Studio
    /// is to implement the IVsPackage interface and register itself with the shell.
    /// This package uses the help er classes defined inside the Managed Package Framework (MPF)
    /// to do it: it derives from the Package class that provides the implementation of the 
    /// IVsPackage interface and uses the registration attributes defined in the framework to 
    /// register itself and its components with the shell.
    /// </summary>
    // This attribute tells the PkgDef creation utility (CreatePkgDef.exe) that this class is
    // a package.
    [PackageRegistration(UseManagedResourcesOnly = true)]
    [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)]
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [ProvideToolWindow(typeof(RegionizerMainWindow))]
    [Guid(GuidList.guidRegionizerPkgString)]
    public sealed class RegionizerPackage : Package
    {
        
        #region Private Variables
        #endregion
        
        #region Constructor
        /// <summary>
        /// Default constructor of the package.
        /// Inside this method you can place any initialization code that does not require 
        /// any Visual Studio service because at this point the package object is created but 
        /// not sited yet inside Visual Studio environment. The place to do all the other 
        /// initialization is the Initialize method.
        /// </summary>
        public RegionizerPackage()
        {
            Trace.WriteLine(string.Format(CultureInfo.CurrentCulture, "Entering constructor for: {0}", this.ToString()));
        }
        #endregion
        
        #region Events
            
            #region MenuItemCallback(object sender, EventArgs e)
            /// <summary>
            /// This function is the callback used to execute a command when the a menu item is clicked.
            /// See the Initialize method to see how the menu item is associated to this function using
            /// the OleMenuCommandService service and the MenuCommand class.
            /// </summary>
            private void MenuItemCallback(object sender, EventArgs e)
            {
                try
                {
                    // show the tool window
                    this.ShowToolWindow(sender, e);
                }
                catch (Exception error)
                {
                    // for debugging only
                    string err = error.ToString();
                }
            }
            #endregion
            
            #region ShowToolWindow(object sender, EventArgs e)
            /// <summary>
            /// This function is called when the user clicks the menu item that shows the 
            /// tool window. See the Initialize method to see how the menu item is associated to 
            /// this function using the OleMenuCommandService service and the MenuCommand class.
            /// </summary>
            private void ShowToolWindow(object sender, EventArgs e)
            {
                // Get the instance number 0 of this tool window. This window is single instance so this instance
                // is actually the only one.
                // The last flag is set to true so that if the tool window does not exists it will be created.
                ToolWindowPane window = this.FindToolWindow(typeof(RegionizerMainWindow), 0, true);
                if ((null == window) || (null == window.Frame))
                {
                    throw new NotSupportedException("Can not create window.");
                }
                IVsWindowFrame windowFrame = (IVsWindowFrame)window.Frame;
                Microsoft.VisualStudio.ErrorHandler.ThrowOnFailure(windowFrame.Show());
            }
            #endregion
            
        #endregion
        
        #region Methods
            
            #region Initialize()
            /// <summary>
            /// Initialization of the package; this method is called right after the package is sited, so this is the place
            /// where you can put all the initilaization code that rely on services provided by VisualStudio.
            /// </summary>
            protected override void Initialize()
            {
                // call the base Initialize
                base.Initialize();
                
                // Add our command handlers for menu (commands must exist in the .vsct file)
                OleMenuCommandService mcs = GetService(typeof(IMenuCommandService)) as OleMenuCommandService;
                if ( null != mcs )
                {
                    // Create the command for the menu item.
                    CommandID menuCommandID = new CommandID(GuidList.guidRegionizerCmdSet, (int)PkgCmdIDList.cmdRegionizerPro);
                    MenuCommand menuItem = new MenuCommand(MenuItemCallback, menuCommandID );
                    mcs.AddCommand( menuItem );

                    // Create the command for the tool window
                    CommandID toolwndCommandID = new CommandID(GuidList.guidRegionizerCmdSet, (int)PkgCmdIDList.cmdRegionizerToolWindow);
                    MenuCommand menuToolWin = new MenuCommand(ShowToolWindow, toolwndCommandID);
                    mcs.AddCommand( menuToolWin );
                }
            }
            #endregion
            
        #endregion
        
    }
    #endregion

}
