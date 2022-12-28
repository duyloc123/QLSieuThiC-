using QuanLyCuaHangDienThoai.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyCuaHangDienThoai
{
    public partial class FrmTimKiemHoaDon : Form
    {
        public FrmTimKiemHoaDon()
        {
            InitializeComponent();
        }

        private void txtTimKiem_TextChange(object sender, EventArgs e)
        {
            SieuThiContextDB db = new SieuThiContextDB();
            String keyword = txtTimKiem.Text.Trim();
            dgvTimKiem.DataSource = db.HoaDons.Where(p => p.KhachHang.tenKhachHang.Contains(keyword))
                .Select(hoadon => new
                {
                    hoadon.maHoaDon,
                    TenKhachHang = hoadon.KhachHang.tenKhachHang,
                    TenNhanVien = hoadon.NhanVien.tenNV,
                    NgayBan = hoadon.ngayBan,
                    TongTien = hoadon.tongTien
                }).ToList();
            dgvTimKiem.DataSource = db.HoaDons.Where(p => p.NhanVien.tenNV.Contains(keyword))
               .Select(hoadon => new
               {
                   hoadon.maHoaDon,
                   TenKhachHang = hoadon.KhachHang.tenKhachHang,
                   TenNhanVien = hoadon.NhanVien.tenNV,
                   NgayBan = hoadon.ngayBan,
                   TongTien = hoadon.tongTien
               }).ToList();
        }
    }
}
