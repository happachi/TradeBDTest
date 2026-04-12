using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;
using TradeBD.Classes;
using TradeBD.Controls;
using TradeBD.Forms.AddForms;

namespace TradeBD.MainForms
{
    public partial class ProductF : Form
    {
        private int _roleId;
        private int _userId;
        private string _fio;

        // Для статистики "Показано X из Y"
        private int _totalRows = 0;
        private int _shownRows = 0;

        public ProductF(int roleId, int userId, string fio)
        {
            InitializeComponent();
            _roleId = roleId;
            _userId = userId;
            _fio = fio;
        }

        private void ProductF_Load(object sender, EventArgs e)
        {
            lblUserInfo.Text = _fio; // Отображаем ФИО или "Гость"

            SetupRoleBasedUI();      // Скрываем/показываем кнопки
            LoadCategories();        // Заполняем фильтр категорий

            cmbSort.SelectedIndex = 0; // Сортировка по умолчанию

            LoadProducts();          // Загружаем товары
        }

        private void SetupRoleBasedUI()
        {
            // RoleID: 1 = Админ, 2 = Менеджер, 3 = Клиент, 0 = Гость

            if (_roleId == 1) // Администратор
            {
                btnAddProduct.Visible = true; // Добавление товаров
                pnlFilters.Visible = true;    // Фильтры
                btnOrders.Visible = true;     // Кнопка заказов
            }
            else if (_roleId == 2) // Менеджер
            {
                btnAddProduct.Visible = false;
                pnlFilters.Visible = true;
                btnOrders.Visible = true;
            }
            else // Гость или Клиент
            {
                btnAddProduct.Visible = false;
                // По ТЗ гость видит список без фильтров
                pnlFilters.Visible = false;
                btnOrders.Visible = false;
            }
        }

