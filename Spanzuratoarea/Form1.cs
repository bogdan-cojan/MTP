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

namespace lab3spanzuratoarea
{
    public partial class Form1 : Form
    {
        string[] cuvinte;
        List<TextBox> myTextBoxs = new List<TextBox>();
        int punctaj = 0;
        int greseli = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void button27_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (File.Exists("B:\\Faculta\\Sem1\\MTP\\Lab\\Lab3\\Exercitii\\lab3spanzuratoarea\\bin\\Debug\\net5.0-windows\\joc.txt"))
            {
                string[] lines = File.ReadAllLines("joc.txt");
                var r = new Random();
                var randLN = r.Next(0, lines.Length - 1);
                string linie = lines[randLN];

                cuvinte = linie.Split(",");

                label3.Text = cuvinte[1];

                for(int i = 0; i <= cuvinte[0].Length-1; i++)
                {
                    TextBox txtB = new TextBox();
                    txtB.Name = "txtBox_" + i.ToString();
                    txtB.Width = 30;
                    txtB.Location = new System.Drawing.Point(47 + (i * (txtB.Width + 10)), 94);
                    myTextBoxs.Add(txtB);
                }

                foreach(TextBox TxtBox in myTextBoxs)
                {
                    groupBox1.Controls.Add(TxtBox);
                }

               

            }
            else { MessageBox.Show("Fisierul nu exista !"); }
        }

        private void buttonLitere_Click(object sender, EventArgs e)
        {
            Button b = sender as Button;
            char[] litere = cuvinte[0].ToCharArray();
            bool check = true;
            string verific = "";

            for (int i = 0; i <= cuvinte[0].Length - 1; i++)
            {
                if (b.Text == litere[i].ToString())
                {
                    greseli = 0;
                    myTextBoxs[i].Text = b.Text;
                    punctaj = punctaj + 10;
                    lblPunctaj.Text = punctaj.ToString();
                    check = false;
                }

            }

            for (int j = 0; j <= cuvinte[0].Length - 1; j++)
            {
                verific = verific + myTextBoxs[j].Text;

                if (verific == cuvinte[0])
                {
                    MessageBox.Show("Felicitari ! Ai ghicit !!!");
                    for (int k = 0; k <= cuvinte[0].Length - 1; k++)
                    {
                        myTextBoxs[k].Enabled = false;
                    }
                    button1.Enabled = false;
                    button2.Enabled = false;
                    button3.Enabled = false;
                    button4.Enabled = false;
                    button5.Enabled = false;
                    button6.Enabled = false;
                    button7.Enabled = false;
                    button8.Enabled = false;
                    button9.Enabled = false;
                    button10.Enabled = false;
                    button11.Enabled = false;
                    button12.Enabled = false;
                    button13.Enabled = false;
                    button14.Enabled = false;
                    button15.Enabled = false;
                    button16.Enabled = false;
                    button17.Enabled = false;
                    button18.Enabled = false;
                    button19.Enabled = false;
                    button20.Enabled = false;
                    button21.Enabled = false;
                    button22.Enabled = false;
                    button23.Enabled = false;
                    button24.Enabled = false;
                    button25.Enabled = false;
                    button26.Enabled = false;
                    button28.Enabled = false;
                }
            }

            

            if (check)
            {
                greseli++;
                punctaj = punctaj - 10;
                lblPunctaj.Text = punctaj.ToString();
                if (greseli == 3)
                {
                    MessageBox.Show("Ai pierdut !");
                    for(int i = 0; i <= cuvinte[0].Length - 1; i++)
                    {
                        myTextBoxs[i].Enabled = false;
                        button1.Enabled = false;
                        button2.Enabled = false;
                        button3.Enabled = false;
                        button4.Enabled = false;
                        button5.Enabled = false;
                        button6.Enabled = false;
                        button7.Enabled = false;
                        button8.Enabled = false;
                        button9.Enabled = false;
                        button10.Enabled = false;
                        button11.Enabled = false;
                        button12.Enabled = false;
                        button13.Enabled = false;
                        button14.Enabled = false;
                        button15.Enabled = false;
                        button16.Enabled = false;
                        button17.Enabled = false;
                        button18.Enabled = false;
                        button19.Enabled = false;
                        button20.Enabled = false;
                        button21.Enabled = false;
                        button22.Enabled = false;
                        button23.Enabled = false;
                        button24.Enabled = false;
                        button25.Enabled = false;
                        button26.Enabled = false;
                        button28.Enabled = false;
                    }
                }
            }
        }

        private void buttonRezolvare_Click(object sender, EventArgs e)
        {
            char[] litere = cuvinte[0].ToCharArray();
            
            for(int i = 0; i <= cuvinte[0].Length - 1; i++)
            {
                myTextBoxs[i].Text = litere[i].ToString();
                myTextBoxs[i].Enabled = false;
                button1.Enabled = false;
                button2.Enabled = false;
                button3.Enabled = false;
                button4.Enabled = false;
                button5.Enabled = false;
                button6.Enabled = false;
                button7.Enabled = false;
                button8.Enabled = false;
                button9.Enabled = false;
                button10.Enabled = false;
                button11.Enabled = false;
                button12.Enabled = false;
                button13.Enabled = false;
                button14.Enabled = false;
                button15.Enabled = false;
                button16.Enabled = false;
                button17.Enabled = false;
                button18.Enabled = false;
                button19.Enabled = false;
                button20.Enabled = false;
                button21.Enabled = false;
                button22.Enabled = false;
                button23.Enabled = false;
                button24.Enabled = false;
                button25.Enabled = false;
                button26.Enabled = false;
                button28.Enabled = false;
            }

            MessageBox.Show("Ai pierdut ! Mai incearca.");
        }
    }
}
