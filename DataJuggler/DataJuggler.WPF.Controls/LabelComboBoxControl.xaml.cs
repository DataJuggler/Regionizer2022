

#region using statements

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Media;
using DataJuggler.WPF.Controls.Interfaces;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections;
using DataJuggler.WPF.Controls.Util;
using x = DataJuggler.WPF.Controls.Objects;

#endregion

namespace DataJuggler.WPF.Controls
{

    #region class LabelComboBoxControl
    /// <summary>
    /// Interaction logic for LabelComboBoxControl.xaml
    /// </summary>
    public partial class LabelComboBoxControl : UserControl
    {
        
        #region Private Variables
        private double labelWidth;
        private string labelText;
        private bool editable;
        private bool enabled;
        private x.ItemCollection items;
        private ISelectedIndexListener selectedIndexListener;
        private string source;
        private CollectionBase list;
        private bool sorted;
        private FontFamily labelFont;
        private FontWeight labelFontWeight;
        private double labelFontSize;
        private FontFamily comboBoxFont;
        private FontWeight comboBoxFontWeight;
        private double comboBoxFontSize;
        #endregion
        
        #region Constructor
        /// <summary>
        /// Create a new instance of a LabelComboBoxControl object.
        /// </summary>
        public LabelComboBoxControl()
        {
            // Create controls
            InitializeComponent();
        }
        #endregion

        #region Methods

            #region Clear()
            /// <summary>
            /// Clear the items in the ComboBox object.
            /// </summary>
            public void Clear()
            {
                // erase the selected index
                this.ComboBox.Items.Clear();
            }
            #endregion

            #region ClearSelection()
            /// <summary>
            /// Clear the selection in this control
            /// </summary>
            public void ClearSelection()
            {
                // erase the selected index
                this.ComboBox.SelectedIndex = this.FindDefaultIndex();
            }
            #endregion

            #region DisplayItems()
            /// <summary>
            /// Display the items
            /// </summary>
            public void DisplayItems()
            {
                // clear the combo box
                this.ComboBox.Items.Clear();

                // if the items exist
                if (this.Items != null)
                {
                    // load the items
                    foreach (object item in this.Items)
                    {
                        // add the item
                        this.ComboBox.Items.Add(item);
                    }

                    // find the default index
                    int defaultIndex = this.FindDefaultIndex();

                    // find the default index
                    this.ComboBox.SelectedIndex = defaultIndex;
                }
            }
            #endregion

            #region DisplayList(bool selectDefaultValue)
            /// <summary>
            /// Display the list
            /// </summary>
            public void DisplayList(bool selectDefaultValue)
            {
                // clear the combo box
                this.ComboBox.Items.Clear();
                
                // if the list exists
                if (this.List != null)
                {
                    // load the lists
                    foreach (object item in this.List)
                    {
                        // add the list
                        this.ComboBox.Items.Add(item);
                    }
                }

                // if selectDefaultValue is true
                if (selectDefaultValue)
                {
                    // find the default index
                    int defaultIndex = FindDefaultIndex();
                    
                    // set the defaultIndex
                    this.ComboBox.SelectedIndex = defaultIndex;
                }
            } 
            #endregion

            #region FindDefaultIndex()
            /// <summary>
            /// Find the DefaultIndex
            /// </summary>
            /// <returns></returns>
            public int FindDefaultIndex()
            {
                // locals
                int index = -1;
                int defaultIndex = -1;

                // load the lists
                foreach (object item in this.ComboBox.Items)
                {
                    // increment index
                    index++;

                    // cast the item as an IListItem
                    IListItem listItem = item as IListItem;

                    // if this is the default item
                    if ((listItem != null) && (listItem.DefaultItem))
                    {
                        // set the defaultIndex
                        defaultIndex = index;

                        // break out of the for loop
                        break;
                    }
                }

                // return value
                return defaultIndex;
            } 
            #endregion

