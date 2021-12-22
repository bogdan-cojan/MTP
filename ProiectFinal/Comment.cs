using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProiectMTP
{
    public partial class Comment : Form
    {
        int CarteId = 0;
        public Comment(int CId)
        {
            InitializeComponent();
            CarteId = CId;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() == "")
            {
                MessageBox.Show("Trebuie completata casuta pentru a se salva informatia !");
            }
            else
            {
                string connect = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=B:\Faculta\Sem1\MTP\Lab\ProiectFinal\ProiectMTP\Biblioteca.mdf;Integrated Security=True";
                SqlConnection cnn = new SqlConnection(connect);
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT COUNT(Descriere) FROM Comentariu WHERE CarteId=" + CarteId + "");
                command.Connection = cnn;
                int comenturi = (int)command.ExecuteScalar();

                if (comenturi > 0)
                {
                    string stmt = "UPDATE Comentariu SET [CarteId]=@carteId, [Descriere]=@descriere WHERE CarteId=" + CarteId + "";
                    SqlCommand sc = new SqlCommand(stmt, cnn);
                    sc.Parameters.AddWithValue("@carteId", CarteId);
                    sc.Parameters.AddWithValue("@descriere", textBox1.Text.Trim());
                    sc.ExecuteNonQuery();
                }
                else
                {
                    string stmt = "INSERT INTO Comentariu ([CarteId], [Descriere]) VALUES (@carteId, @descriere)";
                    SqlCommand sc = new SqlCommand(stmt, cnn);
                    sc.Parameters.AddWithValue("@carteId", CarteId);
                    sc.Parameters.AddWithValue("@descriere", textBox1.Text.Trim());
                    sc.ExecuteNonQuery();
                }
                cnn.Close();

                this.Close();

                MessageBox.Show("Salvare cu succes !");
            }
        }
    }
}
