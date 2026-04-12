using System;
using System.Drawing;
using System.Windows.Forms;

namespace TradeBD.Controls
{
    public partial class OrderItemUserControl : UserControl
    {
        public int OrderID { get; private set; }

        public event EventHandler OrderClicked;

        public OrderItemUserControl()
        {
            InitializeComponent();
            this.Click += (s, e) => OrderClicked?.Invoke(this, EventArgs.Empty);
            foreach (Control c in this.Controls)
            {
                c.Click += (s, e) => OrderClicked?.Invoke(this, EventArgs.Empty);
                if (c.HasChildren)
                {
                    foreach (Control child in c.Controls)
                        child.Click += (s, e) => OrderClicked?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public void SetData(int id, DateTime date, string status, DateTime delDate, string address)
        {
            OrderID = id;

            lblOrderId.Text = $"Заказ №{id}";
            lblStatus.Text = $"Статус заказа: {status}";
            lblAddress.Text = $"Пункт выдачи: {address}";
            lblDate.Text = $"Дата заказа: {date:dd.MM.yyyy}";

            lblDeliveryDate.Text = $"{delDate:dd.MM.yyyy}";

            if (status == "Завершен")
            {
                this.BackColor = Color.WhiteSmoke;
                lblStatus.ForeColor = Color.Green;
            }
            else
            {
                this.BackColor = Color.White;
                lblStatus.ForeColor = Color.OrangeRed;
            }
        }
    }
}