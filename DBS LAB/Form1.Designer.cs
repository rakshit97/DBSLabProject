namespace DBS_LAB
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.u_text = new System.Windows.Forms.TextBox();
            this.p_text = new System.Windows.Forms.TextBox();
            this.u_label = new System.Windows.Forms.Label();
            this.p_label = new System.Windows.Forms.Label();
            this.button_login = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.register = new System.Windows.Forms.LinkLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // u_text
            // 
            this.u_text.Location = new System.Drawing.Point(108, 22);
            this.u_text.Name = "u_text";
            this.u_text.Size = new System.Drawing.Size(132, 20);
            this.u_text.TabIndex = 0;
            // 
            // p_text
            // 
            this.p_text.Location = new System.Drawing.Point(108, 93);
            this.p_text.Name = "p_text";
            this.p_text.Size = new System.Drawing.Size(132, 20);
            this.p_text.TabIndex = 1;
            this.p_text.UseSystemPasswordChar = true;
            // 
            // u_label
            // 
            this.u_label.AutoSize = true;
            this.u_label.Location = new System.Drawing.Point(22, 25);
            this.u_label.Name = "u_label";
            this.u_label.Size = new System.Drawing.Size(55, 13);
            this.u_label.TabIndex = 2;
            this.u_label.Text = "Username";
            // 
            // p_label
            // 
            this.p_label.AutoSize = true;
            this.p_label.Location = new System.Drawing.Point(24, 93);
            this.p_label.Name = "p_label";
            this.p_label.Size = new System.Drawing.Size(53, 13);
            this.p_label.TabIndex = 3;
            this.p_label.Text = "Password";
            // 
            // button_login
            // 
            this.button_login.Location = new System.Drawing.Point(89, 176);
            this.button_login.Name = "button_login";
            this.button_login.Size = new System.Drawing.Size(101, 35);
            this.button_login.TabIndex = 4;
            this.button_login.Text = "Login";
            this.button_login.UseVisualStyleBackColor = true;
            this.button_login.Click += new System.EventHandler(this.button_login_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(49, 234);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "New User?";
            // 
            // register
            // 
            this.register.AutoSize = true;
            this.register.Location = new System.Drawing.Point(115, 234);
            this.register.Name = "register";
            this.register.Size = new System.Drawing.Size(98, 13);
            this.register.TabIndex = 6;
            this.register.TabStop = true;
            this.register.Text = "Create an account!";
            this.register.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.register_LinkClicked);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.u_text);
            this.panel1.Controls.Add(this.register);
            this.panel1.Controls.Add(this.p_text);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.u_label);
            this.panel1.Controls.Add(this.button_login);
            this.panel1.Controls.Add(this.p_label);
            this.panel1.Location = new System.Drawing.Point(97, 52);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(267, 260);
            this.panel1.TabIndex = 7;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(825, 357);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox u_text;
        private System.Windows.Forms.TextBox p_text;
        private System.Windows.Forms.Label u_label;
        private System.Windows.Forms.Label p_label;
        private System.Windows.Forms.Button button_login;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel register;
        private System.Windows.Forms.Panel panel1;
    }
}

