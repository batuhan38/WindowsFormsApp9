using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Xml;

namespace WindowsFormsApp9
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        static void main(String[] Args)
        {
           
        }

        private void btnVeriYazdir_Click(object sender, EventArgs e)
        {
            veri_yaz();
            veri_oku();
           
        }

        private void veri_yaz()
        {
            string bugün = "http://www.tcmb.gov.tr/kurlar/today.xml";
            var xmldoc = new XmlDocument();
            xmldoc.Load(bugün);
            DateTime tarih = Convert.ToDateTime(xmldoc.SelectSingleNode("//Tarih_Date").Attributes["Tarih"].Value);

            string USD = xmldoc.SelectSingleNode("Tarih_Date/Currency [@Kod='USD']/BanknoteSelling").InnerXml;
            label1.Text = string.Format("Tarih{0} USD;{1}", tarih.ToShortDateString(), USD);

            string EURO = xmldoc.SelectSingleNode("Tarih_Date/Currency [@Kod='EUR']/BanknoteSelling").InnerXml;
            label2.Text = string.Format("Tarih{0} EUR;{1}", tarih.ToShortDateString(), EURO);


            string POUND = xmldoc.SelectSingleNode("Tarih_Date/Currency [@Kod='GBP']/BanknoteSelling").InnerXml;
            label3.Text = string.Format("Tarih{0} GBP;{1}", tarih.ToShortDateString(), POUND);


            StreamWriter sw1 = File.AppendText("C:\\Users\\Nurcan\\Desktop\\dövizKurlari.txt");

            sw1.WriteLine(label1.Text);
            sw1.WriteLine(label2.Text);
            sw1.WriteLine(label3.Text);

            sw1.Close();
        }

        private void veri_oku()
        {
            StreamReader sr = File.OpenText("C:\\Users\\Nurcan\\Desktop\\dövizKurlari.txt");
            string metin;
          
            while ((metin=sr.ReadLine())!=null)
            {
                
                listBox1.Items.Add(metin);
            }
            sr.Close();
            string[] metinList = new string[listBox1.Items.Count];
            listBox1.Items.CopyTo(metinList, 0);
            var metinList2 = metinList.Distinct();
            listBox1.Items.Clear();
            foreach (string s in metinList2)
            {
                listBox1.Items.Add(s);
            }
            System.IO.File.WriteAllLines("C:\\Users\\Nurcan\\Desktop\\dövizKurlari.txt", metinList2);
        }

    }
}
