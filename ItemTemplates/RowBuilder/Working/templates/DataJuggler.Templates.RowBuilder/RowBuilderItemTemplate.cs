

#region using statements

using DataJuggler.Blazor.Components.Objects;
using DataJuggler.Blazor.Components.Util;
using DataJuggler.Excelerate;
using DataJuggler.UltimateHelper;
using ObjectLibrary.BusinessObjects;
using System.Collections.Generic;

#endregion

namespace [NamespaceName]
{

    #region class [ClassName]
    /// <summary>
    /// Creates grid rows from a list of [ObjectName] items.
    /// </summary>
    public class [ClassName]
    {  
        
        #region Methods
            
            #region CreateRows(List<[ObjectName]> [ListVariableName], List<GridColumn> columns, int itemsPerPage = 0, int currentPage = -1)
            /// <summary>
            /// This method create a List<Row> for the list of [ObjectName] items passed in
            /// </summary>
            /// <returns>a List<Row></returns>
            public List<Row> CreateRows(List<[ObjectName]> [ListVariableName], List<GridColumn> columns, int itemsPerPage = 0, int currentPage = -1)
            {
                // initial value
                List<Row> rows = null;

                // If the [ListVariableName] collection exists and has one or more items 
                if (ListHelper.HasOneOrMoreItems([ListVariableName], columns))
                {
                    // Get the response
                    PageResponse page = GridHelper.GetPage([ListVariableName], itemsPerPage, currentPage);

                    // If the page.Items collection exists and has one or more items
                    if (ListHelper.HasOneOrMoreItems(page.Items))                    
                    {
                        // Create the rows
                        rows = new List<Row>();

                        // convert to List of [ObjectName]
                        List<[ObjectName]> page[ListVariableName] = page.Items.Cast<[ObjectName]>().ToList();
                    
                        // Iterate the collection of [ObjectName] objects
                        foreach([ObjectName] [VariableName] in page[ListVariableName])
                        {
                            // Create a new instance of a 'Row' object.
                            Row row = new Row();

                            // Iterate the collection of GridColumn objects
                            foreach (GridColumn gridColumn in columns)
                            {
                                // Create a column
                                Column column = gridColumn.ExportAsColumn();

                                // Set the row
                                column.Row = row;

                                // Get the value
                                column.ColumnValue = [VariableName].GetValue(column.ColumnName);

                                // Add this column
                                row.Columns.Add(column);
                            }

                            // Add this row
                            rows.Add(row);
                        }
                    }
                }

                // return value
                return rows;
            }
            #endregion
            
        #endregion
        
    }
    #endregion

}