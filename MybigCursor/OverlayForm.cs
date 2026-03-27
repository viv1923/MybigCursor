using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace MybigCursor
{
    public partial class OverlayForm : Form
    {
        private readonly System.Windows.Forms.Timer _animationTimer = new System.Windows.Forms.Timer();

        private float _currentOpacity = 0f;
        private float _targetOpacity = 0f;

        private float _currentScale = 0.6f;
        private float _targetScale = 0.6f;

        private PointF _currentPos;
        private PointF _targetPos;

        private const int BaseSize = 128;
        public OverlayForm()
        {
            InitializeComponent();

            FormBorderStyle = FormBorderStyle.None;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.Manual;
            TopMost = true;
            BackColor = Color.Lime;
            TransparencyKey = Color.Lime;
            Width = BaseSize;
            Height = BaseSize;

            Opacity = 0;

            _currentPos = new PointF(0, 0);
            _targetPos = new PointF(0, 0);

            _animationTimer.Interval = 16; // ~60 FPS
            _animationTimer.Tick += AnimationTimer_Tick;
            _animationTimer.Start();
        }

        protected override bool ShowWithoutActivation => true;

        private void AnimationTimer_Tick(object? sender, EventArgs e)
        {
            // Smooth opacity
            _currentOpacity += (_targetOpacity - _currentOpacity) * 0.18f;

            // Smooth scale
            _currentScale += (_targetScale - _currentScale) * 0.18f;

            // Smooth position
            _currentPos.X += (_targetPos.X - _currentPos.X) * 0.25f;
            _currentPos.Y += (_targetPos.Y - _currentPos.Y) * 0.25f;

            Left = (int)_currentPos.X;
            Top = (int)_currentPos.Y;

            Opacity = Math.Max(0, Math.Min(1, _currentOpacity));

            int size = (int)(BaseSize * _currentScale);
            Width = size;
            Height = size;

            // Keep hidden when nearly invisible
            if (_currentOpacity < 0.02f && _targetOpacity == 0f)
            {
                if (Visible)
                    Hide();
            }
            else
            {
                if (!Visible)
                    Show();
            }

            Invalidate();
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            float scaleX = Width / 128f;
            float scaleY = Height / 128f;

            PointF[] arrow =
            {
                new PointF(18 * scaleX, 12 * scaleY),
                new PointF(18 * scaleX, 92 * scaleY),
                new PointF(36 * scaleX, 72 * scaleY),
                new PointF(52 * scaleX, 112 * scaleY),
                new PointF(67 * scaleX, 105 * scaleY),
                new PointF(50 * scaleX, 67 * scaleY),
                new PointF(82 * scaleX, 67 * scaleY)
            };

            using SolidBrush fillBrush = new SolidBrush(Color.FromArgb(245, Color.White));
            using Pen outlinePen = new Pen(Color.Black, 3);

            e.Graphics.FillPolygon(fillBrush, arrow);
            e.Graphics.DrawPolygon(outlinePen, arrow);
        }

        public void ShowAtCursor(Point cursorPos, float intensity)
        {
            //_targetPos = new PointF(cursorPos.X + 16, cursorPos.Y + 16);
            _targetPos = new PointF(cursorPos.X - (Width * 0.2f), cursorPos.Y - (Width * 0.2f));

            _targetOpacity = 1f;
            _targetScale = 0.8f + (0.4f * intensity); // between 0.8 and 1.2
        }
        public void HideOverlay()
        {
            _targetOpacity = 0f;
            _targetScale = 0.6f;
        }
    }
}