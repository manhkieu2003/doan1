using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sanpham
{
    internal class sp
    {
        private string masanpham;
        private string tensanpham;
        private float dongianhap;
        private float dongiaban;
        private string ghichu;
        private byte[] anh;
        private int soluong;
        private string maloaisanpham;

        public sp()
        {
        }

        public sp(string masanpham, string tensanpham, float dongianhap, float dongiaban, string ghichu, byte[] anh, int soluong, string maloaisanpham)
        {
            this.Masanpham = masanpham;
            this.Tensanpham = tensanpham;
            this.Dongianhap = dongianhap;
            this.Dongiaban = dongiaban;
            this.Ghichu = ghichu;
            this.Anh = anh;
            this.Soluong = soluong;
            this.Maloaisanpham = maloaisanpham;
        }

        public string Masanpham { get => masanpham; set => masanpham = value; }
        public string Tensanpham { get => tensanpham; set => tensanpham = value; }
        public float Dongianhap { get => dongianhap; set => dongianhap = value; }
        public float Dongiaban { get => dongiaban; set => dongiaban = value; }
        public string Ghichu { get => ghichu; set => ghichu = value; }
        public byte[] Anh { get => anh; set => anh = value; }
        public int Soluong { get => soluong; set => soluong = value; }
        public string Maloaisanpham { get => maloaisanpham; set => maloaisanpham = value; }
    }
}
