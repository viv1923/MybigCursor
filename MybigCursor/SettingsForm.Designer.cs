namespace MybigCursor
{
    partial class SettingsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            UploadImg1 = new Button();
            UploadImg2 = new Button();
            UploadImg3 = new Button();
            pictureBox1 = new PictureBox();
            pictureBox2 = new PictureBox();
            pictureBox3 = new PictureBox();
            EquipImg1 = new Button();
            EquipImg2 = new Button();
            EquipImg3 = new Button();
            btnUnequip = new Button();
            numericUpDown1 = new NumericUpDown();
            btnSave = new Button();
            btnClose = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            SuspendLayout();
            // 
            // UploadImg1
            // 
            UploadImg1.Location = new Point(252, 61);
            UploadImg1.Name = "UploadImg1";
            UploadImg1.Size = new Size(125, 50);
            UploadImg1.TabIndex = 0;
            UploadImg1.Text = "Upload Img 1";
            UploadImg1.UseVisualStyleBackColor = true;
            UploadImg1.Click += UploadImg1_Click;
            // 
            // UploadImg2
            // 
            UploadImg2.Location = new Point(252, 156);
            UploadImg2.Name = "UploadImg2";
            UploadImg2.Size = new Size(125, 45);
            UploadImg2.TabIndex = 1;
            UploadImg2.Text = "Upload Img 2";
            UploadImg2.UseVisualStyleBackColor = true;
            UploadImg2.Click += UploadImg2_Click;
            // 
            // UploadImg3
            // 
            UploadImg3.Location = new Point(252, 249);
            UploadImg3.Name = "UploadImg3";
            UploadImg3.Size = new Size(125, 44);
            UploadImg3.TabIndex = 2;
            UploadImg3.Text = "Upload Img 3";
            UploadImg3.UseVisualStyleBackColor = true;
            UploadImg3.Click += UploadImg3_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(43, 61);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(173, 83);
            pictureBox1.TabIndex = 3;
            pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            pictureBox2.Location = new Point(43, 156);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(173, 84);
            pictureBox2.TabIndex = 4;
            pictureBox2.TabStop = false;
            // 
            // pictureBox3
            // 
            pictureBox3.Location = new Point(43, 249);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(173, 86);
            pictureBox3.TabIndex = 5;
            pictureBox3.TabStop = false;
            // 
            // EquipImg1
            // 
            EquipImg1.Location = new Point(416, 61);
            EquipImg1.Name = "EquipImg1";
            EquipImg1.Size = new Size(108, 50);
            EquipImg1.TabIndex = 6;
            EquipImg1.Text = "Equip Img 1";
            EquipImg1.UseVisualStyleBackColor = true;
            EquipImg1.Click += EquipImg1_Click;
            // 
            // EquipImg2
            // 
            EquipImg2.Location = new Point(416, 156);
            EquipImg2.Name = "EquipImg2";
            EquipImg2.Size = new Size(108, 45);
            EquipImg2.TabIndex = 7;
            EquipImg2.Text = "Equip Img 2";
            EquipImg2.UseVisualStyleBackColor = true;
            EquipImg2.Click += EquipImg2_Click;
            // 
            // EquipImg3
            // 
            EquipImg3.Location = new Point(416, 249);
            EquipImg3.Name = "EquipImg3";
            EquipImg3.Size = new Size(108, 44);
            EquipImg3.TabIndex = 8;
            EquipImg3.Text = "Equip Img 3";
            EquipImg3.UseVisualStyleBackColor = true;
            EquipImg3.Click += EquipImg3_Click;
            // 
            // btnUnequip
            // 
            btnUnequip.Location = new Point(572, 156);
            btnUnequip.Name = "btnUnequip";
            btnUnequip.Size = new Size(169, 45);
            btnUnequip.TabIndex = 9;
            btnUnequip.Text = "Unequip";
            btnUnequip.UseVisualStyleBackColor = true;
            btnUnequip.Click += btnUnequip_Click;
            // 
            // numericUpDown1
            // 
            numericUpDown1.Location = new Point(43, 12);
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new Size(419, 27);
            numericUpDown1.TabIndex = 10;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(509, 379);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(123, 44);
            btnSave.TabIndex = 11;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // btnClose
            // 
            btnClose.Location = new Point(652, 379);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(116, 44);
            btnClose.TabIndex = 12;
            btnClose.Text = "Close";
            btnClose.UseVisualStyleBackColor = true;
            // 
            // SettingsForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnClose);
            Controls.Add(btnSave);
            Controls.Add(numericUpDown1);
            Controls.Add(btnUnequip);
            Controls.Add(EquipImg3);
            Controls.Add(EquipImg2);
            Controls.Add(EquipImg1);
            Controls.Add(pictureBox3);
            Controls.Add(pictureBox2);
            Controls.Add(pictureBox1);
            Controls.Add(UploadImg3);
            Controls.Add(UploadImg2);
            Controls.Add(UploadImg1);
            Name = "SettingsForm";
            Text = "SettingsForm";
            Load += SettingsForm_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Button UploadImg1;
        private Button UploadImg2;
        private Button UploadImg3;
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private PictureBox pictureBox3;
        private Button EquipImg1;
        private Button EquipImg2;
        private Button EquipImg3;
        private Button btnUnequip;
        private NumericUpDown numericUpDown1;
        private Button btnSave;
        private Button btnClose;
    }
}