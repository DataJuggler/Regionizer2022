

#region using statements


#endregion

namespace DataJuggler.Regionizer.UI.Forms
{

    #region class BlazorComponentsForm
    /// <summary>
    /// This class [Enter Class Description]
    /// </summary>
    partial class BlazorComponentsForm
    {
        
        #region Private Variables
        private System.ComponentModel.IContainer components = null;
        private DataJuggler.Win.Controls.LabelComboBoxControl FontComboBox;
        private DataJuggler.Win.Controls.LabelTextBoxBrowserControl RazorFilePicker;
        private DataJuggler.Win.Controls.LabelTextBoxControl NamespaceControl;
        private DataJuggler.Win.Controls.LabelTextBoxControl OutputFileControl;
        private DataJuggler.Win.Controls.LabelTextBoxBrowserControl OutputFolderSelector;
        private System.Windows.Forms.CheckBox SlimCheckBox;
        private System.Windows.Forms.Label LabelLabelColor;
        private System.Windows.Forms.CheckBox CheckedListModeCheckBox;
        private System.Windows.Forms.Button AddCheckedListBox;
        private System.Windows.Forms.Button FileUploadButton;
        private System.Windows.Forms.Button AddTimeComponent;
        private System.Windows.Forms.Button AddCalendarButton;
        private System.Windows.Forms.Button AddToggleButton;
        private System.Windows.Forms.Button AddSaveAndCancelButtons;
        private System.Windows.Forms.Button AddTextBoxButton;
        private System.Windows.Forms.Button AddListBoxButton;
        private System.Windows.Forms.Button AddLabelButton;
        private System.Windows.Forms.Button AddGridButton;
        private System.Windows.Forms.Button AddImageButton;
        private System.Windows.Forms.Button AddCheckBox;
        private System.Windows.Forms.Button AddComboBoxbutton;
        private System.Windows.Forms.Button HiddenButton;
        #endregion
        
        #region Methods
            
            #region Dispose(bool disposing)
            /// <summary>
            /// Clean up any resources being used.
            /// </summary>
            /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
            protected override void Dispose(bool disposing)
            {
                if (disposing && (components != null))
                {
                    components.Dispose();
                }
                base.Dispose(disposing);
            }
            #endregion
            
