
namespace PointOfSaleSedek._0_Authentication
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLogin));
            this.jGradientPanel1 = new JGradient_Panel.JGradientPanel();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.btnLogin = new DevExpress.XtraEditors.SimpleButton();
            this.txtPassword = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.txtUserName = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.radioBtnArbc = new MaterialSkin.Controls.MaterialRadioButton();
            this.radioBtnEnglish = new MaterialSkin.Controls.MaterialRadioButton();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
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
            // btnLogin
            // 
            this.btnLogin.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Success;
            this.btnLogin.Appearance.BackColor2 = System.Drawing.Color.Lime;
            this.btnLogin.Appearance.Options.UseBackColor = true;
            this.btnLogin.Location = new System.Drawing.Point(151, 271);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
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
            this.txtUserName.Click += new System.EventHandler(this.txtUserName_Click);
            this.txtUserName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtUserName_KeyDown);
            // 
            // simpleButton2
            // 
            this.simpleButton2.Appearance.BackColor = DevExpress.LookAndFeel.DXSkinColors.FillColors.Danger;
            this.simpleButton2.Appearance.BackColor2 = System.Drawing.Color.DodgerBlue;
            this.simpleButton2.Appearance.Options.UseBackColor = true;
            this.simpleButton2.Location = new System.Drawing.Point(28, 271);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(116, 40);
            this.simpleButton2.TabIndex = 21;
            this.simpleButton2.Text = "اغلاق";
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // radioBtnArbc
            // 
            this.radioBtnArbc.AutoSize = true;
            this.radioBtnArbc.Depth = 0;
            this.radioBtnArbc.Font = new System.Drawing.Font("Roboto", 10F);
            this.radioBtnArbc.Location = new System.Drawing.Point(211, 225);
            this.radioBtnArbc.Margin = new System.Windows.Forms.Padding(0);
            this.radioBtnArbc.MouseLocation = new System.Drawing.Point(-1, -1);
            this.radioBtnArbc.MouseState = MaterialSkin.MouseState.HOVER;
            this.radioBtnArbc.Name = "radioBtnArbc";
            this.radioBtnArbc.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.radioBtnArbc.Ripple = true;
            this.radioBtnArbc.Size = new System.Drawing.Size(56, 30);
            this.radioBtnArbc.TabIndex = 22;
            this.radioBtnArbc.TabStop = true;
            this.radioBtnArbc.Text = "عربي";
            this.radioBtnArbc.UseVisualStyleBackColor = true;
            this.radioBtnArbc.CheckedChanged += new System.EventHandler(this.radioBtnArbc_CheckedChanged);
            // 
            // radioBtnEnglish
            // 
            this.radioBtnEnglish.AutoSize = true;
            this.radioBtnEnglish.Depth = 0;
            this.radioBtnEnglish.Font = new System.Drawing.Font("Roboto", 10F);
            this.radioBtnEnglish.Location = new System.Drawing.Point(28, 225);
            this.radioBtnEnglish.Margin = new System.Windows.Forms.Padding(0);
            this.radioBtnEnglish.MouseLocation = new System.Drawing.Point(-1, -1);
            this.radioBtnEnglish.MouseState = MaterialSkin.MouseState.HOVER;
            this.radioBtnEnglish.Name = "radioBtnEnglish";
            this.radioBtnEnglish.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.radioBtnEnglish.Ripple = true;
            this.radioBtnEnglish.Size = new System.Drawing.Size(74, 30);
            this.radioBtnEnglish.TabIndex = 23;
            this.radioBtnEnglish.TabStop = true;
            this.radioBtnEnglish.Text = "English";
            this.radioBtnEnglish.UseVisualStyleBackColor = true;
            this.radioBtnEnglish.CheckedChanged += new System.EventHandler(this.radioBtnEnglish_CheckedChanged);
            this.radioBtnEnglish.KeyUp += new System.Windows.Forms.KeyEventHandler(this.radioBtnEnglish_KeyUp);
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
            this.labelControl1.Appearance.Options.UseTextOptions = true;
            this.labelControl1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.labelControl1.LineVisible = true;
            this.labelControl1.Location = new System.Drawing.Point(193, 95);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(74, 13);
            this.labelControl1.TabIndex = 24;
            this.labelControl1.Text = "اسم المستخدم";
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.ForeColor = System.Drawing.Color.Red;
            this.labelControl4.Appearance.Options.UseForeColor = true;
            this.labelControl4.Location = new System.Drawing.Point(125, 332);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(35, 13);
            this.labelControl4.TabIndex = 25;
            this.labelControl4.Text = "Version";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(126, 348);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(32, 13);
            this.labelControl5.TabIndex = 26;
            this.labelControl5.Text = "10.0.1";
            // 
            // labelControl6
            // 
            this.labelControl6.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.labelControl6.Appearance.ForeColor = System.Drawing.Color.White;
            this.labelControl6.Appearance.Options.UseBackColor = true;
            this.labelControl6.Appearance.Options.UseForeColor = true;
            this.labelControl6.Location = new System.Drawing.Point(0, 343);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Padding = new System.Windows.Forms.Padding(5);
            this.labelControl6.Size = new System.Drawing.Size(40, 23);
            this.labelControl6.TabIndex = 27;
            this.labelControl6.Text = "Active";
            // 
            // FrmLogin
            // 
            this.AcceptButton = this.btnLogin;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(296, 366);
            this.Controls.Add(this.labelControl6);
            this.Controls.Add(this.labelControl5);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.radioBtnEnglish);
            this.Controls.Add(this.radioBtnArbc);
            this.Controls.Add(this.simpleButton2);
            this.Controls.Add(this.jGradientPanel1);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtUserName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(296, 314);
            this.Name = "FrmLogin";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
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
        private DevExpress.XtraEditors.SimpleButton btnLogin;
        private MaterialSkin.Controls.MaterialSingleLineTextField txtPassword;
        private MaterialSkin.Controls.MaterialSingleLineTextField txtUserName;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private MaterialSkin.Controls.MaterialRadioButton radioBtnArbc;
        private MaterialSkin.Controls.MaterialRadioButton radioBtnEnglish;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl6;
    }
}