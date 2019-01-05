using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.ServiceModel.Description;
using fogmenni;

namespace consume
{
    public partial class Form1 : Form
    {
        IService1 channel;
        ChannelFactory<IService1> cf;
        public Form1()
        {
            InitializeComponent();
            cf = new ChannelFactory<IService1>(new WebHttpBinding(), "http://localhost:8000");
            cf.Endpoint.Behaviors.Add(new WebHttpBehavior());
            channel = cf.CreateChannel();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<string> evek = new List<string>();
            evek = channel.GetAllMagazin().ToList<string>();
            listBox1.Items.Clear();
            foreach (string item in evek)
                listBox1.Items.Add(item);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<string> evek = new List<string>();
            evek = channel.GetIssuesByYear(textBox2.Text).ToList<string>();
            listBox2.Items.Clear();
            foreach (string item in evek)
                listBox2.Items.Add(item);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Article temp = new Article();
            temp.Author = textBox5.Text;
            temp.AricleId = int.Parse(textBox4.Text);
            temp.Content = textBox6.Text;
            temp.Year = textBox1.Text;
            temp.Issue = textBox3.Text;
            channel.AddArticle(temp);



        }


    }
}
