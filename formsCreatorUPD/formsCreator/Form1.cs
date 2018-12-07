using System;
using System.Security.Cryptography;
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
        Crypt crypt = new Crypt();

        Form2 f2 = new Form2();
        DateTime date = DateTime.Now;

        public void CreateForm(int q)
        {
            f2.Name = "form2";
            f2.MinimumSize = new Size(800, 700);
            f2.AutoScroll = true;

            Timer timer = new Timer();
            timer.Interval = 10;
            timer.Tick += new EventHandler(timer_tick);
            timer.Start();
            Label timerLbl = new Label();
            timerLbl.Name = "timerLbl";
            timerLbl.AutoSize = true;
            timerLbl.Text = "00:00:00";
            timerLbl.Location = new Point(f2.Width - timerLbl.Width + 20, 10);
            timerLbl.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            f2.Controls.Add(timerLbl);

            Panel[] p3 = new Panel[q];
            Label[] lbl = new Label[q];

            for (int i = 0; i < q; i++)
            {
                p3[i] = new Panel();
                p3[i].Name = "pnls" + (i + 1);
                p3[i].AutoSize = true;
                p3[i].Location = new Point(10, i * 115);
                f2.Controls.Add(p3[i]);

                lbl[i] = new Label();
                lbl[i].AutoSize = true;
                lbl[i].Name = "questionLbl" + (i + 1);
                lbl[i].Location = new Point(0, 15);
                lbl[i].Text = "Question " + (i+1);
                p3[i].Controls.Add(lbl[i]);

                lbl[i] = new Label();
                lbl[i].AutoSize = true;
                lbl[i].Name = "lb1l" + (i + 1);
                lbl[i].Location = new Point(0, 30);
                lbl[i].Text = "" + crypt.Decrypt(File.ReadAllLines(pathTxtb.Text).Skip(i).First(), "artmiptv");
                p3[i].Controls.Add(lbl[i]);

                RadioButton[] rbtn = new RadioButton[5];
                for (int k = 0; k < 5; k++)
                {
                    rbtn[k] = new RadioButton();
                    rbtn[k].Text = "" + (k + 1);
                    rbtn[k].Name = "rbtn" + (k + 1);
                    rbtn[k].Size = new Size(50, 50);
                    rbtn[k].Location = new Point(5 + k * 50, 40);
                    p3[i].Controls.Add(rbtn[k]);
                }
            }

            Button sendBtn = new Button();
            sendBtn.Name = "sendBtn";
            sendBtn.Click += sendBtn_Click;
            sendBtn.Text = "Send";
            sendBtn.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            sendBtn.Location = new Point(f2.Width - 100, q * 109 - 30);
            f2.Controls.Add(sendBtn); 
        f2.Show();
        }

        public void Settings(int q)
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
                p2[i].Name = "pnl" + (i + 1);
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
            btn.Name = "saveBtn";
            btn.Click += saveBtn_Click;
            btn.Text = "Save";
            btn.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btn.Location = new Point(p.Width - 100, p.Height - 30);
            p.Controls.Add(btn);

        }

        public void Reader(string path) => CreateForm(File.ReadAllLines(pathTxtb.Text).Length);

        private void timer_tick(object sender, EventArgs e)
        {
            long tick = DateTime.Now.Ticks - date.Ticks;
            DateTime stopWatch = new DateTime();

            stopWatch = stopWatch.AddTicks(tick);
            f2.Controls["timerLbl"].Text = string.Format("{0:HH:mm:ss}", stopWatch);
        }

        private void createBtn_Click(object sender, EventArgs e) {

            if (quantityTxtb.Text != "" & comboBox1.Text != "") Settings(Convert.ToInt32(quantityTxtb.Text));

        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            string[] questions = new string[Convert.ToInt32(quantityTxtb.Text)];

            for (int i = 0; i < Convert.ToInt32(quantityTxtb.Text); i++)
            {
                if (this.Controls["container"].Controls["pnl" + (i + 1)].Controls["tb" + (i + 1)].Text != "")
                {
                    questions[i] = crypt.Encrypt(this.Controls["container"].Controls["pnl" + (i + 1)].Controls["tb" + (i + 1)].Text, "artmiptv");
                }
            }

            StreamWriter textFile = new StreamWriter(@"C:test.atpv");
            textFile.Write(string.Join("\n", questions));
            textFile.Close();
        }

        private void browseBtn_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() != DialogResult.OK) return;
            pathTxtb.Text = openFileDialog1.FileName;
        }

        private void openBtn_Click(object sender, EventArgs e) => Reader(pathTxtb.Text);

        private void sendBtn_Click(object sender, EventArgs e)
        {
            /*for (int i = 0; i < 4; i++)
            {
                if (f2.Controls["pnls" + (i+1)].Controls["rbtn"+ (i + 1)].Checked == true)
                {
                    MessageBox.Show("Done!");
                }
            }*/
        }
        public Form1()
        {
            InitializeComponent();
        }
    }

    public class Crypt
    {
        public string Encrypt(string str, string keyCrypt)
        {
            return Convert.ToBase64String(Encrypt(Encoding.UTF8.GetBytes(str), keyCrypt));
        }

        public string Decrypt(string str, string keyCrypt)
        {
            string Result;
            try
            {
                CryptoStream Cs = InternalDecrypt(Convert.FromBase64String(str), keyCrypt);
                StreamReader Sr = new StreamReader(Cs);

                Result = Sr.ReadToEnd();

                Cs.Close();
                Cs.Dispose();

                Sr.Close();
                Sr.Dispose();
            }
            catch (CryptographicException)
            {
                return null;
            }

            return Result;
        }

        private byte[] Encrypt(byte[] key, string value)
        {
            SymmetricAlgorithm Sa = Rijndael.Create();
            ICryptoTransform Ct = Sa.CreateEncryptor((new PasswordDeriveBytes(value, null)).GetBytes(16), new byte[16]);

            MemoryStream Ms = new MemoryStream();
            CryptoStream Cs = new CryptoStream(Ms, Ct, CryptoStreamMode.Write);

            Cs.Write(key, 0, key.Length);
            Cs.FlushFinalBlock();

            byte[] Result = Ms.ToArray();

            Ms.Close();
            Ms.Dispose();

            Cs.Close();
            Cs.Dispose();

            Ct.Dispose();

            return Result;
        }

        private CryptoStream InternalDecrypt(byte[] key, string value)
        {
            SymmetricAlgorithm sa = Rijndael.Create();
            ICryptoTransform ct = sa.CreateDecryptor((new PasswordDeriveBytes(value, null)).GetBytes(16), new byte[16]);

            MemoryStream ms = new MemoryStream(key);
            return new CryptoStream(ms, ct, CryptoStreamMode.Read);
        }
    }
}
