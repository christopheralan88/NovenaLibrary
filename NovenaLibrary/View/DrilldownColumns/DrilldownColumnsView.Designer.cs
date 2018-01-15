namespace NovenaLibrary.View.DrilldownColumns
{
    partial class DrilldownColumns
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
            this.but_cancel = new System.Windows.Forms.Button();
            this.but_ok = new System.Windows.Forms.Button();
            this.but_remove = new System.Windows.Forms.Button();
            this.but_add = new System.Windows.Forms.Button();
            this.lbl_selected_columns = new System.Windows.Forms.Label();
            this.lbox_selected_columns = new System.Windows.Forms.ListBox();
            this.lbl_available_columns = new System.Windows.Forms.Label();
            this.lbox_available_columns = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // but_cancel
            // 
            this.but_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.but_cancel.Location = new System.Drawing.Point(584, 371);
            this.but_cancel.Name = "but_cancel";
            this.but_cancel.Size = new System.Drawing.Size(75, 23);
            this.but_cancel.TabIndex = 25;
            this.but_cancel.Text = "Cancel";
            this.but_cancel.UseVisualStyleBackColor = true;
            // 
            // but_ok
            // 
            this.but_ok.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.but_ok.Location = new System.Drawing.Point(485, 371);
            this.but_ok.Name = "but_ok";
            this.but_ok.Size = new System.Drawing.Size(75, 23);
            this.but_ok.TabIndex = 24;
            this.but_ok.Text = "OK";
            this.but_ok.UseVisualStyleBackColor = true;
            // 
            // but_remove
            // 
            this.but_remove.Location = new System.Drawing.Point(335, 215);
            this.but_remove.Name = "but_remove";
            this.but_remove.Size = new System.Drawing.Size(75, 23);
            this.but_remove.TabIndex = 23;
            this.but_remove.Text = "<<<";
            this.but_remove.UseVisualStyleBackColor = true;
            // 
            // but_add
            // 
            this.but_add.Location = new System.Drawing.Point(335, 186);
            this.but_add.Name = "but_add";
            this.but_add.Size = new System.Drawing.Size(75, 23);
            this.but_add.TabIndex = 22;
            this.but_add.Text = ">>>";
            this.but_add.UseVisualStyleBackColor = true;
            // 
            // lbl_selected_columns
            // 
            this.lbl_selected_columns.AutoSize = true;
            this.lbl_selected_columns.Location = new System.Drawing.Point(499, 57);
            this.lbl_selected_columns.Name = "lbl_selected_columns";
            this.lbl_selected_columns.Size = new System.Drawing.Size(92, 13);
            this.lbl_selected_columns.TabIndex = 21;
            this.lbl_selected_columns.Text = "Selected Columns";
            // 
            // lbox_selected_columns
            // 
            this.lbox_selected_columns.FormattingEnabled = true;
            this.lbox_selected_columns.HorizontalScrollbar = true;
            this.lbox_selected_columns.Location = new System.Drawing.Point(427, 88);
            this.lbox_selected_columns.Name = "lbox_selected_columns";
            this.lbox_selected_columns.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbox_selected_columns.Size = new System.Drawing.Size(232, 251);
            this.lbox_selected_columns.TabIndex = 20;
            // 
            // lbl_available_columns
            // 
            this.lbl_available_columns.AutoSize = true;
            this.lbl_available_columns.Location = new System.Drawing.Point(147, 57);
            this.lbl_available_columns.Name = "lbl_available_columns";
            this.lbl_available_columns.Size = new System.Drawing.Size(93, 13);
            this.lbl_available_columns.TabIndex = 19;
            this.lbl_available_columns.Text = "Available Columns";
            // 
            // lbox_available_columns
            // 
            this.lbox_available_columns.FormattingEnabled = true;
            this.lbox_available_columns.HorizontalScrollbar = true;
            this.lbox_available_columns.Location = new System.Drawing.Point(87, 88);
            this.lbox_available_columns.Name = "lbox_available_columns";
            this.lbox_available_columns.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbox_available_columns.Size = new System.Drawing.Size(232, 251);
            this.lbox_available_columns.TabIndex = 18;
            // 
            // DrilldownColumns
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(747, 434);
            this.Controls.Add(this.but_cancel);
            this.Controls.Add(this.but_ok);
            this.Controls.Add(this.but_remove);
            this.Controls.Add(this.but_add);
            this.Controls.Add(this.lbl_selected_columns);
            this.Controls.Add(this.lbox_selected_columns);
            this.Controls.Add(this.lbl_available_columns);
            this.Controls.Add(this.lbox_available_columns);
            this.Name = "DrilldownColumns";
            this.Text = "DrilldownColumnsView";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Button but_cancel;
        internal System.Windows.Forms.Button but_ok;
        internal System.Windows.Forms.Button but_remove;
        internal System.Windows.Forms.Button but_add;
        internal System.Windows.Forms.Label lbl_selected_columns;
        internal System.Windows.Forms.ListBox lbox_selected_columns;
        internal System.Windows.Forms.Label lbl_available_columns;
        internal System.Windows.Forms.ListBox lbox_available_columns;
    }
}