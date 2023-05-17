using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
namespace LibSys
{
    public partial class Form1 : Form
    {
        private OleDbConnection con;
        public Form1()
        {
            InitializeComponent();
            con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Z:\\QQ121\\LibSys.mdb");
            loadDatagrid();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void loadDatagrid()
        { 
        con.Open();
            OleDbCommand com = new OleDbCommand("Select * from book order by accession_number asc", con);
            com.ExecuteNonQuery();

            OleDbDataAdapter adap = new OleDbDataAdapter(com);
            DataTable tab = new DataTable();

            adap.Fill(tab);
            grid1.DataSource = tab;
            con.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            con.Open();
            OleDbCommand com = new OleDbCommand("Insert into book values('" +txtno.Text+ "', '" +txttitle.Text+ "', '"
                                                + txtauthor.Text + "')",con);
            com.ExecuteNonQuery();


            MessageBox.Show("Succesfully SAVED!","info", MessageBoxButtons.OK, MessageBoxIcon.Information);

            con.Close();
            loadDatagrid();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            con.Open();

            string num = txtno.Text;

            DialogResult dr = MessageBox.Show("Are your sure you want to delete this?", "Confirm Deletion",
                                                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                OleDbCommand com = new OleDbCommand("Delete from book where accession_number= " + num , con);
                com.ExecuteNonQuery();

                MessageBox.Show("Succesfully DELETED!", "info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("CANCELLED!", "info", MessageBoxButtons.OK, MessageBoxIcon.Information);        

            }
            con.Close();
            loadDatagrid();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            con.Open();
            string up = "Update book SET title = '" + txttitle.Text + "', author= '" +
                         txtauthor.Text + "' where accession_number = " + txtno.Text;
            OleDbCommand com = new OleDbCommand(up, con);               
            com.ExecuteNonQuery();

            MessageBox.Show("Successfully UPDATED!", "info", MessageBoxButtons.OK, MessageBoxIcon.Information);

            con.Close();
            loadDatagrid();
        }

        private void grid1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtno.Text = grid1.Rows[e.RowIndex].Cells["accession_number"].Value.ToString();
            txttitle.Text = grid1.Rows[e.RowIndex].Cells["title"].Value.ToString();
            txtauthor.Text = grid1.Rows[e.RowIndex].Cells["author"].Value.ToString();


        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            con.Open();

            OleDbCommand com = new OleDbCommand("Select * from book where title like'%"+ txtSearch.Text+"%'", con);
            com.ExecuteNonQuery();
            OleDbDataAdapter adap = new OleDbDataAdapter(com);
            DataTable data = new DataTable();

            adap.Fill(data);
            grid1.DataSource = data;

            con.Close();
        }
    }
}
