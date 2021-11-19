

#region using statements

using DataJuggler.Regionizer.CodeModel.Enumerations;

#endregion

namespace DataJuggler.Regionizer.CodeModel.Objects
{

    #region class Replacement
    /// <summary>
    /// This class is used to perform a FindAndReplace operation
    /// when performing an Auto Comment operation.
    /// </summary>
    public class Replacement
    {
        
        #region Private Variables
        private string find;
        private string replace;
        private ReplacementTargetEnum target;
        #endregion

        #region Properties

            #region Find
            /// <summary>
            /// This property gets or sets the value for 'Find'.
            /// </summary>
            public string Find
            {
                get { return find; }
                set { find = value; }
            }
            #endregion
            
            #region Replace
            /// <summary>
            /// This property gets or sets the value for 'Replace'.
            /// </summary>
            public string Replace
            {
                get { return replace; }
                set { replace = value; }
            }
            #endregion
            
            #region Target
            /// <summary>
            /// This property gets or sets the value for 'Target'.
            /// </summary>
            public ReplacementTargetEnum Target
            {
                get { return target; }
                set { target = value; }
            }
            #endregion
            
        #endregion
        
    }
    #endregion

}
