namespace cctalk_apptest
{
    partial class Form1
    {
        /// <summary>
        /// Variabile di progettazione necessaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Liberare le risorse in uso.
        /// </summary>
        /// <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Codice generato da Progettazione Windows Form

        /// <summary>
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        {
			this.components = new System.ComponentModel.Container();
			this.button1 = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.cbBrute = new System.Windows.Forms.CheckBox();
			this.radioButton3 = new System.Windows.Forms.RadioButton();
			this.radioButton2 = new System.Windows.Forms.RadioButton();
			this.radioButton1 = new System.Windows.Forms.RadioButton();
			this.label1 = new System.Windows.Forms.Label();
			this.listBox1 = new System.Windows.Forms.ListBox();
			this.contextMenuListBox = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.clearMoneyCounterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.cbPolling = new System.Windows.Forms.CheckBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.butPollNow = new System.Windows.Forms.Button();
			this.butReady = new System.Windows.Forms.Button();
			this.cbInhibit = new System.Windows.Forms.CheckBox();
			this.initButton = new System.Windows.Forms.Button();
			this.resetButton = new System.Windows.Forms.Button();
			this.deviceNumber = new System.Windows.Forms.NumericUpDown();
			this.label2 = new System.Windows.Forms.Label();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.comNumber = new System.Windows.Forms.NumericUpDown();
			this.configWord = new System.Windows.Forms.TextBox();
			this.groupBox1.SuspendLayout();
			this.contextMenuListBox.SuspendLayout();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.deviceNumber)).BeginInit();
			this.groupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.comNumber)).BeginInit();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.button1.Location = new System.Drawing.Point(6, 96);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(100, 50);
			this.button1.TabIndex = 0;
			this.button1.Text = "Send command";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.cbBrute);
			this.groupBox1.Controls.Add(this.radioButton3);
			this.groupBox1.Controls.Add(this.radioButton2);
			this.groupBox1.Controls.Add(this.radioButton1);
			this.groupBox1.Controls.Add(this.button1);
			this.groupBox1.Enabled = false;
			this.groupBox1.Location = new System.Drawing.Point(449, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(113, 181);
			this.groupBox1.TabIndex = 2;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Raw commands";
			// 
			// cbBrute
			// 
			this.cbBrute.Appearance = System.Windows.Forms.Appearance.Button;
			this.cbBrute.Location = new System.Drawing.Point(6, 152);
			this.cbBrute.Name = "cbBrute";
			this.cbBrute.Size = new System.Drawing.Size(100, 24);
			this.cbBrute.TabIndex = 1;
			this.cbBrute.Text = "Brute force";
			this.cbBrute.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.cbBrute.UseVisualStyleBackColor = true;
			this.cbBrute.CheckedChanged += new System.EventHandler(this.cbBrute_CheckedChanged);
			// 
			// radioButton3
			// 
			this.radioButton3.AutoSize = true;
			this.radioButton3.Location = new System.Drawing.Point(6, 65);
			this.radioButton3.Name = "radioButton3";
			this.radioButton3.Size = new System.Drawing.Size(87, 17);
			this.radioButton3.TabIndex = 0;
			this.radioButton3.Text = "ResetDevice";
			this.radioButton3.UseVisualStyleBackColor = true;
			// 
			// radioButton2
			// 
			this.radioButton2.AutoSize = true;
			this.radioButton2.Location = new System.Drawing.Point(6, 42);
			this.radioButton2.Name = "radioButton2";
			this.radioButton2.Size = new System.Drawing.Size(77, 17);
			this.radioButton2.TabIndex = 0;
			this.radioButton2.Text = "ReadSerial";
			this.radioButton2.UseVisualStyleBackColor = true;
			// 
			// radioButton1
			// 
			this.radioButton1.AutoSize = true;
			this.radioButton1.Checked = true;
			this.radioButton1.Location = new System.Drawing.Point(6, 19);
			this.radioButton1.Name = "radioButton1";
			this.radioButton1.Size = new System.Drawing.Size(79, 17);
			this.radioButton1.TabIndex = 0;
			this.radioButton1.TabStop = true;
			this.radioButton1.Text = "ReadBuffer";
			this.radioButton1.UseVisualStyleBackColor = true;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(3, 13);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(66, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "Com number";
			// 
			// listBox1
			// 
			this.listBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.listBox1.ContextMenuStrip = this.contextMenuListBox;
			this.listBox1.FormattingEnabled = true;
			this.listBox1.Location = new System.Drawing.Point(12, 12);
			this.listBox1.Name = "listBox1";
			this.listBox1.Size = new System.Drawing.Size(431, 407);
			this.listBox1.TabIndex = 3;
			// 
			// contextMenuListBox
			// 
			this.contextMenuListBox.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearToolStripMenuItem,
            this.clearMoneyCounterToolStripMenuItem});
			this.contextMenuListBox.Name = "contextMenuListBox";
			this.contextMenuListBox.Size = new System.Drawing.Size(186, 48);
			// 
			// clearToolStripMenuItem
			// 
			this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
			this.clearToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
			this.clearToolStripMenuItem.Text = "Clear";
			this.clearToolStripMenuItem.Click += new System.EventHandler(this.clearToolStripMenuItem_Click);
			// 
			// clearMoneyCounterToolStripMenuItem
			// 
			this.clearMoneyCounterToolStripMenuItem.Name = "clearMoneyCounterToolStripMenuItem";
			this.clearMoneyCounterToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
			this.clearMoneyCounterToolStripMenuItem.Text = "Clear money counter";
			this.clearMoneyCounterToolStripMenuItem.Click += new System.EventHandler(this.clearMoneyCounterToolStripMenuItem_Click);
			// 
			// cbPolling
			// 
			this.cbPolling.AutoSize = true;
			this.cbPolling.Location = new System.Drawing.Point(3, 3);
			this.cbPolling.Name = "cbPolling";
			this.cbPolling.Size = new System.Drawing.Size(57, 17);
			this.cbPolling.TabIndex = 4;
			this.cbPolling.Text = "Polling";
			this.cbPolling.UseVisualStyleBackColor = true;
			this.cbPolling.CheckedChanged += new System.EventHandler(this.cbPolling_CheckedChanged);
			// 
			// panel1
			// 
			this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panel1.Controls.Add(this.butPollNow);
			this.panel1.Controls.Add(this.butReady);
			this.panel1.Controls.Add(this.cbInhibit);
			this.panel1.Controls.Add(this.cbPolling);
			this.panel1.Enabled = false;
			this.panel1.Location = new System.Drawing.Point(12, 484);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(431, 86);
			this.panel1.TabIndex = 5;
			// 
			// butPollNow
			// 
			this.butPollNow.Location = new System.Drawing.Point(84, 48);
			this.butPollNow.Name = "butPollNow";
			this.butPollNow.Size = new System.Drawing.Size(75, 36);
			this.butPollNow.TabIndex = 5;
			this.butPollNow.Text = "Poll now";
			this.butPollNow.UseVisualStyleBackColor = true;
			this.butPollNow.Click += new System.EventHandler(this.butPollNow_Click);
			// 
			// butReady
			// 
			this.butReady.Location = new System.Drawing.Point(3, 47);
			this.butReady.Name = "butReady";
			this.butReady.Size = new System.Drawing.Size(75, 36);
			this.butReady.TabIndex = 5;
			this.butReady.Text = "Ready?";
			this.butReady.UseVisualStyleBackColor = true;
			this.butReady.Click += new System.EventHandler(this.ready_Click);
			// 
			// cbInhibit
			// 
			this.cbInhibit.AutoSize = true;
			this.cbInhibit.Location = new System.Drawing.Point(3, 26);
			this.cbInhibit.Name = "cbInhibit";
			this.cbInhibit.Size = new System.Drawing.Size(68, 17);
			this.cbInhibit.TabIndex = 4;
			this.cbInhibit.Text = "Inhibiting";
			this.cbInhibit.UseVisualStyleBackColor = true;
			this.cbInhibit.CheckedChanged += new System.EventHandler(this.cbInhibit_CheckedChanged);
			// 
			// initButton
			// 
			this.initButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.initButton.Location = new System.Drawing.Point(455, 484);
			this.initButton.Name = "initButton";
			this.initButton.Size = new System.Drawing.Size(100, 41);
			this.initButton.TabIndex = 6;
			this.initButton.Text = "Init";
			this.initButton.UseVisualStyleBackColor = true;
			this.initButton.Click += new System.EventHandler(this.initButton_Click);
			// 
			// resetButton
			// 
			this.resetButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.resetButton.Enabled = false;
			this.resetButton.Location = new System.Drawing.Point(455, 531);
			this.resetButton.Name = "resetButton";
			this.resetButton.Size = new System.Drawing.Size(100, 39);
			this.resetButton.TabIndex = 6;
			this.resetButton.Text = "Reset";
			this.resetButton.UseVisualStyleBackColor = true;
			this.resetButton.Click += new System.EventHandler(this.resetButton_Click);
			// 
			// deviceNumber
			// 
			this.deviceNumber.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.deviceNumber.Location = new System.Drawing.Point(6, 73);
			this.deviceNumber.Name = "deviceNumber";
			this.deviceNumber.Size = new System.Drawing.Size(100, 20);
			this.deviceNumber.TabIndex = 1;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(3, 57);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(79, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "Device number";
			// 
			// groupBox2
			// 
			this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox2.Controls.Add(this.comNumber);
			this.groupBox2.Controls.Add(this.deviceNumber);
			this.groupBox2.Controls.Add(this.label1);
			this.groupBox2.Controls.Add(this.label2);
			this.groupBox2.Location = new System.Drawing.Point(449, 193);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(113, 100);
			this.groupBox2.TabIndex = 7;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Addresing";
			// 
			// comNumber
			// 
			this.comNumber.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.comNumber.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::cctalk_apptest.Properties.Settings.Default, "DefaultPortNumber", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.comNumber.Location = new System.Drawing.Point(6, 29);
			this.comNumber.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.comNumber.Name = "comNumber";
			this.comNumber.Size = new System.Drawing.Size(100, 20);
			this.comNumber.TabIndex = 1;
			this.comNumber.Value = global::cctalk_apptest.Properties.Settings.Default.DefaultPortNumber;
			// 
			// configWord
			// 
			this.configWord.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.configWord.Location = new System.Drawing.Point(12, 425);
			this.configWord.Multiline = true;
			this.configWord.Name = "configWord";
			this.configWord.Size = new System.Drawing.Size(553, 53);
			this.configWord.TabIndex = 8;
			this.configWord.Text = "2=5;Rub,";
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(574, 582);
			this.Controls.Add(this.configWord);
			this.Controls.Add(this.resetButton);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.initButton);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.listBox1);
			this.Controls.Add(this.groupBox1);
			this.MinimumSize = new System.Drawing.Size(302, 436);
			this.Name = "Form1";
			this.Text = "ccTalk simple demo app";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.contextMenuListBox.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.deviceNumber)).EndInit();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.comNumber)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.RadioButton radioButton2;
		private System.Windows.Forms.RadioButton radioButton1;
		private System.Windows.Forms.ListBox listBox1;
		private System.Windows.Forms.ContextMenuStrip contextMenuListBox;
		private System.Windows.Forms.ToolStripMenuItem clearToolStripMenuItem;
		private System.Windows.Forms.RadioButton radioButton3;
		private System.Windows.Forms.CheckBox cbPolling;
		private System.Windows.Forms.ToolStripMenuItem clearMoneyCounterToolStripMenuItem;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.CheckBox cbInhibit;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.NumericUpDown comNumber;
		private System.Windows.Forms.Button initButton;
		private System.Windows.Forms.Button resetButton;
		private System.Windows.Forms.NumericUpDown deviceNumber;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button butReady;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.CheckBox cbBrute;
		private System.Windows.Forms.Button butPollNow;
		private System.Windows.Forms.TextBox configWord;
    }
}

