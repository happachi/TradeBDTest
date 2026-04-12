using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using TradeBD.Classes;

namespace TradeBD.Forms.AddForms
{
    public partial class AddOrderF : Form
    {
        private int? _orderId;
        private int _roleId;
        private DataTable _dtProducts; // Временное хранилище товаров для Grid

        public AddOrderF(int? orderId, int roleId)
        {
            InitializeComponent();
            _orderId = orderId;
            _roleId = roleId;

            // Инициализация таблицы товаров
            InitializeProductsTable();

            LoadDictionaries(); // Загружаем клиентов и пункты

            ConfigurePermissions();

            if (_orderId.HasValue)
            {
                lblOrderId.Text = $"Заказ №{_orderId}";
                LoadOrderInfo();
                LoadOrderProducts();
                // При редактировании не даем менять клиента и пункт (по упрощенной логике)
                cmbClient.Enabled = false;
                cmbPickupPoint.Enabled = false;
            }
            else
            {
                lblOrderId.Text = "Новый заказ";
                btnDelete.Visible = false;
                dtpDate.Value = DateTime.Now;
                dtpDelivery.Value = DateTime.Now.AddDays(3);
                cmbStatus.SelectedIndex = 0; // "Новый"

                // Включаем элементы для создания
                cmbClient.Enabled = true;
                cmbPickupPoint.Enabled = true;
                btnAddProduct.Visible = true;
            }
        }

        private void InitializeProductsTable()
        {
            _dtProducts = new DataTable();
            _dtProducts.Columns.Add("Артикул");
            _dtProducts.Columns.Add("Товар");
            _dtProducts.Columns.Add("Количество", typeof(int));
            _dtProducts.Columns.Add("Цена за шт.", typeof(decimal));
            dgvProducts.DataSource = _dtProducts;
        }

        private void LoadDictionaries()
        {
            try
            {
                using (var conn = new SqlConnection(ConnectionHelper.ConnectionString))
                {
                    conn.Open();

                    // 1. Клиенты (RoleID = 3)
                    var daClient = new SqlDataAdapter("SELECT UserID, (UserSurname + ' ' + UserName) as FIO FROM [User] WHERE RoleID = 3", conn);
                    var dtClient = new DataTable();
                    daClient.Fill(dtClient);
                    cmbClient.DataSource = dtClient;
                    cmbClient.DisplayMember = "FIO";
                    cmbClient.ValueMember = "UserID";

                    // 2. Пункты выдачи
                    var daPoint = new SqlDataAdapter("SELECT PointID, (City + ', ' + Street) as Addr FROM PickupPoint", conn);
                    var dtPoint = new DataTable();
                    daPoint.Fill(dtPoint);
                    cmbPickupPoint.DataSource = dtPoint;
                    cmbPickupPoint.DisplayMember = "Addr";
                    cmbPickupPoint.ValueMember = "PointID";
                }
            }
            catch { }
        }

        private void ConfigurePermissions()
        {
            if (_roleId == 1) // Админ
            {
                btnDelete.Visible = true;
                btnSave.Enabled = true;
                btnAddProduct.Visible = true;
            }
            else if (_roleId == 2) // Менеджер
            {
                btnDelete.Visible = false;
                btnAddProduct.Visible = false; // Менеджер не добавляет товары
                btnSave.Enabled = true;
            }
        }

