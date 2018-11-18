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
using System.Threading;

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
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //this.Close();
                //throw;
            }



            //listBoxList.Items.Add("CUCO");
            //listBoxList.Items.Add("大蘋果");
            //listBoxList.Items.Add("福來");
            //listBoxList.Items.Add("Mama");
            //listBoxList.Items.Add("吳家豆漿");
            //listBoxList.Items.Add("德東49號食堂(前好粥到)");
            //listBoxList.Items.Add("巷仔口(蛋包飯)");
            //listBoxList.Items.Add("胡家小吃");
            //listBoxList.Items.Add("忠誠牛肉麵");
            //listBoxList.Items.Add("黃家牛肉麵");
            //listBoxList.Items.Add("老北投");
            //listBoxList.Items.Add("Subber");

            listBoxList.Items.Add("東山鴨頭");
            listBoxList.Items.Add("滷夫滷味");
            listBoxList.Items.Add("貴婦滷味");
            listBoxList.Items.Add("上營大屋滷味");
            listBoxList.Items.Add("何家蒸餃");
            listBoxList.Items.Add("石牌蒸餃");
            listBoxList.Items.Add("燈亮有餅");
            listBoxList.Items.Add("阿財鍋貼");
            listBoxList.Items.Add("蚵仔煎&下水");
            listBoxList.Items.Add("吳家豆漿");
            listBoxList.Items.Add("黑殿食堂");
            listBoxList.Items.Add("彈牙麵");
            listBoxList.Items.Add("黃家牛肉麵");
            listBoxList.Items.Add("忠誠牛肉麵");
            listBoxList.Items.Add("蕭家牛肉麵");
            listBoxList.Items.Add("金春發牛肉店");
            listBoxList.Items.Add("新婦海南雞飯");
            listBoxList.Items.Add("滷三塊");
            listBoxList.Items.Add("劉師傅豬腳飯");
            listBoxList.Items.Add("石牌廣東粥");
            listBoxList.Items.Add("鵝肉小吃店");
            listBoxList.Items.Add("北投24小吃店");
            listBoxList.Items.Add("宋家餡餅粥");
            listBoxList.Items.Add("胃太小");
            listBoxList.Items.Add("喜樂廚房");
            listBoxList.Items.Add("蚵仔之家");
            listBoxList.Items.Add("泰之雲");
            listBoxList.Items.Add("東方泰國小館");
            listBoxList.Items.Add("一極鮮");
            listBoxList.Items.Add("越香蘭");
            listBoxList.Items.Add("米夏");
            listBoxList.Items.Add("JB's diner");
            listBoxList.Items.Add("Second Floor");
            listBoxList.Items.Add("老倉庫");
            listBoxList.Items.Add("La Pasta");
            listBoxList.Items.Add("Pino餐廳");
            listBoxList.Items.Add("Chili's Grill & Bar");
            listBoxList.Items.Add("彌生軒YAYOI");
            listBoxList.Items.Add("濟州館");
            listBoxList.Items.Add("高麗味");
            listBoxList.Items.Add("洋蔥");
            listBoxList.Items.Add("弍兩燒肉");
            listBoxList.Items.Add("欣葉日本料理");
            listBoxList.Items.Add("孫東寶");
            listBoxList.Items.Add("Nagi拉麵店");
            listBoxList.Items.Add("爭鮮迴轉壽司");
            listBoxList.Items.Add("Subber");
            listBoxList.Items.Add("福來早餐");
            listBoxList.Items.Add("仟人活力早餐店");
            listBoxList.Items.Add("石牌無名蛋餅");
            listBoxList.Items.Add("MaMa早餐店");
            listBoxList.Items.Add("Cuco漢堡");
            listBoxList.Items.Add("Q burger");
            listBoxList.Items.Add("麥當勞");
            listBoxList.Items.Add("德州美墨炸雞");
            listBoxList.Items.Add("茉莉漢堡");
            listBoxList.Items.Add("摩斯漢堡");
            listBoxList.Items.Add("皇家Pizza");
            listBoxList.Items.Add("必勝客");
            listBoxList.Items.Add("達美樂");
            listBoxList.Items.Add("羊肉爐");
            listBoxList.Items.Add("薑母鴨");
            listBoxList.Items.Add("萬華莊家班麻油雞");
            listBoxList.Items.Add("石牌麻油雞麵線");
            listBoxList.Items.Add("台G店");
            listBoxList.Items.Add("小蒙牛");
            listBoxList.Items.Add("天鍋宴");
            listBoxList.Items.Add("千葉火鍋");
            listBoxList.Items.Add("石二鍋");
            listBoxList.Items.Add("好食多");
            listBoxList.Items.Add("士林夜市隨意吃");
            listBoxList.Items.Add("Costco覓食");
            listBoxList.Items.Add("餐卷爽爽吃");
            listBoxList.Items.Add("自助餐");
            listBoxList.Items.Add("桃園覓食");
            listBoxList.Items.Add("新竹覓食");
            listBoxList.Items.Add("兔兔師下麵");
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
            for (int i = 0; i < 200; i++)
            {
                int varValue = random.Next();
                do
                {
                    varValue = varValue % listBoxList.Items.Count;
                }
                while (varValue > listBoxList.Items.Count);
                intList.Add(varValue);

                toolStripStatusLabel1.Text = listBoxList.Items[varValue].ToString();
                this.Refresh();
                Thread.Sleep(20);
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

            toolStripStatusLabel1.Text = listBoxList.Items[index].ToString();
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
