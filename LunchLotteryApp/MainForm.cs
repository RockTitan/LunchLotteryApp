using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Net;
using System.Net.Sockets;
using System.Diagnostics;

namespace LunchLotteryApp
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                //顯示版本於Form title
                var Program_Title = this.Text;
                var Program_Version = FileVersionInfo.GetVersionInfo(this.GetType().Assembly.Location).ProductVersion;
                List<string> IPList = new List<string>();
                IPHostEntry iphostentry = Dns.GetHostEntry(Dns.GetHostName());
                foreach (IPAddress ipAddress in iphostentry.AddressList)
                {
                    if (ipAddress.AddressFamily == AddressFamily.InterNetwork)
                    {
                        IPList.Add(ipAddress.ToString());
                    }
                }
                this.Text = string.Format(@"{0}  V{1}  ({2}\{3} @{4} : {5})", Program_Title, Program_Version, Environment.UserDomainName, Environment.UserName, Environment.MachineName, IPList[0]);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                //throw;
            }



            listBoxList.Items.Add("CUCO");
            listBoxList.Items.Add("大蘋果");
            listBoxList.Items.Add("福來");
            listBoxList.Items.Add("Mama");
            listBoxList.Items.Add("吳家豆漿");
            listBoxList.Items.Add("德東49號食堂(前好粥到)");
            listBoxList.Items.Add("巷仔口(蛋包飯)");
            listBoxList.Items.Add("胡家小吃");
            listBoxList.Items.Add("忠誠牛肉麵");
            listBoxList.Items.Add("黃家牛肉麵");
            listBoxList.Items.Add("老北投");
            listBoxList.Items.Add("Subber");
        }

        private void textBoxInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                AddItem();
            }
        }

        private void buttonInput_Click(object sender, EventArgs e)
        {
            AddItem();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            DeleteItem();
        }

        private void buttonRun_Click(object sender, EventArgs e)
        {
            Random random = new Random();

            List<int> intList = new List<int>();
            for (int i = 0; i < 10000000; i++)
            {
                int varValue = random.Next();
                do
                {
                    varValue = varValue % listBoxList.Items.Count;
                }
                while (varValue > listBoxList.Items.Count);
                intList.Add(varValue);
            }

            var res = from n in intList
                      group n by n into g
                      orderby g.Count() descending
                      select g;
            var gr = res.First();

            int index = 0;
            foreach (int x in gr)
            {
                index = x;
            }

            MessageBox.Show(listBoxList.Items[index].ToString());
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AddItem()
        {
            bool comp = false;

            if (textBoxInput.Text.Trim() != string.Empty)
            {
                foreach (var item in listBoxList.Items)
                {
                    if (textBoxInput.Text.Trim() == item.ToString())
                    {
                        comp = true;
                    }
                }
                if (comp == false)
                {
                    listBoxList.Items.Add(textBoxInput.Text.Trim());
                }
            }
        }

        private void DeleteItem()
        {
            List<string> tempList = new List<string>();

            foreach (string item in listBoxList.Items)
            {
                tempList.Add(item.ToString());
            }

            foreach (var item in listBoxList.SelectedItems)
            {
                tempList.Remove(item.ToString());
            }

            listBoxList.Items.Clear();
            foreach (var item in tempList)
            {
                listBoxList.Items.Add(item);
            }
        }
    }
}
