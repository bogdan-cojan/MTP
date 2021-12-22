using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace ProiectMTP
{
    public partial class Biblioteca : Form
    {
        public List<string> textLines;
        DataTable table = new DataTable();
        public Biblioteca(string mail)
        {
            InitializeComponent();

            string[] nume = mail.Split("@");
            lblWelcome.Text = "Buna, " + nume[0].ToUpper() + " !";

            this.dataGridView1.DragDrop += new
                 System.Windows.Forms.DragEventHandler(this.dataGridView1_DragDrop);
            this.dataGridView1.DragEnter += new
                 System.Windows.Forms.DragEventHandler(this.dataGridView1_DragEnter);
        }
        private void Biblioteca_Load(object sender, EventArgs e)
        {
            ControlExtension.Draggable(lblWelcome, true);

            // Tab Acasa
            table.Columns.Add("Titlul", typeof(string));
            table.Columns.Add("Autor", typeof(string));
            table.Columns.Add("Gen", typeof(string));

            dataGridView1.DataSource = table;
            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dataGridView1.Columns["Titlul"].ReadOnly = true;
            dataGridView1.Columns["Autor"].ReadOnly = true;
            dataGridView1.Columns["Gen"].ReadOnly = true;
        }
        private void dataGridView1_DragEnter(object sender, System.Windows.Forms.DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.All;
            else
                e.Effect = DragDropEffects.None;
        }
        private void dataGridView1_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
        {
            table.Rows.Clear();

            string[] s = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            textLines = File.ReadAllLines(s[0]).ToList();

            foreach (var line in textLines)
            {
                string[] inregistrare = line.Split(',');

                table.Rows.Add(inregistrare);
            }
        }
        private void Biblioteca_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form1 f = new Form1();
            f.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bool handleError = false;

            if (textLines is null)
            {
                MessageBox.Show("Eroare !\n Trebuie mai intai sa incarcati o biblioteca !");
                handleError = true;
            }

            bool error = false;

            if (!handleError)
            {
                if (!dataGridView1.AreAllCellsSelected(error))
                {
                    DialogResult dresult = MessageBox.Show("Esti sigur ca stergi?", "Alert"
                                      , MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                    if (dresult == DialogResult.OK)
                    {
                        foreach (DataGridViewRow item in this.dataGridView1.SelectedRows)
                        {
                            dataGridView1.Rows.Remove(item);
                        }
                        for (int i = 0; i < textLines.Count; i++)
                        {
                            string[] inregistrare = textLines[i].Split(",");
                            bool found = false;
                            foreach (DataGridViewRow row in this.dataGridView1.Rows)
                            {
                                if (inregistrare[0].Equals(row.Cells[0].Value))
                                {
                                    found = true;
                                    break;
                                }
                            }
                            if (!found)
                            {
                                textLines.RemoveAt(i);
                                i--;
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Eroare ! Selectati unul sau mai multe randuri !");
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            DataView dv = table.DefaultView;
            if (dv != null)
            {
                dv.RowFilter = @"Titlul like '%" + textBox1.Text + "%'";
            }
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            if (textLines is null)
            {
                MessageBox.Show("Eroare !\n Trebuie mai intai sa incarcati o biblioteca !");
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (textLines is null)
            {
                MessageBox.Show("Eroare !\n Trebuie mai intai sa incarcati o biblioteca !");
            }
            else
            {
                string connect = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=B:\Faculta\Sem1\MTP\Lab\ProiectFinal\ProiectMTP\Biblioteca.mdf;Integrated Security=True";
                SqlConnection cnn = new SqlConnection(connect);
                cnn.Open();
                foreach (var line in textLines)
                {
                    string[] inregistrare = line.Split(',');
                    string stmt = "INSERT INTO Carte ([Titlul], [Autor], [Gen]) VALUES (@titlul, @autor, @gen)";
                    SqlCommand sc = new SqlCommand(stmt, cnn);
                    sc.Parameters.AddWithValue("@titlul", inregistrare[0]);
                    sc.Parameters.AddWithValue("@autor", inregistrare[1]);
                    sc.Parameters.AddWithValue("@gen", inregistrare[2]);
                    sc.ExecuteNonQuery();
                }
                cnn.Close();

                MessageBox.Show("Salvare cu succes !");
            }
        }
        private void tabControl1_Click(object sender, EventArgs e)
        {
            //Tab Biblioteca Mea
            string connect = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=B:\Faculta\Sem1\MTP\Lab\ProiectFinal\ProiectMTP\Biblioteca.mdf;Integrated Security=True";
            SqlConnection cnn = new SqlConnection(connect);
            cnn.Open();
            string tabel_date = "select * from Carte";
            SqlDataAdapter da = new SqlDataAdapter(tabel_date, connect);
            DataSet ds = new DataSet();
            da.Fill(ds, "Carte");
            dataGridView2.DataSource = ds.Tables["Carte"].DefaultView;
            cnn.Close();

            dataGridView2.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView2.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView2.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView2.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dataGridView2.Columns[0].ReadOnly = true;
            dataGridView2.Columns[1].ReadOnly = true;
            dataGridView2.Columns[2].ReadOnly = true;
            dataGridView2.Columns[3].ReadOnly = true;
        }
        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count > 1)
            {
                MessageBox.Show("Te rog alege doar o carte pe rand !");
            }
            else
            {
                int CarteId = 0;
                foreach (DataGridViewRow row in this.dataGridView2.SelectedRows)
                {
                    if (row.Cells[0].Value is not null)
                    {
                        CarteId = (int)row.Cells[0].Value;
                    }
                }

                if (CarteId != 0)
                {
                    Comment f = new Comment(CarteId);
                    f.Show();
                }
            }
        }
        private void button5_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count > 1)
            {
                MessageBox.Show("Te rog alege doar o carte pe rand !");
            }
            else
            {
                int CarteId = 0;
                foreach (DataGridViewRow row in this.dataGridView2.SelectedRows)
                {
                    if (row.Cells[0].Value is not null)
                    {
                        CarteId = (int)row.Cells[0].Value;
                    }
                }

                if (CarteId != 0)
                {
                    string connect = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=B:\Faculta\Sem1\MTP\Lab\ProiectFinal\ProiectMTP\Biblioteca.mdf;Integrated Security=True";
                    SqlConnection cnn = new SqlConnection(connect);
                    cnn.Open();
                    SqlCommand command = new SqlCommand("SELECT Descriere FROM Comentariu WHERE CarteId=" + CarteId + "");
                    command.Connection = cnn;
                    string description = (string)command.ExecuteScalar();
                    cnn.Close();

                    if (description is not null)
                    {
                        MessageBox.Show(description, "Comment",
                                     MessageBoxButtons.OK,
                                     MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Adaugati mai intai un comentariu pentru carte !", "Error",
                                     MessageBoxButtons.OK,
                                     MessageBoxIcon.Exclamation);
                    }
                }
            }
        }
    }
}