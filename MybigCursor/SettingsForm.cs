using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
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
        private void UploadImg1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files|*.png;*.jpg;*.jpeg;*.bmp";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                _settings.ImagePath1 = ofd.FileName;
                pictureBox1.Image = Image.FromFile(ofd.FileName);
            }
        }

        private void UploadImg2_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files|*.png;*.jpg;*.jpeg;*.bmp";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                _settings.ImagePath2 = ofd.FileName;
                pictureBox2.Image = Image.FromFile(ofd.FileName);
            }
        }

        private void UploadImg3_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files|*.png;*.jpg;*.jpeg;*.bmp";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                _settings.ImagePath3 = ofd.FileName;
                pictureBox3.Image = Image.FromFile(ofd.FileName);
            }
        }

        private void EquipImg1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(_settings.ImagePath1))
            {
                _settings.EquippedImagePath = _settings.ImagePath1;
                _settings.UseCustomImage = true;
            }
        }

        private void EquipImg2_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(_settings.ImagePath2))
            {
                _settings.EquippedImagePath = _settings.ImagePath2;
                _settings.UseCustomImage = true;
            }
        }

        private void EquipImg3_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(_settings.ImagePath3))
            {
                _settings.EquippedImagePath = _settings.ImagePath3;
                _settings.UseCustomImage = true;
            }
        }

        private void btnUnequip_Click(object sender, EventArgs e)
        {
            _settings.UseCustomImage = false;
            _settings.EquippedImagePath = null;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _settings.ShakeThreshold = (double)numericUpDown1.Value;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            numericUpDown1.Minimum = 500;
            numericUpDown1.Maximum = 50000;
            numericUpDown1.Increment = 200;

            numericUpDown1.Value = (decimal)_settings.ShakeThreshold;
        }
    }
}
