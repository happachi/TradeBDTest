namespace TradeBD.Controls
{
    partial class ProductItemUserControl
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.picProduct = new System.Windows.Forms.PictureBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblManufacturer = new System.Windows.Forms.Label();
            this.lblSupplier = new System.Windows.Forms.Label();
            this.lblPrice = new System.Windows.Forms.Label();
            this.lblOldPrice = new System.Windows.Forms.Label();
            this.lblUnit = new System.Windows.Forms.Label();
            this.lblQuantity = new System.Windows.Forms.Label();
            this.panelDiscount = new System.Windows.Forms.Panel();
            this.lblDiscount = new System.Windows.Forms.Label();
            this.lblDiscountTitle = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picProduct)).BeginInit();
            this.panelDiscount.SuspendLayout();
            this.SuspendLayout();
            // 
            // picProduct (Фото слева)
            // 
            this.picProduct.BackColor = System.Drawing.Color.White;
            this.picProduct.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picProduct.Location = new System.Drawing.Point(15, 15);
            this.picProduct.Name = "picProduct";
            this.picProduct.Size = new System.Drawing.Size(150, 150);
            this.picProduct.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picProduct.TabIndex = 0;
            this.picProduct.TabStop = false;
            // 
            // lblTitle (Категория | Наименование)
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(180, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(202, 19);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "Категория | Наименование";
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.lblDescription.Location = new System.Drawing.Point(180, 40);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(111, 16);
            this.lblDescription.TabIndex = 2;
            this.lblDescription.Text = "Описание товара:";
            // 
            // lblManufacturer
            // 
            this.lblManufacturer.AutoSize = true;
            this.lblManufacturer.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.lblManufacturer.Location = new System.Drawing.Point(180, 60);
            this.lblManufacturer.Name = "lblManufacturer";
            this.lblManufacturer.Size = new System.Drawing.Size(99, 16);
            this.lblManufacturer.TabIndex = 3;
            this.lblManufacturer.Text = "Производитель:";
            // 
            // lblSupplier
            // 
            this.lblSupplier.AutoSize = true;
            this.lblSupplier.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.lblSupplier.Location = new System.Drawing.Point(180, 80);
            this.lblSupplier.Name = "lblSupplier";
            this.lblSupplier.Size = new System.Drawing.Size(77, 16);
            this.lblSupplier.TabIndex = 4;
            this.lblSupplier.Text = "Поставщик:";
            // 
            // lblPrice (Цена и Старая цена)
            // 
            this.lblPrice.AutoSize = true;
            this.lblPrice.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.lblPrice.Location = new System.Drawing.Point(180, 100);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Size = new System.Drawing.Size(44, 17);
            this.lblPrice.TabIndex = 5;
            this.lblPrice.Text = "Цена:";
            // 
            // lblOldPrice (Зачеркнутая цена рядом)
            // 
            this.lblOldPrice.AutoSize = true;
            this.lblOldPrice.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Strikeout);
            this.lblOldPrice.ForeColor = System.Drawing.Color.Gray;
            this.lblOldPrice.Location = new System.Drawing.Point(300, 100);
            this.lblOldPrice.Name = "lblOldPrice";
            this.lblOldPrice.Size = new System.Drawing.Size(0, 16);
            this.lblOldPrice.TabIndex = 6;
            // 
            // lblUnit
            // 
            this.lblUnit.AutoSize = true;
            this.lblUnit.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.lblUnit.Location = new System.Drawing.Point(180, 120);
            this.lblUnit.Name = "lblUnit";
            this.lblUnit.Size = new System.Drawing.Size(130, 16);
            this.lblUnit.TabIndex = 7;
            this.lblUnit.Text = "Единица измерения:";
            // 
            // lblQuantity
            // 
            this.lblQuantity.AutoSize = true;
            this.lblQuantity.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.lblQuantity.Location = new System.Drawing.Point(180, 140);
            this.lblQuantity.Name = "lblQuantity";
            this.lblQuantity.Size = new System.Drawing.Size(137, 16);
            this.lblQuantity.TabIndex = 8;
            this.lblQuantity.Text = "Количество на складе:";
            // 
            // panelDiscount (Правая колонка)
            // 
            this.panelDiscount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelDiscount.Controls.Add(this.lblDiscount);
            this.panelDiscount.Controls.Add(this.lblDiscountTitle);
            this.panelDiscount.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelDiscount.Location = new System.Drawing.Point(698, 0);
            this.panelDiscount.Name = "panelDiscount";
            this.panelDiscount.Size = new System.Drawing.Size(150, 178);
            this.panelDiscount.TabIndex = 9;
            // 
            // lblDiscountTitle
            // 
            this.lblDiscountTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblDiscountTitle.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.lblDiscountTitle.Location = new System.Drawing.Point(0, 0);
            this.lblDiscountTitle.Name = "lblDiscountTitle";
            this.lblDiscountTitle.Size = new System.Drawing.Size(148, 60);
            this.lblDiscountTitle.TabIndex = 0;
            this.lblDiscountTitle.Text = "Действующая\r\nскидка";
            this.lblDiscountTitle.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // lblDiscount
            // 
            this.lblDiscount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDiscount.Font = new System.Drawing.Font("Times New Roman", 20F, System.Drawing.FontStyle.Bold);
            this.lblDiscount.ForeColor = System.Drawing.Color.Green;
            this.lblDiscount.Location = new System.Drawing.Point(0, 60);
            this.lblDiscount.Name = "lblDiscount";
            this.lblDiscount.Size = new System.Drawing.Size(148, 116);
            this.lblDiscount.TabIndex = 1;
            this.lblDiscount.Text = "10%";
            this.lblDiscount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ProductItemUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.panelDiscount);
            this.Controls.Add(this.lblQuantity);
            this.Controls.Add(this.lblUnit);
            this.Controls.Add(this.lblOldPrice);
            this.Controls.Add(this.lblPrice);
            this.Controls.Add(this.lblSupplier);
            this.Controls.Add(this.lblManufacturer);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.picProduct);
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "ProductItemUserControl";
            this.Size = new System.Drawing.Size(850, 180);
            ((System.ComponentModel.ISupportInitialize)(this.picProduct)).EndInit();
            this.panelDiscount.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.PictureBox picProduct;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label lblManufacturer;
        private System.Windows.Forms.Label lblSupplier;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.Label lblOldPrice;
        private System.Windows.Forms.Label lblUnit;
        private System.Windows.Forms.Label lblQuantity;
        private System.Windows.Forms.Panel panelDiscount;
        private System.Windows.Forms.Label lblDiscount;
        private System.Windows.Forms.Label lblDiscountTitle;
    }
}