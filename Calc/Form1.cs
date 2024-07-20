using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;

namespace Calc
{
    public partial class Form1 : Form
    {
        string amaliat = "";
        string holdstring = "";
        string connectionString = "Server=.;Database=calc;User ID=sa;Password=123";
        public void con()
        { 
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string query = "SELECT * from history";
            SqlCommand command = new SqlCommand(query, connection);
            SqlDataReader reader = command.ExecuteReader();
            DataTable dataTable = new DataTable();
            dataTable.Load(reader);
            dataGridView1.DataSource = dataTable;
        }
        public Form1()
        {
            InitializeComponent();
            con();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text += (sender as Button).Text;
        }

        private void button15_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                textBox1.Text = textBox1.Text.Substring(0, textBox1.Text.Length - 1);
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "-")
            {
                textBox1.Clear();
            }
            else if (textBox1.Text == "")
            {
                textBox1.Text = "-";
            }
            else
            {
                amaliat = (sender as Button).Text;
                holdstring = textBox1.Text;
                label1.Text = textBox1.Text + " " + (sender as Button).Text;
                textBox1.Clear();
            }
            
        }

        private void button17_Click(object sender, EventArgs e)
        {
            if(textBox1.Text != "" && label1.Text != "" && amaliat != "" && holdstring != "")
            {
                SqlConnection connection = new SqlConnection(connectionString);
                string query = "INSERT INTO history (firstint, operation, secondint, equal) VALUES (@firstint , @operation, @secondint , @equal);";
                connection.Open();
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@firstInt", holdstring);
                cmd.Parameters.AddWithValue("@operation", amaliat);
                cmd.Parameters.AddWithValue("@secondInt", textBox1.Text);
                switch (amaliat)
                    {
                        case ("-"): 
                            cmd.Parameters.AddWithValue("@equal", (Convert.ToInt32(holdstring)-Convert.ToInt32(textBox1.Text)).ToString());
                            textBox1.Text = (Convert.ToInt32(holdstring) - Convert.ToInt32(textBox1.Text)).ToString();
                            label1.Text = "";
                            holdstring = "";
                            break;
                        case ("/"):
                            cmd.Parameters.AddWithValue("@equal", (Convert.ToInt32(holdstring) / Convert.ToInt32(textBox1.Text)).ToString());
                            textBox1.Text = (Convert.ToInt32(textBox1.Text) / Convert.ToInt32(holdstring)).ToString();
                            label1.Text = "";
                            holdstring = "";
                        break;
                        case ("+"):
                            cmd.Parameters.AddWithValue("@equal", (Convert.ToInt32(holdstring) + Convert.ToInt32(textBox1.Text)).ToString());
                            textBox1.Text = (Convert.ToInt32(textBox1.Text) + Convert.ToInt32(holdstring)).ToString();
                            label1.Text = "";
                            holdstring = "";
                        break;
                        case ("*"):
                            cmd.Parameters.AddWithValue("@equal", (Convert.ToInt32(holdstring) * Convert.ToInt32(textBox1.Text)).ToString());
                            textBox1.Text = (Convert.ToInt32(textBox1.Text) * Convert.ToInt32(holdstring)).ToString();
                            label1.Text = "";
                            holdstring = "";
                        break;
                        default:
                            break;
                    }
                cmd.ExecuteNonQuery();
                connection.Close();
                con();
            }
            
        }

        private void button19_Click(object sender, EventArgs e)
        {
            string query = "DELETE FROM history;";
            SqlConnection cone = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(query, cone);
            cone.Open();
            cmd.ExecuteNonQuery();
            cone.Close();
            con();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];
            string cellValue = Convert.ToString(selectedRow.Cells["equal"].Value);
            textBox1.Text = cellValue;
        }

        public void AmaliatF(string amaliatinp)
        {
            if (textBox1.Text == "-")
            {
                textBox1.Clear();
            }
            else if (textBox1.Text == "")
            {
                textBox1.Text = "-";
            }
            else
            {
                amaliat = amaliatinp;
                holdstring = textBox1.Text;
                label1.Text = textBox1.Text + " " + amaliatinp;
                textBox1.Clear();
            }
        }
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case '0':
                    textBox1.Text += "0";
                    break;
                case '1':
                    textBox1.Text += "1";
                    break;
                case '2':
                    textBox1.Text += "2";
                    break;
                case '3':
                    textBox1.Text += "3";
                    break;
                case '4':
                    textBox1.Text += "4";
                    break;
                case '5':
                    textBox1.Text += "5";
                    break;
                case '6':
                    textBox1.Text += "6";
                    break;
                case '7':
                    textBox1.Text += "7";
                    break;
                case '8':
                    textBox1.Text += "8";
                    break;
                case '9':
                    textBox1.Text += "9";
                    break;
                case '+':
                    AmaliatF("+");
                    break;
                case '-':
                    AmaliatF("-");
                    break;
                case '*':
                    AmaliatF("*");
                    break;
                case '/':
                    AmaliatF("/");
                    break;
            }
            if (e.KeyChar == 13)
            {
                button17_Click(null, null);
            }
            else if (e.KeyChar == 8)  
            button15_Click(null, null); 
        }
    }
}
