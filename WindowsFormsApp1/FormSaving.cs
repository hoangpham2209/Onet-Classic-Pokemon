using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class FormSaving : Form
    {
        private string userName = "";

        public FormSaving()
        {
            InitializeComponent();
        }

        private void save()
        {
            userName = savingName.Text.Trim();

            if (!userName.Equals(""))
            {
                if (!BaiTap.dataUsers.ContainsKey(userName))
                {
                    addDataToFile();
                    this.Close();
                }
                else MessageBox.Show("Name has been taken !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else MessageBox.Show("Name not null !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void addDataToFile()
        {
            BaiTap.dataUsers.Add(userName, BaiTap.score);
            var list = BaiTap.dataUsers.ToList();

            list.Sort((x, y) => x.Value.CompareTo(y.Value));
            list.Reverse();

            StreamWriter output = new StreamWriter(@"data.txt");

            foreach (var i in list)
                output.WriteLine(String.Format("{0}:{1}", i.Key, i.Value));

            output.Close();
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            save();
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Enter)
               save();

            return false;
        }
    }
}
