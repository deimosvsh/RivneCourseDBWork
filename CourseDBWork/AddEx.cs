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

            //DataTable LoadHelper = ConnDB.TryQuery(String.Format("select * from painters where id_painter = '{0}'", EditableID));
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

        private void label9_Click(object sender, EventArgs e)
        {

        }
    }
}
