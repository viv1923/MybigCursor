using System;
using System.IO;
using System.Text.Json;

namespace MybigCursor
{
    public static class SettingsManager
    {
        private static readonly string AppFolder =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "MybigCursor");

        private static readonly string SettingsFile =
            Path.Combine(AppFolder, "settings.json");

        private static readonly string ImagesFolder =
            Path.Combine(AppFolder, "Images");

        public static void EnsureFolders()
        {
            if (!Directory.Exists(AppFolder))
                Directory.CreateDirectory(AppFolder);

            if (!Directory.Exists(ImagesFolder))
                Directory.CreateDirectory(ImagesFolder);
        }

        public static AppSettings Load()
        {
            try
            {
                EnsureFolders();

                if (!File.Exists(SettingsFile))
                    return new AppSettings();

                string json = File.ReadAllText(SettingsFile);
                AppSettings? settings = JsonSerializer.Deserialize<AppSettings>(json);

                return settings ?? new AppSettings();
            }
            catch
            {
                return new AppSettings();
            }
        }

        public static void Save(AppSettings settings)
        {
            try
            {
                EnsureFolders();

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };

                string json = JsonSerializer.Serialize(settings, options);
                File.WriteAllText(SettingsFile, json);
            }
            catch
            {
                // You can log later if needed
            }
        }

        public static string CopyImageToAppFolder(string sourceFilePath, string slotName)
        {
            EnsureFolders();

            string extension = Path.GetExtension(sourceFilePath);
            string fileName = $"{slotName}{extension}";
            string destinationPath = Path.Combine(ImagesFolder, fileName);

            File.Copy(sourceFilePath, destinationPath, true);

            return destinationPath;
        }

        public static void DeleteImageIfExists(string? path)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(path) && File.Exists(path))
                    File.Delete(path);
            }
            catch
            {
                // ignore delete errors for now
            }
        }
    }
}