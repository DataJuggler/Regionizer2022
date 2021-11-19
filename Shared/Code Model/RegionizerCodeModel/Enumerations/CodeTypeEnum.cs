

#region using statements

#endregion

namespace DataJuggler.Regionizer.CodeModel.Enumerations
{

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

}