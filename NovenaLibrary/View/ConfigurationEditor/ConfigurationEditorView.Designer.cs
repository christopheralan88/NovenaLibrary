namespace NovenaLibrary.View.ConfigurationEditor
{
    partial class ConfigurationEditorView
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
            this.tabControl_edit_config = new System.Windows.Forms.TabControl();
            this.tab_add_connection = new System.Windows.Forms.TabPage();
            this.picBox_add = new System.Windows.Forms.PictureBox();
            this.txtbox_conn_nickname = new System.Windows.Forms.TextBox();
            this.lbl_conn_nickname = new System.Windows.Forms.Label();
            this.but_cancel_add = new System.Windows.Forms.Button();
            this.but_save_add = new System.Windows.Forms.Button();
            this.txtbox_conn_string_add = new System.Windows.Forms.TextBox();
            this.lbl_conn_string_add = new System.Windows.Forms.Label();
            this.cbox_db_type_add = new System.Windows.Forms.ComboBox();
            this.lbl_db_type_add = new System.Windows.Forms.Label();
            this.tab_edit_connection = new System.Windows.Forms.TabPage();
            this.picBox_edit = new System.Windows.Forms.PictureBox();
            this.but_delete = new System.Windows.Forms.Button();
            this.but_load_connection = new System.Windows.Forms.Button();
            this.but_cancel_edit = new System.Windows.Forms.Button();
            this.but_save_edit = new System.Windows.Forms.Button();
            this.txtbox_conn_string_edit = new System.Windows.Forms.TextBox();
            this.lbl_connection_string_edit = new System.Windows.Forms.Label();
            this.cbox_db_type_edit = new System.Windows.Forms.ComboBox();
            this.lbl_db_type_edit = new System.Windows.Forms.Label();
            this.lbl_available_connections = new System.Windows.Forms.Label();
            this.lbox_available_connections = new System.Windows.Forms.ListBox();
            this.tabControl_edit_config.SuspendLayout();
            this.tab_add_connection.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_add)).BeginInit();
            this.tab_edit_connection.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_edit)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl_edit_config
            // 
            this.tabControl_edit_config.Controls.Add(this.tab_add_connection);
            this.tabControl_edit_config.Controls.Add(this.tab_edit_connection);
            this.tabControl_edit_config.Location = new System.Drawing.Point(78, 47);
            this.tabControl_edit_config.Margin = new System.Windows.Forms.Padding(2);
            this.tabControl_edit_config.Name = "tabControl_edit_config";
            this.tabControl_edit_config.SelectedIndex = 0;
            this.tabControl_edit_config.Size = new System.Drawing.Size(828, 451);
            this.tabControl_edit_config.TabIndex = 1;
            // 
            // tab_add_connection
            // 
            this.tab_add_connection.Controls.Add(this.picBox_add);
            this.tab_add_connection.Controls.Add(this.txtbox_conn_nickname);
            this.tab_add_connection.Controls.Add(this.lbl_conn_nickname);
            this.tab_add_connection.Controls.Add(this.but_cancel_add);
            this.tab_add_connection.Controls.Add(this.but_save_add);
            this.tab_add_connection.Controls.Add(this.txtbox_conn_string_add);
            this.tab_add_connection.Controls.Add(this.lbl_conn_string_add);
            this.tab_add_connection.Controls.Add(this.cbox_db_type_add);
            this.tab_add_connection.Controls.Add(this.lbl_db_type_add);
            this.tab_add_connection.Location = new System.Drawing.Point(4, 22);
            this.tab_add_connection.Margin = new System.Windows.Forms.Padding(2);
            this.tab_add_connection.Name = "tab_add_connection";
            this.tab_add_connection.Padding = new System.Windows.Forms.Padding(2);
            this.tab_add_connection.Size = new System.Drawing.Size(820, 425);
            this.tab_add_connection.TabIndex = 0;
            this.tab_add_connection.Text = "Add Connection";
            this.tab_add_connection.UseVisualStyleBackColor = true;
            // 
            // picBox_add
            // 
            this.picBox_add.Location = new System.Drawing.Point(595, 31);
            this.picBox_add.Name = "picBox_add";
            this.picBox_add.Size = new System.Drawing.Size(185, 120);
            this.picBox_add.TabIndex = 8;
            this.picBox_add.TabStop = false;
            // 
            // txtbox_conn_nickname
            // 
            this.txtbox_conn_nickname.Location = new System.Drawing.Point(35, 31);
            this.txtbox_conn_nickname.Margin = new System.Windows.Forms.Padding(2);
            this.txtbox_conn_nickname.Name = "txtbox_conn_nickname";
            this.txtbox_conn_nickname.Size = new System.Drawing.Size(210, 20);
            this.txtbox_conn_nickname.TabIndex = 7;
            // 
            // lbl_conn_nickname
            // 
            this.lbl_conn_nickname.AutoSize = true;
            this.lbl_conn_nickname.Location = new System.Drawing.Point(33, 15);
            this.lbl_conn_nickname.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_conn_nickname.Name = "lbl_conn_nickname";
            this.lbl_conn_nickname.Size = new System.Drawing.Size(112, 13);
            this.lbl_conn_nickname.TabIndex = 6;
            this.lbl_conn_nickname.Text = "Connection Nickname";
            // 
            // but_cancel_add
            // 
            this.but_cancel_add.Location = new System.Drawing.Point(116, 161);
            this.but_cancel_add.Margin = new System.Windows.Forms.Padding(2);
            this.but_cancel_add.Name = "but_cancel_add";
            this.but_cancel_add.Size = new System.Drawing.Size(65, 19);
            this.but_cancel_add.TabIndex = 5;
            this.but_cancel_add.Text = "Cancel";
            this.but_cancel_add.UseVisualStyleBackColor = true;
            // 
            // but_save_add
            // 
            this.but_save_add.Location = new System.Drawing.Point(35, 161);
            this.but_save_add.Margin = new System.Windows.Forms.Padding(2);
            this.but_save_add.Name = "but_save_add";
            this.but_save_add.Size = new System.Drawing.Size(69, 19);
            this.but_save_add.TabIndex = 4;
            this.but_save_add.Text = "Save";
            this.but_save_add.UseVisualStyleBackColor = true;
            // 
            // txtbox_conn_string_add
            // 
            this.txtbox_conn_string_add.Location = new System.Drawing.Point(35, 132);
            this.txtbox_conn_string_add.Margin = new System.Windows.Forms.Padding(2);
            this.txtbox_conn_string_add.Name = "txtbox_conn_string_add";
            this.txtbox_conn_string_add.Size = new System.Drawing.Size(522, 20);
            this.txtbox_conn_string_add.TabIndex = 3;
            // 
            // lbl_conn_string_add
            // 
            this.lbl_conn_string_add.AutoSize = true;
            this.lbl_conn_string_add.Location = new System.Drawing.Point(33, 115);
            this.lbl_conn_string_add.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_conn_string_add.Name = "lbl_conn_string_add";
            this.lbl_conn_string_add.Size = new System.Drawing.Size(91, 13);
            this.lbl_conn_string_add.TabIndex = 2;
            this.lbl_conn_string_add.Text = "Connection String";
            // 
            // cbox_db_type_add
            // 
            this.cbox_db_type_add.FormattingEnabled = true;
            this.cbox_db_type_add.Items.AddRange(new object[] {
            "PostgreSQL",
            "Oracle",
            "MySQL",
            "MS SQL Server",
            "Redshift",
            "MS Access",
            "SQLite"});
            this.cbox_db_type_add.Location = new System.Drawing.Point(35, 81);
            this.cbox_db_type_add.Margin = new System.Windows.Forms.Padding(2);
            this.cbox_db_type_add.Name = "cbox_db_type_add";
            this.cbox_db_type_add.Size = new System.Drawing.Size(92, 21);
            this.cbox_db_type_add.TabIndex = 1;
            // 
            // lbl_db_type_add
            // 
            this.lbl_db_type_add.AutoSize = true;
            this.lbl_db_type_add.Location = new System.Drawing.Point(33, 65);
            this.lbl_db_type_add.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_db_type_add.Name = "lbl_db_type_add";
            this.lbl_db_type_add.Size = new System.Drawing.Size(80, 13);
            this.lbl_db_type_add.TabIndex = 0;
            this.lbl_db_type_add.Text = "Database Type";
            // 
            // tab_edit_connection
            // 
            this.tab_edit_connection.Controls.Add(this.picBox_edit);
            this.tab_edit_connection.Controls.Add(this.but_delete);
            this.tab_edit_connection.Controls.Add(this.but_load_connection);
            this.tab_edit_connection.Controls.Add(this.but_cancel_edit);
            this.tab_edit_connection.Controls.Add(this.but_save_edit);
            this.tab_edit_connection.Controls.Add(this.txtbox_conn_string_edit);
            this.tab_edit_connection.Controls.Add(this.lbl_connection_string_edit);
            this.tab_edit_connection.Controls.Add(this.cbox_db_type_edit);
            this.tab_edit_connection.Controls.Add(this.lbl_db_type_edit);
            this.tab_edit_connection.Controls.Add(this.lbl_available_connections);
            this.tab_edit_connection.Controls.Add(this.lbox_available_connections);
            this.tab_edit_connection.Location = new System.Drawing.Point(4, 22);
            this.tab_edit_connection.Margin = new System.Windows.Forms.Padding(2);
            this.tab_edit_connection.Name = "tab_edit_connection";
            this.tab_edit_connection.Padding = new System.Windows.Forms.Padding(2);
            this.tab_edit_connection.Size = new System.Drawing.Size(820, 425);
            this.tab_edit_connection.TabIndex = 1;
            this.tab_edit_connection.Text = "Edit Connection";
            this.tab_edit_connection.UseVisualStyleBackColor = true;
            // 
            // picBox_edit
            // 
            this.picBox_edit.Location = new System.Drawing.Point(616, 13);
            this.picBox_edit.Name = "picBox_edit";
            this.picBox_edit.Size = new System.Drawing.Size(187, 147);
            this.picBox_edit.TabIndex = 14;
            this.picBox_edit.TabStop = false;
            // 
            // but_delete
            // 
            this.but_delete.Location = new System.Drawing.Point(381, 141);
            this.but_delete.Margin = new System.Windows.Forms.Padding(2);
            this.but_delete.Name = "but_delete";
            this.but_delete.Size = new System.Drawing.Size(85, 19);
            this.but_delete.TabIndex = 13;
            this.but_delete.Text = "Delete";
            this.but_delete.UseVisualStyleBackColor = true;
            // 
            // but_load_connection
            // 
            this.but_load_connection.Location = new System.Drawing.Point(304, 177);
            this.but_load_connection.Margin = new System.Windows.Forms.Padding(2);
            this.but_load_connection.Name = "but_load_connection";
            this.but_load_connection.Size = new System.Drawing.Size(73, 43);
            this.but_load_connection.TabIndex = 12;
            this.but_load_connection.Text = "Load Connection";
            this.but_load_connection.UseVisualStyleBackColor = true;
            // 
            // but_cancel_edit
            // 
            this.but_cancel_edit.Location = new System.Drawing.Point(470, 141);
            this.but_cancel_edit.Margin = new System.Windows.Forms.Padding(2);
            this.but_cancel_edit.Name = "but_cancel_edit";
            this.but_cancel_edit.Size = new System.Drawing.Size(78, 19);
            this.but_cancel_edit.TabIndex = 11;
            this.but_cancel_edit.Text = "Cancel";
            this.but_cancel_edit.UseVisualStyleBackColor = true;
            // 
            // but_save_edit
            // 
            this.but_save_edit.Location = new System.Drawing.Point(306, 141);
            this.but_save_edit.Margin = new System.Windows.Forms.Padding(2);
            this.but_save_edit.Name = "but_save_edit";
            this.but_save_edit.Size = new System.Drawing.Size(71, 19);
            this.but_save_edit.TabIndex = 10;
            this.but_save_edit.Text = "Save";
            this.but_save_edit.UseVisualStyleBackColor = true;
            // 
            // txtbox_conn_string_edit
            // 
            this.txtbox_conn_string_edit.Location = new System.Drawing.Point(304, 99);
            this.txtbox_conn_string_edit.Margin = new System.Windows.Forms.Padding(2);
            this.txtbox_conn_string_edit.Name = "txtbox_conn_string_edit";
            this.txtbox_conn_string_edit.Size = new System.Drawing.Size(272, 20);
            this.txtbox_conn_string_edit.TabIndex = 9;
            // 
            // lbl_connection_string_edit
            // 
            this.lbl_connection_string_edit.AutoSize = true;
            this.lbl_connection_string_edit.Location = new System.Drawing.Point(302, 83);
            this.lbl_connection_string_edit.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_connection_string_edit.Name = "lbl_connection_string_edit";
            this.lbl_connection_string_edit.Size = new System.Drawing.Size(91, 13);
            this.lbl_connection_string_edit.TabIndex = 8;
            this.lbl_connection_string_edit.Text = "Connection String";
            // 
            // cbox_db_type_edit
            // 
            this.cbox_db_type_edit.FormattingEnabled = true;
            this.cbox_db_type_edit.Items.AddRange(new object[] {
            "PostgreSQL",
            "Oracle",
            "MySQL",
            "MS SQL Server",
            "Redshift",
            "MS Access",
            "SQLite"});
            this.cbox_db_type_edit.Location = new System.Drawing.Point(305, 45);
            this.cbox_db_type_edit.Margin = new System.Windows.Forms.Padding(2);
            this.cbox_db_type_edit.Name = "cbox_db_type_edit";
            this.cbox_db_type_edit.Size = new System.Drawing.Size(92, 21);
            this.cbox_db_type_edit.TabIndex = 7;
            // 
            // lbl_db_type_edit
            // 
            this.lbl_db_type_edit.AutoSize = true;
            this.lbl_db_type_edit.Location = new System.Drawing.Point(303, 28);
            this.lbl_db_type_edit.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_db_type_edit.Name = "lbl_db_type_edit";
            this.lbl_db_type_edit.Size = new System.Drawing.Size(80, 13);
            this.lbl_db_type_edit.TabIndex = 6;
            this.lbl_db_type_edit.Text = "Database Type";
            // 
            // lbl_available_connections
            // 
            this.lbl_available_connections.AutoSize = true;
            this.lbl_available_connections.Location = new System.Drawing.Point(14, 11);
            this.lbl_available_connections.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_available_connections.Name = "lbl_available_connections";
            this.lbl_available_connections.Size = new System.Drawing.Size(112, 13);
            this.lbl_available_connections.TabIndex = 1;
            this.lbl_available_connections.Text = "Available Connections";
            // 
            // lbox_available_connections
            // 
            this.lbox_available_connections.FormattingEnabled = true;
            this.lbox_available_connections.Location = new System.Drawing.Point(14, 28);
            this.lbox_available_connections.Margin = new System.Windows.Forms.Padding(2);
            this.lbox_available_connections.Name = "lbox_available_connections";
            this.lbox_available_connections.Size = new System.Drawing.Size(229, 381);
            this.lbox_available_connections.TabIndex = 0;
            // 
            // ConfigurationEditorView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(988, 563);
            this.Controls.Add(this.tabControl_edit_config);
            this.Name = "ConfigurationEditorView";
            this.Text = "ConfigurationEditorView";
            this.tabControl_edit_config.ResumeLayout(false);
            this.tab_add_connection.ResumeLayout(false);
            this.tab_add_connection.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_add)).EndInit();
            this.tab_edit_connection.ResumeLayout(false);
            this.tab_edit_connection.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_edit)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl_edit_config;
        private System.Windows.Forms.TabPage tab_add_connection;
        private System.Windows.Forms.PictureBox picBox_add;
        private System.Windows.Forms.TextBox txtbox_conn_nickname;
        private System.Windows.Forms.Label lbl_conn_nickname;
        private System.Windows.Forms.Button but_cancel_add;
        private System.Windows.Forms.Button but_save_add;
        private System.Windows.Forms.TextBox txtbox_conn_string_add;
        private System.Windows.Forms.Label lbl_conn_string_add;
        private System.Windows.Forms.ComboBox cbox_db_type_add;
        private System.Windows.Forms.Label lbl_db_type_add;
        private System.Windows.Forms.TabPage tab_edit_connection;
        private System.Windows.Forms.PictureBox picBox_edit;
        private System.Windows.Forms.Button but_delete;
        private System.Windows.Forms.Button but_load_connection;
        private System.Windows.Forms.Button but_cancel_edit;
        private System.Windows.Forms.Button but_save_edit;
        private System.Windows.Forms.TextBox txtbox_conn_string_edit;
        private System.Windows.Forms.Label lbl_connection_string_edit;
        private System.Windows.Forms.ComboBox cbox_db_type_edit;
        private System.Windows.Forms.Label lbl_db_type_edit;
        private System.Windows.Forms.Label lbl_available_connections;
        private System.Windows.Forms.ListBox lbox_available_connections;
    }
}