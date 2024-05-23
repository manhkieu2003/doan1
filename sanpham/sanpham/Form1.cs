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
using System.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace sanpham
{
    
    public partial class Form1 : Form
    {

        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-7HQBIC8;Initial Catalog=quanlicuahangxedapdien;Integrated Security=True;Encrypt=False");
        public Form1()
        {
            InitializeComponent();
            CenterToScreen();
            //this.StartPosition = FormStartPosition.CenterScreen;cách 2 căn giữa màn hình
        }
        public void capnhatdulieu()
        {
            tb_msp.Clear();
            tb_tensp.Clear();
            tb_dongianhap.Clear();
            tb_dongiaban.Clear();
            tb_sl.Clear();
            tb_ghichu.Clear();
            cb_lsp.Text = "";
            pictureBox1.ImageLocation = null;

        }
        void Viewdata()
        {
            SqlCommand cmd = new SqlCommand("select * from sanpham",conn);
            SqlDataAdapter data = new SqlDataAdapter(cmd);
            //SqlDataAdapter data = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            data.Fill(dt);
            dt.Columns.Add("Image", Type.GetType("System.Byte[]"));
            foreach (DataRow row in dt.Rows)
            {
                row["Image"] = File.ReadAllBytes(row["location"].ToString());
                  dgv.DataSource = dt;
            }
            DataGridViewImageColumn pic = new DataGridViewImageColumn();
            pic = (DataGridViewImageColumn)dgv.Columns[8];
            pic.ImageLayout = DataGridViewImageCellLayout.Zoom;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            Viewdata();
        }

        private void bt_chonanh_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Picture(*.jpg;*.png;*gif;*pdf)| *.jpg;*.png;*.gif;*.pdf";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(ofd.FileName);
                txt_imagepath.Text = ofd.FileName.ToString();
            }
        }

        private void bt_them_Click(object sender, EventArgs e)
        {
            SqlDataAdapter data = new SqlDataAdapter("insert into sanpham (masanpham,tensanpham,dongianhap,dongiaban,ghichu,soluong,maloaisanpham,location)" +
                " values('"+tb_msp.Text+"','"+tb_tensp.Text+"','"+tb_dongianhap.Text+"','"+tb_dongiaban.Text+"','"+tb_ghichu.Text+"','"+tb_sl.Text+"',N'"+cb_lsp.Text+"','"+txt_imagepath.Text+"')",conn);
            DataTable dt = new DataTable();
            data.Fill(dt);
            Viewdata();
            capnhatdulieu();
        }

        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            tb_msp.ReadOnly = true;
            int i;
            i = dgv.CurrentRow.Index;
            tb_msp.Text = dgv.Rows[i].Cells[0].Value.ToString();
            tb_tensp.Text = dgv.Rows[i].Cells[1].Value.ToString();
            tb_dongianhap.Text= dgv.Rows[i].Cells[2].Value.ToString();
            tb_dongiaban.Text = dgv.Rows[i].Cells[3].Value.ToString();
            tb_ghichu.Text = dgv.Rows[i].Cells[4].Value.ToString();
            tb_sl.Text = dgv.Rows[i].Cells[5].Value.ToString();
            cb_lsp.Text = dgv.Rows[i].Cells[6].Value.ToString();
            txt_imagepath.Text = dgv.Rows[i].Cells[7].Value.ToString();
            pictureBox1.ImageLocation = txt_imagepath.Text;
        }

        private void bt_sua_Click(object sender, EventArgs e)
        {
            //int i;
            //i = dgv.CurrentRow.Index;
            //SqlDataAdapter data = new SqlDataAdapter("Update sanpham SET tensanpham='" + tb_tensp.Text + "',dongianhap='" + tb_dongianhap.Text + "',dongiaban='" + tb_dongiaban.Text + "',ghichu='" + tb_ghichu.Text + "',soluong='" + tb_sl.Text + "',loaimasanpham='" + cb_lsp.Text + "',location='" + txt_imagepath.Text + "' where masanpham=" + dgv.Rows[i].Cells[0].Value.ToString() + "", conn);
            //string query = "Update sanpham SET name='" + textBox1.Text + "',location='" + textBox2.Text + "' where id=" + dataGridView1.Rows[i].Cells[1].Value.ToString() + "";
            //DataTable dt = new DataTable();
            //data.Fill(dt);

            //pictureBox1.ImageLocation = null;
            //Viewdata();
            try
            {
                string sql = @"Data Source=DESKTOP-7HQBIC8;Initial Catalog=quanlicuahangxedapdien;Integrated Security=True;Encrypt=False";
                SqlConnection conn = new SqlConnection(sql);
                conn.Open();
                string query = "Update sanpham SET tensanpham = @tensanpham,dongianhap = @dongianhap,dongiaban = @dongiaban,ghichu = @ghichu, soluong = @soluong, maloaisanpham= @maloaisanpham, location=@location where masanpham = @masanpham";
                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("masanpham", tb_msp.Text);
                cmd.Parameters.AddWithValue("tensanpham", tb_tensp.Text);
                cmd.Parameters.AddWithValue("dongianhap", tb_dongianhap.Text);
                cmd.Parameters.AddWithValue("dongiaban", tb_dongiaban.Text);
                cmd.Parameters.AddWithValue("ghichu", tb_ghichu.Text);
               
                cmd.Parameters.AddWithValue("soluong", tb_sl.Text);
                cmd.Parameters.AddWithValue("maloaisanpham", cb_lsp.Text);
                cmd.Parameters.AddWithValue("location", txt_imagepath.Text);
                cmd.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Sửa thành công");

                Viewdata();
                capnhatdulieu();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
            


private void bt_xoa_Click(object sender, EventArgs e)
        {
            //int i;
            //i = dgv.CurrentRow.Index;
            //SqlDataAdapter data = new SqlDataAdapter("delete from  sanpham where tensanpham =" + dgv.Rows[i].Cells[1].Value.ToString() + " ", conn);
            ////string query = "Update sanpham SET name='" + textBox1.Text + "',location='"+textBox2.Text+"' where id="+ dataGridView1.Rows[i].Cells[1].Value.ToString() +"";
            //DataTable dt = new DataTable();
            //data.Fill(dt);

            //pictureBox1.ImageLocation = null;
            //Viewdata();
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                try
                {

                    string sql = @"Data Source=DESKTOP-7HQBIC8;Initial Catalog=quanlicuahangxedapdien;Integrated Security=True;Encrypt=False";
                    SqlConnection conn = new SqlConnection(sql);

                    conn.Open();

                    string query = "delete from sanpham where masanpham=@masanpham";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("masanpham", tb_msp.Text);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Xóa thanh cong");
                    Viewdata();

                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
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
                    string sql = "select * from sanpham where (masanpham=@masanpham) or (tensanpham=@tensanpham) or(soluong=@soluong) or(maloaisanpham=@maloaisanpham)";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("masanpham", tb_msp.Text);
                    cmd.Parameters.AddWithValue("tensanpham", tb_tensp.Text);
                    cmd.Parameters.AddWithValue("soluong", tb_sl.Text);
                    cmd.Parameters.AddWithValue("maloaisanpham", cb_lsp.Text);

                    DataTable data = new DataTable();
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(data);

                    //if (data.Rows.Count > 0)
                    //{
                    //    dataGridView1.DataSource = data;
                    //    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                    //    bt_timkiem.Text = "Dừng tìm";

                    //}
                    data.Columns.Add("Image", Type.GetType("System.Byte[]"));
                    foreach (DataRow row in data.Rows)
                    {
                        row["Image"] = File.ReadAllBytes(row["location"].ToString());
                        dgv.DataSource = data;
                        bt_timkiem.Text = "Dừng tìm";

                    }


                    //else
                    //{
                    //    MessageBox.Show("Không tìm thấy", "Tìm kiếm", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //}
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
            else
            {
                Viewdata();
                bt_timkiem.Text = "Tìm";
            }
        }

        private void bt_thoat_Click(object sender, EventArgs e)
        {

            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát?", "Xác nhận thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            // Nếu người dùng không muốn thoát, hủy sự kiện đóng form
            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void capnhat_Click(object sender, EventArgs e)
        {
            tb_msp.ReadOnly = false;
            tb_msp.Clear();
            tb_tensp.Clear();
            tb_dongianhap.Clear();
            tb_dongiaban.Clear();
            tb_sl.Clear();
            tb_ghichu.Clear();
            cb_lsp.Text = "";
            txt_imagepath.Clear();
            pictureBox1.ImageLocation = null;
        }
    }
}
