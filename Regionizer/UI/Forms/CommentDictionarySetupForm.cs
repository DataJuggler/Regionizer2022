

#region using statements

using System.Windows.Forms;
using DataJuggler.Regionizer.Controls;
using DataJuggler.Regionizer.CodeModel.Objects;

#endregion

namespace DataJuggler.Regionizer.UI.Forms
{

    #region class CommentDictionarySetupForm
    /// <summary>
    /// This form is used to download the comment dictionary and store the path
    /// in the registry.
    /// </summary>
    public partial class CommentDictionarySetupForm : Form
    {
        
        #region Private Variables
        private bool userCancelled;
        private string dictionaryPath;
        #endregion
        
        #region Constructor
        /// <summary>
        /// Create a new instance of a CommentDictionarySetupForm object.
        /// </summary>
        public CommentDictionarySetupForm()
        {
            // Create controls
            InitializeComponent();

            // Perform initializations for this object
            Init();
        }
        #endregion
       
        #region Methods

            #region CloseForm()
            /// <summary>
            /// This is the callback delegate for the control
            /// </summary>
            public void CloseForm()
            {
                // close this object
                this.Close();
            }
            #endregion

            #region UpdateRegistry(DictionaryInfo dictionaryInfo)
            /// <summary>
            /// Update the values in the registry
            /// </summary>
            /// <param name="dictionaryInfo"></param>
            public void UpdateRegistry(DictionaryInfo dictionaryInfo)
            {
                // Set the DictionaryPath
                // RegistryHelper.StoreDictionaryInfo(dictionaryInfo);
            }
            #endregion

            #region Init()
            /// <summary>
            /// This method performs initializations for this object.
            /// </summary>
            public void Init()
            {
                // Set the CloseFormMethod delegate
                this.MainHost.CloseFormMethod = this.CloseForm;

                // Set the UpdateRegistry delegate
                this.MainHost.UpdateRegistryMethod = this.UpdateRegistry;

                // Setup the CloseFormMethod on the control
                this.MainHost.SetupDelegates();
            }
            #endregion

            #region Setup(DictionaryInfo dictionaryInfo)
            /// <summary>
            /// This method sets up this control
            /// </summary>
            internal void Setup(DictionaryInfo dictionaryInfo)
            {
                // call the method on the CommentDictionaryEditor
                CommentDictionaryEditor dictionaryEditor = this.MainHost.Child as CommentDictionaryEditor;

                // if the dictionaryEditor object exists
                if (dictionaryEditor != null)
                {
                    // setup the control
                    dictionaryEditor.Setup(dictionaryInfo);
                }
            }
            #endregion
            
        #endregion

        #region Properties

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
            
            #region UserCancelled
            /// <summary>
            /// This property gets or sets the value for 'UserCancelled'.
            /// </summary>
            public bool UserCancelled
            {
                get { return userCancelled; }
                set { userCancelled = value; }
            }
            #endregion
            
        #endregion

    }
    #endregion

}