        private void LoadOrderInfo()
        {
            try
            {
                using (var conn = new SqlConnection(ConnectionHelper.ConnectionString))
                {
                    conn.Open();
                    string query = @"SELECT OrderDate, OrderDeliveryDate, OrderStatus, OrderUserID, OrderPickupPointID 
                                     FROM [Order] WHERE OrderID = @ID";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@ID", _orderId.Value);

                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        dtpDate.Value = Convert.ToDateTime(reader["OrderDate"]);
                        dtpDelivery.Value = Convert.ToDateTime(reader["OrderDeliveryDate"]);
                        cmbStatus.SelectedItem = reader["OrderStatus"].ToString();
                        cmbClient.SelectedValue = reader["OrderUserID"];
                        cmbPickupPoint.SelectedValue = reader["OrderPickupPointID"];
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void LoadOrderProducts()
        {
            try
            {
                using (var conn = new SqlConnection(ConnectionHelper.ConnectionString))
                {
                    conn.Open();
                    string query = @"
                        SELECT p.ProductArticleNumber, p.ProductName, op.PrimaryCount, p.ProductCost
                        FROM OrderProduct op
                        JOIN Product p ON op.ProductArticleNumber = p.ProductArticleNumber
                        WHERE op.OrderID = @ID";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@ID", _orderId.Value);

                    var reader = cmd.ExecuteReader();
                    _dtProducts.Clear();
                    while (reader.Read())
                    {
                        _dtProducts.Rows.Add(
                            reader["ProductArticleNumber"],
                            reader["ProductName"],
                            reader["PrimaryCount"],
                            reader["ProductCost"]
                        );
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            // Открываем форму выбора товара
            AddOrderProductF form = new AddOrderProductF();
            if (form.ShowDialog() == DialogResult.OK)
            {
                // Проверяем, нет ли уже такого товара в списке
                foreach (DataRow row in _dtProducts.Rows)
                {
                    if (row["Артикул"].ToString() == form.SelectedArticle)
                    {
                        // Если есть - просто увеличиваем количество
                        row["Количество"] = (int)row["Количество"] + form.Count;
                        return;
                    }
                }

                // Если нет - добавляем строку
                _dtProducts.Rows.Add(form.SelectedArticle, form.SelectedName, form.Count, form.Price);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // ПРОВЕРКИ
            if (cmbClient.SelectedValue == null)
            {
                MessageBox.Show("Выберите клиента!"); return;
            }
            if (cmbPickupPoint.SelectedValue == null)
            {
                MessageBox.Show("Выберите пункт выдачи!"); return;
            }
            if (_dtProducts.Rows.Count == 0)
            {
                MessageBox.Show("Добавьте хотя бы один товар!"); return;
            }

            try
            {
                using (var conn = new SqlConnection(ConnectionHelper.ConnectionString))
                {
                    conn.Open();

                    if (_orderId.HasValue)
                    {
                        // UPDATE (Только статус и доставка)
                        string query = @"UPDATE [Order] SET OrderStatus=@St, OrderDeliveryDate=@Date WHERE OrderID=@ID";
                        var cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@St", cmbStatus.Text);
                        cmd.Parameters.AddWithValue("@Date", dtpDelivery.Value);
                        cmd.Parameters.AddWithValue("@ID", _orderId.Value);
                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        // INSERT (Новый заказ)

                        // 1. Генерируем новый OrderID (MAX + 1)
                        var cmdMax = new SqlCommand("SELECT ISNULL(MAX(OrderID), 0) + 1 FROM [Order]", conn);
                        int newId = (int)cmdMax.ExecuteScalar();

                        // 2. Генерируем случайный код получения
                        int code = new Random().Next(100, 999);

                        // 3. Вставляем заказ
                        string insertOrder = @"
                            INSERT INTO [Order] (OrderID, OrderDate, OrderDeliveryDate, OrderPickupPointID, OrderUserID, OrderGetCode, OrderStatus)
                            VALUES (@ID, @Date, @DelDate, @Point, @User, @Code, @Status)";

                        var cmdOrd = new SqlCommand(insertOrder, conn);
                        cmdOrd.Parameters.AddWithValue("@ID", newId);
                        cmdOrd.Parameters.AddWithValue("@Date", dtpDate.Value);
                        cmdOrd.Parameters.AddWithValue("@DelDate", dtpDelivery.Value);
                        cmdOrd.Parameters.AddWithValue("@Point", cmbPickupPoint.SelectedValue);
                        cmdOrd.Parameters.AddWithValue("@User", cmbClient.SelectedValue);
                        cmdOrd.Parameters.AddWithValue("@Code", code);
                        cmdOrd.Parameters.AddWithValue("@Status", cmbStatus.Text);
                        cmdOrd.ExecuteNonQuery();

                        // 4. Вставляем товары
                        foreach (DataRow row in _dtProducts.Rows)
                        {
                            string insertProd = "INSERT INTO OrderProduct (OrderID, ProductArticleNumber, PrimaryCount) VALUES (@OID, @Art, @Cnt)";
                            var cmdProd = new SqlCommand(insertProd, conn);
                            cmdProd.Parameters.AddWithValue("@OID", newId);
                            cmdProd.Parameters.AddWithValue("@Art", row["Артикул"]);
                            cmdProd.Parameters.AddWithValue("@Cnt", row["Количество"]);
                            cmdProd.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show("Сохранено!");
                    DialogResult = DialogResult.OK;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Удалить заказ?", "Подтверждение", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    using (var conn = new SqlConnection(ConnectionHelper.ConnectionString))
                    {
                        conn.Open();
                        new SqlCommand($"DELETE FROM [Order] WHERE OrderID={_orderId}", conn).ExecuteNonQuery();
                    }
                    DialogResult = DialogResult.OK;
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
        }
    }
}