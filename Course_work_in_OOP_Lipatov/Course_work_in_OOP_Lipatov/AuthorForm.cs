using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Course_work_in_OOP_Lipatov
{
    public partial class AuthorForm : Form
    {
        /// <summary>
        /// Таймер для автоматического перехода
        /// </summary>
        private System.Windows.Forms.Timer autoTransitionTimer;

        /// <summary>
        /// Конструктор
        /// </summary>
        public AuthorForm()
        {
            InitializeComponent();
            autoTransitionTimer = new System.Windows.Forms.Timer();
            autoTransitionTimer.Interval = 10000;
            autoTransitionTimer.Tick += AutoTransitionTimer_Tick;
            autoTransitionTimer.Start();
        }

        /// <summary>
        /// Обработчик события срабатывания таймера
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e">Параметры события</param>
        private void AutoTransitionTimer_Tick(object sender, EventArgs e)
        {
            autoTransitionTimer.Stop();
            PerformTransition();
        }

        /// <summary>
        /// Выполняет переход на форму HospitalForm
        /// </summary>
        private void PerformTransition()
        {
            this.Hide();
            var mainForm = new HospitalForm();
            mainForm.ShowDialog();
            this.Close();
        }

        /// <summary>
        /// Обработчик нажатия кнопки "Вход в систему"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRun_Click(object sender, EventArgs e)
        {
            autoTransitionTimer.Stop();
            PerformTransition();
        }

        /// <summary>
        /// Обработчик нажатия кнопки "Выход из системы"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            autoTransitionTimer.Stop();
            Close();
        }
    }
}
