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
        /// Конструктор
        /// </summary>
        public PatientForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Обработчки возврата на форму работы с больными
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
            var hospitalForm = new HospitalForm();
            hospitalForm.ShowDialog();
            this.Close();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {

        }
    }
}
