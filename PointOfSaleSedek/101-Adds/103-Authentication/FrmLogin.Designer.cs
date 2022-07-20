namespace PointOfSaleSedek._101_Adds._103_Authentication
{
    partial class FrmLogin
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
            this.jGradientPanel1 = new JGradient_Panel.JGradientPanel();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btnLogin = new DevExpress.XtraEditors.SimpleButton();
            this.txtPassword = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.txtUserName = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.jGradientPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // jGradientPanel1
            // 
            this.jGradientPanel1.BackColor = System.Drawing.Color.Blue;
            this.jGradientPanel1.ColorBottom = System.Drawing.Color.Empty;
            this.jGradientPanel1.ColorTop = System.Drawing.Color.Empty;
            this.jGradientPanel1.Controls.Add(this.labelControl3);
            this.jGradientPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.jGradientPanel1.Location = new System.Drawing.Point(0, 0);
            this.jGradientPanel1.Name = "jGradientPanel1";
            this.jGradientPanel1.Size = new System.Drawing.Size(296, 69);
            this.jGradientPanel1.TabIndex = 20;
            // 
            // labelControl3
            // 
            this.labelControl3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Appearance.ForeColor = System.Drawing.Color.White;
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Appearance.Options.UseForeColor = true;
            this.labelControl3.Location = new System.Drawing.Point(115, 23);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(79, 23);
            this.labelControl3.TabIndex = 6;
            this.labelControl3.Text = "تسجيل الدخول";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(219, 167);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(48, 13);
            this.labelControl2.TabIndex = 19;
            this.labelControl2.Text = "كلمة السر";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(193, 95);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(74, 13);
            this.labelControl1.TabIndex = 18;
            this.labelControl1.Text = "اسم المستخدم";
            // 
            // btnLogin
            // 
            this.btnLogin.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Success;
            this.btnLogin.Appearance.BackColor2 = System.Drawing.Color.Lime;
            this.btnLogin.Appearance.Options.UseBackColor = true;
            this.btnLogin.Location = new System.Drawing.Point(151, 247);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(116, 40);
            this.btnLogin.TabIndex = 2;
            this.btnLogin.Text = "دخول";
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click_1);
            // 
            // txtPassword
            // 
            this.txtPassword.Depth = 0;
            this.txtPassword.Hint = "";
            this.txtPassword.Location = new System.Drawing.Point(28, 186);
            this.txtPassword.MouseState = MaterialSkin.MouseState.HOVER;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '\0';
            this.txtPassword.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtPassword.SelectedText = "";
            this.txtPassword.SelectionLength = 0;
            this.txtPassword.SelectionStart = 0;
            this.txtPassword.Size = new System.Drawing.Size(239, 23);
            this.txtPassword.TabIndex = 1;
            this.txtPassword.TabStop = false;
            this.txtPassword.UseSystemPasswordChar = true;
            this.txtPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPassword_KeyDown);
            // 
            // txtUserName
            // 
            this.txtUserName.CausesValidation = false;
            this.txtUserName.Depth = 0;
            this.txtUserName.Hint = "";
            this.txtUserName.Location = new System.Drawing.Point(28, 114);
            this.txtUserName.MouseState = MaterialSkin.MouseState.HOVER;
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.PasswordChar = '\0';
            this.txtUserName.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtUserName.SelectedText = "";
            this.txtUserName.SelectionLength = 0;
            this.txtUserName.SelectionStart = 0;
            this.txtUserName.Size = new System.Drawing.Size(239, 23);
            this.txtUserName.TabIndex = 0;
            this.txtUserName.TabStop = false;
            this.txtUserName.UseSystemPasswordChar = false;
            this.txtUserName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtUserName_KeyDown);
            // 
            // simpleButton2
            // 
            this.simpleButton2.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Danger;
            this.simpleButton2.Appearance.BackColor2 = System.Drawing.Color.DodgerBlue;
            this.simpleButton2.Appearance.Options.UseBackColor = true;
            this.simpleButton2.Location = new System.Drawing.Point(28, 247);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(116, 40);
            this.simpleButton2.TabIndex = 21;
            this.simpleButton2.Text = "اغلاق";
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // FrmLogin
            // 
            this.AcceptButton = this.btnLogin;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(296, 314);
            this.Controls.Add(this.simpleButton2);
            this.Controls.Add(this.jGradientPanel1);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtUserName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(296, 314);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(296, 314);
            this.Name = "FrmLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "تسجيل الدخول";
            this.jGradientPanel1.ResumeLayout(false);
            this.jGradientPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private JGradient_Panel.JGradientPanel jGradientPanel1;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton btnLogin;
        private MaterialSkin.Controls.MaterialSingleLineTextField txtPassword;
        private MaterialSkin.Controls.MaterialSingleLineTextField txtUserName;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
    }
}