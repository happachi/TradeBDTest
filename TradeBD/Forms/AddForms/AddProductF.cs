using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using TradeBD.Classes;

namespace TradeBD.Forms.AddForms
{
    public partial class AddProductF : Form
    {
        private readonly string _article;
        private string _currentPhotoName = ""; // Хранит имя файла (например, "boot.jpg")

        public AddProductF(string article = null)
        {
            InitializeComponent();
            _article = article;

            LoadDictionaries();

            if (_article != null)
            {
                txtArticle.ReadOnly = true;
                this.Text = "Редактирование товара";
                LoadProductData();
            }
            else
            {
                this.Text = "Добавление товара";
            }
        }

        private void LoadDictionaries()
        {
            try
            {
                using (var conn = new SqlConnection(ConnectionHelper.ConnectionString))
                {
                    conn.Open();
                    var daCat = new SqlDataAdapter("SELECT DISTINCT ProductCategory FROM Product", conn);
                    var dtCat = new DataTable();
                    daCat.Fill(dtCat);
                    foreach (DataRow row in dtCat.Rows) cmbCategory.Items.Add(row[0].ToString());

                    var daMan = new SqlDataAdapter("SELECT DISTINCT ProductManufacturer FROM Product", conn);
                    var dtMan = new DataTable();
                    daMan.Fill(dtMan);
                    foreach (DataRow row in dtMan.Rows) cmbManufacturer.Items.Add(row[0].ToString());
                }
            }
            catch { }
        }

        private void LoadProductData()
        {
            try
            {
                using (var conn = new SqlConnection(ConnectionHelper.ConnectionString))
                {
                    conn.Open();
                    string query = "SELECT * FROM Product WHERE ProductArticleNumber = @Id";
                    var cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Id", _article);

                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        txtArticle.Text = reader["ProductArticleNumber"].ToString();
                        txtName.Text = reader["ProductName"].ToString();
                        txtDescription.Text = reader["ProductDescription"].ToString();
                        cmbCategory.Text = reader["ProductCategory"].ToString();
                        cmbManufacturer.Text = reader["ProductManufacturer"].ToString();
                        txtSupplier.Text = reader["ProductSupplier"].ToString();
                        numCost.Value = Convert.ToDecimal(reader["ProductCost"]);
                        numDiscount.Value = Convert.ToDecimal(reader["ProductDiscountAmount"]);
                        numQuantity.Value = Convert.ToDecimal(reader["ProductQuantityInStock"]);
                        txtUnit.Text = reader["ProductUnit"].ToString();

                        // --- ЗАГРУЗКА ФОТО ---
                        _currentPhotoName = reader["ProductPhoto"].ToString();
                        lblPhotoName.Text = _currentPhotoName;

                        if (!string.IsNullOrEmpty(_currentPhotoName))
                        {
                            string path = Path.Combine(Application.StartupPath, "Resources", _currentPhotoName);
                            if (File.Exists(path))
                            {
                                // Используем FileStream, чтобы не блокировать файл
                                using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read))
                                {
                                    picPhoto.Image = Image.FromStream(stream);
                                }
                            }
                            else
                            {
                                picPhoto.Image = null; // Или картинка-заглушка
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки: " + ex.Message);
            }
        }

        private void btnLoadImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;|All files|*.*";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // 1. Копируем файл в папку Resources
                    string fileName = Path.GetFileName(ofd.FileName);
                    string resourcesFolder = Path.Combine(Application.StartupPath, "Resources");

                    if (!Directory.Exists(resourcesFolder))
                        Directory.CreateDirectory(resourcesFolder);

                    string destPath = Path.Combine(resourcesFolder, fileName);

                    // Копируем только если это другой файл
                    if (ofd.FileName != destPath)
                    {
                        File.Copy(ofd.FileName, destPath, true);
                    }

                    // 2. Отображаем
                    using (var stream = new FileStream(destPath, FileMode.Open, FileAccess.Read))
                    {
                        picPhoto.Image = Image.FromStream(stream);
                    }

                    // 3. Запоминаем имя для БД
                    _currentPhotoName = fileName;
                    lblPhotoName.Text = fileName;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка загрузки фото: " + ex.Message);
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtArticle.Text) || string.IsNullOrWhiteSpace(txtName.Text) || numCost.Value <= 0)
            {
                MessageBox.Show("Заполните обязательные поля (Артикул, Наименование, Цена)", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string query;
            if (_article == null) // Insert
            {
                query = @"INSERT INTO Product 
                          (ProductArticleNumber, ProductName, ProductDescription, ProductCategory, ProductManufacturer, ProductCost, ProductDiscountAmount, ProductQuantityInStock, ProductUnit, ProductSupplier, ProductPhoto)
                          VALUES 
                          (@Art, @Name, @Desc, @Cat, @Man, @Cost, @Disc, @Qty, @Unit, @Sup, @Photo)";
            }
            else // Update
            {
                query = @"UPDATE Product SET 
                          ProductName=@Name, ProductDescription=@Desc, ProductCategory=@Cat, ProductManufacturer=@Man, 
                          ProductCost=@Cost, ProductDiscountAmount=@Disc, ProductQuantityInStock=@Qty, ProductUnit=@Unit, 
                          ProductSupplier=@Sup, ProductPhoto=@Photo
                          WHERE ProductArticleNumber=@Art";
            }

            try
            {
                using (var conn = new SqlConnection(ConnectionHelper.ConnectionString))
                {
                    conn.Open();
                    var cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Art", txtArticle.Text);
                    cmd.Parameters.AddWithValue("@Name", txtName.Text);
                    cmd.Parameters.AddWithValue("@Desc", txtDescription.Text);
                    cmd.Parameters.AddWithValue("@Cat", cmbCategory.Text);
                    cmd.Parameters.AddWithValue("@Man", cmbManufacturer.Text);
                    cmd.Parameters.AddWithValue("@Cost", numCost.Value);
                    cmd.Parameters.AddWithValue("@Disc", numDiscount.Value);
                    cmd.Parameters.AddWithValue("@Qty", numQuantity.Value);
                    cmd.Parameters.AddWithValue("@Unit", txtUnit.Text);
                    cmd.Parameters.AddWithValue("@Sup", txtSupplier.Text);

                    if (string.IsNullOrEmpty(_currentPhotoName))
                        cmd.Parameters.AddWithValue("@Photo", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("@Photo", _currentPhotoName);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Данные сохранены!");
                    DialogResult = DialogResult.OK;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка сохранения: " + ex.Message);
            }
        }
    }
}