using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using TradeBD.Classes;
using TradeBD.Controls;
using TradeBD.Forms.AddForms;

namespace TradeBD.MainForms
{
    public partial class OrderF : Form
    {
        private int _roleId;
        private int _userId;

        public OrderF(int roleId, int userId)
        {
            InitializeComponent();
            _roleId = roleId;
            _userId = userId;
        }

        private void OrderF_Load(object sender, EventArgs e)
        {
            btnAddOrder.Visible = (_roleId == 1);
            LoadOrders();
        }

        private void LoadOrders()
        {
            flowLayoutPanelOrders.SuspendLayout();
            flowLayoutPanelOrders.Controls.Clear();

            try
            {
                using (var conn = new SqlConnection(ConnectionHelper.ConnectionString))
                {
                    conn.Open();
                    string query = @"
                        SELECT 
                            o.OrderID,
                            o.OrderDate,
                            o.OrderStatus,
                            o.OrderDeliveryDate,
                            (p.City + ', ' + p.Street) AS Address
                        FROM [Order] o
                        LEFT JOIN [PickupPoint] p ON o.OrderPickupPointID = p.PointID
                        ORDER BY o.OrderDate DESC";

                    var cmd = new SqlCommand(query, conn);
                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        var item = new OrderItemUserControl();

                        int id = (int)reader["OrderID"];
                        DateTime date = (DateTime)reader["OrderDate"];
                        string status = reader["OrderStatus"].ToString();
                        DateTime delDate = (DateTime)reader["OrderDeliveryDate"];
                        string addr = reader["Address"].ToString();

                        item.SetData(id, date, status, delDate, addr);

                        item.OrderClicked += (s, e) =>
                        {
                            AddOrderF detailsForm = new AddOrderF(id, _roleId);
                            if (detailsForm.ShowDialog() == DialogResult.OK)
                                LoadOrders();
                        };

                        flowLayoutPanelOrders.Controls.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки заказов: " + ex.Message);
            }
            finally
            {
                flowLayoutPanelOrders.ResumeLayout();
            }
        }

        private void btnAddOrder_Click(object sender, EventArgs e)
        {
            AddOrderF newForm = new AddOrderF(null, _roleId);
            if (newForm.ShowDialog() == DialogResult.OK)
                LoadOrders();
        }

        private void btnBack_Click(object sender, EventArgs e) => this.Close();
    }
}