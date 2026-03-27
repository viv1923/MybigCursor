using System;
using System.Drawing;
using System.Windows.Forms;

namespace MybigCursor
{
    public partial class OverlayForm : Form
    {
        public OverlayForm()
        {
            InitializeComponent();

            FormBorderStyle = FormBorderStyle.None;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.Manual;
            TopMost = true;
            BackColor = Color.Lime;
            TransparencyKey = Color.Lime;
            Width = 128;
            Height = 128;
        }

        protected override bool ShowWithoutActivation => true;

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            Point[] arrow =
            {
                new Point(18, 12),
                new Point(18, 92),
                new Point(36, 72),
                new Point(52, 112),
                new Point(67, 105),
                new Point(50, 67),
                new Point(82, 67)
            };

            using SolidBrush fillBrush = new SolidBrush(Color.White);
            using Pen outlinePen = new Pen(Color.Black, 3);

            e.Graphics.FillPolygon(fillBrush, arrow);
            e.Graphics.DrawPolygon(outlinePen, arrow);
        }

        public void ShowAtCursor(Point cursorPos)
        {
            Left = cursorPos.X + 12;
            Top = cursorPos.Y + 12;

            if (!Visible)
                Show();

            Invalidate();
        }

        public void HideOverlay()
        {
            Hide();
        }
    }
}