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
using System.Runtime.InteropServices;

using Excel = Microsoft.Office.Interop.Excel;

namespace LunchLotteryApp
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        Excel.Application _Excel = null;
        List<RestaurantProp> restaurants = new List<RestaurantProp>();

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
        }


        private void button_SelectFile_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog file = new OpenFileDialog
                {
                    Filter = "Excel files (*.xlsx)|*.xlsx|Excel files (*.xls)|*.xls|All files (*.*)|*.*"
                };
                file.ShowDialog();
                if (file.FileName == string.Empty || file == null)
                {
                    return;
                }
                this.label_FilePath.Text = file.FileName;

                initailExcel();
                operExcel();

                this._Excel.Quit();
                this._Excel = null;
                //確認已經沒有excel工作再回收
                GC.Collect();

                MessageBox.Show("讀取完成 !!\r\n \r\n請選則餐廳群組", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //throw;
            }
        }

        private void comboBox_Group_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                listBoxList.Items.Clear();
                foreach (var item in restaurants)
                {
                    if (item.Category == comboBox_Group.Text)
                    {
                        listBoxList.Items.Add(item.RestaurantName);
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //throw;
            }
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



        void initailExcel()
        {
            //檢查PC有無Excel在執行
            bool flag = false;
            foreach (var item in Process.GetProcesses())
            {
                if (item.ProcessName == "EXCEL")
                {
                    flag = true;
                    break;
                }
            }

            if (!flag)
            {
                this._Excel = new Excel.Application();
            }
            else
            {
                object obj = Marshal.GetActiveObject("Excel.Application");//引用已在執行的Excel
                _Excel = obj as Excel.Application;
            }

            this._Excel.Visible = false;//設false效能會比較好
        }

        void readExcelSheetsName()
        {

        }

        void operExcel()
        {
            Excel.Workbook book = null;
            Excel.Worksheet sheet = null;
            Excel.Range range = null;
            string path = label_FilePath.Text;
            try
            {
                book = _Excel.Workbooks.Open(path);

                for (int i = 1; i <= book.Sheets.Count; i++)
                {
                    sheet = book.Worksheets[i];
                    comboBox_Group.Items.Add(sheet.Name);

                    int totalColumns = sheet.UsedRange.Columns.Count;
                    int totalRows = sheet.UsedRange.Rows.Count;

                    for (int col = 1; col <= totalColumns; col++)
                    {
                        for (int row = 1; row <= totalRows; row++)
                        {
                            range = (Excel.Range)sheet.Cells[row, col];
                            if (range.Value2 != null && range.Value2.ToString().Trim() != string.Empty)
                            {
                                restaurants.Add(new RestaurantProp()
                                {
                                    Category = sheet.Name,
                                    RestaurantName = range.Value2.ToString().Trim()
                                });
                            }
                            
                        }
                    }
                }
            }
            finally
            {
                book.Close();
                book = null;
            }
        }
    }
}
