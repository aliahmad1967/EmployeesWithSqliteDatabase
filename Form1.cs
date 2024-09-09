using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

using System.Data;
using System.Data.SqlClient;

namespace EmployeesWithSqliteDatabase
{
    public partial class Form1 : Form
    {
        //// For Mode
        //List<Control> panels;
        //List<Control> buttons;
        //List<Control> labels;
        //List <Control> textBoxes;
        //List <Control> comboBoxes;
        



        private SQLiteConnection sqlConn;
        private SQLiteCommand sqlCmd;
        private DataSet sqlDs = new DataSet();
        private DataTable sqlDt = new DataTable();
        private SQLiteDataAdapter sqlDb;

        public Form1()
        {
            InitializeComponent();
            uploadData();

            Random rnd = new Random();
            int iEmpNumber = rnd.Next(2975, 32576);
            txtEmpNo.Text = "EMP" + Convert.ToString(iEmpNumber);

            dataGridView1.Columns["EmpNo"].HeaderText = "رقم الموظف";
            dataGridView1.Columns["FullName"].HeaderText = "الاسم الكامل";
            dataGridView1.Columns["Gender"].HeaderText = "الجنس";
            dataGridView1.Columns["Job"].HeaderText = "الوظيفة";
            dataGridView1.Columns["Department"].HeaderText = "القسم";
            dataGridView1.Columns["Phone"].HeaderText = "الموبايل";
            dataGridView1.Columns["CardNumber"].HeaderText = "رقم البطاقة";
            dataGridView1.Columns["DOB"].HeaderText = "تاريخ الميلاد";
            dataGridView1.Columns["Address"].HeaderText = "العنوان";
        }

        // For Mode
        //void Initialize_Add()
        //{
        //    panels = new List<Control>();
        //    buttons = new List<Control>();
        //    labels = new List<Control>();
        //    textBoxes = new List<Control>();
        //    comboBoxes = new List<Control>();
        //    dataGridView1 = new DataGridView();

        //    panels.Add(panel1);
        //    panels.Add(panel2);
        //    panels.Add(panel3);

        //    buttons.Add(btnAdd);
        //    buttons.Add(btnPrint);
        //    buttons.Add(btnReset);
        //    buttons.Add(btnDelete);
        //    buttons.Add(btnExit);
        //    buttons.Add(btnSearch);

        //    labels.Add(label1);
        //    labels.Add(label2);
        //    labels.Add(label3);
        //    labels.Add(label4);
        //    labels.Add(label5);
        //    labels.Add(label6);
        //    labels.Add(label7);
        //    labels.Add(label8);
        //    labels.Add(label9);
        //    labels.Add(label10);


        //    textBoxes.Add(txtEmpNo);
        //    textBoxes.Add(txtFullName);
        //    textBoxes.Add(txtJob);
        //    textBoxes.Add(txtPhone);
        //    textBoxes.Add(txtCardNo);
        //    textBoxes.Add(txtSeach);
        //    textBoxes.Add(txtAddress);

        //    comboBoxes.Add(cboDep);
        //    comboBoxes.Add(cboGender);
        //    comboBoxes.Add(cboTheme);

        //}

        //For Mode
        //void ApplyTheme(Color back, Color pan, Color btn, Color txt, Color tbo, Color lbl, Color fontColor)
        //{
        //    this.BackColor = back;
        //    this.ForeColor = fontColor;
        //    //this.dataGridView1 = new DataGridView();




        //    foreach (Control item in panels)
        //    {
        //        item.BackColor = pan;
        //    }
        //    foreach (Control item in buttons)
        //    {
        //        item.BackColor = btn;
        //        item.ForeColor = fontColor;
        //    }
        //    foreach (Control item in labels)
        //    {
        //        item.BackColor = back;
        //        item.ForeColor = fontColor;
        //    }
        //    foreach (Control item in textBoxes)
        //    {
        //        item.BackColor = txt;
        //        item.ForeColor = fontColor;
        //    }
        //    foreach (Control item in comboBoxes)
        //    {
        //        item.BackColor = tbo;
        //        item.ForeColor = fontColor;
        //    }

        //}

        private void SetConnectDB()
        {
            sqlConn = new SQLiteConnection("Data Source = |DataDirectory|\\Details.db"); // data base in same solution diretory
        }

        private void ExcuteQuery(string QueryData)
        {
            SetConnectDB();
            sqlConn.Open();
            sqlCmd= sqlConn.CreateCommand();
            sqlCmd.CommandText = QueryData;
            sqlCmd.ExecuteNonQuery();
            sqlCmd.Dispose();
            sqlConn.Close();
        }

