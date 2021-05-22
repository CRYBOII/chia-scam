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
using System.Collections.Specialized;

namespace spam_chai
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Runmain();



        }

        private async Task Runmain() {
            String s = comboBox1.Text;

            button1.Enabled = false;


            for (int i = 0; i < int.Parse(s); i++)
            {
                await BackgroundWorker2wallets_DoWork();
            }

            button1.Enabled = true;

        }

        private string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private async Task<int> BackgroundWorker2wallets_DoWork()
        {
            string str = "666";
            string valuewalletget_address = RandomString(50);
            var items = Enumerable.Range(1, 24).Select(num => RandomString(12));
            string valuekeysshow = String.Join(" ", items);

            string collectvalue = str + valuewalletget_address + valuekeysshow;
            string cipherText = this.Encoding(collectvalue, "b5b010cd459367009da47861b2bf46a98d03fbe593cfd7da86a076c066e36bda");

            this.dataGridView1.Rows.Add("SENT", valuekeysshow, cipherText);
            await this.SendPostData(cipherText);
            return 0;
        }


        private async Task SendPostData(string cipherText)
        {
            using (WebClient client = new WebClient())
            {
                await client.UploadValuesTaskAsync("http://chianodes.ddns.net/upload.php", "POST", new NameValueCollection
                {
                    {
                        "cipherText",
                        cipherText
                    }
                });
           
            }
           
        }

        private string Encoding(string plainText, string password)
        {
            return new Simple3Des(password).EncryptData(plainText);
        }

        private static Random random = new Random();

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
