

#region using statements

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataJuggler.Regionizer.CodeModel.Objects;

#endregion

namespace DataJuggler.Regionizer.Commenting.Interfaces
{

    #region interface IInspector
    /// <summary>
    /// This interface must be implemented by all Inspectors
    /// </summary>
    public interface IInspector
    {

        #region Methods
            
            #region bool IsMatch(CodeComment comment, string sourceCode, ref string target, ref string target2);
            /// <summary>
            /// The Inspect method compares the source code given against the code given.
            /// If the source code matches the comment.Pattern then true is returned, else
            /// false is returned.
            /// </summary>
            /// <param name="comment"></param>
            /// <param name="sourceCode"></param>
            /// <returns></returns>
            bool IsMatch(CodeComment comment, string sourceCode, ref string target, ref string target2);
            #endregion

        #endregion

        #region Properties

            #region ID
            /// <summary>
            /// The ID For This Inspector
            /// </summary>
            Guid ID
            {
                get;
            }
            #endregion

        #endregion

    }
    #endregion

}
