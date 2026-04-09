using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Course_work_in_OOP_Lipatov
{
    public partial class HospitalForm : Form
    {
        /// <summary>
        /// 
        /// </summary>
        private DataBaseManager dbManager;

        /// <summary>
        /// 
        /// </summary>
        private List<Patient> patients;

        /// <summary>
        /// 
        /// </summary>
        private List<Patient> filteredPatients;

        /// <summary>
        /// Конструктор
        /// </summary>
        public HospitalForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Обработчик возврата на форму авторизации
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            var mainForm = new AuthorFormcs();
            mainForm.ShowDialog();
            this.Close();
        }

        /// <summary>
        /// Обработчик добавления нового пациента
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            this.Hide();
            var patientForm = new PatientForm();
            patientForm.ShowDialog();
            this.Close();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExport_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRefresh_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnManageDatabases_Click(object sender, EventArgs e)
        {
            
        }
    }
}
