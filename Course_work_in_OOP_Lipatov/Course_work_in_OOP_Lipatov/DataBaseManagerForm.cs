using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Course_work_in_OOP_Lipatov
{
    public partial class DataBaseManagerForm : Form
    {
        /// <summary>
        /// Менеджер для работы с базой данных пациентов
        /// </summary>
        private DataBaseManager dbManager;

        /// <summary>
        /// Флаг, была ли изменена текущая база данных
        /// </summary>
        public bool DatabaseChanged { get; private set; }

        /// <summary>
        /// Инициализирует новый экземпляр формы управления базами данных
        /// </summary>
        /// <param name="manager">Объект менеджера баз данных</param>
        public DataBaseManagerForm(DataBaseManager manager)
        {
            InitializeComponent();
            dbManager = manager;
            DatabaseChanged = false;
            LoadDatabases();
        }

        /// <summary>
        /// Загружает список всех доступных баз данных
        /// </summary>
        private void LoadDatabases()
        {
            listBoxDatabases.Items.Clear();
            var dbs = dbManager.GetAllDatabases();
            foreach (var db in dbs)
            {
                listBoxDatabases.Items.Add(db);
            }
            if (listBoxDatabases.Items.Count > 0)
            {
                listBoxDatabases.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// Обработчик нажатия кнопки "Создать"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCreate_Click(object sender, EventArgs e)
        {
            string newDbName = txtNewDbName.Text.Trim();
            if (string.IsNullOrWhiteSpace(newDbName))
            {
                MessageBox.Show("Введите имя новой базы данных.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (newDbName.IndexOfAny(System.IO.Path.GetInvalidFileNameChars()) >= 0)
            {
                MessageBox.Show("Имя БД содержит недопустимые символы.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            dbManager.CreateDatabase(newDbName);
            LoadDatabases();
            txtNewDbName.Clear();
        }

        /// <summary>
        /// Обработчик нажатия кнопки "Переключиться"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSwitch_Click(object sender, EventArgs e)
        {
            if (listBoxDatabases.SelectedItem == null)
            {
                MessageBox.Show("Выберите базу данных для переключения.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string dbName = listBoxDatabases.SelectedItem.ToString();
            if (dbManager.SwitchToDatabase(dbName))
            {
                DatabaseChanged = true;
                MessageBox.Show($"Переключено на базу данных \"{dbName}\".", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }

        /// <summary>
        /// Обработчик нажатия кнопки "Удалить"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (listBoxDatabases.SelectedItem == null)
            {
                MessageBox.Show("Выберите базу данных для удаления.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string dbName = listBoxDatabases.SelectedItem.ToString();
            if (MessageBox.Show($"Удалить базу данных \"{dbName}\"?\nВосстановить будет невозможно.", "Подтверждение",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                dbManager.DeleteDatabase(dbName);
                LoadDatabases();
            }
        }

        /// <summary>
        /// Обработчик нажатия кнопки "Обновить"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadDatabases();
        }
    }
}
