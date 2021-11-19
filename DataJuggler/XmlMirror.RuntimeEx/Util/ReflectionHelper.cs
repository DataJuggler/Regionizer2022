

#region using statements

using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using XmlMirror.Runtime.Enumerations;
using XmlMirror.Runtime.Objects;

#endregion

namespace XmlMirror.Runtime.Util
{

    #region class ReflectionHelper
    /// <summary>
    /// This class uses reflection to read property attributes
    /// </summary>
    public class ReflectionHelper
    {

        #region ConvertObject(object sourceObject, object destinationObject)
        /// <summary>
        /// This method parses the sourceObject passed in and returns 
        /// the destination object with the values populated using
        /// reflection. The two objects passed in must be identical in
        /// the names of the properties and the number of properties
        /// or an error will occur. 
        /// </summary>
        /// <param name="sourceObject">The object to copy values from.</param>
        /// <param name="destinationObject">The object to paste values
        /// into and the object that will be returned.</param>
        /// <returns>The destination object with the values populated.</returns>
        public static object ConvertObject(object sourceObject, object destinationObject)
        {
            if ((sourceObject != null) && (destinationObject != null))
            {
                // Get the type of object sourceObject is.
                Type sourceType = sourceObject.GetType();

                // Get the type of object destinationObject is.
                Type destinationType = destinationObject.GetType();

                // Get Properties From Source Object
                PropertyInfo[] sourceProperties = sourceType.GetProperties();

                // Get Properties From Destination Object
                PropertyInfo[] destinationProperties = destinationType.GetProperties();

                // loop through each property
                foreach (PropertyInfo sourceInfoObject in sourceProperties)
                {
                    // get source property name
                    string sourcePropertyName = sourceInfoObject.Name;

                    // loop through each property in destination properties
                    // and find the matching property
                    foreach (PropertyInfo destinationInfoObject in destinationProperties)
                    {
                        // get destination property name
                        string destinationPropertyName = destinationInfoObject.Name;

                        // if this is the matching property
                        if (String.Compare(sourcePropertyName, destinationPropertyName) == 0)
                        {
                            // if this the matching property

                            // get source value
                            object sourceValue = destinationInfoObject.GetValue(sourceObject, null);

                            // set the value in destination object
                            destinationInfoObject.SetValue(sourceValue, destinationInfoObject, null);

                            // exit for loop
                            break;
                        }
                    }
                }
            }

            // Return Value
            return destinationObject;
        }
        #endregion

        #region GetPropertyAttributes(object sourceObject, bool skipReadOnlyProperties = false)
        /// <summary>
        /// This method returns the properties for an object instance
        /// with thier values.
        /// </summary>
        /// <param name="sourceObject"></param>
        /// <returns></returns>
        public static List<FieldValuePair> GetPropertyAttributes(object sourceObject, bool skipReadOnlyProperties = false)
        {
            // Initial Value
            List<FieldValuePair> properties = null;

            // if the sourceObject exists
            if (sourceObject != null)
            {
                // Get the type of object sourceObject is.
                Type type = sourceObject.GetType();

                // call the override for this method
                properties = GetPropertyAttributes(type, skipReadOnlyProperties);
            }

            // Return Value
            return properties;
        }
        #endregion

        #region GetPropertyAttributes(Type type, bool skipReadOnlyProperties = false)
        /// <summary>
        /// This method returns the properties for an object type.
        /// This method will have null property values for each field value pair.
        /// </summary>
        /// <param name="sourceObject"></param>
        /// <returns></returns>
        public static List<FieldValuePair> GetPropertyAttributes(Type type, bool skipReadOnlyProperties = false)
        {
            // Initial Value
            List<FieldValuePair> properties = new List<FieldValuePair>();

            // Get the value of this object
            object propertyValue = null;

            // skip is set to true if skipReadOnlyProperties and the property does not have a Setter (GetSetMethod() returns null)
            bool skip = false;

            // if the type exists
            if (type != null)
            {
                // Get a propretyInfo Array from sourceObject
                PropertyInfo[] propertyTypes = type.GetProperties();

                // loop through each property
                foreach (PropertyInfo infoObject in propertyTypes)
                {
                    // reset the value for skip
                    skip = false;

                    // get property name
                    string propertyName = infoObject.Name;

                    // convert the dataType
                    string dataType = infoObject.PropertyType.ToString();

                    // If this is not a 'SystemProperty'.
                    if (!SystemProperty(propertyName))
                    {
                        // if skip read only properties is true
                        if (skipReadOnlyProperties)
                        {
                            // if the GetSetMethod() is null, then skip this property
                            skip = (infoObject.GetSetMethod() == null);
                        }

                        // if the field should not be skipped
                        if (!skip)
                        {
                             // Create new field value pair object
                            FieldValuePair property = new FieldValuePair(propertyName, propertyValue);
                            
                            // if this Field is an Enumeration
                            if (infoObject.PropertyType.IsEnum)
                            {
                                // Set IsEnumeration to true
                                property.IsEnumeration = true;

                                // Get the type for this enum
                                Type enumType = infoObject.PropertyType;
                                
                                // Set teh EnumDataTypeName
                                property.EnumDataTypeName = enumType.ToString();
                                
                                 // set the dataTypeString (which is parsed into the FieldDataType)
                                property.FieldDataTypeString = enumType.Name;
                            }
                            else
                            {
                                // set the dataTypeString (which is parsed into the FieldDataType)
                                property.FieldDataTypeString = dataType;
                            }

                            // Now add this property to properties collection.
                            properties.Add(property);
                        }
                    }
                }
            }

            // Return Value
            return properties;
        }
        #endregion

        #region SystemProperty(string propertyName)
        /// <summary>
        /// Determines if the propertyName passed in is a 
        /// property such as 'ErrorProcessor', 'InitialValues'
        /// or 'Validation'.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        private static bool SystemProperty(string propertyName)
        {
            // Initial Value
            bool systemProperty = false;

            // get a lowercase version of propertyName
            string property = propertyName.ToLower();

            // determine if the property is a 'SystemProperty'.
            switch (propertyName.ToLower())
            {
                case "errorprocessor":
                case "initialvalues":
                case "validation":

                    // this is a 'SystemProperty'.
                    systemProperty = true;
                    // required
                    break;
            }

            // Return Value      
            return systemProperty;
        }
        #endregion

        #region ParseDataType(string propertyName, object sourceObject)
        /// <summary>
        /// This method parses the sourceObject passed in and returns 
        /// the data type (FieldDataTypeEnum) for the property
        /// passed in.
        /// </summary>
        /// <param name="propertyName">Property to determine the data type of.</param>
        /// <param name="sourceObject">Object that this property is an attribute of.</param>
        /// <returns></returns>
        public static FieldDataTypeEnum ParseDataType(string propertyName, object sourceObject)
        {
            // Initial Value
            FieldDataTypeEnum tempDataType = FieldDataTypeEnum.NotSet;

            // Create Reflection Object
            Type mirror = sourceObject.GetType();

            // Verify FieldName exists
            if (!String.IsNullOrEmpty(propertyName))
            {
                // get property from this object.
                PropertyInfo property = mirror.GetProperty(propertyName);

                // verify property is not null.
                if (property != null)
                {
                    // Get Property Type From Object
                    string propertyType = property.PropertyType.ToString();

                    switch (propertyType)
                    {
                        case "System.String":

                            // Set DataType to string
                            tempDataType = FieldDataTypeEnum.String;

                            // required
                            break;
                    }
                }
            }

            // Return Value
            return tempDataType;
        }
        #endregion

    }
    #endregion

}
