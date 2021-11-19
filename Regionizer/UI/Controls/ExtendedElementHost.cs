

#region using statements

using DataJuggler.Regionizer.Controls;
using System.Windows.Forms.Integration;

#endregion

namespace DataJuggler.Regionizer.UI.Controls
{
    public class ExtendedElementHost : ElementHost
    {
        
        #region Private Variables
        private CloseParentForm closeFormMethod;
        private UpdateRegistryCallBack updateRegistryMethod;
        #endregion

        #region Methods

            #region SetupDelegates()
            /// <summary>
            /// This method is used to set the delegates on the editor after they have been set on this control.
            /// </summary>
            public void SetupDelegates()
            {
                // if the child exists
                if ((this.Child != null) && (this.HasCloseFormMethod))
                {
                    // attempt to cast the child as a CommentDictionaryEditor
                    CommentDictionaryEditor editor = this.Child as CommentDictionaryEditor;

                    // if the editor exists
                    if (editor != null)
                    {
                        // setup the callbacks
                        editor.CloseParentMethod = this.CloseFormMethod;
                        editor.UpdateRegistryMethod = this.UpdateRegistryMethod;
                    }
                }
            }
            #endregion

        #endregion

        #region Properties
            
            #region CloseFormMethod
            /// <summary>
            /// This property gets or sets the value for 'CloseFormMethod'.
            /// </summary>
            public CloseParentForm CloseFormMethod
            {
                get { return closeFormMethod; }
                set { closeFormMethod = value; }
            }
            #endregion
            
            #region HasCloseFormMethod
            /// <summary>
            /// This property returns true if this object has a 'CloseFormMethod'.
            /// </summary>
            public bool HasCloseFormMethod
            {
                get
                {
                    // initial value
                    bool hasCloseFormMethod = (this.CloseFormMethod != null);
                    
                    // return value
                    return hasCloseFormMethod;
                }
            }
            #endregion
            
            #region HasUpdateRegistryMethod
            /// <summary>
            /// This property returns true if this object has an 'UpdateRegistryMethod'.
            /// </summary>
            public bool HasUpdateRegistryMethod
            {
                get
                {
                    // initial value
                    bool hasUpdateRegistryMethod = (this.UpdateRegistryMethod != null);
                    
                    // return value
                    return hasUpdateRegistryMethod;
                }
            }
            #endregion
            
            #region UpdateRegistryMethod
            /// <summary>
            /// This property gets or sets the value for 'UpdateRegistryMethod'.
            /// </summary>
            public UpdateRegistryCallBack UpdateRegistryMethod
            {
                get { return updateRegistryMethod; }
                set { updateRegistryMethod = value; }
            }
            #endregion
            
        #endregion

    }
}
