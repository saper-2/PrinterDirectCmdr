namespace PrinterDirectCmdr
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label1 = new System.Windows.Forms.Label();
            this.bSelPrinter = new System.Windows.Forms.Button();
            this.eSelPrinter = new System.Windows.Forms.TextBox();
            this.bOpenPrinter = new System.Windows.Forms.Button();
            this.bClosePrinter = new System.Windows.Forms.Button();
            this.bExecCmd = new System.Windows.Forms.Button();
            this.eCmd = new System.Windows.Forms.TextBox();
            this.eLog = new System.Windows.Forms.TextBox();
            this.eCmdSeq = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.eDocName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.bRunCommands = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cbAutoClear = new System.Windows.Forms.CheckBox();
            this.lblLineCount = new System.Windows.Forms.Label();
            this.bTestSeq = new System.Windows.Forms.Button();
            this.bNewSeq = new System.Windows.Forms.Button();
            this.bOpenSeq = new System.Windows.Forms.Button();
            this.bSaveSeq = new System.Windows.Forms.Button();
            this.dlgOpen = new System.Windows.Forms.OpenFileDialog();
            this.dlgSave = new System.Windows.Forms.SaveFileDialog();
            this.bCmdMenu = new System.Windows.Forms.Button();
            this.cmCommands = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Selected printer:";
            // 
            // bSelPrinter
            // 
            this.bSelPrinter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bSelPrinter.Location = new System.Drawing.Point(597, 12);
            this.bSelPrinter.Name = "bSelPrinter";
            this.bSelPrinter.Size = new System.Drawing.Size(75, 20);
            this.bSelPrinter.TabIndex = 1;
            this.bSelPrinter.Text = "Select...";
            this.bSelPrinter.UseVisualStyleBackColor = true;
            this.bSelPrinter.Click += new System.EventHandler(this.bSelPrinter_Click);
            // 
            // eSelPrinter
            // 
            this.eSelPrinter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.eSelPrinter.Location = new System.Drawing.Point(102, 12);
            this.eSelPrinter.Name = "eSelPrinter";
            this.eSelPrinter.Size = new System.Drawing.Size(489, 20);
            this.eSelPrinter.TabIndex = 2;
            // 
            // bOpenPrinter
            // 
            this.bOpenPrinter.Location = new System.Drawing.Point(12, 74);
            this.bOpenPrinter.Name = "bOpenPrinter";
            this.bOpenPrinter.Size = new System.Drawing.Size(75, 23);
            this.bOpenPrinter.TabIndex = 3;
            this.bOpenPrinter.Text = "Open printer";
            this.bOpenPrinter.UseVisualStyleBackColor = true;
            this.bOpenPrinter.Click += new System.EventHandler(this.bOpenPrinter_Click);
            // 
            // bClosePrinter
            // 
            this.bClosePrinter.Location = new System.Drawing.Point(93, 74);
            this.bClosePrinter.Name = "bClosePrinter";
            this.bClosePrinter.Size = new System.Drawing.Size(75, 23);
            this.bClosePrinter.TabIndex = 4;
            this.bClosePrinter.Text = "Close printer";
            this.bClosePrinter.UseVisualStyleBackColor = true;
            this.bClosePrinter.Click += new System.EventHandler(this.bClosePrinter_Click);
            // 
            // bExecCmd
            // 
            this.bExecCmd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bExecCmd.Image = ((System.Drawing.Image)(resources.GetObject("bExecCmd.Image")));
            this.bExecCmd.Location = new System.Drawing.Point(649, 101);
            this.bExecCmd.Name = "bExecCmd";
            this.bExecCmd.Size = new System.Drawing.Size(23, 24);
            this.bExecCmd.TabIndex = 5;
            this.bExecCmd.UseVisualStyleBackColor = true;
            this.bExecCmd.Click += new System.EventHandler(this.bExecCmd_Click);
            // 
            // eCmd
            // 
            this.eCmd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.eCmd.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.eCmd.Location = new System.Drawing.Point(93, 103);
            this.eCmd.Name = "eCmd";
            this.eCmd.Size = new System.Drawing.Size(550, 20);
            this.eCmd.TabIndex = 2;
            this.eCmd.KeyDown += new System.Windows.Forms.KeyEventHandler(this.eCmd_KeyDown);
            // 
            // eLog
            // 
            this.eLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.eLog.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.eLog.Location = new System.Drawing.Point(12, 362);
            this.eLog.Multiline = true;
            this.eLog.Name = "eLog";
            this.eLog.ReadOnly = true;
            this.eLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.eLog.Size = new System.Drawing.Size(660, 187);
            this.eLog.TabIndex = 2;
            this.eLog.TabStop = false;
            // 
            // eCmdSeq
            // 
            this.eCmdSeq.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.eCmdSeq.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.eCmdSeq.Location = new System.Drawing.Point(44, 142);
            this.eCmdSeq.Multiline = true;
            this.eCmdSeq.Name = "eCmdSeq";
            this.eCmdSeq.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.eCmdSeq.Size = new System.Drawing.Size(628, 185);
            this.eCmdSeq.TabIndex = 2;
            this.eCmdSeq.TextChanged += new System.EventHandler(this.eCmdSeq_TextChanged);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Location = new System.Drawing.Point(12, 64);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(660, 4);
            this.panel1.TabIndex = 6;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Location = new System.Drawing.Point(12, 132);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(660, 4);
            this.panel2.TabIndex = 7;
            // 
            // eDocName
            // 
            this.eDocName.Location = new System.Drawing.Point(145, 38);
            this.eDocName.Name = "eDocName";
            this.eDocName.Size = new System.Drawing.Size(237, 20);
            this.eDocName.TabIndex = 2;
            this.eDocName.Text = ".Net RAW Print";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(127, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Print job document name:";
            // 
            // bRunCommands
            // 
            this.bRunCommands.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bRunCommands.Image = ((System.Drawing.Image)(resources.GetObject("bRunCommands.Image")));
            this.bRunCommands.Location = new System.Drawing.Point(592, 333);
            this.bRunCommands.Name = "bRunCommands";
            this.bRunCommands.Size = new System.Drawing.Size(80, 23);
            this.bRunCommands.TabIndex = 5;
            this.bRunCommands.Text = "Execute";
            this.bRunCommands.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.bRunCommands.UseVisualStyleBackColor = true;
            this.bRunCommands.Click += new System.EventHandler(this.bRunCommands_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(218, 72);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(312, 26);
            this.label4.TabIndex = 0;
            this.label4.Text = "Non-printable ASCII characters escape with \"\\xHH\" (2-digit hex).\r\nEscape \"\\\" with" +
    " \"\\\\\" if following char have to be \"x\".";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 330);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(348, 26);
            this.label5.TabIndex = 0;
            this.label5.Text = "Non-printable ASCII characters escape with \"\\xHH\" (2-digit hex). \r\nEach line will" +
    " be sent separately, no CR/LF will be appended to end line.";
            // 
            // cbAutoClear
            // 
            this.cbAutoClear.AutoSize = true;
            this.cbAutoClear.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbAutoClear.Location = new System.Drawing.Point(592, 80);
            this.cbAutoClear.Name = "cbAutoClear";
            this.cbAutoClear.Size = new System.Drawing.Size(75, 17);
            this.cbAutoClear.TabIndex = 8;
            this.cbAutoClear.Text = "Auto-Clear";
            this.cbAutoClear.UseVisualStyleBackColor = true;
            // 
            // lblLineCount
            // 
            this.lblLineCount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblLineCount.Location = new System.Drawing.Point(502, 338);
            this.lblLineCount.Name = "lblLineCount";
            this.lblLineCount.Size = new System.Drawing.Size(84, 13);
            this.lblLineCount.TabIndex = 9;
            this.lblLineCount.Text = "Lines: 0000";
            this.lblLineCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bTestSeq
            // 
            this.bTestSeq.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bTestSeq.Image = ((System.Drawing.Image)(resources.GetObject("bTestSeq.Image")));
            this.bTestSeq.Location = new System.Drawing.Point(12, 238);
            this.bTestSeq.Name = "bTestSeq";
            this.bTestSeq.Size = new System.Drawing.Size(26, 26);
            this.bTestSeq.TabIndex = 10;
            this.bTestSeq.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.bTestSeq.UseVisualStyleBackColor = true;
            this.bTestSeq.Click += new System.EventHandler(this.bTestSeq_Click);
            // 
            // bNewSeq
            // 
            this.bNewSeq.Image = ((System.Drawing.Image)(resources.GetObject("bNewSeq.Image")));
            this.bNewSeq.Location = new System.Drawing.Point(12, 142);
            this.bNewSeq.Name = "bNewSeq";
            this.bNewSeq.Size = new System.Drawing.Size(26, 26);
            this.bNewSeq.TabIndex = 11;
            this.bNewSeq.UseVisualStyleBackColor = true;
            this.bNewSeq.Click += new System.EventHandler(this.bNewSeq_Click);
            // 
            // bOpenSeq
            // 
            this.bOpenSeq.Image = ((System.Drawing.Image)(resources.GetObject("bOpenSeq.Image")));
            this.bOpenSeq.Location = new System.Drawing.Point(12, 174);
            this.bOpenSeq.Name = "bOpenSeq";
            this.bOpenSeq.Size = new System.Drawing.Size(26, 26);
            this.bOpenSeq.TabIndex = 12;
            this.bOpenSeq.UseVisualStyleBackColor = true;
            this.bOpenSeq.Click += new System.EventHandler(this.bOpenSeq_Click);
            // 
            // bSaveSeq
            // 
            this.bSaveSeq.Image = ((System.Drawing.Image)(resources.GetObject("bSaveSeq.Image")));
            this.bSaveSeq.Location = new System.Drawing.Point(12, 206);
            this.bSaveSeq.Name = "bSaveSeq";
            this.bSaveSeq.Size = new System.Drawing.Size(26, 26);
            this.bSaveSeq.TabIndex = 12;
            this.bSaveSeq.UseVisualStyleBackColor = true;
            this.bSaveSeq.Click += new System.EventHandler(this.bSaveSeq_Click);
            // 
            // dlgOpen
            // 
            this.dlgOpen.FileName = "openFileDialog1";
            this.dlgOpen.Filter = "Text files|*.txt|All files|*.*";
            // 
            // dlgSave
            // 
            this.dlgSave.DefaultExt = "txt";
            this.dlgSave.Filter = "Text files|*.txt|All files|*.*";
            // 
            // bCmdMenu
            // 
            this.bCmdMenu.ContextMenuStrip = this.cmCommands;
            this.bCmdMenu.Location = new System.Drawing.Point(12, 103);
            this.bCmdMenu.Name = "bCmdMenu";
            this.bCmdMenu.Size = new System.Drawing.Size(75, 20);
            this.bCmdMenu.TabIndex = 13;
            this.bCmdMenu.Text = "Command:";
            this.bCmdMenu.UseVisualStyleBackColor = true;
            this.bCmdMenu.Click += new System.EventHandler(this.bCmdMenu_Click);
            // 
            // cmCommands
            // 
            this.cmCommands.Name = "cmCommands";
            this.cmCommands.Size = new System.Drawing.Size(61, 4);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 561);
            this.Controls.Add(this.bCmdMenu);
            this.Controls.Add(this.bSaveSeq);
            this.Controls.Add(this.bOpenSeq);
            this.Controls.Add(this.bNewSeq);
            this.Controls.Add(this.bTestSeq);
            this.Controls.Add(this.lblLineCount);
            this.Controls.Add(this.cbAutoClear);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.bRunCommands);
            this.Controls.Add(this.bExecCmd);
            this.Controls.Add(this.bClosePrinter);
            this.Controls.Add(this.bOpenPrinter);
            this.Controls.Add(this.eDocName);
            this.Controls.Add(this.eCmdSeq);
            this.Controls.Add(this.eLog);
            this.Controls.Add(this.eCmd);
            this.Controls.Add(this.eSelPrinter);
            this.Controls.Add(this.bSelPrinter);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.MinimumSize = new System.Drawing.Size(700, 600);
            this.Name = "Form1";
            this.Text = "RAW Printer Command Writer Tool";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button bSelPrinter;
        private System.Windows.Forms.TextBox eSelPrinter;
        private System.Windows.Forms.Button bOpenPrinter;
        private System.Windows.Forms.Button bClosePrinter;
        private System.Windows.Forms.Button bExecCmd;
        private System.Windows.Forms.TextBox eCmd;
        private System.Windows.Forms.TextBox eLog;
        private System.Windows.Forms.TextBox eCmdSeq;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox eDocName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button bRunCommands;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox cbAutoClear;
        private System.Windows.Forms.Label lblLineCount;
        private System.Windows.Forms.Button bTestSeq;
        private System.Windows.Forms.Button bNewSeq;
        private System.Windows.Forms.Button bOpenSeq;
        private System.Windows.Forms.Button bSaveSeq;
        private System.Windows.Forms.OpenFileDialog dlgOpen;
        private System.Windows.Forms.SaveFileDialog dlgSave;
        private System.Windows.Forms.Button bCmdMenu;
        private System.Windows.Forms.ContextMenuStrip cmCommands;
    }
}

