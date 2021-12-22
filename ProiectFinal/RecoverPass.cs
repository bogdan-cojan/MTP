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
    public partial class RecoverPass : Form
    {
        public RecoverPass()
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
                if (inregistrare[0] == textBox1.Text.Trim())
                {
                    this.Close();
                    MessageBox.Show("Succes ! Parola dvs. este '" + inregistrare[1] + "'\n Puteti intra in cont acum.");
                    handleError = false;
                    break;
                }
                handleError = true;
            }
            if (handleError)
            {
                MessageBox.Show("Emailul gresit sau nu exista in baza de date !");
            }
        }
    }
}
