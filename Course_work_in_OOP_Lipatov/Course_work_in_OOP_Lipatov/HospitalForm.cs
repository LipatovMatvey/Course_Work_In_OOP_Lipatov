using System;
using System.Collections.Generic;
using System.IO;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
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
        private List<Patient>? patients;

        /// <summary>
        /// Отфильтрованный список пациентов
        /// </summary>
        private List<Patient>? filteredPatients;

        /// <summary>
        /// Конструктор
        /// </summary>
        public HospitalForm()
        {
            InitializeComponent();
            dbManager = new DataBaseManager();
            if (!dbManager.HasDatabaseSelected)
            {
                OpenDatabaseManager();
            }
            LoadPatients();
        }

        /// <summary>
        /// Загружает всех пациентов из базы данных и отображает их в DataGridView
        /// </summary>
        private void LoadPatients()
        {
            patients = dbManager.GetAllPatients();
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = patients;
            SetRussianColumnHeaders();
            dataGridView1.Refresh();
        }

        /// <summary>
        /// Обработчик возврата на форму авторизации
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e">Аргументы события</param>
        private void BtnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            var mainForm = new AuthorForm();
            mainForm.ShowDialog();
            this.Close();
        }

        /// <summary>
        /// Обработчик нажатия кнопки "Добавить"
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e">Аргументы события</param>
        private void BtnAdd_Click(object sender, EventArgs e)
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
        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var patient = dataGridView1.SelectedRows[0].DataBoundItem as Patient;
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
        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var patient = dataGridView1.SelectedRows[0].DataBoundItem as Patient;
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
        private void BtnExport_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "PDF файлы (*.pdf)|*.pdf";
                saveFileDialog.FileName = $"patients_export_{DateTime.Now:yyyy_MM_dd_HH_mm_ss}.pdf";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        var patientsToExport = filteredPatients ?? patients ?? new List<Patient>();
                        Document.Create(container =>
                        {
                            container.Page(page =>
                            {
                                page.Size(PageSizes.A4);
                                page.Margin(40);
                                page.Content().Column(col =>
                                {
                                    col.Item().Text("ЭКСПОРТ ДАННЫХ О ПАЦИЕНТАХ").Bold().FontSize(18).AlignCenter();
                                    col.Item().PaddingBottom(10);
                                    col.Item().Text($"Дата экспорта: {DateTime.Now:dd.MM.yyyy HH:mm:ss}");
                                    col.Item().Text($"Всего пациентов: {patientsToExport.Count}");
                                    col.Item().PaddingBottom(10);
                                    col.Item().LineHorizontal(1);
                                    col.Item().PaddingBottom(10);
                                    foreach (var patient in patientsToExport)
                                    {
                                        col.Item().Column(patientColumn =>
                                        {
                                            patientColumn.Item().Text($"ID: {patient.Id}");
                                            patientColumn.Item().Text($"ФИО: {patient.FullName}");
                                            patientColumn.Item().Text($"Возраст: {patient.Age}");
                                            patientColumn.Item().Text($"Пол: {patient.Gender}");
                                            patientColumn.Item().Text($"Диагноз: {patient.Disease}");
                                            patientColumn.Item().Text($"Тяжесть: {patient.Severity}");
                                            patientColumn.Item().Text($"Длительность (дней): {patient.Duration}");
                                            patientColumn.Item().Text($"Отделение: {patient.Department}");
                                            patientColumn.Item().Text(new string('-', 50));
                                        });
                                        col.Item().PaddingBottom(10);
                                    }
                                });
                            });
                        }).GeneratePdf(saveFileDialog.FileName);
                        MessageBox.Show($"Данные успешно экспортированы в PDF:\n{saveFileDialog.FileName}",
                            "Экспорт завершен", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при экспорте: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        /// <summary>
        /// Обработчик нажатия кнопки "Обновить"
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e">Аргументы события</param>
        private void BtnRefresh_Click(object sender, EventArgs e)
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
        private void BtnSearch_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                filteredPatients = dbManager.SearchPatients(txtSearch.Text);
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = filteredPatients;
                SetRussianColumnHeaders();
                dataGridView1.Refresh();
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
        private void BtnManageDatabases_Click(object sender, EventArgs e)
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
        private void BtnClearDatabase_Click(object sender, EventArgs e)
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
        /// Обработчик клика по заголовку столбца DataGridView
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e">Аргументы события, содержащие информацию о столбце</param>
        private void DataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
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
                SetRussianColumnHeaders();
                dataGridView1.Refresh();
            }
        }

        /// <summary>
        /// Обработчик двойного щелчка по ячейке DataGridView
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e">Аргументы события, содержащие индекс строки</param>
        private void DataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                BtnEdit_Click(sender, e);
            }
        }

        /// <summary>
        /// Метод делает заголовки колон на русском языке
        /// </summary>
        private void SetRussianColumnHeaders()
        {
            if (dataGridView1.Columns.Count == 0)
            {
                return;
            }
            if (dataGridView1.Columns["Id"] != null)
            {
                dataGridView1.Columns["Id"].HeaderText = "ID";
            }
            if (dataGridView1.Columns["FullName"] != null)
            {
                dataGridView1.Columns["FullName"].HeaderText = "ФИО";
            }   
            if (dataGridView1.Columns["Age"] != null)
            {
                dataGridView1.Columns["Age"].HeaderText = "Возраст";
            }   
            if (dataGridView1.Columns["Gender"] != null)
            {
                dataGridView1.Columns["Gender"].HeaderText = "Пол";
            }   
            if (dataGridView1.Columns["Disease"] != null)
            {
                dataGridView1.Columns["Disease"].HeaderText = "Диагноз";
            }   
            if (dataGridView1.Columns["Severity"] != null)
            {
                dataGridView1.Columns["Severity"].HeaderText = "Тяжесть";
            }                
            if (dataGridView1.Columns["Duration"] != null)
            {
                dataGridView1.Columns["Duration"].HeaderText = "Дней";
            }
            if (dataGridView1.Columns["Department"] != null)
            {
                dataGridView1.Columns["Department"].HeaderText = "Отделение";
            }
        }
                
    }
}
