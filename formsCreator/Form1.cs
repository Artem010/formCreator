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
namespace formsCreator
{
    public partial class Form1 : Form
    {
        public void encryption(string data)
        {
            string key = "artmiptv";
            //char[] dataChars = data.ToCharArray();
            byte[] dataBytes = Encoding.UTF8.GetBytes(data);
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            int token = 0;
            for (int i = 0; i< keyBytes.Length;i++)
            {
                token += dataBytes[i] + keyBytes[i];
            }
            //MessageBox.Show(token.ToString());
        }

        /*public void decryption(int data)
        {
            string key = "artmiptv";
            //byte[] dataBytes = Encoding.UTF8.GetBytes(data);
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            char[] token = new char[0];
            for (int i = 0; i < keyBytes.Length; i++)
            {
                token[i] = Encoding.Default.GetString((data - keyBytes[keyBytes.Length - i]).Where(x => x != 0).ToArray());
            }
            MessageBox.Show(token.ToString());
        }*/
        public void createBlock(int q)
        {
            Form f2 = new Form();
            f2.Name = "form2";
            f2.MinimumSize = new Size(500, q * 100 + 50);
            //f2.FormBorderStyle = FormBorderStyle.FixedSingle;

            Panel[] p3 = new Panel[q];
            Label[] lbl = new Label[q];

            for (int i = 0; i < q; i++)
            {
                p3[i] = new Panel();
                p3[i].Name = "pnls" + (i+1);
                p3[i].Size = new Size(p3[i].Width - 20, 100);
                
                p3[i].Location = new Point(10, i * 100);
                f2.Controls.Add(p3[i]);

                lbl[i] = new Label();
                lbl[i].Name = "lb1l" + (i+1);
                lbl[i].Location = new Point(0, 10);
                lbl[i].Text = "" + File.ReadAllLines(textBox2.Text).Skip(i).First();                
                p3[i].Controls.Add(lbl[i]);

                RadioButton[] rbtn = new RadioButton[4];
                for (int k = 0; k < 4; k++)
                {
                    rbtn[k] = new RadioButton();
                    rbtn[k].Text = "" + (k + 1);
                    rbtn[k].Name = "rbtn" + (k + 1);
                    rbtn[k].Size = new Size(50, 50);
                    rbtn[k].Location = new Point(k * 50, 20);
                    p3[i].Controls.Add(rbtn[k]);
                }
            }

            Button btnSend = new Button();
            btnSend.Name = "buttonSend";
            btnSend.Click += buttonSend_Click;
            btnSend.Text = "Send";
            btnSend.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnSend.Location = new Point(f2.Width - 100, f2.Height - 70);
            f2.Controls.Add(btnSend);

            f2.Show();
        }

        public void settings(int q)
        {
            Panel p = new Panel();
            p.Name = "container";
            p.Size = new Size(this.Width, q * 100 + 50);
            p.Anchor = AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Left;
            p.Location = new Point(0, label1.Height + 50);
            this.Controls.Add(p);

            Panel[] p2 = new Panel[q];
            Label[] lb = new Label[q];
            TextBox[] tb = new TextBox[q];
            for (int i = 0; i < q; i++)
            {
                p2[i] = new Panel();
                p2[i].Name = "pnl" + (i+1);
                p2[i].Size = new Size(this.Width, 100);
                p2[i].Location = new Point(0, i * 100);
                p2[i].Anchor = AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Left;
                p.Controls.Add(p2[i]);

                lb[i] = new Label();
                lb[i].Name = "lbl" + (i + 1);
                lb[i].Location = new Point(10, 10);
                lb[i].Text = "Question" + (i + 1);
                p2[i].Controls.Add(lb[i]);

                tb[i] = new TextBox();
                tb[i].Name = "tb" + (i + 1).ToString();
                tb[i].Multiline = true;
                tb[i].Size = new Size(p2[i].Width - 50, 50);
                tb[i].Anchor = AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Left;
                tb[i].Location = new Point(10, 40);
                p2[i].Controls.Add(tb[i]);
            }

            Button btn = new Button();
            btn.Name = "buttonSave";
            btn.Click += buttonSave_Click;
            btn.Text = "Save";
            btn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btn.Location = new Point(p.Width - 100, p.Height - 30);
            p.Controls.Add(btn);

        }

        public void Reader(string path)
        {
            //StreamReader str = new StreamReader(path, Encoding.Default);
            createBlock(File.ReadAllLines(textBox2.Text).Length);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                settings(Convert.ToInt32(textBox1.Text));
            }
        }
        private void buttonSave_Click(object sender, EventArgs e)
        {
            string[] questions = new string[Convert.ToInt32(textBox1.Text)];

            for (int i = 0; i < Convert.ToInt32(textBox1.Text); i++)
            {
                if (this.Controls["container"].Controls["pnl" + (i + 1)].Controls["tb" + (i + 1)].Text != "")
                {
                    questions[i] = this.Controls["container"].Controls["pnl" + (i + 1)].Controls["tb" + (i + 1)].Text;
                }
            }

            StreamWriter textFile = new StreamWriter(@"D:textfile.blabla");
            textFile.Write(string.Join("\n", questions));
            textFile.Close();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() != DialogResult.OK) return;
            textBox2.Text = openFileDialog1.FileName;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            Reader(textBox2.Text);
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            /*for (int i = 0; i < File.ReadAllLines(textBox2.Text).Length; i++)
            {
                MessageBox.Show(["f2"].Controls["lbl" + (i + 1)].Text);
                for (int k = 0; k < 4; k++)
                {
                    x.Controls["rbtn" + (k + 1)].Container.Components.
                }
            }*/
        }
        public Form1()
        {
            InitializeComponent();
            encryption("Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy ");
        }

    }
}
