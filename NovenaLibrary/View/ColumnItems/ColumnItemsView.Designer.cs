namespace NovenaLibrary.View.ColumnItems
{
    partial class ColumnItemsView
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
            this.but_add = new System.Windows.Forms.Button();
            this.but_remove = new System.Windows.Forms.Button();
            this.lbl_selected_members = new System.Windows.Forms.Label();
            this.lbox_selected_members = new System.Windows.Forms.ListBox();
            this.gbox_search = new System.Windows.Forms.GroupBox();
            this.txtBox_search = new System.Windows.Forms.TextBox();
            this.gbox_order_by = new System.Windows.Forms.GroupBox();
            this.radBut_asc = new System.Windows.Forms.RadioButton();
            this.radBut_desc = new System.Windows.Forms.RadioButton();
            this.gbox_paging = new System.Windows.Forms.GroupBox();
            this.but_find_members = new System.Windows.Forms.Button();
            this.lbl_paging = new System.Windows.Forms.Label();
            this.but_prior_page = new System.Windows.Forms.Button();
            this.cbox_paging_limit = new System.Windows.Forms.ComboBox();
            this.but_next_page = new System.Windows.Forms.Button();
            this.lbl_available_members = new System.Windows.Forms.Label();
            this.but_cancel = new System.Windows.Forms.Button();
            this.but_ok = new System.Windows.Forms.Button();
            this.lbox_available_members = new System.Windows.Forms.ListBox();
            this.gbox_search.SuspendLayout();
            this.gbox_order_by.SuspendLayout();
            this.gbox_paging.SuspendLayout();
            this.SuspendLayout();
            // 
            // but_add
            // 
            this.but_add.Location = new System.Drawing.Point(270, 274);
            this.but_add.Margin = new System.Windows.Forms.Padding(2);
            this.but_add.Name = "but_add";
            this.but_add.Size = new System.Drawing.Size(35, 19);
            this.but_add.TabIndex = 32;
            this.but_add.Text = ">>>";
            this.but_add.UseVisualStyleBackColor = true;
            // 
            // but_remove
            // 
            this.but_remove.Location = new System.Drawing.Point(270, 297);
            this.but_remove.Margin = new System.Windows.Forms.Padding(2);
            this.but_remove.Name = "but_remove";
            this.but_remove.Size = new System.Drawing.Size(35, 19);
            this.but_remove.TabIndex = 31;
            this.but_remove.Text = "<<<";
            this.but_remove.UseVisualStyleBackColor = true;
            // 
            // lbl_selected_members
            // 
            this.lbl_selected_members.AutoSize = true;
            this.lbl_selected_members.Location = new System.Drawing.Point(318, 172);
            this.lbl_selected_members.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_selected_members.Name = "lbl_selected_members";
            this.lbl_selected_members.Size = new System.Drawing.Size(133, 13);
            this.lbl_selected_members.TabIndex = 30;
            this.lbl_selected_members.Text = "Selected Column Members";
            // 
            // lbox_selected_members
            // 
            this.lbox_selected_members.FormattingEnabled = true;
            this.lbox_selected_members.Location = new System.Drawing.Point(320, 190);
            this.lbox_selected_members.Margin = new System.Windows.Forms.Padding(2);
            this.lbox_selected_members.Name = "lbox_selected_members";
            this.lbox_selected_members.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbox_selected_members.Size = new System.Drawing.Size(170, 303);
            this.lbox_selected_members.TabIndex = 29;
            // 
            // gbox_search
            // 
            this.gbox_search.Controls.Add(this.txtBox_search);
            this.gbox_search.Location = new System.Drawing.Point(107, 32);
            this.gbox_search.Margin = new System.Windows.Forms.Padding(2);
            this.gbox_search.Name = "gbox_search";
            this.gbox_search.Padding = new System.Windows.Forms.Padding(2);
            this.gbox_search.Size = new System.Drawing.Size(162, 55);
            this.gbox_search.TabIndex = 28;
            this.gbox_search.TabStop = false;
            this.gbox_search.Text = "Search";
            // 
            // txtBox_search
            // 
            this.txtBox_search.Location = new System.Drawing.Point(14, 24);
            this.txtBox_search.Margin = new System.Windows.Forms.Padding(2);
            this.txtBox_search.Name = "txtBox_search";
            this.txtBox_search.Size = new System.Drawing.Size(113, 20);
            this.txtBox_search.TabIndex = 7;
            // 
            // gbox_order_by
            // 
            this.gbox_order_by.Controls.Add(this.radBut_asc);
            this.gbox_order_by.Controls.Add(this.radBut_desc);
            this.gbox_order_by.Location = new System.Drawing.Point(107, 85);
            this.gbox_order_by.Margin = new System.Windows.Forms.Padding(2);
            this.gbox_order_by.Name = "gbox_order_by";
            this.gbox_order_by.Padding = new System.Windows.Forms.Padding(2);
            this.gbox_order_by.Size = new System.Drawing.Size(162, 65);
            this.gbox_order_by.TabIndex = 27;
            this.gbox_order_by.TabStop = false;
            this.gbox_order_by.Text = "Order By";
            // 
            // radBut_asc
            // 
            this.radBut_asc.AutoSize = true;
            this.radBut_asc.Checked = true;
            this.radBut_asc.Location = new System.Drawing.Point(14, 20);
            this.radBut_asc.Margin = new System.Windows.Forms.Padding(2);
            this.radBut_asc.Name = "radBut_asc";
            this.radBut_asc.Size = new System.Drawing.Size(139, 17);
            this.radBut_asc.TabIndex = 3;
            this.radBut_asc.TabStop = true;
            this.radBut_asc.Text = "Ascending (A - Z / 1 - 9)";
            this.radBut_asc.UseVisualStyleBackColor = true;
            // 
            // radBut_desc
            // 
            this.radBut_desc.AutoSize = true;
            this.radBut_desc.Location = new System.Drawing.Point(14, 41);
            this.radBut_desc.Margin = new System.Windows.Forms.Padding(2);
            this.radBut_desc.Name = "radBut_desc";
            this.radBut_desc.Size = new System.Drawing.Size(146, 17);
            this.radBut_desc.TabIndex = 4;
            this.radBut_desc.Text = "Descending (Z - A / 9 - 1)";
            this.radBut_desc.UseVisualStyleBackColor = true;
            // 
            // gbox_paging
            // 
            this.gbox_paging.Controls.Add(this.but_find_members);
            this.gbox_paging.Controls.Add(this.lbl_paging);
            this.gbox_paging.Controls.Add(this.but_prior_page);
            this.gbox_paging.Controls.Add(this.cbox_paging_limit);
            this.gbox_paging.Controls.Add(this.but_next_page);
            this.gbox_paging.Location = new System.Drawing.Point(295, 32);
            this.gbox_paging.Margin = new System.Windows.Forms.Padding(2);
            this.gbox_paging.Name = "gbox_paging";
            this.gbox_paging.Padding = new System.Windows.Forms.Padding(2);
            this.gbox_paging.Size = new System.Drawing.Size(153, 119);
            this.gbox_paging.TabIndex = 26;
            this.gbox_paging.TabStop = false;
            this.gbox_paging.Text = "Paging";
            // 
            // but_find_members
            // 
            this.but_find_members.Location = new System.Drawing.Point(38, 65);
            this.but_find_members.Margin = new System.Windows.Forms.Padding(2);
            this.but_find_members.Name = "but_find_members";
            this.but_find_members.Size = new System.Drawing.Size(79, 20);
            this.but_find_members.TabIndex = 14;
            this.but_find_members.Text = "Find Members";
            this.but_find_members.UseVisualStyleBackColor = true;
            // 
            // lbl_paging
            // 
            this.lbl_paging.AutoSize = true;
            this.lbl_paging.Location = new System.Drawing.Point(52, 24);
            this.lbl_paging.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_paging.Name = "lbl_paging";
            this.lbl_paging.Size = new System.Drawing.Size(55, 13);
            this.lbl_paging.TabIndex = 6;
            this.lbl_paging.Text = "Page Size";
            // 
            // but_prior_page
            // 
            this.but_prior_page.Enabled = false;
            this.but_prior_page.Location = new System.Drawing.Point(10, 90);
            this.but_prior_page.Margin = new System.Windows.Forms.Padding(2);
            this.but_prior_page.Name = "but_prior_page";
            this.but_prior_page.Size = new System.Drawing.Size(62, 20);
            this.but_prior_page.TabIndex = 13;
            this.but_prior_page.Text = "Prior Page";
            this.but_prior_page.UseVisualStyleBackColor = true;
            // 
            // cbox_paging_limit
            // 
            this.cbox_paging_limit.FormattingEnabled = true;
            this.cbox_paging_limit.Items.AddRange(new object[] {
            "25",
            "50",
            "75",
            "100"});
            this.cbox_paging_limit.Location = new System.Drawing.Point(55, 41);
            this.cbox_paging_limit.Margin = new System.Windows.Forms.Padding(2);
            this.cbox_paging_limit.Name = "cbox_paging_limit";
            this.cbox_paging_limit.Size = new System.Drawing.Size(51, 21);
            this.cbox_paging_limit.TabIndex = 5;
            // 
            // but_next_page
            // 
            this.but_next_page.Enabled = false;
            this.but_next_page.Location = new System.Drawing.Point(89, 90);
            this.but_next_page.Margin = new System.Windows.Forms.Padding(2);
            this.but_next_page.Name = "but_next_page";
            this.but_next_page.Size = new System.Drawing.Size(59, 20);
            this.but_next_page.TabIndex = 10;
            this.but_next_page.Text = "Next Page";
            this.but_next_page.UseVisualStyleBackColor = true;
            // 
            // lbl_available_members
            // 
            this.lbl_available_members.AutoSize = true;
            this.lbl_available_members.Location = new System.Drawing.Point(89, 172);
            this.lbl_available_members.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_available_members.Name = "lbl_available_members";
            this.lbl_available_members.Size = new System.Drawing.Size(134, 13);
            this.lbl_available_members.TabIndex = 25;
            this.lbl_available_members.Text = "Available Column Members";
            // 
            // but_cancel
            // 
            this.but_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.but_cancel.Location = new System.Drawing.Point(392, 505);
            this.but_cancel.Margin = new System.Windows.Forms.Padding(2);
            this.but_cancel.Name = "but_cancel";
            this.but_cancel.Size = new System.Drawing.Size(56, 19);
            this.but_cancel.TabIndex = 24;
            this.but_cancel.Text = "Cancel";
            this.but_cancel.UseVisualStyleBackColor = true;
            // 
            // but_ok
            // 
            this.but_ok.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.but_ok.Location = new System.Drawing.Point(320, 505);
            this.but_ok.Margin = new System.Windows.Forms.Padding(2);
            this.but_ok.Name = "but_ok";
            this.but_ok.Size = new System.Drawing.Size(56, 19);
            this.but_ok.TabIndex = 23;
            this.but_ok.Text = "OK";
            this.but_ok.UseVisualStyleBackColor = true;
            // 
            // lbox_available_members
            // 
            this.lbox_available_members.FormattingEnabled = true;
            this.lbox_available_members.Location = new System.Drawing.Point(86, 190);
            this.lbox_available_members.Margin = new System.Windows.Forms.Padding(2);
            this.lbox_available_members.Name = "lbox_available_members";
            this.lbox_available_members.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbox_available_members.Size = new System.Drawing.Size(170, 303);
            this.lbox_available_members.TabIndex = 22;
            // 
            // ColumnItemsView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(579, 544);
            this.Controls.Add(this.but_add);
            this.Controls.Add(this.but_remove);
            this.Controls.Add(this.lbl_selected_members);
            this.Controls.Add(this.lbox_selected_members);
            this.Controls.Add(this.gbox_search);
            this.Controls.Add(this.gbox_order_by);
            this.Controls.Add(this.gbox_paging);
            this.Controls.Add(this.lbl_available_members);
            this.Controls.Add(this.but_cancel);
            this.Controls.Add(this.but_ok);
            this.Controls.Add(this.lbox_available_members);
            this.Name = "ColumnItemsView";
            this.Text = "ColumnItemsView";
            this.gbox_search.ResumeLayout(false);
            this.gbox_search.PerformLayout();
            this.gbox_order_by.ResumeLayout(false);
            this.gbox_order_by.PerformLayout();
            this.gbox_paging.ResumeLayout(false);
            this.gbox_paging.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button but_add;
        private System.Windows.Forms.Button but_remove;
        private System.Windows.Forms.Label lbl_selected_members;
        private System.Windows.Forms.ListBox lbox_selected_members;
        private System.Windows.Forms.GroupBox gbox_search;
        private System.Windows.Forms.TextBox txtBox_search;
        private System.Windows.Forms.GroupBox gbox_order_by;
        private System.Windows.Forms.RadioButton radBut_asc;
        private System.Windows.Forms.RadioButton radBut_desc;
        private System.Windows.Forms.GroupBox gbox_paging;
        private System.Windows.Forms.Button but_find_members;
        private System.Windows.Forms.Label lbl_paging;
        private System.Windows.Forms.Button but_prior_page;
        private System.Windows.Forms.Button but_next_page;
        private System.Windows.Forms.Label lbl_available_members;
        private System.Windows.Forms.Button but_cancel;
        private System.Windows.Forms.Button but_ok;
        private System.Windows.Forms.ListBox lbox_available_members;
        private System.Windows.Forms.ComboBox cbox_paging_limit;
    }
}