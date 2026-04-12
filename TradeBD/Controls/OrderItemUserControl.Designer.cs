namespace TradeBD.Controls
{
    partial class OrderItemUserControl
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblOrderId = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblAddress = new System.Windows.Forms.Label();
            this.lblDate = new System.Windows.Forms.Label();
            this.panelRight = new System.Windows.Forms.Panel();
            this.lblDeliveryDate = new System.Windows.Forms.Label();
            this.lblDeliveryTitle = new System.Windows.Forms.Label();
            this.panelRight.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblOrderId (Артикул/Номер заказа)
            // 
            this.lblOrderId.AutoSize = true;
            this.lblOrderId.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Bold);
            this.lblOrderId.Location = new System.Drawing.Point(15, 10);
            this.lblOrderId.Name = "lblOrderId";
            this.lblOrderId.Size = new System.Drawing.Size(81, 22);
            this.lblOrderId.TabIndex = 0;
            this.lblOrderId.Text = "Заказ №";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.lblStatus.Location = new System.Drawing.Point(15, 40);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(104, 19);
            this.lblStatus.TabIndex = 1;
            this.lblStatus.Text = "Статус: Новый";
            // 
            // lblAddress
            // 
            this.lblAddress.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.lblAddress.Location = new System.Drawing.Point(15, 65);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size(500, 20);
            this.lblAddress.TabIndex = 2;
            this.lblAddress.Text = "Адрес: Москва, ул. Ленина";
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.lblDate.ForeColor = System.Drawing.Color.Gray;
            this.lblDate.Location = new System.Drawing.Point(15, 90);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(130, 19);
            this.lblDate.TabIndex = 3;
            this.lblDate.Text = "Создан: 01.01.2025";
            // 
            // panelRight (Правый блок с датой доставки)
            // 
            this.panelRight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelRight.Controls.Add(this.lblDeliveryDate);
            this.panelRight.Controls.Add(this.lblDeliveryTitle);
            this.panelRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelRight.Location = new System.Drawing.Point(648, 0);
            this.panelRight.Name = "panelRight";
            this.panelRight.Size = new System.Drawing.Size(200, 118);
            this.panelRight.TabIndex = 4;
            // 
            // lblDeliveryTitle
            // 
            this.lblDeliveryTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblDeliveryTitle.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.lblDeliveryTitle.Location = new System.Drawing.Point(0, 0);
            this.lblDeliveryTitle.Name = "lblDeliveryTitle";
            this.lblDeliveryTitle.Size = new System.Drawing.Size(198, 40);
            this.lblDeliveryTitle.TabIndex = 0;
            this.lblDeliveryTitle.Text = "Дата доставки";
            this.lblDeliveryTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblDeliveryDate
            // 
            this.lblDeliveryDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDeliveryDate.Font = new System.Drawing.Font("Times New Roman", 16F, System.Drawing.FontStyle.Bold);
            this.lblDeliveryDate.Location = new System.Drawing.Point(0, 40);
            this.lblDeliveryDate.Name = "lblDeliveryDate";
            this.lblDeliveryDate.Size = new System.Drawing.Size(198, 76);
            this.lblDeliveryDate.TabIndex = 1;
            this.lblDeliveryDate.Text = "25.11.2025";
            this.lblDeliveryDate.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // OrderItemUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.panelRight);
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.lblAddress);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.lblOrderId);
            this.Margin = new System.Windows.Forms.Padding(10);
            this.Name = "OrderItemUserControl";
            this.Size = new System.Drawing.Size(850, 120);
            this.panelRight.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblOrderId;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Panel panelRight;
        private System.Windows.Forms.Label lblDeliveryDate;
        private System.Windows.Forms.Label lblDeliveryTitle;
    }
}