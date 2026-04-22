using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Course_work_in_OOP_Lipatov
{
    public partial class HospitalForm : Form
    {
        /// <summary>
        /// Менеджер для работы с базой данных пациентов
        /// </summary>
        private DataBaseManager dbManager;

        /// <summary>
        /// Список всех пациентов, загруженных из базы данных
        /// </summary>
        private List<Patient> patients;

        /// <summary>
        /// Отфильтрованный список пациентов
        /// </summary>
        private List<Patient> filteredPatients;

        /// <summary>
        /// Конструктор
        /// </summary>
        public HospitalForm()
        {
            InitializeComponent();
            dbManager = new DataBaseManager();
            try
            {
                LoadPatients();
            }
            catch
            {
                if (MessageBox.Show("Не удалось подключиться к базе данных. Открыть управление БД для создания/выбора?",
                    "Ошибка подключения", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
                {
                    OpenDatabaseManager();
                }
                else
                {
                    Application.Exit();
                }
            }
        }

        /// <summary>
        /// Загружает всех пациентов из базы данных и отображает их в DataGridView
        /// </summary>
        private void LoadPatients()
        {
            patients = dbManager.GetAllPatients();
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = patients;
            FormatDataGridView();
        }

        /// <summary>
        /// Обработчик возврата на форму авторизации
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e">Аргументы события</param>
        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            var mainForm = new AuthorFormcs();
            mainForm.ShowDialog();
            this.Close();
        }

        /// <summary>
        /// Обработчик нажатия кнопки "Добавить"
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e">Аргументы события</param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            var form = new PatientForm(dbManager: dbManager);
            if (form.ShowDialog() == DialogResult.OK)
            {
                dbManager.AddPatient(form.Patient);
                LoadPatients();
            }
        }

        /// <summary>
        /// Обработчик нажатия кнопки "Редактировать"
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e">Аргументы события</param>
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var patient = (Patient)dataGridView1.SelectedRows[0].DataBoundItem;
                var form = new PatientForm(patient, dbManager);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    dbManager.UpdatePatient(form.Patient);
                    LoadPatients();
                }
            }
            else
            {
                MessageBox.Show("Выберите пациента для редактирования", "Информация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// Обработчик нажатия кнопки "Удалить"
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e">Аргументы события</param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var patient = (Patient)dataGridView1.SelectedRows[0].DataBoundItem;
                if (MessageBox.Show($"Вы уверены, что хотите удалить пациента:\n{patient.FullName}?",
                    "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    dbManager.DeletePatient(patient.Id);
                    LoadPatients();
                }
            }
            else
            {
                MessageBox.Show("Выберите пациента для удаления", "Информация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// Обработчик нажатия кнопки "Экспорт"
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e">Аргументы события</param>
        private void btnExport_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";
                saveFileDialog.FilterIndex = 1;
                saveFileDialog.RestoreDirectory = true;
                saveFileDialog.FileName = $"patients_export_{DateTime.Now:yyyyMMdd_HHmmss}.txt";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        var patientsToExport = filteredPatients ?? patients;
                        using (StreamWriter writer = new StreamWriter(saveFileDialog.FileName, false, System.Text.Encoding.UTF8))
                        {
                            writer.WriteLine("ЭКСПОРТ ДАННЫХ О ПАЦИЕНТАХ");
                            writer.WriteLine($"Дата экспорта: {DateTime.Now:dd.MM.yyyy HH:mm:ss}");
                            writer.WriteLine($"Всего пациентов: {patientsToExport.Count}");
                            writer.WriteLine(new string('=', 100));
                            foreach (var patient in patientsToExport)
                            {
                                writer.WriteLine($"ID: {patient.Id}");
                                writer.WriteLine($"ФИО: {patient.FullName}");
                                writer.WriteLine($"Возраст: {patient.Age}");
                                writer.WriteLine($"Пол: {patient.Gender}");
                                writer.WriteLine($"Диагноз: {patient.Disease}");
                                writer.WriteLine($"Тяжесть: {patient.Severity}");
                                writer.WriteLine($"Длительность (дней): {patient.Duration}");
                                writer.WriteLine($"Отделение: {patient.Department}");
                                writer.WriteLine(new string('-', 50));
                            }
                        }
                        MessageBox.Show($"Данные успешно экспортированы в файл:\n{saveFileDialog.FileName}",
                            "Экспорт завершен", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при экспорте: {ex.Message}", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        /// <summary>
        /// Обработчик нажатия кнопки "Обновить"
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e">Аргументы события</param>
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            filteredPatients = null;
            LoadPatients();
        }

        /// <summary>
        /// Обработчик нажатия кнопки "Поиск"
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e">Аргументы события</param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                filteredPatients = dbManager.SearchPatients(txtSearch.Text);
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = filteredPatients;
                FormatDataGridView();
            }
            else
            {
                LoadPatients();
            }
        }

        /// <summary>
        /// Обработчик нажатия кнопки "Управление БД"
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e">Аргументы события</param>
        private void btnManageDatabases_Click(object sender, EventArgs e)
        {
            OpenDatabaseManager();
        }

        /// <summary>
        /// Открывает форму управления базами данных
        /// </summary>
        private void OpenDatabaseManager()
        {
            var dbManagerForm = new DataBaseManagerForm(dbManager);
            dbManagerForm.ShowDialog();
            if (dbManagerForm.DatabaseChanged)
            {
                LoadPatients();
            }
        }

        /// <summary>
        /// Обработчик нажатия кнопки очистки базы данных
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e">Аргументы события</param>
        private void btnClearDatabase_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "ВНИМАНИЕ! Это действие удалит ВСЕХ пациентов из базы данных!\n\nВы уверены, что хотите продолжить?",
                "Очистка базы данных",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                result = MessageBox.Show(
                    "Это действие нельзя отменить!\nТочно удалить всех пациентов?",
                    "Подтверждение очистки",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Exclamation);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        dbManager.ClearAllPatients();
                        LoadPatients();
                        MessageBox.Show("База данных успешно очищена!",
                            "Операция завершена",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при очистке базы данных: {ex.Message}",
                            "Ошибка",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }
            }
        }

        /// <summary>
        /// Настраивает внешний вид и заголовки столбцов таблицы DataGridView
        /// </summary>
        private void FormatDataGridView()
        {
            if (dataGridView1.Columns.Count > 0)
            {
                dataGridView1.Columns["Id"].HeaderText = "ID";
                dataGridView1.Columns["Id"].Width = 30;
                dataGridView1.Columns["FullName"].HeaderText = "ФИО";
                dataGridView1.Columns["FullName"].Width = 140;
                dataGridView1.Columns["Age"].HeaderText = "Возраст";
                dataGridView1.Columns["Age"].Width = 70;
                dataGridView1.Columns["Gender"].HeaderText = "Пол";
                dataGridView1.Columns["Gender"].Width = 80;
                dataGridView1.Columns["Disease"].HeaderText = "Диагноз";
                dataGridView1.Columns["Disease"].Width = 150;
                dataGridView1.Columns["Severity"].HeaderText = "Тяжесть";
                dataGridView1.Columns["Severity"].Width = 100;
                dataGridView1.Columns["Duration"].HeaderText = "Дней";
                dataGridView1.Columns["Duration"].Width = 50;
                dataGridView1.Columns["Department"].HeaderText = "Отделение";
                dataGridView1.Columns["Department"].Width = 130;
            }
            dataGridView1.Refresh();
        }

        /// <summary>
        /// Обработчик клика по заголовку столбца DataGridView
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e">Аргументы события, содержащие информацию о столбце</param>
        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var data = filteredPatients ?? patients;
                if (data == null || data.Count == 0) return;
                switch (dataGridView1.Columns[e.ColumnIndex].HeaderText)
                {
                    case "ФИО":
                        data = data.OrderBy(p => p.FullName).ToList();
                        break;
                    case "Возраст":
                        data = data.OrderBy(p => p.Age).ToList();
                        break;
                    case "Тяжесть":
                        var severityOrder = new Dictionary<string, int>
                        {
                            { "Легкая", 1 },
                            { "Средняя", 2 },
                            { "Тяжелая", 3 }
                        };
                        data = data.OrderBy(p => severityOrder.ContainsKey(p.Severity) ? severityOrder[p.Severity] : 4).ToList();
                        break;
                    case "Отделение":
                        data = data.OrderBy(p => p.Department).ToList();
                        break;
                    case "Дней":
                        data = data.OrderBy(p => p.Duration).ToList();
                        break;
                    case "Диагноз":
                        data = data.OrderBy(p => p.Disease).ToList();
                        break;
                }
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = data;
                FormatDataGridView();
            }
        }

        /// <summary>
        /// Обработчик двойного щелчка по ячейке DataGridView
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e">Аргументы события, содержащие индекс строки</param>
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                btnEdit_Click(sender, e);
            }
        }
    }
}
