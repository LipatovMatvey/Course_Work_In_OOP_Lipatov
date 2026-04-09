namespace Course_work_in_OOP_Lipatov
{
    partial class PatientForm
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
            label1 = new Label();
            txtFullName = new TextBox();
            label2 = new Label();
            numAge = new NumericUpDown();
            label3 = new Label();
            cmbGender = new ComboBox();
            label4 = new Label();
            txtDisease = new TextBox();
            label5 = new Label();
            cmbSeverity = new ComboBox();
            label6 = new Label();
            numDuration = new NumericUpDown();
            label7 = new Label();
            txtDepartment = new TextBox();
            btnSave = new Button();
            btnCancel = new Button();
            panel1 = new Panel();
            label8 = new Label();
            ((System.ComponentModel.ISupportInitialize)numAge).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numDuration).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label1.Location = new Point(30, 100);
            label1.Name = "label1";
            label1.Size = new Size(97, 18);
            label1.TabIndex = 0;
            label1.Text = "Полное имя:";
            // 
            // txtFullName
            // 
            txtFullName.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txtFullName.BackColor = Color.WhiteSmoke;
            txtFullName.BorderStyle = BorderStyle.FixedSingle;
            txtFullName.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, 204);
            txtFullName.Location = new Point(148, 96);
            txtFullName.Margin = new Padding(3, 4, 3, 4);
            txtFullName.Name = "txtFullName";
            txtFullName.Size = new Size(250, 24);
            txtFullName.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label2.Location = new Point(30, 144);
            label2.Name = "label2";
            label2.Size = new Size(70, 18);
            label2.TabIndex = 2;
            label2.Text = "Возраст:";
            // 
            // numAge
            // 
            numAge.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            numAge.BackColor = Color.WhiteSmoke;
            numAge.BorderStyle = BorderStyle.FixedSingle;
            numAge.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, 204);
            numAge.Location = new Point(148, 141);
            numAge.Margin = new Padding(3, 4, 3, 4);
            numAge.Maximum = new decimal(new int[] { 120, 0, 0, 0 });
            numAge.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numAge.Name = "numAge";
            numAge.Size = new Size(100, 24);
            numAge.TabIndex = 3;
            numAge.Value = new decimal(new int[] { 30, 0, 0, 0 });
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label3.Location = new Point(30, 188);
            label3.Name = "label3";
            label3.Size = new Size(41, 18);
            label3.TabIndex = 4;
            label3.Text = "Пол:";
            // 
            // cmbGender
            // 
            cmbGender.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cmbGender.BackColor = Color.WhiteSmoke;
            cmbGender.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbGender.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, 204);
            cmbGender.FormattingEnabled = true;
            cmbGender.Items.AddRange(new object[] { "Мужской", "Женский" });
            cmbGender.Location = new Point(148, 184);
            cmbGender.Margin = new Padding(3, 4, 3, 4);
            cmbGender.Name = "cmbGender";
            cmbGender.Size = new Size(100, 26);
            cmbGender.TabIndex = 5;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label4.Location = new Point(30, 231);
            label4.Name = "label4";
            label4.Size = new Size(71, 18);
            label4.TabIndex = 6;
            label4.Text = "Диагноз:";
            // 
            // txtDisease
            // 
            txtDisease.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txtDisease.BackColor = Color.WhiteSmoke;
            txtDisease.BorderStyle = BorderStyle.FixedSingle;
            txtDisease.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, 204);
            txtDisease.Location = new Point(148, 228);
            txtDisease.Margin = new Padding(3, 4, 3, 4);
            txtDisease.Name = "txtDisease";
            txtDisease.Size = new Size(250, 24);
            txtDisease.TabIndex = 7;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label5.Location = new Point(30, 275);
            label5.Name = "label5";
            label5.Size = new Size(71, 18);
            label5.TabIndex = 8;
            label5.Text = "Тяжесть:";
            // 
            // cmbSeverity
            // 
            cmbSeverity.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cmbSeverity.BackColor = Color.WhiteSmoke;
            cmbSeverity.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbSeverity.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, 204);
            cmbSeverity.FormattingEnabled = true;
            cmbSeverity.Items.AddRange(new object[] { "Легкая", "Средняя", "Тяжелая" });
            cmbSeverity.Location = new Point(148, 271);
            cmbSeverity.Margin = new Padding(3, 4, 3, 4);
            cmbSeverity.Name = "cmbSeverity";
            cmbSeverity.Size = new Size(100, 26);
            cmbSeverity.TabIndex = 9;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label6.Location = new Point(30, 319);
            label6.Name = "label6";
            label6.Size = new Size(113, 18);
            label6.TabIndex = 10;
            label6.Text = "Длительность:";
            // 
            // numDuration
            // 
            numDuration.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            numDuration.BackColor = Color.WhiteSmoke;
            numDuration.BorderStyle = BorderStyle.FixedSingle;
            numDuration.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, 204);
            numDuration.Location = new Point(148, 316);
            numDuration.Margin = new Padding(3, 4, 3, 4);
            numDuration.Maximum = new decimal(new int[] { 365, 0, 0, 0 });
            numDuration.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numDuration.Name = "numDuration";
            numDuration.Size = new Size(100, 24);
            numDuration.TabIndex = 11;
            numDuration.Value = new decimal(new int[] { 7, 0, 0, 0 });
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label7.Location = new Point(30, 362);
            label7.Name = "label7";
            label7.Size = new Size(89, 18);
            label7.TabIndex = 12;
            label7.Text = "Отделение:";
            // 
            // txtDepartment
            // 
            txtDepartment.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txtDepartment.BackColor = Color.WhiteSmoke;
            txtDepartment.BorderStyle = BorderStyle.FixedSingle;
            txtDepartment.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, 204);
            txtDepartment.Location = new Point(148, 359);
            txtDepartment.Margin = new Padding(3, 4, 3, 4);
            txtDepartment.Name = "txtDepartment";
            txtDepartment.Size = new Size(250, 24);
            txtDepartment.TabIndex = 13;
            // 
            // btnSave
            // 
            btnSave.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnSave.BackColor = Color.SeaGreen;
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
            btnSave.ForeColor = Color.White;
            btnSave.Location = new Point(148, 419);
            btnSave.Margin = new Padding(3, 4, 3, 4);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(120, 44);
            btnSave.TabIndex = 14;
            btnSave.Text = "Сохранить";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += btnSave_Click;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnCancel.BackColor = Color.SteelBlue;
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
            btnCancel.ForeColor = Color.White;
            btnCancel.Location = new Point(278, 419);
            btnCancel.Margin = new Padding(3, 4, 3, 4);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(120, 44);
            btnCancel.TabIndex = 15;
            btnCancel.Text = "Отмена";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += btnCancel_Click;
            // 
            // panel1
            // 
            panel1.BackColor = Color.SteelBlue;
            panel1.Controls.Add(label8);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(3, 4, 3, 4);
            panel1.Name = "panel1";
            panel1.Size = new Size(450, 75);
            panel1.TabIndex = 16;
            // 
            // label8
            // 
            label8.Anchor = AnchorStyles.Top;
            label8.AutoSize = true;
            label8.BackColor = Color.SteelBlue;
            label8.Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Bold, GraphicsUnit.Point, 204);
            label8.ForeColor = Color.White;
            label8.Location = new Point(70, 19);
            label8.Name = "label8";
            label8.Padding = new Padding(10, 12, 10, 12);
            label8.Size = new Size(274, 53);
            label8.TabIndex = 0;
            label8.Text = "ФОРМА ПАЦИЕНТА";
            // 
            // PatientForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.WhiteSmoke;
            ClientSize = new Size(450, 500);
            Controls.Add(panel1);
            Controls.Add(btnCancel);
            Controls.Add(btnSave);
            Controls.Add(txtDepartment);
            Controls.Add(label7);
            Controls.Add(numDuration);
            Controls.Add(label6);
            Controls.Add(cmbSeverity);
            Controls.Add(label5);
            Controls.Add(txtDisease);
            Controls.Add(label4);
            Controls.Add(cmbGender);
            Controls.Add(label3);
            Controls.Add(numAge);
            Controls.Add(label2);
            Controls.Add(txtFullName);
            Controls.Add(label1);
            Margin = new Padding(3, 4, 3, 4);
            Name = "PatientForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Пациент";
            ((System.ComponentModel.ISupportInitialize)numAge).EndInit();
            ((System.ComponentModel.ISupportInitialize)numDuration).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();

        }
        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtFullName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numAge;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbGender;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtDisease;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbSeverity;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numDuration;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtDepartment;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label8;
    }
}