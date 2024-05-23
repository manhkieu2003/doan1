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
using System.Linq.Expressions;
using System.Diagnostics.Eventing.Reader;

namespace qlnhanvien
{
    public partial class Form1 : Form
    {
        static string sql = @"Data Source=DESKTOP-7HQBIC8;Initial Catalog=quanlicuahangxedapdien;Integrated Security=True;Encrypt=False";
        SqlConnection conn = new SqlConnection(sql);
        public Form1()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }
        private void xoatextbox()
        {
            tb_mnv.Clear();
            tb_tennv.Clear();
            dt_ngaysinh.Text = "";
            cb_gt.Text = "";
            tb_diachi.Clear();
            tb_sdt.Clear();
            tb_mnv.Focus();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                    string query = "select * from nhanvien";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(reader);
                    dgv.DataSource = dt;
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

        private void bt_them_Click(object sender, EventArgs e)
        {
            if (tb_mnv.Equals(""))
            {
                MessageBox.Show("Không được để trống", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                    string query = "Insert into nhanvien values(@manv,@tennv,@ngaysinh,@gioitinh,@diachi,@sodienthoai)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@manv", tb_mnv.Text);
                    cmd.Parameters.AddWithValue("@tennv", tb_tennv.Text);
                    cmd.Parameters.AddWithValue("@ngaysinh", dt_ngaysinh.Value.ToString("yyyy/MM/dd"));
                    cmd.Parameters.AddWithValue("@gioitinh", cb_gt.Text);
                    cmd.Parameters.AddWithValue("@diachi", tb_diachi.Text);
                    cmd.Parameters.AddWithValue("sodienthoai", Int32.Parse(tb_sdt.Text));
                    cmd.ExecuteNonQuery();
                }
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                Form1_Load(sender, e);
                xoatextbox();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int i;
            i = dgv.CurrentRow.Index;
            tb_mnv.Text = dgv.Rows[i].Cells[0].Value.ToString();
            tb_tennv.Text = dgv.Rows[i].Cells[1].Value.ToString();
            dt_ngaysinh.Text = dgv.Rows[i].Cells[2].Value.ToString();
            cb_gt.Text = dgv.Rows[i].Cells[3].Value.ToString();
            tb_diachi.Text = dgv.Rows[i].Cells[4].Value.ToString();
            tb_sdt.Text = dgv.Rows[i].Cells[5].Value.ToString();

        }

        private void bt_sua_Click(object sender, EventArgs e)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                    string query = "update nhanvien set tennv=@tennv,ngaysinh=@ngaysinh,gioitinh=@gioitinh,diachi=@diachi,sodienthoai=@sodienthoai where manv=@manv";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@manv", tb_mnv.Text);
                    cmd.Parameters.AddWithValue("@tennv", tb_tennv.Text);
                    cmd.Parameters.AddWithValue("@ngaysinh", dt_ngaysinh.Value.ToString("yyyy/MM/dd"));
                    cmd.Parameters.AddWithValue("@gioitinh", cb_gt.Text);
                    cmd.Parameters.AddWithValue("@diachi", tb_diachi.Text);
                    cmd.Parameters.AddWithValue("sodienthoai", Int32.Parse(tb_sdt.Text));
                    cmd.ExecuteNonQuery();
                }
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                Form1_Load(sender, e);
                xoatextbox();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bt_xoa_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);


            if (result == DialogResult.Yes)
            {
                try
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                        string query = "delete from nhanvien where manv='" + tb_mnv.Text + "'";
                        SqlCommand cmd = new SqlCommand(query, conn);

                        cmd.ExecuteNonQuery();
                    }
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                    Form1_Load(sender, e);
                    xoatextbox();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                MessageBox.Show("Dữ liệu đã được xóa!");
            }
            else
            {
                // Người dùng không muốn xóa
                MessageBox.Show("Hành động xóa đã bị hủy!");
            }

        }

        private void bt_timkiem_Click(object sender, EventArgs e)
        {
            if (bt_timkiem.Text == "Tìm")
            {
                try
                {


                    string query = "select * from nhanvien where (manv=@manv) or (tennv=@tennv) or(ngaysinh=@ngaysinh)  or (gioitinh=@gioitinh) or (diachi=@diachi) or (sodienthoai=@sodienthoai)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("manv", tb_mnv.Text);
                    cmd.Parameters.AddWithValue("tennv", tb_tennv.Text);
                    cmd.Parameters.AddWithValue("ngaysinh", dt_ngaysinh.Value.ToString("MM/dd/yyyy"));
                    cmd.Parameters.AddWithValue("gioitinh", cb_gt.Text);
                    cmd.Parameters.AddWithValue("diachi", tb_diachi.Text);
                    cmd.Parameters.AddWithValue("sodienthoai", tb_sdt.Text);
                    DataTable data = new DataTable();
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(data);
                    if (data.Rows.Count > 0)
                    {
                        dgv.DataSource = data;
                        dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                        bt_timkiem.Text = "Dừng tìm";


                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy", "Tìm kiếm", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Tìm kiếm", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }



            }
            else
            {
                Form1_Load(sender, e);
                bt_timkiem.Text = "Tìm";
            }
        }

        private void bt_capnhat_Click(object sender, EventArgs e)
        {
            try
            {
                if(conn.State==ConnectionState.Closed)
                {
                    conn.Open();
                }
                xoatextbox();

                string sql = "select * from nhanvien";
                SqlCommand cmd = new SqlCommand(sql, conn);


                SqlDataReader reader = cmd.ExecuteReader();
              
                DataTable dt = new DataTable();
                dt.Load(reader);
               

                dgv.DataSource = dt;
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                Form1_Load(sender, e);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bt_thoát_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát?", "Xác nhận thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            // Nếu người dùng không muốn thoát, hủy sự kiện đóng form
            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}
