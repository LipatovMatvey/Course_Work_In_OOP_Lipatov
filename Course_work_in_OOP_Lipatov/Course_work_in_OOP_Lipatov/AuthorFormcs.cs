using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Course_work_in_OOP_Lipatov
{
    public partial class AuthorFormcs : Form
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public AuthorFormcs()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Обработчик нажатия кнопки "Вход в систему"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRun_Click(object sender, EventArgs e)
        {
            this.Hide();
            var mainForm = new HospitalForm();
            mainForm.ShowDialog();
            this.Close();
        }

        /// <summary>
        /// Обработчик нажатия кнопки "Выход из системы"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
