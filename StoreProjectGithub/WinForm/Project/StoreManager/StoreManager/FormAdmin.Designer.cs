namespace StoreManager
{
    partial class FormAdmin
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
            dgvAccountList = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dgvAccountList).BeginInit();
            SuspendLayout();
            // 
            // dgvAccountList
            // 
            dgvAccountList.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvAccountList.Location = new Point(31, 27);
            dgvAccountList.Name = "dgvAccountList";
            dgvAccountList.RowHeadersWidth = 51;
            dgvAccountList.RowTemplate.Height = 29;
            dgvAccountList.Size = new Size(554, 396);
            dgvAccountList.TabIndex = 0;
            // 
            // FormAdmin
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(dgvAccountList);
            Name = "FormAdmin";
            Text = "FormAdmin";
            ((System.ComponentModel.ISupportInitialize)dgvAccountList).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dgvAccountList;
    }
}