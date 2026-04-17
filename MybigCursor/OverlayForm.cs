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

        private Image? _overlayImage = null;
        private bool _useCustomImage = false;

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

            SetStyle(ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint, true);
            UpdateStyles();
        }

        protected override bool ShowWithoutActivation => true;

        private void AnimationTimer_Tick(object? sender, EventArgs e)
        {
            // Smooth opacity fade
            _currentOpacity += (_targetOpacity - _currentOpacity) * 0.18f;

            // Smooth scale animation
            _currentScale += (_targetScale - _currentScale) * 0.18f;

            // Smooth follow movement
            _currentPos.X += (_targetPos.X - _currentPos.X) * 0.25f;
            _currentPos.Y += (_targetPos.Y - _currentPos.Y) * 0.25f;

            int size = (int)(BaseSize * _currentScale);
            if (size < 1)
                size = 1;

            Width = size;
            Height = size;

            Left = (int)_currentPos.X;
            Top = (int)_currentPos.Y;

            Opacity = Math.Max(0, Math.Min(1, _currentOpacity));

            // Only hide when almost fully faded out
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

            if (_useCustomImage && _overlayImage != null)
            {
                e.Graphics.DrawImage(_overlayImage, 0, 0, Width, Height);
            }
            else
            {
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

                using SolidBrush fillBrush = new SolidBrush(Color.White);
                using Pen outlinePen = new Pen(Color.Black, 3);

                e.Graphics.FillPolygon(fillBrush, arrow);
                e.Graphics.DrawPolygon(outlinePen, arrow);
            }
        }

        // Use this if Form1 is passing intensity
        public void ShowAtCursor(Point cursorPos, float intensity)
        {
            _targetPos = new PointF(
                cursorPos.X - (Width * 0.2f),
                cursorPos.Y - (Width * 0.2f)
            );

            _targetOpacity = 1f;
            _targetScale = 0.8f + (0.4f * intensity); // 0.8 to 1.2
        }

        // Keep this overload too, in case Form1 is calling ShowAtCursor(currentPos)
        public void ShowAtCursor(Point cursorPos)
        {
            _targetPos = new PointF(
                cursorPos.X - (Width * 0.2f),
                cursorPos.Y - (Width * 0.2f)
            );

            _targetOpacity = 1f;
            _targetScale = 1f;
        }

        public void HideOverlay()
        {
            _targetOpacity = 0f;
            _targetScale = 0.6f;
        }

        public void SetOverlayImage(string imagePath)
        {
            try
            {
                if (!System.IO.File.Exists(imagePath))
                    return;

                _overlayImage?.Dispose();
                _overlayImage = null;

                using (var temp = Image.FromFile(imagePath))
                {
                    _overlayImage = new Bitmap(temp);
                }

                _useCustomImage = true;
                Invalidate();
            }
            catch
            {
                _useCustomImage = false;
            }
        }

        public void ClearOverlayImage()
        {
            _useCustomImage = false;

            if (_overlayImage != null)
            {
                _overlayImage.Dispose();
                _overlayImage = null;
            }

            Invalidate();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            if (_overlayImage != null)
            {
                _overlayImage.Dispose();
                _overlayImage = null;
            }

            base.OnFormClosed(e);
        }
    }
}