            #region Init()
            /// <summary>
            /// This method performs initializations for this object.
            /// </summary>
            private void Init()
            {
                // create the items
                this.Items = new x.ItemCollection();
                
                // default to sorted
                this.Sorted = true;
                
                // set the default label width
                this.LabelWidth = 80;
                
                // default to editable
                this.Editable = true;
                
                // set the default TextAlign
                this.LabelTextAlign = System.Windows.HorizontalAlignment.Right;
            }
            #endregion

            #region SelectListItem(int primaryID)
            /// <summary>
            /// Select an item in the list by the primary key.
            /// The list must be filled with IListItem objects.
            /// </summary>
            /// <param name="primaryKey"></param>
            internal void SelectListItem(int primaryID)
            {
                // initial value
                int index = -1;
                int selectedIndex = -1;
                int defaultIndex = -1;
            
                // if the comboBox exists
                if (this.ComboBox != null) 
                {
                    // iterate the items in the combobox
                    foreach (object item in this.ComboBox.Items)
                    {
                        // increment index
                        index++;
                    
                        // cast the object as a IListItem
                        IListItem listItem = item as IListItem;
                        
                        // if the listItem exists and the PrimaryKey matches
                        if ((listItem != null) && (listItem.PrimaryID == primaryID))
                        {
                            // set the selectedIndex
                            selectedIndex = index;
                        
                            // break out of loop
                            break;
                        }
                        else if ((listItem != null) && (listItem.DefaultItem))
                        {
                            // set the defaultIndex
                            defaultIndex = index;
                        }
                    }
                    
                    if (selectedIndex >= 0)
                    {
                        // set the selectedIndex
                        this.ComboBox.SelectedIndex = selectedIndex;
                    }
                    else if (defaultIndex >= 0)
                    {
                        // set the selectedIndex
                        this.ComboBox.SelectedIndex = defaultIndex;
                    }
                }
            } 
            #endregion

            #region Setup(string labelText, int selectedIndex)
            /// <summary>
            /// This method prepares this control to be shown.
            /// </summary>
            /// <param name="labelText"></param>
            /// <param name="textBoxText"></param>
            public void Setup(string labelText, int selectedIndex)
            {
                // set the properties
                this.LabelText = labelText;
                this.ComboBox.SelectedIndex = selectedIndex;
            } 
            #endregion

            #region UIEnable()
            /// <summary>
            /// This method determines if the Textbox is enabled or not based upon the value
            /// of Editable and Updateable.
            /// </summary>
            private void UIEnable()
            {
                // enabled or not
                bool enabled = this.Editable;
                
                // enable the text box if enabled
                this.ComboBox.IsEnabled = enabled;
            }  
            #endregion

            #region FindItemIndexByValue(string itemValue, bool alowPartialMatch = false)
            /// <summary>
            /// This method finds an item by the ID value.
            /// </summary>
            public int FindItemIndexByValue(string itemValue, bool alowPartialMatch = false)
            {
                // initial value
                int index = -1;

                // locals
                int tempIndex = -1;
                string listItemValue = "";
                string listItemValue2 = "";
                string itemSearchValue = "";

                // verify we have an itemValue
                if (!String.IsNullOrEmpty(itemValue))
                {
                    // get a lowercase value
                    itemSearchValue = itemValue.ToLower();

                    // iterate the ListItems
                    foreach (ListBoxItem listItem in this.ComboBox.Items)
                    {
                        // increment tempIndex
                        tempIndex++;

                        // if there is value
                        if (listItem.Content != null)
                        {
                            // Get a lowercase version and put ba
                            listItemValue = listItem.Content.ToString().ToLower();
                            listItemValue2 = listItem.Content.ToString().ToLower().Replace("_", " ");

                            // if this is the item being sought
                            if (listItemValue == itemSearchValue)
                            {
                                // set the selected index value
                                index = tempIndex;

                                // break out of loop
                                break;
                            }
                            else if (listItemValue2 == itemSearchValue)
                            {
                                // set the selected index value
                                index = tempIndex;

                                // break out of loop
                                break;
                            }

                            // if a partial match is ok
                            if (alowPartialMatch)
                            {
                                // if this is the item being sought
                                if (listItemValue.Contains(itemSearchValue))
                                {
                                    // set the selected index value
                                    index = tempIndex;

                                    // break out of loop
                                    break;
                                }
                            }
                        }
                    }
                }

                // return value
                return index;
            }
            #endregion

