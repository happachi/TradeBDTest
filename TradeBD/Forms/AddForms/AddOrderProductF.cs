using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using TradeBD.Classes;

namespace TradeBD.Forms.AddForms
{
    public partial class AddOrderProductF : Form
    {
        public string SelectedArticle { get; private set; }
        public string SelectedName { get; private set; }
        public int Count { get; private set; }
        public decimal Price { get; private set; }

        public AddOrderProductF()
        {
            InitializeComponent();

            // --- НАСТРОЙКИ ИНТЕРФЕЙСА ---

            // 1. Порядок команд для предотвращения вылета (Сначала Source, потом Mode)
            cmbProducts.AutoCompleteSource = AutoCompleteSource.ListItems;
            cmbProducts.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            // 2. Ограничение высоты списка (скроллбар)
            // Это главное, чтобы список не "улетал" вверх
            cmbProducts.MaxDropDownItems = 10;
            cmbProducts.IntegralHeight = false; // Помогает точнее отрисовывать границы списка

            // 3. Ширина выпадающего списка
            // Ставим большое значение (например, 700), чтобы текст влезал полностью,
            // даже если само поле на форме узкое.
            cmbProducts.DropDownWidth = 700;

            LoadProducts();
        }

        private void LoadProducts()
        {
            try
            {
                using (var conn = new SqlConnection(ConnectionHelper.ConnectionString))
                {
                    conn.Open();
                    var adapter = new SqlDataAdapter("SELECT ProductArticleNumber, ProductName, ProductCost FROM Product ORDER BY ProductName", conn);
                    var dt = new DataTable();
                    adapter.Fill(dt);

                    dt.Columns.Add("DisplayMember", typeof(string));

                    foreach (DataRow row in dt.Rows)
                    {
                        // --- НИКАКОЙ ОБРЕЗКИ ТЕКСТА ---
                        // Просто формируем строку: Полное Название (Артикул)
                        string fullName = row["ProductName"].ToString();
                        string article = row["ProductArticleNumber"].ToString();

                        row["DisplayMember"] = $"{fullName} ({article})";
                    }

                    cmbProducts.DataSource = dt;
                    cmbProducts.DisplayMember = "DisplayMember";
                    cmbProducts.ValueMember = "ProductArticleNumber";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (cmbProducts.SelectedItem == null) return;

            DataRowView row = (DataRowView)cmbProducts.SelectedItem;

            SelectedArticle = row["ProductArticleNumber"].ToString();
            SelectedName = row["ProductName"].ToString();
            Price = Convert.ToDecimal(row["ProductCost"]);
            Count = (int)numCount.Value;

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}