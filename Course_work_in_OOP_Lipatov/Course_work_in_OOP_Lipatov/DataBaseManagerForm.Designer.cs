namespace Course_work_in_OOP_Lipatov
{
    partial class DataBaseManagerForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataBaseManagerForm));
            listBoxDatabases = new ListBox();
            txtNewDbName = new TextBox();
            btnCreate = new Button();
            btnDelete = new Button();
            btnSwitch = new Button();
            btnRefresh = new Button();
            label1 = new Label();
            label2 = new Label();
            panelHeader = new Panel();
            labelTitle = new Label();
            panelHeader.SuspendLayout();
            SuspendLayout();
            // 
            // listBoxDatabases
            // 
            listBoxDatabases.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            listBoxDatabases.BackColor = Color.WhiteSmoke;
            listBoxDatabases.BorderStyle = BorderStyle.FixedSingle;
            listBoxDatabases.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, 204);
            listBoxDatabases.FormattingEnabled = true;
            listBoxDatabases.Location = new Point(30, 112);
            listBoxDatabases.Margin = new Padding(3, 4, 3, 4);
            listBoxDatabases.Name = "listBoxDatabases";
            listBoxDatabases.Size = new Size(399, 182);
            listBoxDatabases.TabIndex = 1;
            // 
            // txtNewDbName
            // 
            txtNewDbName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtNewDbName.BackColor = Color.WhiteSmoke;
            txtNewDbName.BorderStyle = BorderStyle.FixedSingle;
            txtNewDbName.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, 204);
            txtNewDbName.Location = new Point(30, 350);
            txtNewDbName.Margin = new Padding(3, 4, 3, 4);
            txtNewDbName.Name = "txtNewDbName";
            txtNewDbName.Size = new Size(399, 24);
            txtNewDbName.TabIndex = 3;
            // 
            // btnCreate
            // 
            btnCreate.BackColor = Color.SeaGreen;
            btnCreate.FlatAppearance.BorderSize = 0;
            btnCreate.FlatStyle = FlatStyle.Flat;
            btnCreate.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
            btnCreate.ForeColor = Color.White;
            btnCreate.Location = new Point(30, 400);
            btnCreate.Margin = new Padding(3, 4, 3, 4);
            btnCreate.Name = "btnCreate";
            btnCreate.Size = new Size(148, 44);
            btnCreate.TabIndex = 5;
            btnCreate.Text = "Создать";
            btnCreate.UseVisualStyleBackColor = false;
            btnCreate.Click += BtnCreate_Click;
            // 
            // btnDelete
            // 
            btnDelete.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnDelete.BackColor = Color.IndianRed;
            btnDelete.FlatAppearance.BorderSize = 0;
            btnDelete.FlatStyle = FlatStyle.Flat;
            btnDelete.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
            btnDelete.ForeColor = Color.White;
            btnDelete.Location = new Point(281, 400);
            btnDelete.Margin = new Padding(3, 4, 3, 4);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(148, 44);
            btnDelete.TabIndex = 6;
            btnDelete.Text = "Удалить";
            btnDelete.UseVisualStyleBackColor = false;
            btnDelete.Click += BtnDelete_Click;
            // 
            // btnSwitch
            // 
            btnSwitch.BackColor = Color.SteelBlue;
            btnSwitch.FlatAppearance.BorderSize = 0;
            btnSwitch.FlatStyle = FlatStyle.Flat;
            btnSwitch.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
            btnSwitch.ForeColor = Color.White;
            btnSwitch.Location = new Point(30, 462);
            btnSwitch.Margin = new Padding(3, 4, 3, 4);
            btnSwitch.Name = "btnSwitch";
            btnSwitch.Size = new Size(148, 44);
            btnSwitch.TabIndex = 7;
            btnSwitch.Text = "Переключиться";
            btnSwitch.UseVisualStyleBackColor = false;
            btnSwitch.Click += BtnSwitch_Click;
            // 
            // btnRefresh
            // 
            btnRefresh.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnRefresh.BackColor = Color.DarkCyan;
            btnRefresh.FlatAppearance.BorderSize = 0;
            btnRefresh.FlatStyle = FlatStyle.Flat;
            btnRefresh.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
            btnRefresh.ForeColor = Color.White;
            btnRefresh.Location = new Point(281, 462);
            btnRefresh.Margin = new Padding(3, 4, 3, 4);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(148, 44);
            btnRefresh.TabIndex = 8;
            btnRefresh.Text = "Обновить";
            btnRefresh.UseVisualStyleBackColor = false;
            btnRefresh.Click += BtnRefresh_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label1.Location = new Point(27, 88);
            label1.Name = "label1";
            label1.Size = new Size(185, 18);
            label1.TabIndex = 2;
            label1.Text = "Доступные базы данных:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, 204);
            label2.Location = new Point(27, 325);
            label2.Name = "label2";
            label2.Size = new Size(183, 18);
            label2.TabIndex = 4;
            label2.Text = "Имя новой базы данных:";
            // 
            // panelHeader
            // 
            panelHeader.BackColor = Color.SteelBlue;
            panelHeader.Controls.Add(labelTitle);
            panelHeader.Dock = DockStyle.Top;
            panelHeader.Location = new Point(0, 0);
            panelHeader.Margin = new Padding(3, 4, 3, 4);
            panelHeader.Name = "panelHeader";
            panelHeader.Size = new Size(441, 75);
            panelHeader.TabIndex = 0;
            // 
            // labelTitle
            // 
            labelTitle.Anchor = AnchorStyles.Top;
            labelTitle.AutoSize = true;
            labelTitle.BackColor = Color.SteelBlue;
            labelTitle.Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Bold, GraphicsUnit.Point, 204);
            labelTitle.ForeColor = Color.White;
            labelTitle.Location = new Point(-5, 11);
            labelTitle.Name = "labelTitle";
            labelTitle.Padding = new Padding(10, 12, 10, 12);
            labelTitle.Size = new Size(431, 53);
            labelTitle.TabIndex = 0;
            labelTitle.Text = "УПРАВЛЕНИЕ БАЗАМИ ДАННЫХ";
            // 
            // DataBaseManagerForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.WhiteSmoke;
            ClientSize = new Size(441, 538);
            Controls.Add(btnRefresh);
            Controls.Add(btnSwitch);
            Controls.Add(btnDelete);
            Controls.Add(btnCreate);
            Controls.Add(label2);
            Controls.Add(txtNewDbName);
            Controls.Add(label1);
            Controls.Add(listBoxDatabases);
            Controls.Add(panelHeader);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(3, 4, 3, 4);
            MinimizeBox = false;
            Name = "DataBaseManagerForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Управление базами данных";
            panelHeader.ResumeLayout(false);
            panelHeader.PerformLayout();
            ResumeLayout(false);
            PerformLayout();

        }

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.ListBox listBoxDatabases;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNewDbName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnSwitch;
        private System.Windows.Forms.Button btnRefresh;
    }
}