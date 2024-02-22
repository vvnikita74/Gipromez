using System;
using System.Windows.Forms;
using Npgsql;
using System.Drawing.Drawing2D;
using System.Drawing;

namespace Gipromez
{
    public partial class AuthForm : Form
    {

        string connectionString = "Host=localhost;Port=5432;Username=adminPassword=9832;Database=users";

        private bool CheckUserCredentials(string username, string password)
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("SELECT COUNT(*) FROM users WHERE username = @username AND password = @password", conn))
                {
                    cmd.Parameters.AddWithValue("username", username);
                    cmd.Parameters.AddWithValue("password", password); // В реальном приложении пароли должны храниться в зашифрованном виде
                    int userCount = Convert.ToInt32(cmd.ExecuteScalar());
                    return userCount > 0;
                }
            }
        }

        public AuthForm()
        {
            InitializeComponent();

            // Удаление стандартной рамки окна
            this.FormBorderStyle = FormBorderStyle.None;

            // Создание скругленных углов
            GraphicsPath path = new GraphicsPath();
            int radius = 96; // Радиус скругления
            path.AddArc(new Rectangle(0, 0, radius, radius), 180, 90);
            path.AddArc(new Rectangle(Width - radius, 0, radius, radius), -90, 90);
            path.AddArc(new Rectangle(Width - radius, Height - radius, radius, radius), 0, 90);
            path.AddArc(new Rectangle(0, Height - radius, radius, radius), 90, 90);
            path.CloseAllFigures();

            // Применение формы к окну
            this.Region = new Region(path);

        }

        private void roundedButton1_Click(object sender, EventArgs e)
        {

        }
    }
}
