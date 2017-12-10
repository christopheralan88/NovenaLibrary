namespace NovenaLibrary.View.LogIn
{
    partial class LogInView
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
            this.lbl_passwordMessage = new System.Windows.Forms.Label();
            this.lbl_usernameMessage = new System.Windows.Forms.Label();
            this.button_Cancel = new System.Windows.Forms.Button();
            this.button_SignIn = new System.Windows.Forms.Button();
            this.label_password = new System.Windows.Forms.Label();
            this.label_username = new System.Windows.Forms.Label();
            this.txtBox_password = new System.Windows.Forms.TextBox();
            this.txtBox_username = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lbl_passwordMessage
            // 
            this.lbl_passwordMessage.AutoSize = true;
            this.lbl_passwordMessage.Location = new System.Drawing.Point(143, 83);
            this.lbl_passwordMessage.Name = "lbl_passwordMessage";
            this.lbl_passwordMessage.Size = new System.Drawing.Size(0, 13);
            this.lbl_passwordMessage.TabIndex = 15;
            // 
            // lbl_usernameMessage
            // 
            this.lbl_usernameMessage.AutoSize = true;
            this.lbl_usernameMessage.Location = new System.Drawing.Point(143, 19);
            this.lbl_usernameMessage.Name = "lbl_usernameMessage";
            this.lbl_usernameMessage.Size = new System.Drawing.Size(0, 13);
            this.lbl_usernameMessage.TabIndex = 14;
            // 
            // button_Cancel
            // 
            this.button_Cancel.Location = new System.Drawing.Point(246, 154);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(75, 23);
            this.button_Cancel.TabIndex = 13;
            this.button_Cancel.Text = "Cancel";
            this.button_Cancel.UseVisualStyleBackColor = true;
            // 
            // button_SignIn
            // 
            this.button_SignIn.Location = new System.Drawing.Point(147, 154);
            this.button_SignIn.Name = "button_SignIn";
            this.button_SignIn.Size = new System.Drawing.Size(75, 23);
            this.button_SignIn.TabIndex = 12;
            this.button_SignIn.Text = "Sign In";
            this.button_SignIn.UseVisualStyleBackColor = true;
            // 
            // label_password
            // 
            this.label_password.AutoSize = true;
            this.label_password.Location = new System.Drawing.Point(84, 102);
            this.label_password.Name = "label_password";
            this.label_password.Size = new System.Drawing.Size(53, 13);
            this.label_password.TabIndex = 11;
            this.label_password.Text = "Password";
            // 
            // label_username
            // 
            this.label_username.AutoSize = true;
            this.label_username.Location = new System.Drawing.Point(82, 43);
            this.label_username.Name = "label_username";
            this.label_username.Size = new System.Drawing.Size(55, 13);
            this.label_username.TabIndex = 10;
            this.label_username.Text = "Username";
            // 
            // txtBox_password
            // 
            this.txtBox_password.Location = new System.Drawing.Point(143, 99);
            this.txtBox_password.Name = "txtBox_password";
            this.txtBox_password.PasswordChar = '*';
            this.txtBox_password.Size = new System.Drawing.Size(188, 20);
            this.txtBox_password.TabIndex = 9;
            // 
            // txtBox_username
            // 
            this.txtBox_username.Location = new System.Drawing.Point(143, 43);
            this.txtBox_username.Name = "txtBox_username";
            this.txtBox_username.Size = new System.Drawing.Size(188, 20);
            this.txtBox_username.TabIndex = 8;
            // 
            // LogInView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(445, 206);
            this.Controls.Add(this.lbl_passwordMessage);
            this.Controls.Add(this.lbl_usernameMessage);
            this.Controls.Add(this.button_Cancel);
            this.Controls.Add(this.button_SignIn);
            this.Controls.Add(this.label_password);
            this.Controls.Add(this.label_username);
            this.Controls.Add(this.txtBox_password);
            this.Controls.Add(this.txtBox_username);
            this.Name = "LogInView";
            this.Text = "LogInView";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_passwordMessage;
        private System.Windows.Forms.Label lbl_usernameMessage;
        private System.Windows.Forms.Button button_Cancel;
        private System.Windows.Forms.Button button_SignIn;
        private System.Windows.Forms.Label label_password;
        private System.Windows.Forms.Label label_username;
        private System.Windows.Forms.TextBox txtBox_password;
        private System.Windows.Forms.TextBox txtBox_username;
    }
}