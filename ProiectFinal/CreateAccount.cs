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
    public partial class CreateAccount : Form
    {
        public CreateAccount()
        {
            InitializeComponent();
        }

        bool IsValidEmail(string eMail)
        {
            bool Result = false;

            try
            {
                var eMailValidator = new System.Net.Mail.MailAddress(eMail);

                Result = (eMail.LastIndexOf(".") > eMail.LastIndexOf("@"));
            }
            catch
            {
                Result = false;
            };

            return Result;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool email = IsValidEmail(textBox1.Text.Trim());
            bool handleError = false;
            if (!email)
            {
                MessageBox.Show("Email incorect !");
                handleError = true;
            }

            if (textBox2.Text.Trim() != textBox3.Text.Trim())
            {
                MessageBox.Show("Casutele pentru parola trebuie sa fie identice !");
                handleError = true;
            }

            if (!handleError)
            {
                using StreamWriter file = new(@"B:\Faculta\Sem1\MTP\Lab\ProiectFinal\ProiectMTP\Fisiere\User.txt", append: true);
                string line = textBox1.Text.Trim() + "," + textBox2.Text.Trim();
                file.WriteLine(line);
                this.Close();
                MessageBox.Show("Cont creat cu succes !");
            }
        }
    }
}
