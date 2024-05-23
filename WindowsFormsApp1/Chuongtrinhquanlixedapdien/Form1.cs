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
using System.Data.SqlClient;
using System.Data;

namespace Chuongtrinhquanlixedapdien
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

       

        private void Form1_Load(object sender, EventArgs e)
        {
            matkhau.PasswordChar = '*';
        }

        

        private void bt_dangnhap_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-7HQBIC8;Initial Catalog=quanlicuahangxedapdien;Integrated Security=True;Encrypt=False");
            try {

                conn.Open();
                string taikhoan = tentk.Text;
                string matkhaus = matkhau.Text;
                string sql = "select * from taikhoan where tentaikhoan='" + taikhoan + "' and matkhau='" + matkhaus + "'";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader data = cmd.ExecuteReader();
                if(data.Read()==true)
                {
                    MessageBox.Show("Đăng nhập thành công");
                    Form3 f3 = new Form3();
                    f3.Visible = true;
                }
                else
                {
                    MessageBox.Show("Đăng nhập thất bại");
                }
                

            }catch(Exception ex)
            {
                MessageBox.Show("Lỗi kết nối");
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
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