            #region LoadItems(Type enumType)
            /// <summary>
            /// This method loads a combobox with the enum values
            /// </summary>
            public void LoadItems(Type enumType)
            {
                // locals
                string[] names = null;
                Array values = null;
                string itemValue = "";
                int index = -1;
                string formattedName = "";

                // clear the list
                this.ComboBox.Items.Clear();

                // verifyh the object is an emum
                if (enumType.IsEnum)
                {
                    // get the names from the enum
                    names = Enum.GetNames(enumType);
                    values = Enum.GetValues(enumType);

                    // if there are one or more names
                    if ((names != null) && (names.Length > 0) && (values != null) && (values.Length == names.Length))
                    {
                        // iterate the names
                        foreach (string name in names)
                        {
                            // increment index
                            index++;

                            // set the itemValue
                            itemValue = values.GetValue(index).ToString();

                            // replace out any underscores with spaces
                            formattedName = name.Replace("_", " ");

                            // create a list item
                            ListBoxItem listItem = new ListBoxItem();
                            listItem.Content = itemValue;
                            listItem.Name= formattedName;

                            // add this item
                            this.ComboBox.Items.Add(listItem);
                        }
                    }
                }
            }
            #endregion

            #region LoadItems<T>(List<T> items, bool addEmptyRowAtTop = false)
            /// <summary>
            /// This method loads a list of any type 
            /// </summary>
            public void LoadItems<T>(List<T> items, bool addEmptyRowAtTop = false)
            {
                // clear the list
                this.ComboBox.Items.Clear();

                // if the list exists
                if ((items != null) && (items.Count() > 0))
                {
                    // if addEmptyRowAtTop is true
                    if (addEmptyRowAtTop)
                    {
                        // add this item
                        this.ComboBox.Items.Add("");
                    }

                    // iterate the list
                    foreach (object item in items)
                    {
                        // if the item exists
                        if (item != null)
                        {
                            // set the value
                            IPrimaryKey primaryKey = item as IPrimaryKey;

                            // if the object is a PrimaryKey object
                            if (primaryKey != null)
                            {
                                // create a list item
                                ListBoxItem listItem = new ListBoxItem();

                                // set the name and content
                                listItem.Name = item.ToString();
                                listItem.Content = primaryKey.PrimaryKeyValue.ToString();

                                // add this item
                                this.ComboBox.Items.Add(listItem);
                            }
                            else
                            {
                                // add this item
                                this.ComboBox.Items.Add(item);
                            }
                        }
                    }
                }
            }
            #endregion

            #region SelectItemByValue(string itemValue)
            /// <summary>
            /// This method searches all of the items in the ComboBox
            /// and if the value matches that item is selected
            /// </summary>
            internal void SelectItemByValue(string itemValue)
            {
                // local
                int indexToSelect = -1;

                // if the combo box has one or more items
                if (this.ComboBox.Items.Count > 0)
                {
                    // find the index of the item to select
                    indexToSelect = FindItemIndexByValue(itemValue);

                    // set the SelectedIndex
                    this.ComboBox.SelectedIndex = indexToSelect;
                }
            }
            #endregion

            #region SetFocusToComboBox()
            /// <summary>
            /// This method sets focus to the ComboBox
            /// </summary>
            public void SetFocusToComboBox()
            {
                // Set Focus to the ComboBox
                this.ComboBox.Focus();
            }
            #endregion

        #endregion

        #region Properties

            #region ComboBoxControl
            /// <summary>
            /// This property returns the ComboBox
            /// </summary>
            public ComboBox ComboBoxControl
            {
                get
                {
                    // return the ComboBox
                    return this.ComboBox;
                }
            }
            #endregion

