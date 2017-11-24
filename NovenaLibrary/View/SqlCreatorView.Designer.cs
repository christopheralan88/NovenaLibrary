namespace NovenaLibrary.View
{
    partial class SqlCreatorView
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
            this.lbl_table = new System.Windows.Forms.Label();
            this.cbox_table = new System.Windows.Forms.ComboBox();
            this.but_down = new System.Windows.Forms.Button();
            this.but_up = new System.Windows.Forms.Button();
            this.lbox_selected_columns = new System.Windows.Forms.CheckedListBox();
            this.lbox_available_columns = new System.Windows.Forms.ListBox();
            this.lbl_selected_columns = new System.Windows.Forms.Label();
            this.lbl_available_columns = new System.Windows.Forms.Label();
            this.but_remove = new System.Windows.Forms.Button();
            this.but_add = new System.Windows.Forms.Button();
            this.but_add_row = new System.Windows.Forms.Button();
            this.txt_box_limit = new System.Windows.Forms.TextBox();
            this.lbl_limit = new System.Windows.Forms.Label();
            this.but_delete_row = new System.Windows.Forms.Button();
            this.but_column_items = new System.Windows.Forms.Button();
            this.lbl_criteria = new System.Windows.Forms.Label();
            this.but_cancel = new System.Windows.Forms.Button();
            this.but_ok = new System.Windows.Forms.Button();
            this.ckbox_refresh = new System.Windows.Forms.CheckBox();
            this.datagrid_criteria = new System.Windows.Forms.DataGridView();
            this.AndOr = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.FrontParenthesis = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Operator = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Filter = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EndParenthesis = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.datagrid_criteria)).BeginInit();
            this.SuspendLayout();
            // 
            // lbl_table
            // 
            this.lbl_table.AutoSize = true;
            this.lbl_table.Location = new System.Drawing.Point(44, 55);
            this.lbl_table.Name = "lbl_table";
            this.lbl_table.Size = new System.Drawing.Size(34, 13);
            this.lbl_table.TabIndex = 52;
            this.lbl_table.Text = "Table";
            // 
            // cbox_table
            // 
            this.cbox_table.FormattingEnabled = true;
            this.cbox_table.Location = new System.Drawing.Point(83, 53);
            this.cbox_table.Name = "cbox_table";
            this.cbox_table.Size = new System.Drawing.Size(216, 21);
            this.cbox_table.TabIndex = 51;
            // 
            // but_down
            // 
            this.but_down.Location = new System.Drawing.Point(410, 264);
            this.but_down.Name = "but_down";
            this.but_down.Size = new System.Drawing.Size(50, 39);
            this.but_down.TabIndex = 67;
            this.but_down.Text = "Down";
            this.but_down.UseVisualStyleBackColor = true;
            // 
            // but_up
            // 
            this.but_up.Location = new System.Drawing.Point(410, 219);
            this.but_up.Name = "but_up";
            this.but_up.Size = new System.Drawing.Size(50, 39);
            this.but_up.TabIndex = 66;
            this.but_up.Text = "Up";
            this.but_up.UseVisualStyleBackColor = true;
            // 
            // lbox_selected_columns
            // 
            this.lbox_selected_columns.CheckOnClick = true;
            this.lbox_selected_columns.FormattingEnabled = true;
            this.lbox_selected_columns.HorizontalScrollbar = true;
            this.lbox_selected_columns.Location = new System.Drawing.Point(250, 129);
            this.lbox_selected_columns.Name = "lbox_selected_columns";
            this.lbox_selected_columns.Size = new System.Drawing.Size(154, 349);
            this.lbox_selected_columns.TabIndex = 65;
            // 
            // lbox_available_columns
            // 
            this.lbox_available_columns.FormattingEnabled = true;
            this.lbox_available_columns.HorizontalScrollbar = true;
            this.lbox_available_columns.Location = new System.Drawing.Point(12, 131);
            this.lbox_available_columns.Name = "lbox_available_columns";
            this.lbox_available_columns.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbox_available_columns.Size = new System.Drawing.Size(151, 355);
            this.lbox_available_columns.TabIndex = 64;
            // 
            // lbl_selected_columns
            // 
            this.lbl_selected_columns.AutoSize = true;
            this.lbl_selected_columns.Location = new System.Drawing.Point(286, 146);
            this.lbl_selected_columns.Name = "lbl_selected_columns";
            this.lbl_selected_columns.Size = new System.Drawing.Size(92, 13);
            this.lbl_selected_columns.TabIndex = 63;
            this.lbl_selected_columns.Text = "Selected Columns";
            // 
            // lbl_available_columns
            // 
            this.lbl_available_columns.AutoSize = true;
            this.lbl_available_columns.Location = new System.Drawing.Point(41, 146);
            this.lbl_available_columns.Name = "lbl_available_columns";
            this.lbl_available_columns.Size = new System.Drawing.Size(93, 13);
            this.lbl_available_columns.TabIndex = 62;
            this.lbl_available_columns.Text = "Available Columns";
            // 
            // but_remove
            // 
            this.but_remove.Location = new System.Drawing.Point(169, 258);
            this.but_remove.Name = "but_remove";
            this.but_remove.Size = new System.Drawing.Size(75, 23);
            this.but_remove.TabIndex = 61;
            this.but_remove.Text = "<<<";
            this.but_remove.UseVisualStyleBackColor = true;
            // 
            // but_add
            // 
            this.but_add.Location = new System.Drawing.Point(169, 219);
            this.but_add.Name = "but_add";
            this.but_add.Size = new System.Drawing.Size(75, 23);
            this.but_add.TabIndex = 60;
            this.but_add.Text = ">>>";
            this.but_add.UseVisualStyleBackColor = true;
            // 
            // but_add_row
            // 
            this.but_add_row.Location = new System.Drawing.Point(686, 68);
            this.but_add_row.Name = "but_add_row";
            this.but_add_row.Size = new System.Drawing.Size(96, 23);
            this.but_add_row.TabIndex = 77;
            this.but_add_row.Text = "Add Row";
            this.but_add_row.UseVisualStyleBackColor = true;
            // 
            // txt_box_limit
            // 
            this.txt_box_limit.Location = new System.Drawing.Point(733, 514);
            this.txt_box_limit.Name = "txt_box_limit";
            this.txt_box_limit.Size = new System.Drawing.Size(100, 20);
            this.txt_box_limit.TabIndex = 76;
            this.txt_box_limit.Text = "900000";
            // 
            // lbl_limit
            // 
            this.lbl_limit.AutoSize = true;
            this.lbl_limit.Location = new System.Drawing.Point(637, 519);
            this.lbl_limit.Name = "lbl_limit";
            this.lbl_limit.Size = new System.Drawing.Size(90, 13);
            this.lbl_limit.TabIndex = 75;
            this.lbl_limit.Text = "Limit Records To:";
            // 
            // but_delete_row
            // 
            this.but_delete_row.Location = new System.Drawing.Point(788, 68);
            this.but_delete_row.Name = "but_delete_row";
            this.but_delete_row.Size = new System.Drawing.Size(96, 23);
            this.but_delete_row.TabIndex = 74;
            this.but_delete_row.Text = "Delete Row";
            this.but_delete_row.UseVisualStyleBackColor = true;
            // 
            // but_column_items
            // 
            this.but_column_items.Location = new System.Drawing.Point(890, 68);
            this.but_column_items.Name = "but_column_items";
            this.but_column_items.Size = new System.Drawing.Size(96, 23);
            this.but_column_items.TabIndex = 73;
            this.but_column_items.Text = "Column Items";
            this.but_column_items.UseVisualStyleBackColor = true;
            // 
            // lbl_criteria
            // 
            this.lbl_criteria.AutoSize = true;
            this.lbl_criteria.Location = new System.Drawing.Point(475, 82);
            this.lbl_criteria.Name = "lbl_criteria";
            this.lbl_criteria.Size = new System.Drawing.Size(70, 13);
            this.lbl_criteria.TabIndex = 72;
            this.lbl_criteria.Text = "Query Criteria";
            // 
            // but_cancel
            // 
            this.but_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.but_cancel.Location = new System.Drawing.Point(938, 511);
            this.but_cancel.Name = "but_cancel";
            this.but_cancel.Size = new System.Drawing.Size(75, 23);
            this.but_cancel.TabIndex = 71;
            this.but_cancel.Text = "Cancel";
            this.but_cancel.UseVisualStyleBackColor = true;
            // 
            // but_ok
            // 
            this.but_ok.Location = new System.Drawing.Point(845, 511);
            this.but_ok.Name = "but_ok";
            this.but_ok.Size = new System.Drawing.Size(75, 23);
            this.but_ok.TabIndex = 70;
            this.but_ok.Text = "OK";
            this.but_ok.UseVisualStyleBackColor = true;
            // 
            // ckbox_refresh
            // 
            this.ckbox_refresh.AutoSize = true;
            this.ckbox_refresh.Location = new System.Drawing.Point(640, 491);
            this.ckbox_refresh.Name = "ckbox_refresh";
            this.ckbox_refresh.Size = new System.Drawing.Size(144, 17);
            this.ckbox_refresh.TabIndex = 69;
            this.ckbox_refresh.Text = "Refresh Column Headers";
            this.ckbox_refresh.UseVisualStyleBackColor = true;
            // 
            // datagrid_criteria
            // 
            this.datagrid_criteria.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.datagrid_criteria.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.AndOr,
            this.FrontParenthesis,
            this.Column,
            this.Operator,
            this.Filter,
            this.EndParenthesis});
            this.datagrid_criteria.Location = new System.Drawing.Point(478, 106);
            this.datagrid_criteria.Name = "datagrid_criteria";
            this.datagrid_criteria.Size = new System.Drawing.Size(612, 379);
            this.datagrid_criteria.TabIndex = 68;
            // 
            // AndOr
            // 
            this.AndOr.DataPropertyName = "AndOr";
            this.AndOr.Frozen = true;
            this.AndOr.HeaderText = "And/Or";
            this.AndOr.Items.AddRange(new object[] {
            "And",
            "Or"});
            this.AndOr.Name = "AndOr";
            this.AndOr.Width = 65;
            // 
            // FrontParenthesis
            // 
            this.FrontParenthesis.DataPropertyName = "FrontParenthesis";
            this.FrontParenthesis.Frozen = true;
            this.FrontParenthesis.HeaderText = "front_parenthesis";
            this.FrontParenthesis.Name = "FrontParenthesis";
            this.FrontParenthesis.Visible = false;
            this.FrontParenthesis.Width = 20;
            // 
            // Column
            // 
            this.Column.DataPropertyName = "Column";
            this.Column.Frozen = true;
            this.Column.HeaderText = "Column";
            this.Column.Name = "Column";
            this.Column.Width = 200;
            // 
            // Operator
            // 
            this.Operator.DataPropertyName = "Operator";
            this.Operator.Frozen = true;
            this.Operator.HeaderText = "Operator";
            this.Operator.Items.AddRange(new object[] {
            "=",
            "<>",
            ">=",
            "<=",
            ">",
            "<",
            "Like",
            "Not Like",
            "In",
            "Not In"});
            this.Operator.Name = "Operator";
            this.Operator.Width = 70;
            // 
            // Filter
            // 
            this.Filter.DataPropertyName = "Filter";
            this.Filter.Frozen = true;
            this.Filter.HeaderText = "Filter";
            this.Filter.Name = "Filter";
            this.Filter.Width = 275;
            // 
            // EndParenthesis
            // 
            this.EndParenthesis.DataPropertyName = "EndParenthesis";
            this.EndParenthesis.Frozen = true;
            this.EndParenthesis.HeaderText = "end_parenthesis";
            this.EndParenthesis.Name = "EndParenthesis";
            this.EndParenthesis.Visible = false;
            this.EndParenthesis.Width = 20;
            // 
            // SqlCreatorView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1102, 561);
            this.Controls.Add(this.but_add_row);
            this.Controls.Add(this.txt_box_limit);
            this.Controls.Add(this.lbl_limit);
            this.Controls.Add(this.but_delete_row);
            this.Controls.Add(this.but_column_items);
            this.Controls.Add(this.lbl_criteria);
            this.Controls.Add(this.but_cancel);
            this.Controls.Add(this.but_ok);
            this.Controls.Add(this.ckbox_refresh);
            this.Controls.Add(this.datagrid_criteria);
            this.Controls.Add(this.but_down);
            this.Controls.Add(this.but_up);
            this.Controls.Add(this.lbox_selected_columns);
            this.Controls.Add(this.lbox_available_columns);
            this.Controls.Add(this.lbl_selected_columns);
            this.Controls.Add(this.lbl_available_columns);
            this.Controls.Add(this.but_remove);
            this.Controls.Add(this.but_add);
            this.Controls.Add(this.lbl_table);
            this.Controls.Add(this.cbox_table);
            this.Name = "SqlCreatorView";
            this.Text = "SqlCreatorView";
            ((System.ComponentModel.ISupportInitialize)(this.datagrid_criteria)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Label lbl_table;
        internal System.Windows.Forms.ComboBox cbox_table;
        internal System.Windows.Forms.Button but_down;
        internal System.Windows.Forms.Button but_up;
        internal System.Windows.Forms.CheckedListBox lbox_selected_columns;
        internal System.Windows.Forms.ListBox lbox_available_columns;
        internal System.Windows.Forms.Label lbl_selected_columns;
        internal System.Windows.Forms.Label lbl_available_columns;
        internal System.Windows.Forms.Button but_remove;
        internal System.Windows.Forms.Button but_add;
        internal System.Windows.Forms.Button but_add_row;
        internal System.Windows.Forms.TextBox txt_box_limit;
        internal System.Windows.Forms.Label lbl_limit;
        internal System.Windows.Forms.Button but_delete_row;
        internal System.Windows.Forms.Button but_column_items;
        internal System.Windows.Forms.Label lbl_criteria;
        internal System.Windows.Forms.Button but_cancel;
        internal System.Windows.Forms.Button but_ok;
        internal System.Windows.Forms.CheckBox ckbox_refresh;
        internal System.Windows.Forms.DataGridView datagrid_criteria;
        private System.Windows.Forms.DataGridViewComboBoxColumn AndOr;
        private System.Windows.Forms.DataGridViewTextBoxColumn FrontParenthesis;
        private System.Windows.Forms.DataGridViewComboBoxColumn Column;
        private System.Windows.Forms.DataGridViewComboBoxColumn Operator;
        private System.Windows.Forms.DataGridViewTextBoxColumn Filter;
        private System.Windows.Forms.DataGridViewTextBoxColumn EndParenthesis;
    }
}