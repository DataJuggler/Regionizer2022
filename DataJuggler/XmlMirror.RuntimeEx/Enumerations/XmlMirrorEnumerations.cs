

#region using statements

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#endregion

namespace XmlMirror.Runtime.Enumerations
{

    #region DataTypeEnum
    /// <summary>
    /// This is ported from DataClassBuilder.Net.DataManager.Enumerations
    /// </summary>
    public enum DataTypeEnum : int
    {
        NotSupported = 0,
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
        Object = 10005,
        GenericList = 10006,
        Enumeration = 10007
    }
    #endregion

    #region DocumentType
    /// <summary>
    /// This enum is the type of Help document to launch.
    /// </summary>
    public enum DocumentType
    {
        Word = 0,
        Pdf = 1
    }
    #endregion

    #region FieldDataTypeEnum
    /// <summary>
    /// This enumeration is the choices for 'DataType'
    /// for a 'RequiredField'. This makes generating
    /// the validation possible.
    /// </summary>
    [Serializable]
    public enum FieldDataTypeEnum : int
    {
        NotSet = 0,
        String = 1,
        Integer = 2,
        MustBeTrueBoolean = 3
    }
    #endregion

    #region InstanceTypeEnum
    /// <summary>
    /// This enumeration is used to keep track of the type of element we should map to.
    /// When Singleton is seleced there must be a source property in the XmlMap.
    /// When using Multiple there must be a Target property and a Parent property. 
    /// </summary>
    [Serializable]
    public enum InstanceTypeEnum
    {
        Unknown = 0,
        Singleton = 1,
        Multiple = 2
    }
    #endregion
    
    #region MapTypeEnum
    /// <summary>
    /// This enumeration is used to keep track of the type of element we should map to.
    /// </summary>
    [Serializable]
    public enum MapTypeEnum
    {
        DoNotMap = -1,
        Unknown = 0, 
        MapToAttribute = 1,
        MapToElement = 2
    }
    #endregion

    #region MirrorTypeEnum
    /// <summary>
    /// This enumeration is used to determine the type of object to build
    /// </summary>
    public enum MirrorTypeEnum
    {
        Unknown = 0,
        Singleton = 1,
        Collection = 2,
        Writer = 3
    }
    #endregion

    #region OutputTypeEnum : int
    /// <summary>
    /// This enumeration is used to determine if we are creating a parser or creating a writer
    /// </summary>
    public enum OutputTypeEnum : int
    {
        Unknown = 0,
        Parser = 1,
        Writer = 2    
    }
    #endregion
    
}
