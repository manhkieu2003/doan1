using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lichsuban
{
    public partial class Form1 : Form
    {
        static string sql = @"Data Source=MSI\SQLEXPRESS03;Initial Catalog=DA1;Integrated Security=True;Encrypt=False";
        SqlConnection conn = new SqlConnection(sql);
        SqlCommand cmd;
        SqlDataAdapter dta;
        DataSet ds;
        DataTable dt;
        public Form1()
        {
            InitializeComponent();
        }

        public void hienthihd()
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                    string query = "select * from HoaDon";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(reader);
                    dgv1.DataSource = dt;
                }
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void hienthicthd()
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                    string query = "select * from ChiTietHD";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(reader);
                    dgv2.DataSource = dt;
                }
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát?", "Xác nhận thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            // Nếu người dùng không muốn thoát, hủy sự kiện đóng form
            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (tim.Text == "Tìm")
            {
                try
                {
                    sql = "select * from HoaDon where (MaHD=@MaHD)";
                    cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("MaHD", mahd.Text);
                    //sql = "select * from ChiTietHD where (MaHD=@MaHD)";
                    //cmd = new SqlCommand(sql, conn);
                    //cmd.Parameters.AddWithValue("MaHD", mahd.Text);

                    //sql = "select * from ChiTietHD where (MaSP=@MaSP)";
                    //cmd = new SqlCommand(sql, conn);
                    //                  cmd.Parameters.AddWithValue("MaSP", masp.Text);
                    DataTable data = new DataTable();
                    dta = new SqlDataAdapter(cmd);
                    dta.Fill(data);
                    if (data.Rows.Count > 0)
                    {
                        dgv1.DataSource = data;
                        dgv1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                        tim.Text = "Dừng";
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy", "Tìm kiếm", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Không tìm thấy", "Tìm kiếm", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                //LoadData();
                Form1_Load(sender, e);
                tim.Text = "Tìm";
            }
        }
        //private void LoadData()
        //{
        //    sql = "select * from HoaDon";
        //    cmd = new SqlCommand(sql, conn);
        //    dta = new SqlDataAdapter(cmd);
        //    cmd.ExecuteNonQuery();
        //    ds = new DataSet();
        //    dta.Fill(ds);

        //    sql = "select * from ChiTietHD";
        //    cmd = new SqlCommand(sql, conn);
        //    dta = new SqlDataAdapter(cmd);
        //    cmd.ExecuteNonQuery();
        //    ds = new DataSet();
        //    dta.Fill(ds);

        //    dgv1.DataSource = ds.Tables[0];
        //    dgv1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        //}

        private void Form1_Load(object sender, EventArgs e)
        {
            hienthihd();
            hienthicthd();
        }
    }
}
