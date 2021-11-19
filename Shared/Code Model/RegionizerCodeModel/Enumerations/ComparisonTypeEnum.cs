

#region using statements

#endregion

namespace DataJuggler.Regionizer.CodeModel.Enumerations
{

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

}
