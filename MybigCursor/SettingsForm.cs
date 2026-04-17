using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace MybigCursor
{
    public partial class SettingsForm : Form
    {
        private AppSettings _settings;

        public SettingsForm(AppSettings settings)
        {
            InitializeComponent();
            _settings = settings;
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            numericUpDown1.Minimum = 1000;
            numericUpDown1.Maximum = 50000;
            numericUpDown1.Increment = 500;

            decimal threshold = (decimal)_settings.ShakeThreshold;

            if (threshold < numericUpDown1.Minimum)
                threshold = numericUpDown1.Minimum;

            if (threshold > numericUpDown1.Maximum)
                threshold = numericUpDown1.Maximum;

            numericUpDown1.Value = threshold;

            LoadPreview(pictureBox1, _settings.ImagePath1);
            LoadPreview(pictureBox2, _settings.ImagePath2);
            LoadPreview(pictureBox3, _settings.ImagePath3);

            RefreshEquippedUI();
            lblThresholdInfo.Text = $"Current threshold: {_settings.ShakeThreshold:F0} px/s";
        }

        private void UploadImg1_Click(object sender, EventArgs e)
        {
            _settings.ImagePath1 = PickAndStoreImage("slot1", _settings.ImagePath1);
            LoadPreview(pictureBox1, _settings.ImagePath1);

            if (_settings.EquippedImagePath == null && !string.IsNullOrWhiteSpace(_settings.ImagePath1))
            {
                _settings.EquippedImagePath = _settings.ImagePath1;
                _settings.UseCustomImage = true;
            }

            RefreshEquippedUI();
        }

        private void UploadImg2_Click(object sender, EventArgs e)
        {
            _settings.ImagePath2 = PickAndStoreImage("slot2", _settings.ImagePath2);
            LoadPreview(pictureBox2, _settings.ImagePath2);

            RefreshEquippedUI();
        }

        private void UploadImg3_Click(object sender, EventArgs e)
        {
            _settings.ImagePath3 = PickAndStoreImage("slot3", _settings.ImagePath3);
            LoadPreview(pictureBox3, _settings.ImagePath3);

            RefreshEquippedUI();
        }

        private void EquipImg1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(_settings.ImagePath1) && File.Exists(_settings.ImagePath1))
            {
                _settings.EquippedImagePath = _settings.ImagePath1;
                _settings.UseCustomImage = true;
                RefreshEquippedUI();
            }
        }

        private void EquipImg2_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(_settings.ImagePath2) && File.Exists(_settings.ImagePath2))
            {
                _settings.EquippedImagePath = _settings.ImagePath2;
                _settings.UseCustomImage = true;
                RefreshEquippedUI();
            }
        }

        private void EquipImg3_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(_settings.ImagePath3) && File.Exists(_settings.ImagePath3))
            {
                _settings.EquippedImagePath = _settings.ImagePath3;
                _settings.UseCustomImage = true;
                RefreshEquippedUI();
            }
        }

        private void btnUnequip_Click(object sender, EventArgs e)
        {
            _settings.UseCustomImage = false;
            _settings.EquippedImagePath = null;
            RefreshEquippedUI();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _settings.ShakeThreshold = (double)numericUpDown1.Value;
            SettingsManager.Save(_settings);
            DialogResult = DialogResult.OK;
            Close();
        }

        private void LoadPreview(PictureBox pictureBox, string? imagePath)
        {
            if (pictureBox.Image != null)
            {
                pictureBox.Image.Dispose();
                pictureBox.Image = null;
            }

            if (!string.IsNullOrWhiteSpace(imagePath) && File.Exists(imagePath))
            {
                using (var temp = Image.FromFile(imagePath))
                {
                    pictureBox.Image = new Bitmap(temp);
                }
            }
        }

        private void RefreshEquippedUI()
        {
            UploadImg1.BackColor = SystemColors.ControlLight;
            UploadImg2.BackColor = SystemColors.ControlLight;
            UploadImg3.BackColor = SystemColors.ControlLight;

            lblEquipped.Text = _settings.UseCustomImage && !string.IsNullOrWhiteSpace(_settings.EquippedImagePath)
                ? $"Equipped: {GetEquippedSlotName()}"
                : "Equipped: Default Cursor";

            if (_settings.UseCustomImage)
            {
                if (_settings.EquippedImagePath == _settings.ImagePath1)
                    UploadImg1.BackColor = Color.LightGreen;
                else if (_settings.EquippedImagePath == _settings.ImagePath2)
                    UploadImg2.BackColor = Color.LightGreen;
                else if (_settings.EquippedImagePath == _settings.ImagePath3)
                    UploadImg3.BackColor = Color.LightGreen;
            }
        }

        private string GetEquippedSlotName()
        {
            if (_settings.EquippedImagePath == _settings.ImagePath1)
                return "Slot 1";

            if (_settings.EquippedImagePath == _settings.ImagePath2)
                return "Slot 2";

            if (_settings.EquippedImagePath == _settings.ImagePath3)
                return "Slot 3";

            return "Custom Image";
        }

        private string? PickAndStoreImage(string slotName, string? oldPath)
        {
            using OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files|*.png;*.jpg;*.jpeg;*.bmp;*.webp";

            if (ofd.ShowDialog() != DialogResult.OK)
                return oldPath;

            string savedPath = SettingsManager.CopyImageToAppFolder(ofd.FileName, slotName);

            if (!string.IsNullOrWhiteSpace(oldPath) &&
                oldPath != savedPath &&
                File.Exists(oldPath))
            {
                SettingsManager.DeleteImageIfExists(oldPath);
            }

            return savedPath;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            lblThresholdInfo.Text = $"Current threshold: {numericUpDown1.Value} px/s";
        }

    }
}