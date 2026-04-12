using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace TradeBD.Controls
{
    public partial class ProductItemUserControl : UserControl
    {
        public event EventHandler ProductDoubleClicked;

        public ProductItemUserControl()
        {
            InitializeComponent();

            // Проброс кликов от всех элементов
            SubscribeToClick(this);
        }

        private void SubscribeToClick(Control parent)
        {
            parent.DoubleClick += (s, e) => ProductDoubleClicked?.Invoke(this, EventArgs.Empty);
            foreach (Control c in parent.Controls)
            {
                c.DoubleClick += (s, e) => ProductDoubleClicked?.Invoke(this, EventArgs.Empty);
                if (c.HasChildren) SubscribeToClick(c);
            }
        }

        public void SetData(string article, string name, string description, string category,
                            decimal cost, int discount, string manufacturer,
                            string supplier, int quantity, string unit, string photoName)
        {
            // Заголовок по макету: Категория | Наименование
            lblTitle.Text = $"{category} | {name}";

            lblDescription.Text = string.IsNullOrWhiteSpace(description) ? "Описание отсутствует" : description;
            lblManufacturer.Text = $"Производитель: {manufacturer}";
            lblSupplier.Text = $"Поставщик: {supplier}";
            lblUnit.Text = $"Единица измерения: {unit}";
            lblQuantity.Text = $"Количество на складе: {quantity}";

            // Логика цены
            if (discount > 0)
            {
                decimal newPrice = cost - (cost * discount / 100);

                lblPrice.Text = $"Цена: {decimal.Round(newPrice, 2)} руб.";

                lblOldPrice.Text = $"{decimal.Round(cost, 2)} руб.";
                lblOldPrice.Visible = true;

                lblDiscount.Text = $"{discount}%";
                lblDiscount.Visible = true;
                lblDiscountTitle.Visible = true;
            }
            else
            {
                lblPrice.Text = $"Цена: {decimal.Round(cost, 2)} руб.";
                lblOldPrice.Visible = false;
                lblDiscount.Text = "";
                lblDiscountTitle.Visible = false; // Скрываем заголовок скидки, если её нет
            }

            // Цвет фона
            if (discount > 15)
                this.BackColor = ColorTranslator.FromHtml("#7FFF00");
            else
                this.BackColor = Color.White;

            // Фото
            try
            {
                string path = Path.Combine(Application.StartupPath, "Resources", photoName);
                if (!string.IsNullOrEmpty(photoName) && File.Exists(path))
                {
                    using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read))
                    {
                        picProduct.Image = Image.FromStream(stream);
                    }
                }
                else
                {
                    picProduct.Image = Properties.Resources.picture;
                }
            }
            catch
            {
                picProduct.Image = null;
            }
        }
    }
}