namespace TradeBD.Forms.AddForms
{
    partial class AddOrderF
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing) { if (disposing && (components != null)) components.Dispose(); base.Dispose(disposing); }

        private void InitializeComponent()
        {
            this.lblOrderId = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.lblDate = new System.Windows.Forms.Label();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.lblDelivery = new System.Windows.Forms.Label();
            this.dtpDelivery = new System.Windows.Forms.DateTimePicker();
            this.lblClient = new System.Windows.Forms.Label();
            this.cmbClient = new System.Windows.Forms.ComboBox(); // <-- Изменили на ComboBox
            this.lblPickupPoint = new System.Windows.Forms.Label(); // <-- Новое
            this.cmbPickupPoint = new System.Windows.Forms.ComboBox(); // <-- Новое
            this.dgvProducts = new System.Windows.Forms.DataGridView();
            this.lblProducts = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnAddProduct = new System.Windows.Forms.Button(); // <-- Новая кнопка
            ((System.ComponentModel.ISupportInitialize)(this.dgvProducts)).BeginInit();
            this.SuspendLayout();

            // lblOrderId
            this.lblOrderId.AutoSize = true;
            this.lblOrderId.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Bold);
            this.lblOrderId.Location = new System.Drawing.Point(20, 20);
            this.lblOrderId.Name = "lblOrderId";
            this.lblOrderId.Size = new System.Drawing.Size(92, 22);
            this.lblOrderId.Text = "Заказ №...";

            // Status
            this.lblStatus.Location = new System.Drawing.Point(20, 60);
            this.lblStatus.Text = "Статус:";
            this.lblStatus.AutoSize = true;

            this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatus.Items.AddRange(new object[] { "Новый", "Завершен" });
            this.cmbStatus.Location = new System.Drawing.Point(24, 82);
            this.cmbStatus.Size = new System.Drawing.Size(200, 27);

            // Date
            this.lblDate.Location = new System.Drawing.Point(250, 60);
            this.lblDate.Text = "Дата заказа:";
            this.lblDate.AutoSize = true;

            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDate.Location = new System.Drawing.Point(254, 83);
            this.dtpDate.Size = new System.Drawing.Size(150, 26);

            // Delivery
            this.lblDelivery.Location = new System.Drawing.Point(430, 60);
            this.lblDelivery.Text = "Дата доставки:";
            this.lblDelivery.AutoSize = true;

            this.dtpDelivery.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDelivery.Location = new System.Drawing.Point(434, 83);
            this.dtpDelivery.Size = new System.Drawing.Size(150, 26);

            // Client (ComboBox)
            this.lblClient.Location = new System.Drawing.Point(20, 120);
            this.lblClient.Text = "Клиент:";
            this.lblClient.AutoSize = true;

            this.cmbClient.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbClient.Location = new System.Drawing.Point(24, 142);
            this.cmbClient.Size = new System.Drawing.Size(300, 27);

            // PickupPoint (Новое)
            this.lblPickupPoint.Location = new System.Drawing.Point(340, 120);
            this.lblPickupPoint.Text = "Пункт выдачи:";
            this.lblPickupPoint.AutoSize = true;

            this.cmbPickupPoint.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPickupPoint.Location = new System.Drawing.Point(344, 142);
            this.cmbPickupPoint.Size = new System.Drawing.Size(300, 27);

            // Products Grid
            this.dgvProducts.AllowUserToAddRows = false;
            this.dgvProducts.AllowUserToDeleteRows = false;
            this.dgvProducts.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvProducts.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dgvProducts.Location = new System.Drawing.Point(24, 230);
            this.dgvProducts.ReadOnly = true;
            this.dgvProducts.Size = new System.Drawing.Size(740, 200);

            // Products Label & Button
            this.lblProducts.Location = new System.Drawing.Point(20, 200);
            this.lblProducts.Text = "Состав заказа:";
            this.lblProducts.AutoSize = true;
            this.lblProducts.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);

            this.btnAddProduct.Location = new System.Drawing.Point(614, 190);
            this.btnAddProduct.Size = new System.Drawing.Size(150, 30);
            this.btnAddProduct.Text = "+ Добавить товар";
            this.btnAddProduct.BackColor = System.Drawing.Color.White;
            this.btnAddProduct.Click += new System.EventHandler(this.btnAddProduct_Click);

            // Save & Delete
            this.btnSave.Location = new System.Drawing.Point(24, 450);
            this.btnSave.Size = new System.Drawing.Size(120, 40);
            this.btnSave.Text = "Сохранить";
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(0, 250, 154);
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);

            this.btnDelete.Location = new System.Drawing.Point(644, 450);
            this.btnDelete.Size = new System.Drawing.Size(120, 40);
            this.btnDelete.Text = "Удалить";
            this.btnDelete.BackColor = System.Drawing.Color.MistyRose;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);

            // Form
            this.ClientSize = new System.Drawing.Size(800, 520);
            this.Controls.Add(this.btnAddProduct);
            this.Controls.Add(this.cmbPickupPoint);
            this.Controls.Add(this.lblPickupPoint);
            this.Controls.Add(this.cmbClient);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.lblProducts);
            this.Controls.Add(this.dgvProducts);
            this.Controls.Add(this.lblClient);
            this.Controls.Add(this.dtpDelivery);
            this.Controls.Add(this.lblDelivery);
            this.Controls.Add(this.dtpDate);
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.cmbStatus);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.lblOrderId);
            this.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Детали заказа";
            ((System.ComponentModel.ISupportInitialize)(this.dgvProducts)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblOrderId;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Label lblDelivery;
        private System.Windows.Forms.DateTimePicker dtpDelivery;
        private System.Windows.Forms.Label lblClient;
        private System.Windows.Forms.ComboBox cmbClient; // Changed to ComboBox
        private System.Windows.Forms.Label lblPickupPoint; // New
        private System.Windows.Forms.ComboBox cmbPickupPoint; // New
        private System.Windows.Forms.DataGridView dgvProducts;
        private System.Windows.Forms.Label lblProducts;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnAddProduct; // New
    }
}