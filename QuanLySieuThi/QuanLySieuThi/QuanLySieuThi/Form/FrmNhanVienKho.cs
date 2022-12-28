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
    public partial class FrmNhanVienKho : Form
    {
        public FrmNhanVienKho()
        {
            InitializeComponent();
        }
        private void btnThoat_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
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

        private void btnDangXuat_Click_1(object sender, EventArgs e)
        {
            DialogResult dt = MessageBox.Show("Bạn có muốn đăng xuất tài khoản ?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dt == DialogResult.Yes)
            {
                this.Hide();
                FrmDangNhap frmDangNhap = new FrmDangNhap();
                frmDangNhap.Show();
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void btnNhapHang_Click(object sender, EventArgs e)
        {
            loadFrm(new FrmXuatHang());
        }

        private void btnNhapHang_Click_1(object sender, EventArgs e)
        {
            loadFrm(new FrmNhapHang());
        }

        private void btnHangHoa_Click_1(object sender, EventArgs e)
        {
            loadFrm(new FrmHangHoa());
        }
    }
}