        private void uploadData()
        {
            SetConnectDB();
            sqlConn.Open();
            sqlCmd = sqlConn.CreateCommand();
            string CommandText = "Select * from Details";
            sqlDb = new SQLiteDataAdapter(CommandText, sqlConn);
            sqlDs.Reset();
            sqlDb.Fill(sqlDs);
            sqlDt = sqlDs.Tables[0];
            dataGridView1.DataSource= sqlDt;
            sqlConn.Close();
        }
            
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("هل تريد الخروج ؟", "تأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Application.Exit();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "تأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Information);


            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                dtpDBO.ResetText();
                foreach (Control txt in panel1.Controls)
                {
                    if (txt is TextBox)
                        ((TextBox)txt).Clear();
                }
                foreach (Control cbo in panel1.Controls)
                {
                    if (cbo is ComboBox)
                        ((ComboBox)cbo).Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "تأكيد", MessageBoxButtons.OK, MessageBoxIcon.Information);


            }

            Random rnd = new Random();
            int iEmpNumber = rnd.Next(2975, 32576);
            txtEmpNo.Text = "EMP" + Convert.ToString(iEmpNumber);

        }

        Bitmap bitmap;
        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                int hight = dataGridView1.Height;
                dataGridView1.Height = dataGridView1.RowCount * dataGridView1.RowTemplate.Height * 2;
                bitmap = new Bitmap(dataGridView1.Width, dataGridView1.Height);
                dataGridView1.DrawToBitmap(bitmap, new Rectangle(0,0,dataGridView1.Width,dataGridView1.Height));
                printPreviewDialog1.PrintPreviewControl.Zoom = 1;
                printPreviewDialog1.ShowDialog();
                dataGridView1.Height=hight;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "تأكيد", MessageBoxButtons.OK, MessageBoxIcon.Information);


            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(bitmap,0,0);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string QueryData = "insert into Details(EmpNo,FullName,Gender,Job,Department,Phone,CardNumber,DOB,Address)"
                + "values('"
                + txtEmpNo.Text
                + "','"
                + txtFullName.Text
                + "','"
                + cboGender.Text
                + "','"
                + txtJob.Text
                + "','"
                + cboDep.Text
                + "','"
                + txtPhone.Text
                + "','"
                + txtCardNo.Text
                + "','"
                + dtpDBO.Text
                + "','"
                + txtAddress.Text
                + "')";
            ExcuteQuery(QueryData);
            uploadData();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string QueryData = "delete from Details where EmpNo = '" + txtEmpNo.Text + "'";

            ExcuteQuery(QueryData);
            uploadData();


        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //For Theme
            //Initialize_Add();
            //cboTheme.SelectedIndex = 0;
            //----------------

            try
            {
                if (sqlConn.State == ConnectionState.Closed)
                    sqlConn.Open();
                using (SQLiteDataAdapter sqlDb = new SQLiteDataAdapter("select * from Details", sqlConn))
                {
                    DataTable sqlDt = new DataTable("FullName");
                    sqlDb.Fill(sqlDt);
                    dataGridView1.DataSource = sqlDt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "تأكيد", MessageBoxButtons.OK, MessageBoxIcon.Information);


            }


        }

        private void txtSeach_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)13)
            {
                DataView dv = sqlDt.DefaultView;
                dv.RowFilter= string.Format("FullName like'%{0}%'",txtSeach.Text);
                dataGridView1.DataSource= dv.ToTable();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                txtEmpNo.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                txtFullName.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                cboGender.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
                txtJob.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
                cboDep.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
                txtPhone.Text = dataGridView1.SelectedRows[0].Cells[6].Value.ToString();
                txtCardNo.Text = dataGridView1.SelectedRows[0].Cells[7].Value.ToString();
                dtpDBO.Text = dataGridView1.SelectedRows[0].Cells[8].Value.ToString();
                txtAddress.Text = dataGridView1.SelectedRows[0].Cells[9].Value.ToString();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "تأكيد", MessageBoxButtons.OK, MessageBoxIcon.Information);


            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            DataView dv = sqlDt.DefaultView;
            dv.RowFilter = string.Format("FullName like'%{0}%'", txtSeach.Text);
            dataGridView1.DataSource = dv.ToTable();
        }

        //For Mode
        //private void cboTheme_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (cboTheme.Text == "Light")
        //    {
        //        ApplyTheme(Color.White, Color.White, Color.AliceBlue, Color.White, Color.White, Color.White, Color.Black);
        //    }
        //    else if (cboTheme.Text == "Dark")
        //    {
        //        ApplyTheme(Color.FromArgb(31, 31, 31), Color.FromArgb(31, 31, 31), Color.FromArgb(86, 42, 45), Color.FromArgb(53, 54, 58), Color.FromArgb(53, 54, 58), Color.FromArgb(53, 54, 58), Color.FromArgb(176, 176, 176));
        //    }
        //}

        private void txtAddress_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
