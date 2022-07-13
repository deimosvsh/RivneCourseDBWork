using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CourseDBWork
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            ConnDB.TryDeffConnection();
            dataGridView1.DataSource = ConnDB.TryQuery("select * from dbo.exhibitions");
            dataGridView2.DataSource = ConnDB.TryQuery("select * from dbo.painters");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddForm addForm = new AddForm();
            addForm.ShowDialog();
            //if (addForm.DialogResult == DialogResult.OK)
            //    dataGridView2.DataSource = ConnDB.TryQuery("select * from dbo.painters");
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            AddForm addForm = new AddForm();
            addForm.ShowDialog();
            if (addForm.DialogResult == DialogResult.OK)
                dataGridView2.DataSource = ConnDB.TryQuery("select * from dbo.painters");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            ConnDB.TryQuery(String.Format("DELETE FROM [dbo].[painters] WHERE dbo.painters.id_painter = '{0}';", dataGridView2.CurrentCell.RowIndex+1));
            dataGridView2.DataSource = ConnDB.TryQuery("select * from dbo.painters");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            DataTable dtIDHelper = ConnDB.TryQuery(String.Format("select id_painter from painters where painter_name = '{0}'", dataGridView2.Rows[dataGridView2.CurrentCell.RowIndex].Cells[1].Value));
            AddForm addForm = new AddForm(Int32.Parse(dtIDHelper.Rows[0][0].ToString()));
            addForm.ShowDialog();
            if (addForm.DialogResult == DialogResult.OK)
                dataGridView2.DataSource = ConnDB.TryQuery("select * from dbo.painters");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DataTable dtIDHelper = ConnDB.TryQuery(String.Format("select id_painter from painters where painter_name = '{0}'", dataGridView2.Rows[dataGridView2.CurrentCell.RowIndex].Cells[1].Value));
            AddEx addEx = new AddEx(Int32.Parse(dtIDHelper.Rows[0][0].ToString()));
            addEx.ShowDialog();
            if (addEx.DialogResult == DialogResult.OK)
                dataGridView2.DataSource = ConnDB.TryQuery("select * from dbo.painters");
        }
    }

    public static class ConnDB
    {
        public static string sLastQuery;
        public static DataSet dsLastDataSet;
        public static SqlDataAdapter adapter;
        public static SqlConnection connection;
        public static string Connect = @"Data Source=localhost;Initial Catalog=GALA2;Integrated Security=True";

        public static int EditableId = 0;
        public static string sEditableId = "";

        public static bool Shazam = false;

        public static int iCurrentVet;

        public static void TryDeffConnection()
        {
            try
            {
                using (connection = new SqlConnection(Connect))
                {
                    using (adapter = new SqlDataAdapter("select * from tech;", connection))
                    {
                        DataSet ds = new DataSet();
                        adapter.Fill(ds);
                        Console.WriteLine("default connection established");
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Connection error:\n" + ex.Message);
            }
        }

        public static SqlConnection TryConnection(string sConn)
        {
            try
            {
                using (connection = new SqlConnection(sConn))
                {
                    using (adapter = new SqlDataAdapter("select * from Painters;", connection))
                    {
                        DataSet ds = new DataSet();
                        adapter.Fill(ds);
                        Console.WriteLine("new connection established and stored");
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Connection error:\n" + ex.Message);
            }

            return connection;
        }

        public static DataTable TryQuery(string query)
        {
            DataTable tmp = new DataTable();
            if (connection == null || adapter == null)
                TryDeffConnection();
            else
            {
                try
                {
                    using (connection = new SqlConnection(Connect))
                    {
                        using (adapter = new SqlDataAdapter(query, connection))
                        {
                            dsLastDataSet = new DataSet();
                            adapter.Fill(dsLastDataSet);

                            sLastQuery = query;

                            Console.WriteLine(sLastQuery);
                        }
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Connection error:\n" + ex.Message);
                }

                if (dsLastDataSet.Tables.Count == 0)
                    return tmp;
                else
                    return dsLastDataSet.Tables[0];
            }

            return null;
        }
    }
}
