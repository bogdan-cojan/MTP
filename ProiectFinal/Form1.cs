using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProiectMTP
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] utilizatori = File.ReadAllLines(@"B:\Faculta\Sem1\MTP\Lab\ProiectFinal\ProiectMTP\Fisiere\User.txt");
            bool handleError = false;
            foreach (var line in utilizatori)
            {
                string[] inregistrare = line.Split(',');
                if(inregistrare[0]==textBox1.Text.Trim() && inregistrare[1] == textBox2.Text.Trim())
                {
                    this.Hide();
                    Biblioteca f = new Biblioteca(inregistrare[0]);
                    f.Show();
                    handleError = false;
                    break;
                }
                handleError = true;
            }
            if (handleError)
            {
                MessageBox.Show("Email sau Parola incorecte !");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            RecoverPass f = new RecoverPass();
            f.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            CreateAccount f = new CreateAccount();
            f.Show();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ControlExtension.Draggable(label1,true);
            ControlExtension.Draggable(label2, true);
            ControlExtension.Draggable(textBox1, true);
            ControlExtension.Draggable(textBox2, true);
            ControlExtension.Draggable(button1, true);
            ControlExtension.Draggable(button2, true);
            ControlExtension.Draggable(button3, true);
        }
    }
}
