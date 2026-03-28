using System;

namespace MybigCursor
{
    public class AppSettings
    {
        public string? ImagePath1 { get; set; }
        public string? ImagePath2 { get; set; }
        public string? ImagePath3 { get; set; }

        public string? EquippedImagePath { get; set; }

        public bool UseCustomImage { get; set; } = false;

        public double ShakeThreshold { get; set; } = 15000.0;
    }
}