        private void LoadCategories()
        {
            cmbFilterCategory.Items.Clear();
            cmbFilterCategory.Items.Add("Все типы");
            cmbFilterCategory.SelectedIndex = 0;

            try
            {
                using (var conn = new SqlConnection(ConnectionHelper.ConnectionString))
                {
                    conn.Open();
                    var cmd = new SqlCommand("SELECT DISTINCT ProductCategory FROM Product", conn);
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        cmbFilterCategory.Items.Add(reader[0].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки категорий: " + ex.Message);
            }
        }

        private void LoadProducts()
        {
            flowLayoutPanelProducts.SuspendLayout();
            flowLayoutPanelProducts.Controls.Clear();

            string baseQuery = "SELECT * FROM Product WHERE 1=1";
            List<SqlParameter> parameters = new List<SqlParameter>();

            if (pnlFilters.Visible)
            {
                if (!string.IsNullOrWhiteSpace(txtSearch.Text))
                {
                    baseQuery += " AND (ProductName LIKE @Search OR ProductDescription LIKE @Search)";
                    parameters.Add(new SqlParameter("@Search", $"%{txtSearch.Text.Trim()}%"));
                }

                if (cmbFilterCategory.SelectedIndex > 0)
                {
                    baseQuery += " AND ProductCategory = @Category";
                    parameters.Add(new SqlParameter("@Category", cmbFilterCategory.SelectedItem.ToString()));
                }

                switch (cmbSort.SelectedIndex)
                {
                    case 1: // Цена возр.
                        baseQuery += " ORDER BY ProductCost ASC";
                        break;
                    case 2: // Цена убыв.
                        baseQuery += " ORDER BY ProductCost DESC";
                        break;
                    default: // По умолчанию
                        baseQuery += " ORDER BY ProductArticleNumber";
                        break;
                }
            }
            else
            {
                baseQuery += " ORDER BY ProductArticleNumber";
            }

            try
            {
                using (var conn = new SqlConnection(ConnectionHelper.ConnectionString))
                {
                    conn.Open();
                    var cmd = new SqlCommand(baseQuery, conn);
                    cmd.Parameters.AddRange(parameters.ToArray());

                    var reader = cmd.ExecuteReader();
                    int count = 0;

                    while (reader.Read())
                    {
                        count++;
                        var item = new ProductItemUserControl();

                        // 3. Считываем ВСЕ поля из базы данных
                        // Обратите внимание: имена полей должны точно совпадать с БД
                        string art = reader["ProductArticleNumber"].ToString();
                        string name = reader["ProductName"].ToString();
                        string desc = reader["ProductDescription"].ToString();
                        string cat = reader["ProductCategory"].ToString();       // Категория
                        decimal cost = Convert.ToDecimal(reader["ProductCost"]);
                        int disc = Convert.ToInt32(reader["ProductDiscountAmount"]);
                        string man = reader["ProductManufacturer"].ToString();
                        string sup = reader["ProductSupplier"].ToString();       // Поставщик
                        int qty = Convert.ToInt32(reader["ProductQuantityInStock"]); // Количество
                        string unit = reader["ProductUnit"].ToString();          // Ед. изм.
                        string photo = reader["ProductPhoto"].ToString();

                        item.SetData(art, name, desc, cat, cost, disc, man, sup, qty, unit, photo);

                        if (_roleId == 1)
                        {
                            ContextMenuStrip cm = new ContextMenuStrip();
                            var delItem = cm.Items.Add("Удалить товар");
                            delItem.Click += (s, ev) => DeleteProduct(art, name);
                            item.ContextMenuStrip = cm;

                            item.ProductDoubleClicked += (s, ev) =>
                            {
                                AddProductF editForm = new AddProductF(art);
                                if (editForm.ShowDialog() == DialogResult.OK)
                                {
                                    LoadProducts();
                                }
                            };
                        }

                        flowLayoutPanelProducts.Controls.Add(item);
                    }
                    _shownRows = count;
                }

                using (var conn = new SqlConnection(ConnectionHelper.ConnectionString))
                {
                    conn.Open();
                    var cmd = new SqlCommand("SELECT COUNT(*) FROM Product", conn);
                    _totalRows = (int)cmd.ExecuteScalar();
                }

                lblCountInfo.Text = $"Показано: {_shownRows} из {_totalRows}";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки товаров: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                flowLayoutPanelProducts.ResumeLayout();
            }
        }

        private void DeleteProduct(string article, string name)
        {
            if (MessageBox.Show($"Вы уверены, что хотите удалить товар '{name}'?", "Удаление",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    using (var conn = new SqlConnection(ConnectionHelper.ConnectionString))
                    {
                        conn.Open();
                        // Важно: в базе должен быть ON DELETE CASCADE для OrderProduct, 
                        // иначе сначала нужно удалять связанные записи.
                        var cmd = new SqlCommand("DELETE FROM Product WHERE ProductArticleNumber = @Id", conn);
                        cmd.Parameters.AddWithValue("@Id", article);
                        cmd.ExecuteNonQuery();
                    }
                    LoadProducts();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка удаления: " + ex.Message);
                }
            }
        }

        // --- Обработчики событий ---

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            AddProductF addForm = new AddProductF();
            if (addForm.ShowDialog() == DialogResult.OK)
            {
                LoadProducts();
            }
        }

        private void btnOrders_Click(object sender, EventArgs e)
        {
            OrderF orderForm = new OrderF(_roleId, _userId);
            this.Hide();
            orderForm.ShowDialog();
            this.Show();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close(); // Закрываем форму, возвращаемся к авторизации
        }

        // События фильтрации (вызывают перезагрузку списка)
        private void txtSearch_TextChanged(object sender, EventArgs e) => LoadProducts();
        private void cmbSort_SelectedIndexChanged(object sender, EventArgs e) => LoadProducts();
        private void cmbFilterCategory_SelectedIndexChanged(object sender, EventArgs e) => LoadProducts();
    }
}