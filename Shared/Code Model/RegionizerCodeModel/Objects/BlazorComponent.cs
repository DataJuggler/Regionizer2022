

#region using statements

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#endregion

namespace DataJuggler.Regionizer.CodeModel.Objects
{

    #region class BlazorComponent
    /// <summary>
    /// This class is used for parsing razor markup
    /// </summary>
    public class BlazorComponent
    {
        
        #region Private Variables
        private string name;
        private string type;
        private bool hasNameParameter;
        private bool registered;
        #endregion
        
        #region Constructor
        /// <summary>
        /// Create a new instance of a 'BlazorComponent' object.
        /// </summary>
        public BlazorComponent(string name, string type, bool hasNameParameter)
        {
            // store the args
            Name = name;
            Type = type;
            HasNameParameter = hasNameParameter;
        }
        #endregion

        #region Methods

            #region ToString()
            /// <summary>
            /// method returns the String
            /// </summary>
            public override string ToString()
            {
                // return the name + type
                return Type + " " + Name;
            }
            #endregion

        #endregion

        #region Properties

            #region HasNameParameter
            /// <summary>
            /// This property gets or sets the value for 'HasNameParameter'.
            /// </summary>
            public bool HasNameParameter
            {
                get { return hasNameParameter; }
                set { hasNameParameter = value; }
            }
            #endregion
            
            #region Name
            /// <summary>
            /// This property gets or sets the value for 'Name'.
            /// </summary>
            public string Name
            {
                get { return name; }
                set { name = value; }
            }
            #endregion
            
            #region Registered
            /// <summary>
            /// This property gets or sets the value for 'Registered'.
            /// This is used by the WireUpComponents button click.
            /// When comparing lists of registered razor components vs registered code componets
            /// so we can get the unregistered components that need to be registered. 
            /// </summary>
            public bool Registered
            {
                get { return registered; }
                set { registered = value; }
            }
            #endregion
            
            #region Type
            /// <summary>
            /// This property gets or sets the value for 'Type'.
            /// </summary>
            public string Type
            {
                get { return type; }
                set { type = value; }
            }
            #endregion
            
        #endregion
        
    }
    #endregion

}
