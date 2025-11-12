using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataJuggler.Regionizer.CodeModel.Enumerations
{
    #region enum CodeScopeEnum
    /// <summary>
    /// This enum represents the AccessModifier for a CodeLine or CodeBlock object.
    /// </summary>
    public enum CodeScopeEnum : int
    {
        Private = 0,
        Protected = 1,
        ProtectedInternal = 2,
        Internal = 3,
        Public = 4
    } 
    #endregion

    #region CodeTypeEnum : int
    /// <summary>
    /// This enum is used by CodeBlock objects
    /// </summary>
    public enum CodeTypeEnum : int
    {
        NotSet = 0,
        UsingStatement = 1,
        Namespace = 2,
        Class = 3,
        Constant = 4,
        Enumerations = 5,
        PrivateVariables = 6,
        Constructor = 7,
        Event = 8,
        Method = 9,
        Property = 10,
        Delegate = 11,
        Struct = 12,
        Interface = 13,
        Comment = 14,
        RegionLine = 15
    } 
	#endregion
    
    #region ComparisonTypeEnum : int
    /// <summary>
    /// This enum is used by the IfStatement object to determine the type of comparison between
    /// the source and target objectgs.
    /// </summary>
    public enum ComparisonTypeEnum : int
    {
        NotSet = 0,
        LessThan = - 2,
        LessThanOrEqual = -1,
        Equal = 0,
        GreaterThanOrEqual = 1,
        GreaterThan = 2,
        NotEqual = 3
    }
    #endregion

    #region ComponentTypeEnum : int
    /// <summary>
    /// This enum is for the type of Component to add
    /// </summary>
    public enum ComponentTypeEnum : int
    {
        Unknown = 0,
        TextBox = 1,
        ComboBox = 2,
        ComboBoxCheckedList = 3,
        Calendar = 4,
        Time = 5,
        Toggle = 6,
        CheckBox = 7,
        InformationBox = 8,
        Grid = 9,
        Label = 10,
        FileUpload = 11,
        ProgressBar = 12
    }
    #endregion

    #region DataTypeEnum : int
    /// <summary>
    /// This is from DataJuggler.NET, but easiest way was just copy it here
    /// </summary>
    public enum DataTypeEnum : int
    {
        NotSupported = 0,
		Object = 1,
        Autonumber = 3,
        Currency = 6,
        DateTime = 7,
        Double = 5,
        Integer = 2,
        Percentage = 4,
        String = 130,
        YesNo = 11,
        Decimal = 12,
        DataTable = 10000,
        Binary = 10001,
        Boolean = 10002,
        Guid = 10003,
        Custom = 10004,
        Enumeration = 10005
    }
    #endregion

    #region ReplacementTargetEnum
    /// <summary>
    /// This enumeration is used to determine the target of Replacement and is applied
    /// </summary>
    public enum ReplacementTargetEnum : int
    {
        ApplyToSource = 0,
        ApplyToTarget1  = 1,
        ApplyToTarget2 = 2  
    }
    #endregion

}
