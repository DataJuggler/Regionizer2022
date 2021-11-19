

#region using statements

using DataJuggler.WPF.Controls.Interfaces;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

#endregion

namespace DataJuggler.WPF.Controls.TabControls
{

    #region class TabHostControl
    /// <summary>
    /// Interaction logic for TabHostControl.xaml
    /// </summary>
    public partial class TabHostControl : UserControl
    {
        
        #region Private Variables
        private TabButton selectedTab;
        private List<TabButton> tabs;
        #endregion
        
        #region Constructor
        /// <summary>
        /// This class is used to host the TabButton controls.
        /// </summary>
        public TabHostControl()
        {
            // Create controls
            InitializeComponent();

            // Perform initializations for this object
            Init();
        }
        #endregion

        #region Methods

            #region GetTabButton(int buttonNumber)
            /// <summary>
            /// This method returns the Tab Button
            /// </summary>
            public TabButton GetTabButton(int buttonNumber)
            {
                // initial value
                TabButton tabButton = null;

                // Determine which button to show or hide
                switch (buttonNumber)
                {
                    case 1:

                        // Set the return value
                        tabButton = this.TabButton1;

                        // required
                        break;

                    case 2:

                        // Set the return value
                        tabButton = this.TabButton2;

                        // required
                        break;

                    case 3:

                        // Set the return value
                        tabButton = this.TabButton3;

                        // required
                        break;

                    case 4:

                        // Set the return value
                        tabButton = this.TabButton4;

                        // required
                        break;

                    case 5:

                        // Set the return value
                        tabButton = this.TabButton5;

                        // required
                        break;

                    case 6:

                        // Set the return value
                        tabButton = this.TabButton6;

                        // required
                        break;
                }

                // return value
                return tabButton;
            }
            #endregion

            #region HideButton(int buttonNumber, bool showButton = false)
            /// <summary>
            /// This method returns the Button
            /// </summary>
            public void HideButton(int buttonNumber, bool showButton = false)
            {
                // Get the tabs button
                TabButton tabButton = GetTabButton(buttonNumber);

                // if the tabButton exists
                if (tabButton != null)
                {
                    // if the button should be shown
                    if (showButton)
                    {
                        // Show or hide the button
                        tabButton.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        // Show or hide the button
                        tabButton.Visibility = Visibility.Hidden;
                    }
                }
            }
            #endregion

            #region Init()
            /// <summary>
            /// This method performs initializations for this object.
            /// </summary>
            public void Init()
            {
                // Select the first button
                this.TabButton1.Selected = true;
            }
            #endregion

            #region SetButtonWidth(int width)
            /// <summary>
            /// This method sets the Button Width for all tabs
            /// </summary>
            public void SetButtonWidth(int width)
            {
                // if the tabs exist
                if (this.HasTabs)
                {
                    // iterate the tabs
                    foreach (TabButton tab in this.Tabs)
                    {
                        // set the width of this button
                        tab.Width = width;
                    }
                }
            }
            #endregion

            #region SetButtonWidth(int buttonNumber, int width)
            /// <summary>
            /// This method returns the Button Width
            /// </summary>
            public void SetButtonWidth(int buttonNumber, int width)
            {
                // Get the tabs button
                TabButton tabButton = GetTabButton(buttonNumber);

                // if the tabButton exists
                if (tabButton != null)
                {
                    // Show or hide the button
                    tabButton.Width = width;
                }
            }
            #endregion

            #region SetupButton(int buttonNumber, string buttonText)
            /// <summary>
            /// This method returns the Button
            /// </summary>
            public void SetupButton(int buttonNumber, string buttonText)
            {
                // Get the tabs button
                TabButton tabButton = GetTabButton(buttonNumber);

                // if the tabButton exists
                if (tabButton != null)
                {
                    // Show or hide the button
                    tabButton.ButtonText = buttonText;
                }
            }
            #endregion

            #region TabSelected(TabButton selectedTab)
            /// <summary>
            /// A button was selected
            /// </summary>
            internal void TabSelected(TabButton selectedTab)
            {
                // set the selected tabs
                this.SelectedTab = selectedTab;

                // if the Tabs collection exists
                if ((this.HasTabs) && (this.HasSelectedTab))
                {
                    // iterate the tabs
                    foreach (TabButton button in this.Tabs)
                    {
                        // if is not the button selected
                        if (button.ButtonNumber != selectedTab.ButtonNumber)
                        {
                            // un select this button
                            button.Selected = false;
                        }
                    }
                }

                // if the ParentTabHostControl exists
                if (this.HasParentTabHostControl)
                {
                    // Notify the parent a button was selected
                    this.ParentTabHostControl.TabSelected(selectedTab);
                }
            }
            #endregion
            
        #endregion

        #region Properties

            #region Tabs
            /// <summary>
            /// This property gets or sets the value for 'Tabs'.
            /// </summary>
            public List<TabButton> Tabs
            {
                get { return tabs; }
                set { tabs = value; }
            }
            #endregion

            #region HasTabs
            /// <summary>
            /// This property returns true if this object has a 'Tabs'.
            /// </summary>
            public bool HasTabs
            {
                get
                {
                    // initial value
                    bool hasButtons = (this.Tabs != null);

                    // return value
                    return hasButtons;
                }
            }
            #endregion

            #region HasParentTabHostControl
            /// <summary>
            /// This property returns true if this object has a 'ParentTabHostControl'.
            /// </summary>
            public bool HasParentTabHostControl
            {
                get
                {
                    // initial value
                    bool hasParentTabHostControl = (this.ParentTabHostControl != null);

                    // return value
                    return hasParentTabHostControl;
                }
            }
            #endregion

            #region HasSelectedTab
            /// <summary>
            /// This property returns true if this object has a 'SelectedTab'.
            /// </summary>
            public bool HasSelectedTab
            {
                get
                {
                    // initial value
                    bool hasSelectedTab = (this.SelectedTab != null);

                    // return value
                    return hasSelectedTab;
                }
            }
            #endregion

            #region HasSelectedTabNumber
            /// <summary>
            /// This property returns true if the 'SelectedTabNumber' is set.
            /// </summary>
            public bool HasSelectedTabNumber
            {
                get
                {
                    // initial value
                    bool hasSelectedTabNumber = (this.SelectedTabNumber > 0);

                    // return value
                    return hasSelectedTabNumber;
                }
            }
            #endregion

            #region ParentTabHostControl
            /// <summary>
            /// This read only property returns the value for 'ParentTabHostControl'.
            /// </summary>
            public ITabHostControlParent ParentTabHostControl
            {
                get
                {
                    // initial value
                    ITabHostControlParent parentTabHost = this.Parent as ITabHostControlParent;

                    // return value
                    return parentTabHost;
                }
            }
            #endregion

            #region SelectedTab
            /// <summary>
            /// This property gets or sets the value for 'SelectedTab'.
            /// </summary>
            public TabButton SelectedTab
            {
                get { return selectedTab; }
                set { selectedTab = value; }
            }
            #endregion

            #region SelectedTabNumber
            /// <summary>
            /// This read only property gets or sets the value for 'SelectedTab'.
            /// </summary>
            public int SelectedTabNumber
            {
                get
                {
                    // initial value
                    int selectedTabNumber = 0;

                    // if the SelectedTab exists
                    if (this.HasSelectedTab)
                    {
                        // set the button number
                        selectedTabNumber = this.SelectedTab.ButtonNumber;
                    }

                    // return value
                    return selectedTabNumber;
                }
            }
            #endregion
            
        #endregion
        
    }
    #endregion

}
