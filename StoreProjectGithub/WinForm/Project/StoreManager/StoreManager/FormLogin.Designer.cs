namespace StoreManager
{
    partial class formLogin
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
            buttonLogin = new Button();
            buttonExit = new Button();
            panelPassword = new Panel();
            textBoxPassword = new TextBox();
            labelPassword = new Label();
            labelLogin = new Label();
            textBoxLogin = new TextBox();
            panelLogin = new Panel();
            panelPassword.SuspendLayout();
            panelLogin.SuspendLayout();
            SuspendLayout();
            // 
            // buttonLogin
            // 
            buttonLogin.Font = new Font("Segoe UI", 7.8F, FontStyle.Regular, GraphicsUnit.Point);
            buttonLogin.Location = new Point(223, 158);
            buttonLogin.Name = "buttonLogin";
            buttonLogin.Size = new Size(110, 26);
            buttonLogin.TabIndex = 3;
            buttonLogin.Text = "Đăng nhập";
            buttonLogin.UseVisualStyleBackColor = true;
            buttonLogin.Click += buttonLogin_Click;
            // 
            // buttonExit
            // 
            buttonExit.Font = new Font("Segoe UI", 7.8F, FontStyle.Regular, GraphicsUnit.Point);
            buttonExit.Location = new Point(349, 158);
            buttonExit.Name = "buttonExit";
            buttonExit.Size = new Size(110, 26);
            buttonExit.TabIndex = 4;
            buttonExit.Text = "Thoát";
            buttonExit.UseVisualStyleBackColor = true;
            buttonExit.Click += buttonExit_Click;
            // 
            // panelPassword
            // 
            panelPassword.Controls.Add(textBoxPassword);
            panelPassword.Controls.Add(labelPassword);
            panelPassword.Location = new Point(15, 90);
            panelPassword.Name = "panelPassword";
            panelPassword.Size = new Size(460, 64);
            panelPassword.TabIndex = 2;
            // 
            // textBoxPassword
            // 
            textBoxPassword.Font = new Font("Open Sans", 10.2F, FontStyle.Regular, GraphicsUnit.Point);
            textBoxPassword.Location = new Point(135, 14);
            textBoxPassword.Name = "textBoxPassword";
            textBoxPassword.Size = new Size(312, 31);
            textBoxPassword.TabIndex = 1;
            textBoxPassword.Text = "123";
            textBoxPassword.UseSystemPasswordChar = true;
            // 
            // labelPassword
            // 
            labelPassword.AutoSize = true;
            labelPassword.Font = new Font("Open Sans", 10.2F, FontStyle.Regular, GraphicsUnit.Point);
            labelPassword.Location = new Point(42, 17);
            labelPassword.Name = "labelPassword";
            labelPassword.Size = new Size(87, 23);
            labelPassword.TabIndex = 0;
            labelPassword.Text = "Mật khẩu:";
            // 
            // labelLogin
            // 
            labelLogin.AutoSize = true;
            labelLogin.Font = new Font("Open Sans", 10.2F, FontStyle.Regular, GraphicsUnit.Point);
            labelLogin.Location = new Point(3, 17);
            labelLogin.Name = "labelLogin";
            labelLogin.Size = new Size(129, 23);
            labelLogin.TabIndex = 0;
            labelLogin.Text = "Tên đăng nhập:";
            labelLogin.Click += label1_Click_1;
            // 
            // textBoxLogin
            // 
            textBoxLogin.Font = new Font("Open Sans", 10.2F, FontStyle.Regular, GraphicsUnit.Point);
            textBoxLogin.Location = new Point(138, 14);
            textBoxLogin.Name = "textBoxLogin";
            textBoxLogin.Size = new Size(312, 31);
            textBoxLogin.TabIndex = 1;
            textBoxLogin.Text = "Khang";
            // 
            // panelLogin
            // 
            panelLogin.Controls.Add(textBoxLogin);
            panelLogin.Controls.Add(labelLogin);
            panelLogin.Location = new Point(12, 20);
            panelLogin.Name = "panelLogin";
            panelLogin.Size = new Size(463, 64);
            panelLogin.TabIndex = 0;
            // 
            // formLogin
            // 
            AcceptButton = buttonLogin;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ClientSize = new Size(483, 204);
            Controls.Add(panelPassword);
            Controls.Add(buttonExit);
            Controls.Add(buttonLogin);
            Controls.Add(panelLogin);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "formLogin";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Đăng nhập";
            FormClosing += formLogin_FormClosing;
            Load += formLogin_Load;
            panelPassword.ResumeLayout(false);
            panelPassword.PerformLayout();
            panelLogin.ResumeLayout(false);
            panelLogin.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private Button buttonLogin;
        private Button buttonExit;
        private Panel panelPassword;
        private TextBox textBoxPassword;
        private Label labelPassword;
        private Label labelLogin;
        private TextBox textBoxLogin;
        private Panel panelLogin;
    }
}