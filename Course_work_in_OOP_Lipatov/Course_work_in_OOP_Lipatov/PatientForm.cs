using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Course_work_in_OOP_Lipatov
{
    public partial class PatientForm : Form
    {
        /// <summary>
        /// Пациент для редактирования или создания
        /// </summary>
        public Patient Patient { get; private set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        public PatientForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Конструктор для редактирования существующего пациента
        /// </summary>
        /// <param name="patient">Пациент для редактирования</param>
        public PatientForm(Patient patient = null) : this()
        {
            if (patient != null)
            {
                txtFullName.Text = patient.FullName;
                numAge.Value = patient.Age;
                cmbGender.Text = patient.Gender;
                txtDisease.Text = patient.Disease;
                cmbSeverity.Text = patient.Severity;
                numDuration.Value = patient.Duration;
                txtDepartment.Text = patient.Department;
                Patient = patient;
            }
            else
            {
                Patient = new Patient();
            }
        }

        /// <summary>
        /// Обработчки возврата на форму работы с больными
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateForm())
            {
                Patient = new Patient
                {
                    Id = Patient?.Id ?? 0,
                    FullName = txtFullName.Text.Trim(),
                    Age = (int)numAge.Value,
                    Gender = cmbGender.Text,
                    Disease = txtDisease.Text.Trim(),
                    Severity = cmbSeverity.Text,
                    Duration = (int)numDuration.Value,
                    Department = txtDepartment.Text.Trim()
                };
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        /// <summary>
        /// Валидация формы
        /// </summary>
        /// <returns>True если форма валидна</returns>
        private bool ValidateForm()
        {
            if (string.IsNullOrWhiteSpace(txtFullName.Text))
            {
                MessageBox.Show("Введите ФИО пациента", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtFullName.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtDisease.Text))
            {
                MessageBox.Show("Введите диагноз", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtDisease.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtDepartment.Text))
            {
                MessageBox.Show("Введите отделение", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtDepartment.Focus();
                return false;
            }
            if (cmbGender.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите пол пациента", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbGender.Focus();
                return false;
            }
            if (cmbSeverity.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите тяжесть заболевания", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbSeverity.Focus();
                return false;
            }
            return true;
        }
    }
}
