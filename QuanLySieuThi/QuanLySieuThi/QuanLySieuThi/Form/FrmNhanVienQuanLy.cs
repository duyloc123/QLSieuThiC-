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
    public partial class FrmNhanVienQuanLy : Form
    {
        SieuThiContextDB db = new SieuThiContextDB();
        public FrmNhanVienQuanLy()
        {
            InitializeComponent();
        }

        private void btnThoat_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            DialogResult dt = MessageBox.Show("Bạn có muốn đăng xuất tài khoản ?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dt == DialogResult.Yes)
            {
                this.Hide();
                FrmDangNhap frmDangNhap = new FrmDangNhap();
                frmDangNhap.Show();
            }
        }

        private void loadFrm(object Form)
        {
            if (this.panelmain.Controls.Count > 0)
                this.panelmain.Controls.RemoveAt(0);
            Form f = Form as Form;
            f.TopLevel = false;
            f.Dock = DockStyle.Fill;
            this.panelmain.Controls.Add(f);
            this.panelmain.Tag = f;
            f.Show();
        }

        private void btnNhanVien_Click(object sender, EventArgs e)
        {
            loadFrm(new FrmNhanVien());
        }

        private void btnKhachHang_Click(object sender, EventArgs e)
        {
            loadFrm(new FrmKhachHang());
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            loadFrm(new FrmNCC());
        }

        private void btnHangHoa_Click(object sender, EventArgs e)
        {
            loadFrm(new FrmHangHoa());
        }

        private void btnNhapHang_Click(object sender, EventArgs e)
        {
            loadFrm(new FrmNhapHang());
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            loadFrm(new FrmDoanhThu());
        }

        private void btnHangHoa_Click_1(object sender, EventArgs e)
        {
            loadFrm(new FrmHangHoa());
        }

        private void guna2Button1_Click_1(object sender, EventArgs e)
        {
            loadFrm(new FrmLoaiHang());
        }

        private void btnNhanVien_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
