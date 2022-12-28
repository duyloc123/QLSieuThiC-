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
    public partial class FrmDangNhap : Form
    {
        public FrmDangNhap()
        {
            InitializeComponent();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        SieuThiContextDB db = new SieuThiContextDB();
        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            if (txtTaiKhoan.Text == "")
            {
                MessageBox.Show("Mời bạn nhập tên tài khoản!");
            }
            else if (txtMatKhau.Text == "")
            {
                MessageBox.Show("Mời bạn nhập mật khẩu!");
            }
            else
            {
                    TaiKhoan tk= db.TaiKhoans.FirstOrDefault(x => x.TenTaiKhoan == txtTaiKhoan.Text && x.MatKhau == txtMatKhau.Text && x.LoaiTaiKhoan.Equals("quanly"));
                    TaiKhoan tk2 = db.TaiKhoans.FirstOrDefault(x => x.TenTaiKhoan == txtTaiKhoan.Text && x.MatKhau == txtMatKhau.Text && x.LoaiTaiKhoan.Equals("banhang"));
                    TaiKhoan tk3 = db.TaiKhoans.FirstOrDefault(x => x.TenTaiKhoan == txtTaiKhoan.Text && x.MatKhau == txtMatKhau.Text && x.LoaiTaiKhoan.Equals("kho"));
                if (tk != null)
                {
                    MessageBox.Show("Đăng nhập thành công!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Hide();
                    FrmNhanVienQuanLy frmQuanLy = new FrmNhanVienQuanLy();
                    frmQuanLy.Show();
                }
                else if (tk2 != null)
                {
                    MessageBox.Show("Đăng nhập thành công!","Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Hide();
                    FrmNhanVienBanHang frmBanHang = new FrmNhanVienBanHang();
                    frmBanHang.Show();
                }
                else if(tk3 != null)
                {
                    MessageBox.Show("Đăng nhập thành công!","Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Hide();
                    FrmNhanVienKho frmNhanVienKho = new FrmNhanVienKho();
                    frmNhanVienKho.Show();
                }
                else
                {
                    MessageBox.Show("Sai tên tài khoản hoặc mật khẩu , Vui lòng nhập lại", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information) ;
                }
            }
        }
    }
}
