using System.Drawing.Drawing2D;
using System.Drawing;
using System;
using System.Windows.Forms;



namespace Gipromez
{
    public partial class Form1 : Form
    {

        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;


        public Form1()
        {
            InitializeComponent();

            // Handle mouse click
            this.MouseDown += new MouseEventHandler(Form1_MouseDown);
            this.MouseMove += new MouseEventHandler(Form1_MouseMove);
            this.MouseUp += new MouseEventHandler(Form1_MouseUp);

            // Удаление стандартной рамки окна
            this.FormBorderStyle = FormBorderStyle.None;

            // Создание скругленных углов
            GraphicsPath path = new GraphicsPath();
            int radius = 50; // Радиус скругления
            path.AddArc(new Rectangle(0, 0, radius, radius), 180, 90);
            path.AddArc(new Rectangle(Width - radius, 0, radius, radius), -90, 90);
            path.AddArc(new Rectangle(Width - radius, Height - radius, radius, radius), 0, 90);
            path.AddArc(new Rectangle(0, Height - radius, radius, radius), 90, 90);
            path.CloseAllFigures();

            // Применение формы к окну
            this.Region = new Region(path);

        }

        void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                dragging = true;
                dragCursorPoint = Cursor.Position;
                dragFormPoint = this.Location;
            }
        }

        void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(dif));
            }
        }

        void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }


        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            // Обновление формы при изменении размеров окна
            var path = new GraphicsPath();
            int radius = 50;
            path.AddArc(new Rectangle(0, 0, radius, radius), 180, 90);
            path.AddArc(new Rectangle(Width - radius, 0, radius, radius), -90, 90);
            path.AddArc(new Rectangle(Width - radius, Height - radius, radius, radius), 0, 90);
            path.AddArc(new Rectangle(0, Height - radius, radius, radius), 90, 90);
            path.CloseAllFigures();
            this.Region = new Region(path);
        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {
            ActiveForm.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
