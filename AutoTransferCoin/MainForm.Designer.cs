using AutoTransactionToken.Log;

namespace AutoTransactionToken
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            startButton = new Button();
            StartApp = new Button();
            TransactionSmartButton = new Button();
            DumpXML = new Button();
            DataView = new DataGridView();
            Update = new System.Windows.Forms.Timer(components);
            SortWindow = new Button();
            EnergyDelegateButton = new Button();
            TransactionUltimaButton = new Button();
            TransactionUltimaFromMainButton = new Button();
            EnergyRecoveryButton = new Button();
            ClearAppDataAndReloginButton = new Button();
            StopApp = new Button();
            RegisterButton = new Button();
            TransactionBullToken = new Button();
            SmartWallet = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)DataView).BeginInit();
            SuspendLayout();
            // 
            // startButton
            // 
            startButton.Location = new Point(33, 16);
            startButton.Name = "startButton";
            startButton.Size = new Size(75, 23);
            startButton.TabIndex = 0;
            startButton.Text = "Start";
            startButton.UseVisualStyleBackColor = true;
            startButton.Click += StartButton_Click;
            // 
            // StartApp
            // 
            StartApp.Location = new Point(114, 16);
            StartApp.Name = "StartApp";
            StartApp.Size = new Size(75, 23);
            StartApp.TabIndex = 1;
            StartApp.Text = "Start App";
            StartApp.UseVisualStyleBackColor = true;
            StartApp.Click += StartApp_Click;
            // 
            // TransactionSmartButton
            // 
            TransactionSmartButton.Location = new Point(195, 16);
            TransactionSmartButton.Name = "TransactionSmartButton";
            TransactionSmartButton.Size = new Size(132, 23);
            TransactionSmartButton.TabIndex = 5;
            TransactionSmartButton.Text = "Transaction Smart";
            TransactionSmartButton.UseVisualStyleBackColor = true;
            TransactionSmartButton.Click += TransactionSmartButton_Click;
            // 
            // DumpXML
            // 
            DumpXML.Location = new Point(33, 45);
            DumpXML.Name = "DumpXML";
            DumpXML.Size = new Size(107, 23);
            DumpXML.TabIndex = 6;
            DumpXML.Text = "Dump XML";
            DumpXML.UseVisualStyleBackColor = true;
            DumpXML.Click += button1_Click;
            // 
            // DataView
            // 
            DataView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            DataView.BackgroundColor = Color.WhiteSmoke;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = SystemColors.ButtonHighlight;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.ButtonHighlight;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            DataView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            DataView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DataView.ColumnHeadersVisible = false;
            DataView.Columns.AddRange(new DataGridViewColumn[] { SmartWallet });
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.MenuBar;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = Color.Yellow;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.WindowText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            DataView.DefaultCellStyle = dataGridViewCellStyle2;
            DataView.EnableHeadersVisualStyles = false;
            DataView.ImeMode = ImeMode.Off;
            DataView.Location = new Point(0, 114);
            DataView.Name = "DataView";
            DataView.ReadOnly = true;
            DataView.RightToLeft = RightToLeft.No;
            DataView.Size = new Size(1366, 428);
            DataView.TabIndex = 7;
            DataView.RowPostPaint += DataView_RowPostPaint;
            // 
            // Update
            // 
            Update.Enabled = true;
            Update.Interval = 300;
            Update.Tick += Update_Tick;
            // 
            // SortWindow
            // 
            SortWindow.Location = new Point(146, 45);
            SortWindow.Name = "SortWindow";
            SortWindow.Size = new Size(107, 23);
            SortWindow.TabIndex = 8;
            SortWindow.Text = "Sort Window";
            SortWindow.UseVisualStyleBackColor = true;
            SortWindow.Click += SortWindow_Click;
            // 
            // EnergyDelegateButton
            // 
            EnergyDelegateButton.Location = new Point(333, 16);
            EnergyDelegateButton.Name = "EnergyDelegateButton";
            EnergyDelegateButton.Size = new Size(134, 23);
            EnergyDelegateButton.TabIndex = 9;
            EnergyDelegateButton.Text = "Energy Delegate";
            EnergyDelegateButton.UseVisualStyleBackColor = true;
            EnergyDelegateButton.Click += EnergyDelegate_Click;
            // 
            // TransactionUltimaButton
            // 
            TransactionUltimaButton.Location = new Point(473, 16);
            TransactionUltimaButton.Name = "TransactionUltimaButton";
            TransactionUltimaButton.Size = new Size(132, 23);
            TransactionUltimaButton.TabIndex = 10;
            TransactionUltimaButton.Text = "Transaction Ultima";
            TransactionUltimaButton.UseVisualStyleBackColor = true;
            TransactionUltimaButton.Click += TransactionUltimaButton_Click;
            // 
            // TransactionUltimaFromMainButton
            // 
            TransactionUltimaFromMainButton.Location = new Point(611, 16);
            TransactionUltimaFromMainButton.Name = "TransactionUltimaFromMainButton";
            TransactionUltimaFromMainButton.Size = new Size(201, 23);
            TransactionUltimaFromMainButton.TabIndex = 11;
            TransactionUltimaFromMainButton.Text = "Transaction Ultima From Main";
            TransactionUltimaFromMainButton.UseVisualStyleBackColor = true;
            TransactionUltimaFromMainButton.Click += TransactionUltimaFromMainButton_Click;
            // 
            // EnergyRecoveryButton
            // 
            EnergyRecoveryButton.Location = new Point(818, 16);
            EnergyRecoveryButton.Name = "EnergyRecoveryButton";
            EnergyRecoveryButton.Size = new Size(140, 23);
            EnergyRecoveryButton.TabIndex = 12;
            EnergyRecoveryButton.Text = "Energy Recovery";
            EnergyRecoveryButton.UseVisualStyleBackColor = true;
            EnergyRecoveryButton.Click += EnergyRecoveryButton_Click;
            // 
            // ClearAppDataAndReloginButton
            // 
            ClearAppDataAndReloginButton.Location = new Point(370, 45);
            ClearAppDataAndReloginButton.Name = "ClearAppDataAndReloginButton";
            ClearAppDataAndReloginButton.Size = new Size(168, 23);
            ClearAppDataAndReloginButton.TabIndex = 13;
            ClearAppDataAndReloginButton.Text = "Clear Data App And Relogin";
            ClearAppDataAndReloginButton.UseVisualStyleBackColor = true;
            ClearAppDataAndReloginButton.Click += ClearAppDataAndReloginButton_Click;
            // 
            // StopApp
            // 
            StopApp.Location = new Point(259, 45);
            StopApp.Name = "StopApp";
            StopApp.Size = new Size(105, 23);
            StopApp.TabIndex = 14;
            StopApp.Text = "Stop App";
            StopApp.UseVisualStyleBackColor = true;
            StopApp.Click += StopApp_Click;
            // 
            // RegisterButton
            // 
            RegisterButton.Location = new Point(544, 45);
            RegisterButton.Name = "RegisterButton";
            RegisterButton.Size = new Size(105, 23);
            RegisterButton.TabIndex = 15;
            RegisterButton.Text = "Register";
            RegisterButton.UseVisualStyleBackColor = true;
            RegisterButton.Click += RegisterButton_Click;
            // 
            // TransactionBullToken
            // 
            TransactionBullToken.Location = new Point(964, 16);
            TransactionBullToken.Name = "TransactionBullToken";
            TransactionBullToken.Size = new Size(119, 23);
            TransactionBullToken.TabIndex = 16;
            TransactionBullToken.Text = "Transaction Bull";
            TransactionBullToken.UseVisualStyleBackColor = true;
            TransactionBullToken.Click += TransactionBullButton_Click;
            // 
            // SmartWallet
            // 
            SmartWallet.HeaderText = "Smart Wallet";
            SmartWallet.MinimumWidth = 50;
            SmartWallet.Name = "SmartWallet";
            SmartWallet.ReadOnly = true;
            SmartWallet.Width = 1500;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1369, 542);
            Controls.Add(TransactionBullToken);
            Controls.Add(RegisterButton);
            Controls.Add(StopApp);
            Controls.Add(ClearAppDataAndReloginButton);
            Controls.Add(EnergyRecoveryButton);
            Controls.Add(TransactionUltimaFromMainButton);
            Controls.Add(TransactionUltimaButton);
            Controls.Add(EnergyDelegateButton);
            Controls.Add(SortWindow);
            Controls.Add(DataView);
            Controls.Add(DumpXML);
            Controls.Add(TransactionSmartButton);
            Controls.Add(StartApp);
            Controls.Add(startButton);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "MainForm";
            Text = "Auto Transaction Token";
            Load += MainForm_Load;
            ((System.ComponentModel.ISupportInitialize)DataView).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Button startButton;
        private Button StartApp;
        private Button TransactionSmartButton;
        private Button DumpXML;
        private DataGridView DataView;
        private System.Windows.Forms.Timer Update;
        private Button SortWindow;
        private Button EnergyDelegateButton;
        private Button TransactionUltimaButton;
        private Button TransactionUltimaFromMainButton;
        private Button EnergyRecoveryButton;
        private Button ClearAppDataAndReloginButton;
        private Button StopApp;
        private Button RegisterButton;
        private Button TransactionBullToken;
        private DataGridViewTextBoxColumn SmartWallet;
    }
}
