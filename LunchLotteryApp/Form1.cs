using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LunchLotteryApp
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
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
