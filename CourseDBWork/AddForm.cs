using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CourseDBWork
{
    public partial class AddForm : Form
    {
        DataTable dtArtist, dtType, dtTech;
        public Dictionary<string, string> ArtNames = new Dictionary<string, string>();
        public Dictionary<string, string> ArtStyle = new Dictionary<string, string>();
        public Dictionary<string, string> ArtTech = new Dictionary<string, string>();

        public int EditableID = 1;

        public AddForm()
        {
            InitializeComponent();
        }
        public AddForm(int EID)
        {
            InitializeComponent();
            EditableID = EID;
            button5.Visible = true;
            button2.Visible = false;
            dtArtist = ConnDB.TryQuery("SELECT * FROM [painters]");
            dtType = ConnDB.TryQuery("SELECT * FROM [Type]");
            dtTech = ConnDB.TryQuery("SELECT * FROM [Tech]");

            DataTable LoadHelper = ConnDB.TryQuery(String.Format("select * from painters where id_painter = '{0}'", EditableID));

            //if (EditableID > 1)
            {
                textBox1.Text = LoadHelper.Rows[0][1].ToString();
                textBox4.Text = LoadHelper.Rows[0][3].ToString();
                dateTimePicker1.Value = DateTime.Parse(LoadHelper.Rows[0][2].ToString());
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dtArtist != null)
            {
                EditableID++;
                if(EditableID > 1 && EditableID < dtArtist.Rows.Count)
                {
                    textBox1.Text = dtArtist.Rows[EditableID][1].ToString();
                    textBox4.Text = dtArtist.Rows[EditableID][3].ToString();
                    dateTimePicker1.Value = DateTime.Parse(dtArtist.Rows[EditableID][2].ToString());
                }
            }
            //textBox2.Text
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dtArtist != null)
            {
                EditableID--;
                if (EditableID > 1 && EditableID < dtArtist.Rows.Count)
                {
                    textBox1.Text = dtArtist.Rows[EditableID][1].ToString();
                    textBox4.Text = dtArtist.Rows[EditableID][3].ToString();
                    dateTimePicker1.Value = DateTime.Parse(dtArtist.Rows[EditableID][2].ToString());
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string tmp = textBox1.Text;
            string formatter = dateTimePicker1.Value.Date.ToString("yyyy-MM-dd");
            ConnDB.TryQuery(String.Format("UPDATE[dbo].[painters] SET [painter_name] = '{0}', [painter_birth] = '{1}', [painter_genre] = '{2}' WHERE[dbo].[painters].[id_painter] = '{3}';",
                                                                     tmp.Replace("'", "`"), formatter, textBox4.Text.Replace("'", "`"), EditableID));
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox4.Clear();
        }

        private void AddForm_Load(object sender, EventArgs e)
        {
            dtArtist = ConnDB.TryQuery("SELECT * FROM [painters]");
            dtType = ConnDB.TryQuery("SELECT * FROM [Type]");
            dtTech = ConnDB.TryQuery("SELECT * FROM [Tech]");

            if (comboBox1.Items.Count <= 0)
                for (int i = 0; i < dtArtist.Rows.Count; i++)
                {
                    ArtNames.Add(dtArtist.Rows[i][0].ToString(), dtArtist.Rows[i][1].ToString());
                    comboBox1.Items.Add(dtArtist.Rows[i][0].ToString() + " " + dtArtist.Rows[i][1].ToString());
                }

            if (comboBox2.Items.Count <= 0)
                for (int i = 0; i < dtType.Rows.Count; i++)
                {
                    ArtStyle.Add(dtType.Rows[i][0].ToString(), dtType.Rows[i][1].ToString());
                    comboBox2.Items.Add(dtType.Rows[i][0].ToString() + " " + dtType.Rows[i][1].ToString());
                }

            if (comboBox3.Items.Count <= 0)
                for (int i = 0; i < dtTech.Rows.Count; i++)
                {
                    ArtTech.Add(dtTech.Rows[i][0].ToString(), dtTech.Rows[i][1].ToString());
                    comboBox3.Items.Add(dtTech.Rows[i][0].ToString() + " " + dtTech.Rows[i][1].ToString());
                }

            comboBox1.SelectedItem = comboBox1.Items[0];
            comboBox2.SelectedItem = comboBox1.Items[0];
            comboBox3.SelectedItem = comboBox1.Items[0];
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string tmp = textBox1.Text;
            string formatter = dateTimePicker1.Value.Date.ToString("yyyy-MM-dd");
            ConnDB.TryQuery(String.Format("INSERT INTO [dbo].[painters] ([painter_name], [painter_birth], [painter_genre]) VALUES ('{0}', '{1}', '{2}');",
                                                                     tmp.Replace("'", "`"), formatter, textBox4.Text.Replace("'", "`")));
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
