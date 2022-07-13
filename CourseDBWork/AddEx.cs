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
    public partial class AddEx : Form
    {
        public AddEx()
        {
            InitializeComponent();
        }

        DataTable dtPictures;
        public Dictionary<string, string> Pictures = new Dictionary<string, string>();

        public int EditableID = 1;

        public AddEx(int EID)
        {
            InitializeComponent();
            EditableID = EID;
            dtPictures = ConnDB.TryQuery("SELECT * FROM [pictures]");

            DataTable LoadHelper = ConnDB.TryQuery(String.Format("select * from painters where id_painter = '{0}'", EditableID));
                        
                //textBox1.Text = LoadHelper.Rows[0][1].ToString();
                //textBox4.Text = LoadHelper.Rows[0][3].ToString();
                dateTimePicker1.Value = DateTime.Parse(LoadHelper.Rows[0][1].ToString());
                dateTimePicker2.Value = DateTime.Parse(LoadHelper.Rows[0][2].ToString());
        }
        private void AddForm_Load(object sender, EventArgs e)
        {
            dtPictures = ConnDB.TryQuery("SELECT * FROM [pictures]");

            if (comboBox1.Items.Count <= 0)
                for (int i = 0; i < dtPictures.Rows.Count; i++)
                {
                    Pictures.Add(dtPictures.Rows[i][0].ToString(), dtPictures.Rows[i][1].ToString());
                    comboBox1.Items.Add(dtPictures.Rows[i][1].ToString());
                }

            comboBox1.SelectedItem = comboBox1.Items[0];
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //TODO
            // додати виставку
            string tmp = textBox8.Text;
            string formatter = dateTimePicker2.Value.Date.ToString("yyyy-MM-dd");
            string paintId = "";
            string techID = "";
            string typeID = "";
            foreach (var i in Pictures)
                if (i.Value == comboBox1.SelectedItem.ToString()) paintId = i.Key.ToString();
            ConnDB.TryQuery(String.Format("INSERT INTO[dbo].[painting]([pic_name],[pic_date],[id_painter],[id_tech],[id_type]) VALUES('{0}', '{1}', '{2}', '{3}', '{4}');",
            textBox8.Text.Replace("'", "`"), formatter, paintId, techID, typeID));
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox8.Clear();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //TODO
            // додати виставку
            string tmp = textBox8.Text;
            string formatter = dateTimePicker1.Value.Date.ToString("yyyy-MM-dd");
            ConnDB.TryQuery(String.Format("UPDATE[dbo].[painters] SET [painter_name] = '{0}', [painter_birth] = '{1}', [painter_genre] = '{2}' WHERE[dbo].[painters].[id_painter] = '{3}';",
                                                                     tmp.Replace("'", "`"), formatter, textBox8.Text.Replace("'", "`"), EditableID));
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