            #region ComboBoxFont
            /// <summary>
            /// This property gets or sets the value for 'ComboBoxFont'.
            /// </summary>
            public FontFamily ComboBoxFont
            {
                get { return comboBoxFont; }
                set 
                { 
                    // set the value
                    comboBoxFont = value;

                    // if the ComboBoxControl exists
                    if (this.HasComboBoxControl)
                    {
                        // set the value on the control
                        this.ComboBox.FontFamily = value;
                    }
                }
            }
            #endregion
            
            #region ComboBoxFontSize
            /// <summary>
            /// This property gets or sets the value for 'ComboBoxFontSize'.
            /// </summary>
            public double ComboBoxFontSize
            {
                get { return comboBoxFontSize; }
                set 
                { 
                    // set the value
                    comboBoxFontSize = value;

                    // if the control exists
                    if (this.HasComboBoxControl)
                    {
                        // set the value on the control
                        this.ComboBoxControl.FontSize = value;
                    }
                }
            }
            #endregion
            
            #region ComboBoxFontWeight
            /// <summary>
            /// This property gets or sets the value for 'ComboBoxFontWeight'.
            /// </summary>
            public FontWeight ComboBoxFontWeight
            {
                get { return comboBoxFontWeight; }
                set 
                { 
                    // set the value
                    comboBoxFontWeight = value;

                    // if the ComboBoxControl exists
                    if (this.HasComboBoxControl)
                    {
                        // set the value on the control
                        this.ComboBox.FontWeight = value;
                    }
                }
            }
            #endregion
            
            #region Editable
            /// <summary>
            /// This property gets or sets the value for 'Editable'.
            /// </summary>
            public bool Editable
            {
                get { return editable; }
                set 
                { 
                    // set the value
                    editable = value; 

                    // enable the DockPanel if the ComboBox is editable
                    if (this.MainDockPanel != null)
                    {
                        // set the value
                        this.ComboBoxControl.IsEditable = value;
                    }
                }
            }
            #endregion
            
            #region Enabled
            /// <summary>
            /// This property gets or sets the value for 'Enabled'.
            /// </summary>
            public bool Enabled
            {
                get { return enabled; }
                set
                {
                    // set the value
                    enabled = value;

                    // if the ComboBoxControl exists
                    if (this.HasComboBoxControl)
                    {
                        // set the value on the control
                        this.ComboBoxControl.IsEnabled = enabled;
                    }
                }
            }
            #endregion
            
            #region HasComboBoxControl
            /// <summary>
            /// This property returns true if this object has a 'ComboBoxControl'.
            /// </summary>
            public bool HasComboBoxControl
            {
                get
                {
                    // initial value
                    bool hasComboBoxControl = (this.ComboBoxControl != null);
                    
                    // return value
                    return hasComboBoxControl;
                }
            }
            #endregion
            
            #region HasItems
            /// <summary>
            /// This property returns true if this object has an 'Items'.
            /// </summary>
            public bool HasItems
            {
                get
                {
                    // initial value
                    bool hasItems = (this.Items != null);
                    
                    // return value
                    return hasItems;
                }
            }
            #endregion
            
            #region HasLabelControl
            /// <summary>
            /// This property returns true if this object has a 'LabelControl'.
            /// </summary>
            public bool HasLabelControl
            {
                get
                {
                    // initial value
                    bool hasLabelControl = (this.LabelControl != null);
                    
                    // return value
                    return hasLabelControl;
                }
            }
            #endregion
            
            #region Items
            /// <summary>
            /// This property gets or sets the value for 'Items'.
            /// </summary>
            public x.ItemCollection Items
            {
                get { return items; }
                set { items = value; }
            }
            #endregion
            
            #region LabelControl
            /// <summary>
            /// This read only property returns the 'Label' from this control.
            /// </summary>
            public Label LabelControl
            {
                get
                {
                    // return the Label
                    return this.Label;
                }
            }
            #endregion
            