            #region InitializeComponent()
            /// <summary>
            /// Required method for Designer support - do not modify
            /// the contents of this method with the code editor.
            /// </summary>
            private void InitializeComponent()
            {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BlazorComponentsForm));
            this.SlimCheckBox = new System.Windows.Forms.CheckBox();
            this.LabelLabelColor = new System.Windows.Forms.Label();
            this.CheckedListModeCheckBox = new System.Windows.Forms.CheckBox();
            this.AddCheckedListBox = new System.Windows.Forms.Button();
            this.FileUploadButton = new System.Windows.Forms.Button();
            this.AddTimeComponent = new System.Windows.Forms.Button();
            this.AddCalendarButton = new System.Windows.Forms.Button();
            this.AddToggleButton = new System.Windows.Forms.Button();
            this.AddSaveAndCancelButtons = new System.Windows.Forms.Button();
            this.AddTextBoxButton = new System.Windows.Forms.Button();
            this.AddListBoxButton = new System.Windows.Forms.Button();
            this.AddLabelButton = new System.Windows.Forms.Button();
            this.AddGridButton = new System.Windows.Forms.Button();
            this.AddImageButton = new System.Windows.Forms.Button();
            this.AddCheckBox = new System.Windows.Forms.Button();
            this.AddComboBoxbutton = new System.Windows.Forms.Button();
            this.HiddenButton = new System.Windows.Forms.Button();
            this.OutputFileControl = new DataJuggler.Win.Controls.LabelTextBoxControl();
            this.OutputFolderSelector = new DataJuggler.Win.Controls.LabelTextBoxBrowserControl();
            this.NamespaceControl = new DataJuggler.Win.Controls.LabelTextBoxControl();
            this.RazorFilePicker = new DataJuggler.Win.Controls.LabelTextBoxBrowserControl();
            this.FontComboBox = new DataJuggler.Win.Controls.LabelComboBoxControl();
            this.ObjectNameControl = new DataJuggler.Win.Controls.LabelTextBoxControl();
            this.ClassNameControl = new DataJuggler.Win.Controls.LabelTextBoxControl();
            this.ListNameControl = new DataJuggler.Win.Controls.LabelTextBoxControl();
            this.VariableNameControl = new DataJuggler.Win.Controls.LabelTextBoxControl();
            this.CreateRowBuildButton = new System.Windows.Forms.Button();
            this.StatusLabel = new System.Windows.Forms.Label();
            this.StatusTimer = new System.Windows.Forms.Timer(this.components);
            this.AddImage = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // SlimCheckBox
            // 
            this.SlimCheckBox.AutoSize = true;
            this.SlimCheckBox.BackColor = System.Drawing.Color.Transparent;
            this.SlimCheckBox.Location = new System.Drawing.Point(200, 120);
            this.SlimCheckBox.Name = "SlimCheckBox";
            this.SlimCheckBox.Size = new System.Drawing.Size(123, 27);
            this.SlimCheckBox.TabIndex = 44;
            this.SlimCheckBox.TabStop = false;
            this.SlimCheckBox.Text = "Slim Version";
            this.SlimCheckBox.UseVisualStyleBackColor = false;
            // 
            // LabelLabelColor
            // 
            this.LabelLabelColor.AutoSize = true;
            this.LabelLabelColor.BackColor = System.Drawing.Color.Transparent;
            this.LabelLabelColor.ForeColor = System.Drawing.Color.White;
            this.LabelLabelColor.Location = new System.Drawing.Point(196, 48);
            this.LabelLabelColor.Name = "LabelLabelColor";
            this.LabelLabelColor.Size = new System.Drawing.Size(100, 23);
            this.LabelLabelColor.TabIndex = 42;
            this.LabelLabelColor.Text = "Label Color:";
            // 
            // CheckedListModeCheckBox
            // 
            this.CheckedListModeCheckBox.AutoSize = true;
            this.CheckedListModeCheckBox.BackColor = System.Drawing.Color.Transparent;
            this.CheckedListModeCheckBox.Location = new System.Drawing.Point(47, 200);
            this.CheckedListModeCheckBox.Name = "CheckedListModeCheckBox";
            this.CheckedListModeCheckBox.Size = new System.Drawing.Size(123, 27);
            this.CheckedListModeCheckBox.TabIndex = 29;
            this.CheckedListModeCheckBox.TabStop = false;
            this.CheckedListModeCheckBox.Text = "Checked List";
            this.CheckedListModeCheckBox.UseVisualStyleBackColor = false;
            // 
            // AddCheckedListBox
            // 
            this.AddCheckedListBox.BackColor = System.Drawing.Color.Transparent;
            this.AddCheckedListBox.BackgroundImage = global::Regionizer.ProjectResources.GlassButtonBlack;
            this.AddCheckedListBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.AddCheckedListBox.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.AddCheckedListBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AddCheckedListBox.Location = new System.Drawing.Point(30, 112);
            this.AddCheckedListBox.Name = "AddCheckedListBox";
            this.AddCheckedListBox.Size = new System.Drawing.Size(147, 36);
            this.AddCheckedListBox.TabIndex = 41;
            this.AddCheckedListBox.TabStop = false;
            this.AddCheckedListBox.Text = "Checked List Box";
            this.AddCheckedListBox.UseVisualStyleBackColor = false;
            this.AddCheckedListBox.Click += new System.EventHandler(this.AddCheckedListBox_Click);
            this.AddCheckedListBox.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.AddCheckedListBox.MouseLeave += new System.EventHandler(this.Button_MouseLeave);
            // 
            // FileUploadButton
            // 
            this.FileUploadButton.BackColor = System.Drawing.Color.Transparent;
            this.FileUploadButton.BackgroundImage = global::Regionizer.ProjectResources.GlassButtonBlack;
            this.FileUploadButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.FileUploadButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.FileUploadButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.FileUploadButton.Location = new System.Drawing.Point(30, 232);
            this.FileUploadButton.Name = "FileUploadButton";
            this.FileUploadButton.Size = new System.Drawing.Size(147, 36);
            this.FileUploadButton.TabIndex = 40;
            this.FileUploadButton.TabStop = false;
            this.FileUploadButton.Text = "File Upload";
            this.FileUploadButton.UseVisualStyleBackColor = false;
            this.FileUploadButton.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.FileUploadButton.MouseLeave += new System.EventHandler(this.Button_MouseLeave);
            // 
            // AddTimeComponent
            // 
            this.AddTimeComponent.BackColor = System.Drawing.Color.Transparent;
            this.AddTimeComponent.BackgroundImage = global::Regionizer.ProjectResources.GlassButtonBlack;
            this.AddTimeComponent.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.AddTimeComponent.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.AddTimeComponent.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AddTimeComponent.Location = new System.Drawing.Point(30, 614);
            this.AddTimeComponent.Name = "AddTimeComponent";
            this.AddTimeComponent.Size = new System.Drawing.Size(147, 36);
            this.AddTimeComponent.TabIndex = 39;
            this.AddTimeComponent.TabStop = false;
            this.AddTimeComponent.Text = "Time Picker";
            this.AddTimeComponent.UseVisualStyleBackColor = false;
            this.AddTimeComponent.Click += new System.EventHandler(this.AddTimeComponent_Click);
            this.AddTimeComponent.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.AddTimeComponent.MouseLeave += new System.EventHandler(this.Button_MouseLeave);
            // 
            // AddCalendarButton
            // 
            this.AddCalendarButton.BackColor = System.Drawing.Color.Transparent;
            this.AddCalendarButton.BackgroundImage = global::Regionizer.ProjectResources.GlassButtonBlack;
            this.AddCalendarButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.AddCalendarButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.AddCalendarButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AddCalendarButton.Location = new System.Drawing.Point(30, 16);
            this.AddCalendarButton.Name = "AddCalendarButton";
            this.AddCalendarButton.Size = new System.Drawing.Size(147, 36);
            this.AddCalendarButton.TabIndex = 38;
            this.AddCalendarButton.TabStop = false;
            this.AddCalendarButton.Text = "Calendar";
            this.AddCalendarButton.UseVisualStyleBackColor = false;
            this.AddCalendarButton.Click += new System.EventHandler(this.AddCalendarButton_Click);
            this.AddCalendarButton.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.AddCalendarButton.MouseLeave += new System.EventHandler(this.Button_MouseLeave);
            // 
            // AddToggleButton
            // 
            this.AddToggleButton.BackColor = System.Drawing.Color.Transparent;
            this.AddToggleButton.BackgroundImage = global::Regionizer.ProjectResources.GlassButtonBlack;
            this.AddToggleButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.AddToggleButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.AddToggleButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AddToggleButton.Location = new System.Drawing.Point(30, 662);
            this.AddToggleButton.Name = "AddToggleButton";
            this.AddToggleButton.Size = new System.Drawing.Size(147, 36);
            this.AddToggleButton.TabIndex = 37;
            this.AddToggleButton.TabStop = false;
            this.AddToggleButton.Text = "Toggle";
            this.AddToggleButton.UseVisualStyleBackColor = false;
            this.AddToggleButton.Click += new System.EventHandler(this.AddToggleButton_Click);
            this.AddToggleButton.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.AddToggleButton.MouseLeave += new System.EventHandler(this.Button_MouseLeave);
            // 
            // AddSaveAndCancelButtons
            // 
            this.AddSaveAndCancelButtons.BackColor = System.Drawing.Color.Transparent;
            this.AddSaveAndCancelButtons.BackgroundImage = global::Regionizer.ProjectResources.GlassButtonBlack;
            this.AddSaveAndCancelButtons.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.AddSaveAndCancelButtons.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.AddSaveAndCancelButtons.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AddSaveAndCancelButtons.Location = new System.Drawing.Point(30, 518);
            this.AddSaveAndCancelButtons.Name = "AddSaveAndCancelButtons";
            this.AddSaveAndCancelButtons.Size = new System.Drawing.Size(147, 36);
            this.AddSaveAndCancelButtons.TabIndex = 36;
            this.AddSaveAndCancelButtons.TabStop = false;
            this.AddSaveAndCancelButtons.Text = "Save && Cancel";
            this.AddSaveAndCancelButtons.UseVisualStyleBackColor = false;
            this.AddSaveAndCancelButtons.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.AddSaveAndCancelButtons.MouseLeave += new System.EventHandler(this.Button_MouseLeave);
            // 
            // AddTextBoxButton
            // 
            this.AddTextBoxButton.BackColor = System.Drawing.Color.Transparent;
            this.AddTextBoxButton.BackgroundImage = global::Regionizer.ProjectResources.GlassButtonBlack;
            this.AddTextBoxButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.AddTextBoxButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.AddTextBoxButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AddTextBoxButton.Location = new System.Drawing.Point(30, 566);
            this.AddTextBoxButton.Name = "AddTextBoxButton";
            this.AddTextBoxButton.Size = new System.Drawing.Size(147, 36);
            this.AddTextBoxButton.TabIndex = 35;
            this.AddTextBoxButton.TabStop = false;
            this.AddTextBoxButton.Text = "Text Box";
            this.AddTextBoxButton.UseVisualStyleBackColor = false;
            this.AddTextBoxButton.Click += new System.EventHandler(this.AddTextBoxButton_Click);
            this.AddTextBoxButton.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.AddTextBoxButton.MouseLeave += new System.EventHandler(this.Button_MouseLeave);
            // 
            // AddListBoxButton
            // 
            this.AddListBoxButton.BackColor = System.Drawing.Color.Transparent;
            this.AddListBoxButton.BackgroundImage = global::Regionizer.ProjectResources.GlassButtonBlack;
            this.AddListBoxButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.AddListBoxButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.AddListBoxButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AddListBoxButton.Location = new System.Drawing.Point(30, 470);
            this.AddListBoxButton.Name = "AddListBoxButton";
            this.AddListBoxButton.Size = new System.Drawing.Size(147, 36);
            this.AddListBoxButton.TabIndex = 34;
            this.AddListBoxButton.TabStop = false;
            this.AddListBoxButton.Text = "List Box";
            this.AddListBoxButton.UseVisualStyleBackColor = false;
            this.AddListBoxButton.Click += new System.EventHandler(this.AddListBoxButton_Click);
            this.AddListBoxButton.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.AddListBoxButton.MouseLeave += new System.EventHandler(this.Button_MouseLeave);
            // 
            // AddLabelButton
            // 
            this.AddLabelButton.BackColor = System.Drawing.Color.Transparent;
            this.AddLabelButton.BackgroundImage = global::Regionizer.ProjectResources.GlassButtonBlack;
            this.AddLabelButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.AddLabelButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.AddLabelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AddLabelButton.Location = new System.Drawing.Point(30, 422);
            this.AddLabelButton.Name = "AddLabelButton";
            this.AddLabelButton.Size = new System.Drawing.Size(147, 36);
            this.AddLabelButton.TabIndex = 33;
            this.AddLabelButton.TabStop = false;
            this.AddLabelButton.Text = "Label";
            this.AddLabelButton.UseVisualStyleBackColor = false;
            this.AddLabelButton.Click += new System.EventHandler(this.AddLabelButton_Click);
            this.AddLabelButton.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.AddLabelButton.MouseLeave += new System.EventHandler(this.Button_MouseLeave);
            // 
            // AddGridButton
            // 
            this.AddGridButton.BackColor = System.Drawing.Color.Transparent;
            this.AddGridButton.BackgroundImage = global::Regionizer.ProjectResources.GlassButtonBlack;
            this.AddGridButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.AddGridButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.AddGridButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AddGridButton.Location = new System.Drawing.Point(30, 280);
            this.AddGridButton.Name = "AddGridButton";
            this.AddGridButton.Size = new System.Drawing.Size(147, 36);
            this.AddGridButton.TabIndex = 32;
            this.AddGridButton.TabStop = false;
            this.AddGridButton.Text = "Grid";
            this.AddGridButton.UseVisualStyleBackColor = false;
            this.AddGridButton.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.AddGridButton.MouseLeave += new System.EventHandler(this.Button_MouseLeave);
            // 
            // AddImageButton
            // 
            this.AddImageButton.BackColor = System.Drawing.Color.Transparent;
            this.AddImageButton.BackgroundImage = global::Regionizer.ProjectResources.GlassButtonBlack;
            this.AddImageButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.AddImageButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.AddImageButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AddImageButton.Location = new System.Drawing.Point(30, 374);
            this.AddImageButton.Name = "AddImageButton";
            this.AddImageButton.Size = new System.Drawing.Size(147, 36);
            this.AddImageButton.TabIndex = 31;
            this.AddImageButton.TabStop = false;
            this.AddImageButton.Text = "Image Button";
            this.AddImageButton.UseVisualStyleBackColor = false;
            this.AddImageButton.Click += new System.EventHandler(this.AddImageButton_Click);
            this.AddImageButton.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.AddImageButton.MouseLeave += new System.EventHandler(this.Button_MouseLeave);
            // 
            // AddCheckBox
            // 
            this.AddCheckBox.BackColor = System.Drawing.Color.Transparent;
            this.AddCheckBox.BackgroundImage = global::Regionizer.ProjectResources.GlassButtonBlack;
            this.AddCheckBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.AddCheckBox.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.AddCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AddCheckBox.Location = new System.Drawing.Point(30, 64);
            this.AddCheckBox.Name = "AddCheckBox";
            this.AddCheckBox.Size = new System.Drawing.Size(147, 36);
            this.AddCheckBox.TabIndex = 30;
            this.AddCheckBox.TabStop = false;
            this.AddCheckBox.Text = "Check Box";
            this.AddCheckBox.UseVisualStyleBackColor = false;
            this.AddCheckBox.Click += new System.EventHandler(this.AddCheckBox_Click);
            this.AddCheckBox.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.AddCheckBox.MouseLeave += new System.EventHandler(this.Button_MouseLeave);
            // 
            // AddComboBoxbutton
            // 
            this.AddComboBoxbutton.BackColor = System.Drawing.Color.Transparent;
            this.AddComboBoxbutton.BackgroundImage = global::Regionizer.ProjectResources.GlassButtonBlack;
            this.AddComboBoxbutton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.AddComboBoxbutton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.AddComboBoxbutton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AddComboBoxbutton.Location = new System.Drawing.Point(30, 160);
            this.AddComboBoxbutton.Name = "AddComboBoxbutton";
            this.AddComboBoxbutton.Size = new System.Drawing.Size(147, 36);
            this.AddComboBoxbutton.TabIndex = 28;
            this.AddComboBoxbutton.TabStop = false;
            this.AddComboBoxbutton.Text = "Combo Box";
            this.AddComboBoxbutton.UseVisualStyleBackColor = false;
            this.AddComboBoxbutton.Click += new System.EventHandler(this.AddComboBoxbutton_Click);
            this.AddComboBoxbutton.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.AddComboBoxbutton.MouseLeave += new System.EventHandler(this.Button_MouseLeave);
            // 
            // HiddenButton
            // 
            this.HiddenButton.BackColor = System.Drawing.Color.Transparent;
            this.HiddenButton.BackgroundImage = global::Regionizer.ProjectResources.GlassButtonBlack;
            this.HiddenButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.HiddenButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.HiddenButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.HiddenButton.Location = new System.Drawing.Point(-1000, 655);
            this.HiddenButton.Name = "HiddenButton";
            this.HiddenButton.Size = new System.Drawing.Size(147, 42);
            this.HiddenButton.TabIndex = 49;
            this.HiddenButton.TabStop = false;
            this.HiddenButton.Text = "Hidden";
            this.HiddenButton.UseVisualStyleBackColor = false;
            // 
            // OutputFileControl
            // 
            this.OutputFileControl.BackColor = System.Drawing.Color.Transparent;
            this.OutputFileControl.BottomMargin = 0;
            this.OutputFileControl.Editable = true;
            this.OutputFileControl.Encrypted = false;
            this.OutputFileControl.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OutputFileControl.LabelBottomMargin = 0;
            this.OutputFileControl.LabelColor = System.Drawing.Color.White;
            this.OutputFileControl.LabelFont = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold);
            this.OutputFileControl.LabelText = "Output File:";
            this.OutputFileControl.LabelTopMargin = 0;
            this.OutputFileControl.LabelWidth = 120;
            this.OutputFileControl.Location = new System.Drawing.Point(183, 348);
            this.OutputFileControl.MultiLine = false;
            this.OutputFileControl.Name = "OutputFileControl";
            this.OutputFileControl.OnTextChangedListener = null;
            this.OutputFileControl.PasswordMode = false;
            this.OutputFileControl.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.OutputFileControl.Size = new System.Drawing.Size(368, 32);
            this.OutputFileControl.TabIndex = 104;
            this.OutputFileControl.TextBoxBottomMargin = 0;
            this.OutputFileControl.TextBoxDisabledColor = System.Drawing.Color.LightGray;
            this.OutputFileControl.TextBoxEditableColor = System.Drawing.Color.White;
            this.OutputFileControl.TextBoxFont = new System.Drawing.Font("Verdana", 12F);
            this.OutputFileControl.TextBoxTopMargin = 0;
            // 
            // OutputFolderSelector
            // 
            this.OutputFolderSelector.BackColor = System.Drawing.Color.Transparent;
            this.OutputFolderSelector.BrowseButtonTextColor = System.Drawing.Color.Empty;
            this.OutputFolderSelector.BrowseType = DataJuggler.Win.Controls.Enumerations.BrowseTypeEnum.Folder;
            this.OutputFolderSelector.ButtonImage = ((System.Drawing.Image)(resources.GetObject("OutputFolderSelector.ButtonImage")));
            this.OutputFolderSelector.ButtonWidth = 48;
            this.OutputFolderSelector.DisabledLabelColor = System.Drawing.Color.LightGray;
            this.OutputFolderSelector.Editable = false;
            this.OutputFolderSelector.EnabledLabelColor = System.Drawing.Color.White;
            this.OutputFolderSelector.Filter = null;
            this.OutputFolderSelector.Font = new System.Drawing.Font("Verdana", 12F);
            this.OutputFolderSelector.HideBrowseButton = false;
            this.OutputFolderSelector.LabelBottomMargin = 0;
            this.OutputFolderSelector.LabelColor = System.Drawing.Color.White;
            this.OutputFolderSelector.LabelFont = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold);
            this.OutputFolderSelector.LabelText = "Output Dir:";
            this.OutputFolderSelector.LabelTopMargin = 0;
            this.OutputFolderSelector.LabelWidth = 120;
            this.OutputFolderSelector.Location = new System.Drawing.Point(183, 305);
            this.OutputFolderSelector.Name = "OutputFolderSelector";
            this.OutputFolderSelector.OnTextChangedListener = null;
            this.OutputFolderSelector.OpenCallback = null;
            this.OutputFolderSelector.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.OutputFolderSelector.SelectedPath = null;
            this.OutputFolderSelector.Size = new System.Drawing.Size(412, 32);
            this.OutputFolderSelector.StartPath = null;
            this.OutputFolderSelector.TabIndex = 103;
            this.OutputFolderSelector.TextBoxBottomMargin = 0;
            this.OutputFolderSelector.TextBoxDisabledColor = System.Drawing.Color.Empty;
            this.OutputFolderSelector.TextBoxEditableColor = System.Drawing.Color.Empty;
            this.OutputFolderSelector.TextBoxFont = new System.Drawing.Font("Verdana", 12F);
            this.OutputFolderSelector.TextBoxTopMargin = 0;
            this.OutputFolderSelector.Theme = DataJuggler.Win.Controls.Enumerations.ThemeEnum.Dark;
            // 
            // NamespaceControl
            // 
            this.NamespaceControl.BackColor = System.Drawing.Color.Transparent;
            this.NamespaceControl.BottomMargin = 0;
            this.NamespaceControl.Editable = true;
            this.NamespaceControl.Encrypted = false;
            this.NamespaceControl.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NamespaceControl.LabelBottomMargin = 0;
            this.NamespaceControl.LabelColor = System.Drawing.Color.White;
            this.NamespaceControl.LabelFont = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold);
            this.NamespaceControl.LabelText = "Namespace:";
            this.NamespaceControl.LabelTopMargin = 0;
            this.NamespaceControl.LabelWidth = 120;
            this.NamespaceControl.Location = new System.Drawing.Point(183, 219);
            this.NamespaceControl.MultiLine = false;
            this.NamespaceControl.Name = "NamespaceControl";
            this.NamespaceControl.OnTextChangedListener = null;
            this.NamespaceControl.PasswordMode = false;
            this.NamespaceControl.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.NamespaceControl.Size = new System.Drawing.Size(368, 32);
            this.NamespaceControl.TabIndex = 101;
            this.NamespaceControl.TextBoxBottomMargin = 0;
            this.NamespaceControl.TextBoxDisabledColor = System.Drawing.Color.LightGray;
            this.NamespaceControl.TextBoxEditableColor = System.Drawing.Color.White;
            this.NamespaceControl.TextBoxFont = new System.Drawing.Font("Verdana", 12F);
            this.NamespaceControl.TextBoxTopMargin = 0;
            // 
            // RazorFilePicker
            // 
            this.RazorFilePicker.BackColor = System.Drawing.Color.Transparent;
            this.RazorFilePicker.BrowseButtonTextColor = System.Drawing.Color.Empty;
            this.RazorFilePicker.BrowseType = DataJuggler.Win.Controls.Enumerations.BrowseTypeEnum.File;
            this.RazorFilePicker.ButtonImage = ((System.Drawing.Image)(resources.GetObject("RazorFilePicker.ButtonImage")));
            this.RazorFilePicker.ButtonWidth = 48;
            this.RazorFilePicker.DisabledLabelColor = System.Drawing.Color.LightGray;
            this.RazorFilePicker.Editable = false;
            this.RazorFilePicker.EnabledLabelColor = System.Drawing.Color.White;
            this.RazorFilePicker.Filter = null;
            this.RazorFilePicker.Font = new System.Drawing.Font("Verdana", 12F);
            this.RazorFilePicker.HideBrowseButton = false;
            this.RazorFilePicker.LabelBottomMargin = 0;
            this.RazorFilePicker.LabelColor = System.Drawing.Color.White;
            this.RazorFilePicker.LabelFont = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold);
            this.RazorFilePicker.LabelText = "Razor File:";
            this.RazorFilePicker.LabelTopMargin = 0;
            this.RazorFilePicker.LabelWidth = 120;
            this.RazorFilePicker.Location = new System.Drawing.Point(183, 176);
            this.RazorFilePicker.Name = "RazorFilePicker";
            this.RazorFilePicker.OnTextChangedListener = null;
            this.RazorFilePicker.OpenCallback = null;
            this.RazorFilePicker.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.RazorFilePicker.SelectedPath = null;
            this.RazorFilePicker.Size = new System.Drawing.Size(412, 32);
            this.RazorFilePicker.StartPath = null;
            this.RazorFilePicker.TabIndex = 100;
            this.RazorFilePicker.TextBoxBottomMargin = 0;
            this.RazorFilePicker.TextBoxDisabledColor = System.Drawing.Color.Empty;
            this.RazorFilePicker.TextBoxEditableColor = System.Drawing.Color.Empty;
            this.RazorFilePicker.TextBoxFont = new System.Drawing.Font("Verdana", 12F);
            this.RazorFilePicker.TextBoxTopMargin = 0;
            this.RazorFilePicker.Theme = DataJuggler.Win.Controls.Enumerations.ThemeEnum.Black;
            // 
            // FontComboBox
            // 
            this.FontComboBox.BackColor = System.Drawing.Color.Transparent;
            this.FontComboBox.ComboBoxLeftMargin = 1;
            this.FontComboBox.ComboBoxText = "";
            this.FontComboBox.ComoboBoxFont = null;
            this.FontComboBox.Editable = true;
            this.FontComboBox.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FontComboBox.HideLabel = false;
            this.FontComboBox.LabelBottomMargin = 0;
            this.FontComboBox.LabelColor = System.Drawing.Color.White;
            this.FontComboBox.LabelFont = null;
            this.FontComboBox.LabelText = null;
            this.FontComboBox.LabelTopMargin = 0;
            this.FontComboBox.LabelWidth = 0;
            this.FontComboBox.List = null;
            this.FontComboBox.Location = new System.Drawing.Point(200, 74);
            this.FontComboBox.Name = "FontComboBox";
            this.FontComboBox.SelectedIndex = -1;
            this.FontComboBox.SelectedIndexListener = null;
            this.FontComboBox.Size = new System.Drawing.Size(181, 28);
            this.FontComboBox.Sorted = true;
            this.FontComboBox.Source = null;
            this.FontComboBox.TabIndex = 43;
            // 
            // ObjectNameControl
            // 
            this.ObjectNameControl.BackColor = System.Drawing.Color.Transparent;
            this.ObjectNameControl.BottomMargin = 0;
            this.ObjectNameControl.Editable = true;
            this.ObjectNameControl.Encrypted = false;
            this.ObjectNameControl.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ObjectNameControl.LabelBottomMargin = 0;
            this.ObjectNameControl.LabelColor = System.Drawing.Color.White;
            this.ObjectNameControl.LabelFont = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold);
            this.ObjectNameControl.LabelText = "Obj Name:";
            this.ObjectNameControl.LabelTopMargin = 0;
            this.ObjectNameControl.LabelWidth = 120;
            this.ObjectNameControl.Location = new System.Drawing.Point(183, 262);
            this.ObjectNameControl.MultiLine = false;
            this.ObjectNameControl.Name = "ObjectNameControl";
            this.ObjectNameControl.OnTextChangedListener = null;
            this.ObjectNameControl.PasswordMode = false;
            this.ObjectNameControl.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.ObjectNameControl.Size = new System.Drawing.Size(368, 32);
            this.ObjectNameControl.TabIndex = 102;
            this.ObjectNameControl.TextBoxBottomMargin = 0;
            this.ObjectNameControl.TextBoxDisabledColor = System.Drawing.Color.LightGray;
            this.ObjectNameControl.TextBoxEditableColor = System.Drawing.Color.White;
            this.ObjectNameControl.TextBoxFont = new System.Drawing.Font("Verdana", 12F);
            this.ObjectNameControl.TextBoxTopMargin = 0;
            // 
            // ClassNameControl
            // 
            this.ClassNameControl.BackColor = System.Drawing.Color.Transparent;
            this.ClassNameControl.BottomMargin = 0;
            this.ClassNameControl.Editable = true;
            this.ClassNameControl.Encrypted = false;
            this.ClassNameControl.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ClassNameControl.LabelBottomMargin = 0;
            this.ClassNameControl.LabelColor = System.Drawing.Color.White;
            this.ClassNameControl.LabelFont = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold);
            this.ClassNameControl.LabelText = "Class Name:";
            this.ClassNameControl.LabelTopMargin = 0;
            this.ClassNameControl.LabelWidth = 120;
            this.ClassNameControl.Location = new System.Drawing.Point(183, 391);
            this.ClassNameControl.MultiLine = false;
            this.ClassNameControl.Name = "ClassNameControl";
            this.ClassNameControl.OnTextChangedListener = null;
            this.ClassNameControl.PasswordMode = false;
            this.ClassNameControl.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.ClassNameControl.Size = new System.Drawing.Size(368, 32);
            this.ClassNameControl.TabIndex = 105;
            this.ClassNameControl.TextBoxBottomMargin = 0;
            this.ClassNameControl.TextBoxDisabledColor = System.Drawing.Color.LightGray;
            this.ClassNameControl.TextBoxEditableColor = System.Drawing.Color.White;
            this.ClassNameControl.TextBoxFont = new System.Drawing.Font("Verdana", 12F);
            this.ClassNameControl.TextBoxTopMargin = 0;
            // 
            // ListNameControl
            // 
            this.ListNameControl.BackColor = System.Drawing.Color.Transparent;
            this.ListNameControl.BottomMargin = 0;
            this.ListNameControl.Editable = true;
            this.ListNameControl.Encrypted = false;
            this.ListNameControl.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ListNameControl.LabelBottomMargin = 0;
            this.ListNameControl.LabelColor = System.Drawing.Color.White;
            this.ListNameControl.LabelFont = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold);
            this.ListNameControl.LabelText = "List Name:";
            this.ListNameControl.LabelTopMargin = 0;
            this.ListNameControl.LabelWidth = 120;
            this.ListNameControl.Location = new System.Drawing.Point(183, 434);
            this.ListNameControl.MultiLine = false;
            this.ListNameControl.Name = "ListNameControl";
            this.ListNameControl.OnTextChangedListener = null;
            this.ListNameControl.PasswordMode = false;
            this.ListNameControl.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.ListNameControl.Size = new System.Drawing.Size(368, 32);
            this.ListNameControl.TabIndex = 106;
            this.ListNameControl.TextBoxBottomMargin = 0;
            this.ListNameControl.TextBoxDisabledColor = System.Drawing.Color.LightGray;
            this.ListNameControl.TextBoxEditableColor = System.Drawing.Color.White;
            this.ListNameControl.TextBoxFont = new System.Drawing.Font("Verdana", 12F);
            this.ListNameControl.TextBoxTopMargin = 0;
            // 
            // VariableNameControl
            // 
            this.VariableNameControl.BackColor = System.Drawing.Color.Transparent;
            this.VariableNameControl.BottomMargin = 0;
            this.VariableNameControl.Editable = true;
            this.VariableNameControl.Encrypted = false;
            this.VariableNameControl.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VariableNameControl.LabelBottomMargin = 0;
            this.VariableNameControl.LabelColor = System.Drawing.Color.White;
            this.VariableNameControl.LabelFont = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold);
            this.VariableNameControl.LabelText = "Var Name:";
            this.VariableNameControl.LabelTopMargin = 0;
            this.VariableNameControl.LabelWidth = 120;
            this.VariableNameControl.Location = new System.Drawing.Point(183, 477);
            this.VariableNameControl.MultiLine = false;
            this.VariableNameControl.Name = "VariableNameControl";
            this.VariableNameControl.OnTextChangedListener = null;
            this.VariableNameControl.PasswordMode = false;
            this.VariableNameControl.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.VariableNameControl.Size = new System.Drawing.Size(368, 32);
            this.VariableNameControl.TabIndex = 107;
            this.VariableNameControl.TextBoxBottomMargin = 0;
            this.VariableNameControl.TextBoxDisabledColor = System.Drawing.Color.LightGray;
            this.VariableNameControl.TextBoxEditableColor = System.Drawing.Color.White;
            this.VariableNameControl.TextBoxFont = new System.Drawing.Font("Verdana", 12F);
            this.VariableNameControl.TextBoxTopMargin = 0;
            // 
            // CreateRowBuildButton
            // 
            this.CreateRowBuildButton.BackColor = System.Drawing.Color.Transparent;
            this.CreateRowBuildButton.BackgroundImage = global::Regionizer.ProjectResources.GlassButtonBlack;
            this.CreateRowBuildButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CreateRowBuildButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.CreateRowBuildButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CreateRowBuildButton.Location = new System.Drawing.Point(346, 523);
            this.CreateRowBuildButton.Name = "CreateRowBuildButton";
            this.CreateRowBuildButton.Size = new System.Drawing.Size(205, 36);
            this.CreateRowBuildButton.TabIndex = 108;
            this.CreateRowBuildButton.TabStop = false;
            this.CreateRowBuildButton.Text = "Create Row Builder";
            this.CreateRowBuildButton.UseVisualStyleBackColor = false;
            this.CreateRowBuildButton.Click += new System.EventHandler(this.CreateRowBuildButton_Click);
            this.CreateRowBuildButton.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.CreateRowBuildButton.MouseLeave += new System.EventHandler(this.Button_MouseLeave);
            // 
            // StatusLabel
            // 
            this.StatusLabel.BackColor = System.Drawing.Color.Transparent;
            this.StatusLabel.ForeColor = System.Drawing.Color.White;
            this.StatusLabel.Location = new System.Drawing.Point(200, 665);
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(351, 23);
            this.StatusLabel.TabIndex = 56;
            this.StatusLabel.Text = "File Created!";
            this.StatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.StatusLabel.Visible = false;
            // 
            // StatusTimer
            // 
            this.StatusTimer.Interval = 5000;
            this.StatusTimer.Tick += new System.EventHandler(this.StatusTimer_Tick);
            // 
            // AddImage
            // 
            this.AddImage.BackColor = System.Drawing.Color.Transparent;
            this.AddImage.BackgroundImage = global::Regionizer.ProjectResources.GlassButtonBlack;
            this.AddImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.AddImage.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.AddImage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AddImage.Location = new System.Drawing.Point(30, 328);
            this.AddImage.Name = "AddImage";
            this.AddImage.Size = new System.Drawing.Size(147, 36);
            this.AddImage.TabIndex = 109;
            this.AddImage.TabStop = false;
            this.AddImage.Text = "Image";
            this.AddImage.UseVisualStyleBackColor = false;
            this.AddImage.Click += new System.EventHandler(this.AddImage_Click);
            this.AddImage.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.AddImage.MouseLeave += new System.EventHandler(this.Button_MouseLeave);
            // 
            // BlazorComponentsForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackgroundImage = global::Regionizer.ProjectResources.BlazorComponentFormBackgrounds;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(607, 723);
            this.Controls.Add(this.AddImage);
            this.Controls.Add(this.StatusLabel);
            this.Controls.Add(this.CreateRowBuildButton);
            this.Controls.Add(this.VariableNameControl);
            this.Controls.Add(this.ListNameControl);
            this.Controls.Add(this.ClassNameControl);
            this.Controls.Add(this.ObjectNameControl);
            this.Controls.Add(this.HiddenButton);
            this.Controls.Add(this.OutputFileControl);
            this.Controls.Add(this.OutputFolderSelector);
            this.Controls.Add(this.NamespaceControl);
            this.Controls.Add(this.RazorFilePicker);
            this.Controls.Add(this.SlimCheckBox);
            this.Controls.Add(this.FontComboBox);
            this.Controls.Add(this.LabelLabelColor);
            this.Controls.Add(this.CheckedListModeCheckBox);
            this.Controls.Add(this.AddCheckedListBox);
            this.Controls.Add(this.FileUploadButton);
            this.Controls.Add(this.AddTimeComponent);
            this.Controls.Add(this.AddCalendarButton);
            this.Controls.Add(this.AddToggleButton);
            this.Controls.Add(this.AddSaveAndCancelButtons);
            this.Controls.Add(this.AddTextBoxButton);
            this.Controls.Add(this.AddListBoxButton);
            this.Controls.Add(this.AddLabelButton);
            this.Controls.Add(this.AddGridButton);
            this.Controls.Add(this.AddImageButton);
            this.Controls.Add(this.AddCheckBox);
            this.Controls.Add(this.AddComboBoxbutton);
            this.Font = new System.Drawing.Font("Calibri", 14F);
            this.ForeColor = System.Drawing.Color.White;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "BlazorComponentsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "BlazorComponentsForm";
            this.TopMost = true;
            this.MouseEnter += new System.EventHandler(this.Button_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.Button_MouseLeave);
            this.ResumeLayout(false);
            this.PerformLayout();

            }
        #endregion

        #endregion

        private Win.Controls.LabelTextBoxControl ObjectNameControl;
        private Win.Controls.LabelTextBoxControl ClassNameControl;
        private Win.Controls.LabelTextBoxControl ListNameControl;
        private Win.Controls.LabelTextBoxControl VariableNameControl;
        private System.Windows.Forms.Button CreateRowBuildButton;
        private System.Windows.Forms.Label StatusLabel;
        private System.Windows.Forms.Timer StatusTimer;
        private System.Windows.Forms.Button AddImage;
    }
    #endregion

}
