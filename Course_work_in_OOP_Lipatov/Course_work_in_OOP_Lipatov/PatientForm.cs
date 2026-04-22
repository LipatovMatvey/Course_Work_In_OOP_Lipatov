using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Course_work_in_OOP_Lipatov
{
    public partial class PatientForm : Form
    {
        /// <summary>
        /// Регулярное выражение для проверки ФИО пациента.
        /// </summary>
        private static readonly Regex FullNameRegex = new(@"^\p{L}[\p{L}\s\-]*$");

        /// <summary>
        /// Регулярное выражение для проверки названия диагноза.
        /// </summary>
        private static readonly Regex DiseaseRegex = new(@"^\p{L}[\p{L}\d\s\-,.()]*$");

        /// <summary>
        /// Регулярное выражение для проверки названия отделения.
        /// </summary>
        private static readonly Regex DepartmentRegex = new(@"^\p{L}[\p{L}\d\s\-]*$");

        /// <summary>
        /// Менеджер базы данных для проверки дубликатов и сохранения
        /// </summary>
        private DataBaseManager dbManager;

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
        /// Конструктор с параметрами
        /// </summary>
        /// <param name="patient">Пациент для редактирования или создания</param>
        /// <param name="dbManager">Менеджер базы данных для проверки дубликатов и сохранения</param>
        public PatientForm(Patient patient = null, DataBaseManager dbManager = null) : this()
        {
            this.dbManager = dbManager ?? new DataBaseManager();
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
        /// <param name="sender">Объект, вызвавший событие</param>
        /// <param name="e">Аргументы события</param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        /// <summary>
        /// Обработчик нажатия кнопки "Сохранить" — валидирует поля, 
        /// проверяет на дубликаты и закрывает форму с результатом OK при успешном сохранении
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e">Аргументы события</param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateForm())
            {
                var candidate = new Patient
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
                try
                {
                    if (dbManager != null && dbManager.IsDuplicate(candidate))
                    {
                        MessageBox.Show("Пациент с такими данными уже существует.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Не удалось выполнить проверку дубликатов: {ex.Message}", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                Patient = candidate;
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
            if (!FullNameRegex.IsMatch(txtFullName.Text.Trim()))
            {
                MessageBox.Show("Неверный формат ФИО. Используйте только буквы, пробелы и дефис.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtFullName.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtDisease.Text))
            {
                MessageBox.Show("Введите диагноз", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtDisease.Focus();
                return false;
            }
            if (!DiseaseRegex.IsMatch(txtDisease.Text.Trim()))
            {
                MessageBox.Show("Неверный формат диагноза.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtDisease.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtDepartment.Text))
            {
                MessageBox.Show("Введите отделение", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtDepartment.Focus();
                return false;
            }
            if (!DepartmentRegex.IsMatch(txtDepartment.Text.Trim()))
            {
                MessageBox.Show("Неверный формат отделения.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
