using System.ComponentModel;
using Microsoft.Web.WebView2.WinForms;

namespace Haven_Launcher
{
    partial class HavenLauncher
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
            lbxAddons = new ListBox();
            btnInstallAddon = new Button();
            btnPlay = new Button();
            btnInstall = new Button();
            progressBar = new ProgressBar();
            lblProgress = new Label();
            btnCancel = new Button();
            btnModifySettings = new Button();
            webView21 = new WebView2();
            btnWotlk = new Button();
            btnLegion = new Button();
            ((ISupportInitialize)webView21).BeginInit();
            SuspendLayout();
            // 
            // lbxAddons
            // 
            lbxAddons.BackColor = SystemColors.HighlightText;
            lbxAddons.ForeColor = SystemColors.ActiveCaptionText;
            lbxAddons.FormattingEnabled = true;
            lbxAddons.ItemHeight = 15;
            lbxAddons.Location = new Point(39, 44);
            lbxAddons.Name = "lbxAddons";
            lbxAddons.Size = new Size(204, 229);
            lbxAddons.TabIndex = 1;
            // 
            // btnInstallAddon
            // 
            btnInstallAddon.BackColor = Color.Transparent;
            btnInstallAddon.BackgroundImageLayout = ImageLayout.None;
            btnInstallAddon.ForeColor = SystemColors.ActiveCaptionText;
            btnInstallAddon.Location = new Point(38, 279);
            btnInstallAddon.Name = "btnInstallAddon";
            btnInstallAddon.Size = new Size(204, 26);
            btnInstallAddon.TabIndex = 2;
            btnInstallAddon.Text = "Install Selected Addon";
            btnInstallAddon.UseVisualStyleBackColor = false;
            btnInstallAddon.Click += btnInstallAddon_Click;
            // 
            // btnPlay
            // 
            btnPlay.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnPlay.BackColor = Color.Transparent;
            btnPlay.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnPlay.ForeColor = SystemColors.ActiveCaptionText;
            btnPlay.Location = new Point(39, 552);
            btnPlay.Name = "btnPlay";
            btnPlay.Size = new Size(204, 58);
            btnPlay.TabIndex = 3;
            btnPlay.Text = "Play Legion!";
            btnPlay.UseVisualStyleBackColor = false;
            btnPlay.Click += btnPlay_Click;
            // 
            // btnInstall
            // 
            btnInstall.BackColor = Color.Transparent;
            btnInstall.ForeColor = SystemColors.ActiveCaptionText;
            btnInstall.Location = new Point(38, 373);
            btnInstall.Name = "btnInstall";
            btnInstall.Size = new Size(204, 31);
            btnInstall.TabIndex = 4;
            btnInstall.Text = "Install Legion";
            btnInstall.UseVisualStyleBackColor = false;
            btnInstall.Click += btnInstall_Click;
            // 
            // progressBar
            // 
            progressBar.BackColor = Color.Black;
            progressBar.ForeColor = Color.MediumSeaGreen;
            progressBar.Location = new Point(38, 410);
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(204, 23);
            progressBar.TabIndex = 5;
            // 
            // lblProgress
            // 
            lblProgress.AutoSize = true;
            lblProgress.BackColor = Color.Transparent;
            lblProgress.ForeColor = SystemColors.ActiveCaptionText;
            lblProgress.Location = new Point(38, 436);
            lblProgress.Name = "lblProgress";
            lblProgress.Size = new Size(205, 15);
            lblProgress.TabIndex = 6;
            lblProgress.Text = "XX$ XXXXXMB / XXXXXMB  XXXMBPS";
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.Transparent;
            btnCancel.ForeColor = SystemColors.ActiveCaptionText;
            btnCancel.Location = new Point(39, 454);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(204, 23);
            btnCancel.TabIndex = 7;
            btnCancel.Text = "Cancel Install";
            btnCancel.UseVisualStyleBackColor = false;
            // 
            // btnModifySettings
            // 
            btnModifySettings.BackColor = Color.Transparent;
            btnModifySettings.ForeColor = SystemColors.ActiveCaptionText;
            btnModifySettings.Location = new Point(39, 515);
            btnModifySettings.Name = "btnModifySettings";
            btnModifySettings.Size = new Size(204, 22);
            btnModifySettings.TabIndex = 9;
            btnModifySettings.Text = "Change Installation Path";
            btnModifySettings.UseVisualStyleBackColor = false;
            btnModifySettings.Click += btnModifySettings_Click;
            // 
            // webView21
            // 
            webView21.AllowExternalDrop = false;
            webView21.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            webView21.BackColor = SystemColors.AppWorkspace;
            webView21.CreationProperties = null;
            webView21.DefaultBackgroundColor = Color.White;
            webView21.ForeColor = Color.LimeGreen;
            webView21.Location = new Point(257, 12);
            webView21.Name = "webView21";
            webView21.Size = new Size(751, 598);
            webView21.Source = new Uri("http://legionhaven.com/changelog.html", UriKind.Absolute);
            webView21.TabIndex = 0;
            webView21.ZoomFactor = 1D;
            // 
            // btnWotlk
            // 
            btnWotlk.Location = new Point(141, 12);
            btnWotlk.Name = "btnWotlk";
            btnWotlk.Size = new Size(101, 26);
            btnWotlk.TabIndex = 13;
            btnWotlk.Text = "WotLK";
            btnWotlk.UseVisualStyleBackColor = true;
            btnWotlk.Click += btnWotlk_Click;
            // 
            // btnLegion
            // 
            btnLegion.Location = new Point(39, 12);
            btnLegion.Name = "btnLegion";
            btnLegion.Size = new Size(101, 26);
            btnLegion.TabIndex = 16;
            btnLegion.Text = "Legion";
            btnLegion.UseVisualStyleBackColor = true;
            btnLegion.Click += btnLegion_Click;
            // 
            // HavenLauncher
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.AppWorkspace;
            ClientSize = new Size(1020, 622);
            Controls.Add(btnLegion);
            Controls.Add(btnWotlk);
            Controls.Add(webView21);
            Controls.Add(btnModifySettings);
            Controls.Add(lbxAddons);
            Controls.Add(progressBar);
            Controls.Add(btnInstall);
            Controls.Add(btnInstallAddon);
            Controls.Add(lblProgress);
            Controls.Add(btnCancel);
            Controls.Add(btnPlay);
            Name = "HavenLauncher";
            Text = "Haven Launcher";
            Load += HavenLauncher_Load;
            ((ISupportInitialize)webView21).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox lbxAddons;
        private Button btnInstallAddon;
        private Button btnPlay;
        private Button btnInstall;
        private ProgressBar progressBar;
        private Label lblProgress;
        private Button btnCancel;
        private Button btnModifySettings;
        private WebView2 webView21;
        private Button btnWotlk;
        private Button btnLegion;
    }
}
