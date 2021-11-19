

#region using statements

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataJuggler.Regionizer.Commenting.Interfaces;

#endregion

namespace DataJuggler.Regionizer.Commenting.Inspectors
{

    #region class InspectorManager
    /// <summary>
    /// This class is used to register and use Inspectors.
    /// </summary>
    public class InspectorManager
    {
        
        #region Private Variables
        private List<IInspector> inspectors;
        #endregion

        #region Properties

            #region Inspectors
            /// <summary>
            /// This property gets or sets the value for 'Inspectors'.
            /// </summary>
            public List<IInspector> Inspectors
            {
                get { return inspectors; }
                set { inspectors = value; }
            }
            #endregion
            
        #endregion
        
    }
    #endregion

}
