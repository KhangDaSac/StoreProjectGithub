namespace StoreManager
{
    partial class FormOrderDetail
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
            ColumnHeader columnHeader20;
            lsvListOrderDetailDisplay = new ListView();
            columnHeader21 = new ColumnHeader();
            columnHeader22 = new ColumnHeader();
            columnHeader23 = new ColumnHeader();
            columnHeader24 = new ColumnHeader();
            columnHeader25 = new ColumnHeader();
            columnHeader26 = new ColumnHeader();
            columnHeader27 = new ColumnHeader();
            columnHeader20 = new ColumnHeader();
            SuspendLayout();
            // 
            // columnHeader20
            // 
            columnHeader20.Text = "OrderDetailID";
            columnHeader20.Width = 120;
            // 
            // lsvListOrderDetailDisplay
            // 
            lsvListOrderDetailDisplay.Columns.AddRange(new ColumnHeader[] { columnHeader20, columnHeader21, columnHeader22, columnHeader23, columnHeader24, columnHeader25, columnHeader26, columnHeader27 });
            lsvListOrderDetailDisplay.ForeColor = SystemColors.Desktop;
            lsvListOrderDetailDisplay.FullRowSelect = true;
            lsvListOrderDetailDisplay.GridLines = true;
            lsvListOrderDetailDisplay.Location = new Point(21, 77);
            lsvListOrderDetailDisplay.MultiSelect = false;
            lsvListOrderDetailDisplay.Name = "lsvListOrderDetailDisplay";
            lsvListOrderDetailDisplay.Size = new Size(1140, 664);
            lsvListOrderDetailDisplay.TabIndex = 3;
            lsvListOrderDetailDisplay.UseCompatibleStateImageBehavior = false;
            lsvListOrderDetailDisplay.View = View.Details;
            // 
            // columnHeader21
            // 
            columnHeader21.Text = "OrderID";
            columnHeader21.Width = 100;
            // 
            // columnHeader22
            // 
            columnHeader22.Text = "ProductID";
            columnHeader22.Width = 100;
            // 
            // columnHeader23
            // 
            columnHeader23.Text = "ProductName";
            columnHeader23.Width = 200;
            // 
            // columnHeader24
            // 
            columnHeader24.Text = "Unit";
            columnHeader24.Width = 100;
            // 
            // columnHeader25
            // 
            columnHeader25.Text = "Price";
            columnHeader25.Width = 200;
            // 
            // columnHeader26
            // 
            columnHeader26.Text = "Quantity";
            columnHeader26.Width = 100;
            // 
            // columnHeader27
            // 
            columnHeader27.Text = "Charge";
            columnHeader27.Width = 200;
            // 
            // FormOrderDetail
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1182, 753);
            Controls.Add(lsvListOrderDetailDisplay);
            Name = "FormOrderDetail";
            Text = "Chi tiết hóa đơn";
            ResumeLayout(false);
        }

        #endregion

        private ListView lsvListOrderDetailDisplay;
        private ColumnHeader columnHeader21;
        private ColumnHeader columnHeader22;
        private ColumnHeader columnHeader23;
        private ColumnHeader columnHeader24;
        private ColumnHeader columnHeader25;
        private ColumnHeader columnHeader26;
        private ColumnHeader columnHeader27;
    }
}