            #region LabelFontFamily
            /// <summary>
            /// This property gets or sets the value for 'LabelFontFamily'.
            /// </summary>
            public FontFamily LabelFontFamily
            {
                get { return labelFont; }
                set 
                { 
                    // set the value
                    labelFont = value;

                    // if the LabelControl exists
                    if (this.HasLabelControl)
                    {
                        this.LabelControl.FontFamily = value;
                    }
                }
            }
            #endregion
            
            #region LabelFontSize
            /// <summary>
            /// This property gets or sets the value for 'LabelFontSize'.
            /// </summary>
            public double LabelFontSize
            {
                get { return labelFontSize; }
                set 
                { 
                    // set the value
                    labelFontSize = value;

                    // if the Label exists
                    if (this.HasLabelControl)
                    {
                        // set the fontsize on the label
                        this.Label.FontSize = value;
                    }
                }
            }
            #endregion
            
            #region LabelFontWeight
            /// <summary>
            /// This property gets or sets the value for 'LabelFontWeight'.
            /// </summary>
            public FontWeight LabelFontWeight
            {
                get { return labelFontWeight; }
                set 
                { 
                    // set the value
                    labelFontWeight = value;

                    // if the LabelControl exists
                    if (this.HasLabelControl)
                    {
                        // set the value on the control
                        this.LabelControl.FontWeight = value;
                    }
                }
            }
            #endregion
            
            #region LabelText
            /// <summary>
            /// This property gets or sets the value for 'LabelText'.
            /// </summary>
            public string LabelText
            {
                get 
                { 
                    // return value
                    return labelText;
                }
                set
                { 
                    // set the value
                    labelText = value;

                    // if the LabelControl exists
                    if (this.HasLabelControl)
                    {
                        // set the content
                        this.LabelControl.Content = value;
                    }
                }
            }
            #endregion
            
            #region LabelTextAlign
            /// <summary>
            /// This method sets the HorizontalContentAlignment for the Label.
            /// </summary>
            public HorizontalAlignment LabelTextAlign
            {
                get
                {
                    return this.Label.HorizontalContentAlignment;
                }
                set
                {
                    // set the value
                    this.Label.HorizontalContentAlignment = value;
                }
            }
            #endregion
            
            #region LabelWidth
            /// <summary>
            /// This property gets or sets the value for 'LabelWidth'.
            /// </summary>
            public double LabelWidth
            {
                get { return labelWidth; }
                set 
                { 
                    // set the value
                    labelWidth = value;

                    // if the LabelControl exists
                    if (this.HasLabelControl)
                    {
                        // set the content
                        this.LabelControl.Width = value;
                    }
                }
            }
            #endregion
            
            #region List
            /// <summary>
            /// This property gets or sets the value for 'List'.
            /// This property adds the items in the list to the combobox
            /// </summary>
            public CollectionBase List
            {
                get { return list; }
                set 
                { 
                    // set the value
                    list = value;

                    // if the ComboBoxControl exists
                    if (this.HasComboBoxControl)
                    {
                        // clear the list
                        this.ComboBoxControl.Items.Clear();

                        // if there are one or more items in the collection
                        if (ListHelper.HasOneOrMoreItems(value))
                        {
                            // iterate the items
                            foreach (object item in value)
                            {
                                // add this item
                                this.ComboBoxControl.Items.Add(item);
                            }
                        }
                    }
                }
            }
            #endregion
            
            #region SelectedIndexListener
            /// <summary>
            /// This property gets or sets the value for 'SelectedIndexListener'.
            /// </summary>
            public ISelectedIndexListener SelectedIndexListener
            {
                get { return selectedIndexListener; }
                set { selectedIndexListener = value; }
            }
            #endregion
            
            #region Sorted
            /// <summary>
            /// This property gets or sets the value for 'Sorted'.
            /// </summary>
            public bool Sorted
            {
                get { return sorted; }
                set 
                { 
                    // set the value
                    sorted = value;

                    // To do: Perform Sort
                }
            }
            #endregion
            
            #region Source
            /// <summary>
            /// This property gets or sets the value for 'Source'.
            /// </summary>
            public string Source
            {
                get { return source; }
                set { source = value; }
            }
            #endregion
            
        #endregion
        
    }
    #endregion

}
