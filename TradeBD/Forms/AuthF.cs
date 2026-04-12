using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using TradeBD.Classes;
using TradeBD.MainForms;

namespace TradeBD
{
    public partial class AuthF : Form
    {
        public AuthF()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string login = txtLogin.Text.Trim();
            string pass = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(pass))
            {
                MessageBox.Show("Введите логин и пароль!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Убрали try-catch, чтобы видеть чистое исключение
            using (SqlConnection conn = new SqlConnection(ConnectionHelper.ConnectionString))
            {
                conn.Open();
                string query = @"
                    SELECT u.UserID, u.RoleID, r.RoleName, u.UserSurname, u.UserName 
                    FROM [User] u
                    JOIN [Role] r ON u.RoleID = r.RoleID
                    WHERE u.UserLogin = @Login AND u.UserPassword = @Pass";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Login", login);
                cmd.Parameters.AddWithValue("@Pass", pass);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    int userId = (int)reader["UserID"];
                    int roleId = (int)reader["RoleID"];
                    string roleName = reader["RoleName"].ToString();
                    string fio = $"{reader["UserSurname"]} {reader["UserName"]}";

                    MessageBox.Show($"Добро пожаловать, {fio}!\nВаша роль: {roleName}", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    ProductF productForm = new ProductF(roleId, userId, fio);
                    this.Hide();
                    productForm.ShowDialog(); // <--- Здесь вылетит ошибка
                    this.Show();
                }
                else
                {
                    MessageBox.Show("Неверный логин или пароль.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnGuest_Click(object sender, EventArgs e)
        {
            // Убрали try-catch
            ProductF productForm = new ProductF(0, 0, "Гость");
            this.Hide();
            productForm.ShowDialog(); // <--- Здесь вылетит ошибка
            this.Show();
        }
    }
}