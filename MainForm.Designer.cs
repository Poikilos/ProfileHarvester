/*
 * Created by SharpDevelop.
 * User: Jakeg7505
 * Date: 10/27/2011
 * Time: 7:51 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace ProfileHarvester
{
    partial class MainForm
    {
        /// <summary>
        /// Designer variable used to keep track of non-visual components.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        
        /// <summary>
        /// Disposes resources used by the form.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                if (components != null) {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }
        
        /// <summary>
        /// This method is required for Windows Forms designer support.
        /// Do not change the method contents inside the source code editor. The Forms designer might
        /// not be able to load this method if it was changed manually.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyLocalDocumentsToNetworkDriveIfExistsrecursiveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.moveLocalDocumentsToNetworkDriveIfExistsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.collectGameFilesToCurrentUserHAsTimeUserNameComputerNameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiPath = new System.Windows.Forms.ToolStripMenuItem();
            this.clearTmpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.moveTmpFilesToTheOwnersTmpOnTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.moveLocalTempToNetworkDriveTempToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.profilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fCAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.silverToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setPasswordToStringInInputBoxToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tbStatus = new System.Windows.Forms.TextBox();
            this.lblBusy = new System.Windows.Forms.Label();
            this.lblLocation = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.remoteTempLabel = new System.Windows.Forms.Label();
            this.remoteProfilesLabel = new System.Windows.Forms.Label();
            this.localProfilesLabel = new System.Windows.Forms.Label();
            this.tbLocation = new System.Windows.Forms.TextBox();
            this.lblInput0 = new System.Windows.Forms.Label();
            this.tbInput0 = new System.Windows.Forms.TextBox();
            this.readOnlyCheckBox = new System.Windows.Forms.CheckBox();
            this.localProfilesTextBox = new System.Windows.Forms.TextBox();
            this.remoteProfilesTextBox = new System.Windows.Forms.TextBox();
            this.localDocsTextBox = new System.Windows.Forms.TextBox();
            this.localDocsLabel = new System.Windows.Forms.Label();
            this.doNotDetectOtherRemoteRootsCheckBox = new System.Windows.Forms.CheckBox();
            this.remoteDocsTextBox = new System.Windows.Forms.TextBox();
            this.remoteDocsLabel = new System.Windows.Forms.Label();
            this.createIfNotOnDestCheckBox = new System.Windows.Forms.CheckBox();
            this.processPublicProfileCheckBox = new System.Windows.Forms.CheckBox();
            this.detectComputerNameCheckBox = new System.Windows.Forms.CheckBox();
            this.remoteTempTextBox = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.lbMain = new System.Windows.Forms.ListBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                                    this.fileToolStripMenuItem,
                                    this.profilesToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(8, 3, 0, 3);
            this.menuStrip1.Size = new System.Drawing.Size(976, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.MenuStrip1ItemClicked);
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                                    this.copyLocalDocumentsToNetworkDriveIfExistsrecursiveToolStripMenuItem,
                                    this.moveLocalDocumentsToNetworkDriveIfExistsToolStripMenuItem,
                                    this.collectGameFilesToCurrentUserHAsTimeUserNameComputerNameToolStripMenuItem,
                                    this.tsmiPath,
                                    this.clearTmpToolStripMenuItem,
                                    this.moveTmpFilesToTheOwnersTmpOnTToolStripMenuItem,
                                    this.moveLocalTempToNetworkDriveTempToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 19);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // copyLocalDocumentsToNetworkDriveIfExistsrecursiveToolStripMenuItem
            // 
            this.copyLocalDocumentsToNetworkDriveIfExistsrecursiveToolStripMenuItem.Name = "copyLocalDocumentsToNetworkDriveIfExistsrecursiveToolStripMenuItem";
            this.copyLocalDocumentsToNetworkDriveIfExistsrecursiveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Shift) 
                                    | System.Windows.Forms.Keys.B)));
            this.copyLocalDocumentsToNetworkDriveIfExistsrecursiveToolStripMenuItem.Size = new System.Drawing.Size(567, 22);
            this.copyLocalDocumentsToNetworkDriveIfExistsrecursiveToolStripMenuItem.Text = "Copy Local Documents to Network Drive If Exists (recursive)";
            this.copyLocalDocumentsToNetworkDriveIfExistsrecursiveToolStripMenuItem.Click += new System.EventHandler(this.CopyLocalDocumentsToNetworkDriveIfExistsrecursiveToolStripMenuItemClick);
            // 
            // moveLocalDocumentsToNetworkDriveIfExistsToolStripMenuItem
            // 
            this.moveLocalDocumentsToNetworkDriveIfExistsToolStripMenuItem.Name = "moveLocalDocumentsToNetworkDriveIfExistsToolStripMenuItem";
            this.moveLocalDocumentsToNetworkDriveIfExistsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Shift) 
                                    | System.Windows.Forms.Keys.D)));
            this.moveLocalDocumentsToNetworkDriveIfExistsToolStripMenuItem.Size = new System.Drawing.Size(567, 22);
            this.moveLocalDocumentsToNetworkDriveIfExistsToolStripMenuItem.Text = "&Move Local Documents To Network Drive If Exists (recursive)";
            this.moveLocalDocumentsToNetworkDriveIfExistsToolStripMenuItem.Click += new System.EventHandler(this.MoveLocalDocumentsToNetworkDriveIfExistsToolStripMenuItemClick);
            // 
            // collectGameFilesToCurrentUserHAsTimeUserNameComputerNameToolStripMenuItem
            // 
            this.collectGameFilesToCurrentUserHAsTimeUserNameComputerNameToolStripMenuItem.Name = "collectGameFilesToCurrentUserHAsTimeUserNameComputerNameToolStripMenuItem";
            this.collectGameFilesToCurrentUserHAsTimeUserNameComputerNameToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Shift) 
                                    | System.Windows.Forms.Keys.L)));
            this.collectGameFilesToCurrentUserHAsTimeUserNameComputerNameToolStripMenuItem.Size = new System.Drawing.Size(567, 22);
            this.collectGameFilesToCurrentUserHAsTimeUserNameComputerNameToolStripMenuItem.Text = "Collect Lepidopterist data to Current User H as Time-UserName-ComputerName";
            this.collectGameFilesToCurrentUserHAsTimeUserNameComputerNameToolStripMenuItem.Click += new System.EventHandler(this.CollectGameFilesToCurrentUserHAsTimeUserNameComputerNameToolStripMenuItemClick);
            // 
            // tsmiPath
            // 
            this.tsmiPath.Name = "tsmiPath";
            this.tsmiPath.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Shift) 
                                    | System.Windows.Forms.Keys.P)));
            this.tsmiPath.Size = new System.Drawing.Size(567, 22);
            this.tsmiPath.Text = "Add Location to PATH permanently";
            this.tsmiPath.Click += new System.EventHandler(this.TsmiPathClick);
            // 
            // clearTmpToolStripMenuItem
            // 
            this.clearTmpToolStripMenuItem.Name = "clearTmpToolStripMenuItem";
            this.clearTmpToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Shift) 
                                    | System.Windows.Forms.Keys.C)));
            this.clearTmpToolStripMenuItem.Size = new System.Drawing.Size(567, 22);
            this.clearTmpToolStripMenuItem.Text = "Clear tmp";
            this.clearTmpToolStripMenuItem.Click += new System.EventHandler(this.ClearTmpToolStripMenuItemClick);
            // 
            // moveTmpFilesToTheOwnersTmpOnTToolStripMenuItem
            // 
            this.moveTmpFilesToTheOwnersTmpOnTToolStripMenuItem.Name = "moveTmpFilesToTheOwnersTmpOnTToolStripMenuItem";
            this.moveTmpFilesToTheOwnersTmpOnTToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Shift) 
                                    | System.Windows.Forms.Keys.O)));
            this.moveTmpFilesToTheOwnersTmpOnTToolStripMenuItem.Size = new System.Drawing.Size(567, 22);
            this.moveTmpFilesToTheOwnersTmpOnTToolStripMenuItem.Text = "Move tmp files to the owner\'s tmp on T";
            this.moveTmpFilesToTheOwnersTmpOnTToolStripMenuItem.Click += new System.EventHandler(this.MoveTmpFilesToTheOwnersTmpOnTToolStripMenuItemClick);
            // 
            // moveLocalTempToNetworkDriveTempToolStripMenuItem
            // 
            this.moveLocalTempToNetworkDriveTempToolStripMenuItem.Name = "moveLocalTempToNetworkDriveTempToolStripMenuItem";
            this.moveLocalTempToNetworkDriveTempToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Shift) 
                                    | System.Windows.Forms.Keys.T)));
            this.moveLocalTempToNetworkDriveTempToolStripMenuItem.Size = new System.Drawing.Size(567, 22);
            this.moveLocalTempToNetworkDriveTempToolStripMenuItem.Text = "Move Local Temp to Network Drive Temp";
            this.moveLocalTempToNetworkDriveTempToolStripMenuItem.Click += new System.EventHandler(this.MoveLocalTempToNetworkDriveTempToolStripMenuItemClick);
            // 
            // profilesToolStripMenuItem
            // 
            this.profilesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                                    this.fCAToolStripMenuItem,
                                    this.silverToolStripMenuItem,
                                    this.setPasswordToStringInInputBoxToolStripMenuItem});
            this.profilesToolStripMenuItem.Name = "profilesToolStripMenuItem";
            this.profilesToolStripMenuItem.Size = new System.Drawing.Size(58, 19);
            this.profilesToolStripMenuItem.Text = "P&rofiles";
            // 
            // fCAToolStripMenuItem
            // 
            this.fCAToolStripMenuItem.Name = "fCAToolStripMenuItem";
            this.fCAToolStripMenuItem.Size = new System.Drawing.Size(350, 22);
            this.fCAToolStripMenuItem.Text = "F&CA";
            this.fCAToolStripMenuItem.Click += new System.EventHandler(this.FCAToolStripMenuItemClick);
            // 
            // silverToolStripMenuItem
            // 
            this.silverToolStripMenuItem.Name = "silverToolStripMenuItem";
            this.silverToolStripMenuItem.Size = new System.Drawing.Size(350, 22);
            this.silverToolStripMenuItem.Text = "Sil&ver";
            this.silverToolStripMenuItem.Click += new System.EventHandler(this.SilverToolStripMenuItemClick);
            // 
            // setPasswordToStringInInputBoxToolStripMenuItem
            // 
            this.setPasswordToStringInInputBoxToolStripMenuItem.Name = "setPasswordToStringInInputBoxToolStripMenuItem";
            this.setPasswordToStringInInputBoxToolStripMenuItem.Size = new System.Drawing.Size(350, 22);
            this.setPasswordToStringInInputBoxToolStripMenuItem.Text = "Set Password to Custom Value and store for later use";
            this.setPasswordToStringInInputBoxToolStripMenuItem.Click += new System.EventHandler(this.SetPasswordToStringInInputBoxToolStripMenuItemClick);
            // 
            // tbStatus
            // 
            this.tbStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tbStatus.Location = new System.Drawing.Point(0, 566);
            this.tbStatus.Margin = new System.Windows.Forms.Padding(4);
            this.tbStatus.Name = "tbStatus";
            this.tbStatus.ReadOnly = true;
            this.tbStatus.Size = new System.Drawing.Size(976, 27);
            this.tbStatus.TabIndex = 1;
            // 
            // lblBusy
            // 
            this.lblBusy.AutoSize = true;
            this.lblBusy.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBusy.Location = new System.Drawing.Point(4, 251);
            this.lblBusy.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBusy.Name = "lblBusy";
            this.lblBusy.Size = new System.Drawing.Size(173, 37);
            this.lblBusy.TabIndex = 2;
            this.lblBusy.Text = "Working...";
            this.lblBusy.Visible = false;
            // 
            // lblLocation
            // 
            this.lblLocation.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblLocation.AutoSize = true;
            this.lblLocation.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLocation.Location = new System.Drawing.Point(87, 41);
            this.lblLocation.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(67, 19);
            this.lblLocation.TabIndex = 0;
            this.lblLocation.Text = "Location";
            this.lblLocation.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.remoteTempLabel, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.remoteProfilesLabel, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.localProfilesLabel, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.lblLocation, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tbLocation, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblInput0, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tbInput0, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.readOnlyCheckBox, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.localProfilesTextBox, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.remoteProfilesTextBox, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.localDocsTextBox, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.localDocsLabel, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.doNotDetectOtherRemoteRootsCheckBox, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.remoteDocsTextBox, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.remoteDocsLabel, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.createIfNotOnDestCheckBox, 2, 5);
            this.tableLayoutPanel1.Controls.Add(this.processPublicProfileCheckBox, 2, 4);
            this.tableLayoutPanel1.Controls.Add(this.detectComputerNameCheckBox, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.remoteTempTextBox, 1, 6);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(4, 4);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 7;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(968, 243);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // remoteTempLabel
            // 
            this.remoteTempLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.remoteTempLabel.AutoSize = true;
            this.remoteTempLabel.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.remoteTempLabel.Location = new System.Drawing.Point(4, 216);
            this.remoteTempLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.remoteTempLabel.Name = "remoteTempLabel";
            this.remoteTempLabel.Size = new System.Drawing.Size(150, 19);
            this.remoteTempLabel.TabIndex = 6;
            this.remoteTempLabel.Text = "RemoteProfilesTemp";
            this.remoteTempLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // remoteProfilesLabel
            // 
            this.remoteProfilesLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.remoteProfilesLabel.AutoSize = true;
            this.remoteProfilesLabel.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.remoteProfilesLabel.Location = new System.Drawing.Point(41, 111);
            this.remoteProfilesLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.remoteProfilesLabel.Name = "remoteProfilesLabel";
            this.remoteProfilesLabel.Size = new System.Drawing.Size(113, 19);
            this.remoteProfilesLabel.TabIndex = 5;
            this.remoteProfilesLabel.Text = "RemoteProfiles";
            this.remoteProfilesLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // localProfilesLabel
            // 
            this.localProfilesLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.localProfilesLabel.AutoSize = true;
            this.localProfilesLabel.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.localProfilesLabel.Location = new System.Drawing.Point(59, 76);
            this.localProfilesLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.localProfilesLabel.Name = "localProfilesLabel";
            this.localProfilesLabel.Size = new System.Drawing.Size(95, 19);
            this.localProfilesLabel.TabIndex = 4;
            this.localProfilesLabel.Text = "LocalProfiles";
            this.localProfilesLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.localProfilesLabel.Click += new System.EventHandler(this.ProfilesLabelClick);
            // 
            // tbLocation
            // 
            this.tbLocation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbLocation.Location = new System.Drawing.Point(162, 37);
            this.tbLocation.Margin = new System.Windows.Forms.Padding(4);
            this.tbLocation.Name = "tbLocation";
            this.tbLocation.Size = new System.Drawing.Size(556, 27);
            this.tbLocation.TabIndex = 1;
            this.tbLocation.Text = "C:\\utils\\kivy;C:\\utils\\kivy\\Python;C:\\utils\\kivy\\gstreamer\\bin;C:\\utils\\kivy\\MinG" +
            "W\\bin;";
            // 
            // lblInput0
            // 
            this.lblInput0.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblInput0.AutoSize = true;
            this.lblInput0.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInput0.Location = new System.Drawing.Point(56, 7);
            this.lblInput0.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblInput0.Name = "lblInput0";
            this.lblInput0.Size = new System.Drawing.Size(98, 19);
            this.lblInput0.TabIndex = 0;
            this.lblInput0.Text = "Custom Value";
            this.lblInput0.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblInput0.Click += new System.EventHandler(this.LblInput0Click);
            // 
            // tbInput0
            // 
            this.tbInput0.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbInput0.Location = new System.Drawing.Point(161, 3);
            this.tbInput0.Name = "tbInput0";
            this.tbInput0.PasswordChar = '●';
            this.tbInput0.Size = new System.Drawing.Size(558, 27);
            this.tbInput0.TabIndex = 2;
            // 
            // readOnlyCheckBox
            // 
            this.readOnlyCheckBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.readOnlyCheckBox.AutoSize = true;
            this.readOnlyCheckBox.Location = new System.Drawing.Point(725, 39);
            this.readOnlyCheckBox.Name = "readOnlyCheckBox";
            this.readOnlyCheckBox.Size = new System.Drawing.Size(91, 23);
            this.readOnlyCheckBox.TabIndex = 3;
            this.readOnlyCheckBox.Text = "ReadOnly";
            this.readOnlyCheckBox.UseVisualStyleBackColor = true;
            // 
            // localProfilesTextBox
            // 
            this.localProfilesTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.localProfilesTextBox.Location = new System.Drawing.Point(162, 72);
            this.localProfilesTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.localProfilesTextBox.Name = "localProfilesTextBox";
            this.localProfilesTextBox.Size = new System.Drawing.Size(556, 27);
            this.localProfilesTextBox.TabIndex = 1;
            this.localProfilesTextBox.TextChanged += new System.EventHandler(this.LocalProfilesTextBoxTextChanged);
            // 
            // remoteProfilesTextBox
            // 
            this.remoteProfilesTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.remoteProfilesTextBox.Location = new System.Drawing.Point(162, 107);
            this.remoteProfilesTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.remoteProfilesTextBox.Name = "remoteProfilesTextBox";
            this.remoteProfilesTextBox.Size = new System.Drawing.Size(556, 27);
            this.remoteProfilesTextBox.TabIndex = 1;
            this.remoteProfilesTextBox.TextChanged += new System.EventHandler(this.LocalProfilesTextBoxTextChanged);
            // 
            // localDocsTextBox
            // 
            this.localDocsTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.localDocsTextBox.Location = new System.Drawing.Point(162, 142);
            this.localDocsTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.localDocsTextBox.Name = "localDocsTextBox";
            this.localDocsTextBox.Size = new System.Drawing.Size(556, 27);
            this.localDocsTextBox.TabIndex = 1;
            this.localDocsTextBox.Text = "%USERPROFILE%\\Documents";
            this.localDocsTextBox.TextChanged += new System.EventHandler(this.LocalProfilesTextBoxTextChanged);
            // 
            // localDocsLabel
            // 
            this.localDocsLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.localDocsLabel.AutoSize = true;
            this.localDocsLabel.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.localDocsLabel.Location = new System.Drawing.Point(27, 146);
            this.localDocsLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.localDocsLabel.Name = "localDocsLabel";
            this.localDocsLabel.Size = new System.Drawing.Size(127, 19);
            this.localDocsLabel.TabIndex = 5;
            this.localDocsLabel.Text = "LocalProfilesDocs";
            this.localDocsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // doNotDetectOtherRemoteRootsCheckBox
            // 
            this.doNotDetectOtherRemoteRootsCheckBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.doNotDetectOtherRemoteRootsCheckBox.AutoSize = true;
            this.doNotDetectOtherRemoteRootsCheckBox.Location = new System.Drawing.Point(725, 109);
            this.doNotDetectOtherRemoteRootsCheckBox.Name = "doNotDetectOtherRemoteRootsCheckBox";
            this.doNotDetectOtherRemoteRootsCheckBox.Size = new System.Drawing.Size(191, 23);
            this.doNotDetectOtherRemoteRootsCheckBox.TabIndex = 3;
            this.doNotDetectOtherRemoteRootsCheckBox.Text = "Do not detect other roots";
            this.doNotDetectOtherRemoteRootsCheckBox.UseVisualStyleBackColor = true;
            // 
            // remoteDocsTextBox
            // 
            this.remoteDocsTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.remoteDocsTextBox.Location = new System.Drawing.Point(162, 177);
            this.remoteDocsTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.remoteDocsTextBox.Name = "remoteDocsTextBox";
            this.remoteDocsTextBox.Size = new System.Drawing.Size(556, 27);
            this.remoteDocsTextBox.TabIndex = 1;
            this.remoteDocsTextBox.Text = "%USERNAME%\\Recovered Files\\Documents";
            this.remoteDocsTextBox.TextChanged += new System.EventHandler(this.LocalProfilesTextBoxTextChanged);
            // 
            // remoteDocsLabel
            // 
            this.remoteDocsLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.remoteDocsLabel.AutoSize = true;
            this.remoteDocsLabel.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.remoteDocsLabel.Location = new System.Drawing.Point(9, 181);
            this.remoteDocsLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.remoteDocsLabel.Name = "remoteDocsLabel";
            this.remoteDocsLabel.Size = new System.Drawing.Size(145, 19);
            this.remoteDocsLabel.TabIndex = 5;
            this.remoteDocsLabel.Text = "RemoteProfilesDocs";
            this.remoteDocsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // createIfNotOnDestCheckBox
            // 
            this.createIfNotOnDestCheckBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.createIfNotOnDestCheckBox.AutoSize = true;
            this.createIfNotOnDestCheckBox.Location = new System.Drawing.Point(725, 179);
            this.createIfNotOnDestCheckBox.Name = "createIfNotOnDestCheckBox";
            this.createIfNotOnDestCheckBox.Size = new System.Drawing.Size(240, 23);
            this.createIfNotOnDestCheckBox.TabIndex = 3;
            this.createIfNotOnDestCheckBox.Text = "Create USERNAME folder if none";
            this.createIfNotOnDestCheckBox.UseVisualStyleBackColor = true;
            this.createIfNotOnDestCheckBox.CheckedChanged += new System.EventHandler(this.CreateIfNotOnDestCheckBoxCheckedChanged);
            // 
            // processPublicProfileCheckBox
            // 
            this.processPublicProfileCheckBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.processPublicProfileCheckBox.AutoSize = true;
            this.processPublicProfileCheckBox.Location = new System.Drawing.Point(725, 148);
            this.processPublicProfileCheckBox.Name = "processPublicProfileCheckBox";
            this.processPublicProfileCheckBox.Size = new System.Drawing.Size(15, 14);
            this.processPublicProfileCheckBox.TabIndex = 3;
            this.processPublicProfileCheckBox.UseVisualStyleBackColor = true;
            this.processPublicProfileCheckBox.Visible = false;
            // 
            // detectComputerNameCheckBox
            // 
            this.detectComputerNameCheckBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.detectComputerNameCheckBox.AutoSize = true;
            this.detectComputerNameCheckBox.Location = new System.Drawing.Point(725, 78);
            this.detectComputerNameCheckBox.Name = "detectComputerNameCheckBox";
            this.detectComputerNameCheckBox.Size = new System.Drawing.Size(15, 14);
            this.detectComputerNameCheckBox.TabIndex = 3;
            this.detectComputerNameCheckBox.UseVisualStyleBackColor = true;
            this.detectComputerNameCheckBox.Visible = false;
            // 
            // remoteTempTextBox
            // 
            this.remoteTempTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.remoteTempTextBox.Location = new System.Drawing.Point(162, 212);
            this.remoteTempTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.remoteTempTextBox.Name = "remoteTempTextBox";
            this.remoteTempTextBox.Size = new System.Drawing.Size(556, 27);
            this.remoteTempTextBox.TabIndex = 7;
            this.remoteTempTextBox.Text = "%USERNAME%\\Temp";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblBusy, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.lbMain, 0, 2);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 25);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 170F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(976, 541);
            this.tableLayoutPanel2.TabIndex = 4;
            // 
            // lbMain
            // 
            this.lbMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbMain.FormattingEnabled = true;
            this.lbMain.HorizontalScrollbar = true;
            this.lbMain.ItemHeight = 19;
            this.lbMain.Location = new System.Drawing.Point(3, 374);
            this.lbMain.Name = "lbMain";
            this.lbMain.Size = new System.Drawing.Size(970, 156);
            this.lbMain.TabIndex = 4;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 200;
            this.timer1.Tick += new System.EventHandler(this.Timer1Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(976, 593);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.tbStatus);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainForm";
            this.Text = "ProfileHarvester";
            this.Load += new System.EventHandler(this.MainFormLoad);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        private System.Windows.Forms.TextBox remoteTempTextBox;
        private System.Windows.Forms.Label remoteTempLabel;
        private System.Windows.Forms.ToolStripMenuItem moveLocalTempToNetworkDriveTempToolStripMenuItem;
        private System.Windows.Forms.CheckBox processPublicProfileCheckBox;
        private System.Windows.Forms.ToolStripMenuItem copyLocalDocumentsToNetworkDriveIfExistsrecursiveToolStripMenuItem;
        private System.Windows.Forms.CheckBox detectComputerNameCheckBox;
        private System.Windows.Forms.Label remoteDocsLabel;
        private System.Windows.Forms.TextBox remoteDocsTextBox;
        private System.Windows.Forms.Label localDocsLabel;
        private System.Windows.Forms.TextBox localDocsTextBox;
        private System.Windows.Forms.CheckBox createIfNotOnDestCheckBox;
        private System.Windows.Forms.CheckBox doNotDetectOtherRemoteRootsCheckBox;
        private System.Windows.Forms.TextBox remoteProfilesTextBox;
        private System.Windows.Forms.Label remoteProfilesLabel;
        private System.Windows.Forms.Label localProfilesLabel;
        private System.Windows.Forms.TextBox localProfilesTextBox;
        private System.Windows.Forms.CheckBox readOnlyCheckBox;
        private System.Windows.Forms.ListBox lbMain;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TextBox tbInput0;
        private System.Windows.Forms.Label lblInput0;
        private System.Windows.Forms.ToolStripMenuItem setPasswordToStringInInputBoxToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fCAToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem silverToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem profilesToolStripMenuItem;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.ToolStripMenuItem moveTmpFilesToTheOwnersTmpOnTToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearTmpToolStripMenuItem;
        private System.Windows.Forms.TextBox tbLocation;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lblLocation;
        private System.Windows.Forms.ToolStripMenuItem tsmiPath;
        private System.Windows.Forms.ToolStripMenuItem collectGameFilesToCurrentUserHAsTimeUserNameComputerNameToolStripMenuItem;
        private System.Windows.Forms.Label lblBusy;
        private System.Windows.Forms.TextBox tbStatus;
        private System.Windows.Forms.ToolStripMenuItem moveLocalDocumentsToNetworkDriveIfExistsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip1;
    }
}
