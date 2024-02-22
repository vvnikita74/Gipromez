using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Gipromez.Buttons
{
    public class RoundedButton: Button
    {
        protected override void OnPaint(PaintEventArgs pevent)
        {
            GraphicsPath grPath = new GraphicsPath();
            float radius = 120.0F; // Радиус закругления углов
            grPath.AddArc(new RectangleF(0, 0, radius, radius), 180, 90);
            grPath.AddArc(new RectangleF(this.Width - radius - 1, 0, radius, radius), -90, 90);
            grPath.AddArc(new RectangleF(this.Width - radius - 1, this.Height - radius - 1, radius, radius), 0, 90);
            grPath.AddArc(new RectangleF(0, this.Height - radius - 1, radius, radius), 90, 90);
            grPath.CloseAllFigures();

            this.Region = new Region(grPath);
            base.OnPaint(pevent);
          
        }

    }
}
