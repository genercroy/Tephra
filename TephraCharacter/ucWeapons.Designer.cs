namespace TephraCharacter
{
    partial class ucWeapons
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblName = new System.Windows.Forms.Label();
            this.lblAP = new System.Windows.Forms.Label();
            this.lblReady = new System.Windows.Forms.Label();
            this.lblStrike = new System.Windows.Forms.Label();
            this.lblAcuracy = new System.Windows.Forms.Label();
            this.lblDamageClass = new System.Windows.Forms.Label();
            this.btnRoll = new System.Windows.Forms.Button();
            this.txtDamage = new System.Windows.Forms.RichTextBox();
            this.chkModifiers = new System.Windows.Forms.CheckedListBox();
            this.nCount = new System.Windows.Forms.NumericUpDown();
            this.lblCount = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nCount)).BeginInit();
            this.SuspendLayout();
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(0, 4);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(79, 13);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "Weapon Name";
            // 
            // lblAP
            // 
            this.lblAP.AutoSize = true;
            this.lblAP.Location = new System.Drawing.Point(129, 4);
            this.lblAP.Name = "lblAP";
            this.lblAP.Size = new System.Drawing.Size(56, 13);
            this.lblAP.TabIndex = 1;
            this.lblAP.Text = "AP to use:";
            // 
            // lblReady
            // 
            this.lblReady.AutoSize = true;
            this.lblReady.Location = new System.Drawing.Point(229, 4);
            this.lblReady.Name = "lblReady";
            this.lblReady.Size = new System.Drawing.Size(70, 13);
            this.lblReady.TabIndex = 2;
            this.lblReady.Text = "AP to Ready:";
            // 
            // lblStrike
            // 
            this.lblStrike.AutoSize = true;
            this.lblStrike.Location = new System.Drawing.Point(229, 26);
            this.lblStrike.Name = "lblStrike";
            this.lblStrike.Size = new System.Drawing.Size(37, 13);
            this.lblStrike.TabIndex = 4;
            this.lblStrike.Text = "Strike:";
            // 
            // lblAcuracy
            // 
            this.lblAcuracy.AutoSize = true;
            this.lblAcuracy.Location = new System.Drawing.Point(129, 26);
            this.lblAcuracy.Name = "lblAcuracy";
            this.lblAcuracy.Size = new System.Drawing.Size(55, 13);
            this.lblAcuracy.TabIndex = 3;
            this.lblAcuracy.Text = "Accuracy:";
            // 
            // lblDamageClass
            // 
            this.lblDamageClass.AutoSize = true;
            this.lblDamageClass.Location = new System.Drawing.Point(4, 50);
            this.lblDamageClass.Name = "lblDamageClass";
            this.lblDamageClass.Size = new System.Drawing.Size(78, 13);
            this.lblDamageClass.TabIndex = 5;
            this.lblDamageClass.Text = "Damage Class:";
            // 
            // btnRoll
            // 
            this.btnRoll.Location = new System.Drawing.Point(232, 42);
            this.btnRoll.Name = "btnRoll";
            this.btnRoll.Size = new System.Drawing.Size(75, 20);
            this.btnRoll.TabIndex = 6;
            this.btnRoll.Text = "Roll";
            this.btnRoll.UseVisualStyleBackColor = true;
            this.btnRoll.Click += new System.EventHandler(this.btnRoll_Click);
            // 
            // txtDamage
            // 
            this.txtDamage.Location = new System.Drawing.Point(132, 67);
            this.txtDamage.Name = "txtDamage";
            this.txtDamage.Size = new System.Drawing.Size(175, 49);
            this.txtDamage.TabIndex = 8;
            this.txtDamage.Text = "";
            // 
            // chkModifiers
            // 
            this.chkModifiers.FormattingEnabled = true;
            this.chkModifiers.Location = new System.Drawing.Point(4, 67);
            this.chkModifiers.Name = "chkModifiers";
            this.chkModifiers.Size = new System.Drawing.Size(120, 49);
            this.chkModifiers.TabIndex = 9;
            // 
            // nCount
            // 
            this.nCount.Enabled = false;
            this.nCount.Location = new System.Drawing.Point(208, 121);
            this.nCount.Name = "nCount";
            this.nCount.Size = new System.Drawing.Size(35, 20);
            this.nCount.TabIndex = 10;
            this.nCount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblCount
            // 
            this.lblCount.AutoSize = true;
            this.lblCount.Location = new System.Drawing.Point(132, 123);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(70, 13);
            this.lblCount.TabIndex = 11;
            this.lblCount.Text = "Enemy Count";
            // 
            // ucWeapons
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.lblCount);
            this.Controls.Add(this.nCount);
            this.Controls.Add(this.chkModifiers);
            this.Controls.Add(this.txtDamage);
            this.Controls.Add(this.btnRoll);
            this.Controls.Add(this.lblDamageClass);
            this.Controls.Add(this.lblStrike);
            this.Controls.Add(this.lblAcuracy);
            this.Controls.Add(this.lblReady);
            this.Controls.Add(this.lblAP);
            this.Controls.Add(this.lblName);
            this.Name = "ucWeapons";
            this.Size = new System.Drawing.Size(311, 147);
            this.Load += new System.EventHandler(this.ucWeapons_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nCount)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblAP;
        private System.Windows.Forms.Label lblReady;
        private System.Windows.Forms.Label lblStrike;
        private System.Windows.Forms.Label lblAcuracy;
        private System.Windows.Forms.Label lblDamageClass;
        private System.Windows.Forms.Button btnRoll;
        private System.Windows.Forms.RichTextBox txtDamage;
        private System.Windows.Forms.CheckedListBox chkModifiers;
        private System.Windows.Forms.NumericUpDown nCount;
        private System.Windows.Forms.Label lblCount;
    }
}
