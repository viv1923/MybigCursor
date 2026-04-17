//namespace MybigCursor
//{
//    public partial class Form1 : Form
//    {
//        private readonly System.Windows.Forms.Timer _timer = new System.Windows.Forms.Timer();
//        private Point _lastMousePos;
//        private DateTime _lastTime;

//        private readonly Queue<(double speed, DateTime time)> _recentSpeeds = new();

//        private bool _shakeActive = false;
//        private DateTime _shakeUntil = DateTime.MinValue;

//        // You can tune these later
//        private const double ShakeThreshold = 5000.0; // pixels per second
//        private const int SpeedWindowMs = 150;
//        private const int ShakeHoldMs = 400;

//        public Form1()
//        {
//            InitializeComponent();

//            _lastMousePos = Cursor.Position;
//            _lastTime = DateTime.Now;

//            _timer.Interval = 16; // about 60 times/sec
//            _timer.Tick += Timer_Tick;
//            _timer.Start();
//        }

//        private void Timer_Tick(object? sender, EventArgs e)
//        {
//            Point currentPos = Cursor.Position;
//            DateTime now = DateTime.Now;

//            double dt = (now - _lastTime).TotalSeconds;
//            if (dt <= 0)
//                return;

//            int dx = currentPos.X - _lastMousePos.X;
//            int dy = currentPos.Y - _lastMousePos.Y;

//            double distance = Math.Sqrt(dx * dx + dy * dy);
//            double speed = distance / dt;

//            _recentSpeeds.Enqueue((speed, now));

//            while (_recentSpeeds.Count > 0 &&
//                   (now - _recentSpeeds.Peek().time).TotalMilliseconds > SpeedWindowMs)
//            {
//                _recentSpeeds.Dequeue();
//            }

//            double avgSpeed = 0;
//            if (_recentSpeeds.Count > 0)
//            {
//                double total = 0;
//                foreach (var item in _recentSpeeds)
//                    total += item.speed;

//                avgSpeed = total / _recentSpeeds.Count;
//            }

//            if (avgSpeed > ShakeThreshold)
//            {
//                _shakeActive = true;
//                _shakeUntil = now.AddMilliseconds(ShakeHoldMs);
//            }

//            if (_shakeActive && now > _shakeUntil)
//            {
//                _shakeActive = false;
//            }

//             lblStatus.Text = _shakeActive
//                ? $"SHAKE DETECTED | Avg speed: {avgSpeed:F0}"
//                : $"Normal | Avg speed: {avgSpeed:F0}";

//            _lastMousePos = currentPos;
//            _lastTime = now;
//        }

//        private void lblStatus_Click(object sender, EventArgs e)
//        {

//        }
//    }
//}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MybigCursor
{
    public partial class Form1 : Form
    {
        private readonly System.Windows.Forms.Timer _timer = new System.Windows.Forms.Timer();
        private Point _lastMousePos;
        private DateTime _lastTime;

        private readonly Queue<(double speed, DateTime time)> _recentSpeeds = new();

        private bool _shakeActive = false;
        private DateTime _shakeUntil = DateTime.MinValue;

        private readonly OverlayForm _overlay;

        //private const double ShakeThreshold = 9000.0;
        private AppSettings _settings = new AppSettings();
        private const int SpeedWindowMs = 150;
        private const int ShakeHoldMs = 400;

        public Form1()
        {
            InitializeComponent();
            _settings = SettingsManager.Load();

            _overlay = new OverlayForm();
            _overlay.Hide();

            ApplySettings();

            _lastMousePos = Cursor.Position;
            _lastTime = DateTime.Now;

            _timer.Interval = 16;
            _timer.Tick += Timer_Tick;
            _timer.Start();
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            Point currentPos = Cursor.Position;
            DateTime now = DateTime.Now;

            double dt = (now - _lastTime).TotalSeconds;
            if (dt <= 0)
                return;

            int dx = currentPos.X - _lastMousePos.X;
            int dy = currentPos.Y - _lastMousePos.Y;

            double distance = Math.Sqrt(dx * dx + dy * dy);
            double speed = distance / dt;

            _recentSpeeds.Enqueue((speed, now));

            while (_recentSpeeds.Count > 0 &&
                   (now - _recentSpeeds.Peek().time).TotalMilliseconds > SpeedWindowMs)
            {
                _recentSpeeds.Dequeue();
            }

            double avgSpeed = 0;
            if (_recentSpeeds.Count > 0)
            {
                double total = 0;
                foreach (var item in _recentSpeeds)
                    total += item.speed;

                avgSpeed = total / _recentSpeeds.Count;
            }

            if (avgSpeed > _settings.ShakeThreshold)
            {
                _shakeActive = true;
                _shakeUntil = now.AddMilliseconds(ShakeHoldMs);
            }

            if (_shakeActive && now > _shakeUntil)
            {
                _shakeActive = false;
            }

            lblStatus.Text = _shakeActive
                ? $"SHAKE DETECTED | Avg speed: {avgSpeed:F0} px/s | Threshold: {_settings.ShakeThreshold:F0}"
                : $"Normal | Avg speed: {avgSpeed:F0} px/s | Threshold: {_settings.ShakeThreshold:F0}";

            //float intensity = (float)Math.Min(1.0, avgSpeed / 15000.0);
            if (_shakeActive)
                _overlay.ShowAtCursor(currentPos);
            else
                _overlay.HideOverlay();

            _lastMousePos = currentPos;
            _lastTime = now;
        }
        private void ApplySettings()
        {
            if (_settings.UseCustomImage &&
                !string.IsNullOrWhiteSpace(_settings.EquippedImagePath) &&
                System.IO.File.Exists(_settings.EquippedImagePath))
            {
                _overlay.SetOverlayImage(_settings.EquippedImagePath);
            }
            else
            {
                _overlay.ClearOverlayImage();
            }
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            SettingsManager.Save(_settings);
            _overlay.Close();
            base.OnFormClosing(e);
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            using (SettingsForm frm = new SettingsForm(_settings))
            {
                frm.ShowDialog();
                ApplySettings();
                SettingsManager.Save(_settings);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
