using QuanLyCuaHangDienThoai.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Migrations;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyCuaHangDienThoai
{
    public partial class FrmKhachHang : Form
    {
        public FrmKhachHang()
        {
            InitializeComponent();
            SieuThiContextDB db = new SieuThiContextDB();
            List<KhachHang> listkhachHangs = db.KhachHangs.ToList();
            BindGrid(listkhachHangs);

            txtMKH.Enabled = false;
            txtTKH.Enabled = false;
            txtSoDienThoai.Enabled = false;
            txtDiaChi.Enabled = false;
        }

        private void loadDgv()
        {
            SieuThiContextDB db = new SieuThiContextDB();

            List<KhachHang> listkhachHangs = db.KhachHangs.ToList();
            BindGrid(listkhachHangs);
            txtMKH.Enabled = false;
            txtTKH.Enabled = false;
            txtSoDienThoai.Enabled = false;
            txtDiaChi.Enabled = false;
        }
        private void loadForm()
        {
            txtTKH.Clear();
            txtMKH.Clear();
            txtDiaChi.Clear();
            txtSoDienThoai.Clear();
        }
        private void BindGrid(List<KhachHang> listkhachHangs)
        {
            dgvKhachHang.Rows.Clear();
            foreach (var item in listkhachHangs)
            {
                int index = dgvKhachHang.Rows.Add();
                dgvKhachHang.Rows[index].Cells[0].Value = item.maKhachHang;
                dgvKhachHang.Rows[index].Cells[1].Value = item.tenKhachHang;
                dgvKhachHang.Rows[index].Cells[2].Value = item.diaChi;
                dgvKhachHang.Rows[index].Cells[3].Value = item.soDienThoai;
            }
        }

        private void FrmKhachHang_Load(object sender, EventArgs e)
        {
            try
            {
                SieuThiContextDB db = new SieuThiContextDB();
                List<KhachHang> listkhachHangs = db.KhachHangs.ToList();
                BindGrid(listkhachHangs);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            txtMKH.Enabled = true;
            txtTKH.Enabled = true;
            txtSoDienThoai.Enabled = true;
            txtDiaChi.Enabled = true;
            txtMKH.Focus();

            try
            {
                if (txtMKH.Text == "" || txtTKH.Text == "" || txtDiaChi.Text == "" || txtSoDienThoai.Text == "")
                    throw new Exception("Mời bạn nhập  thông tin khách hàng");
                int dienthoaiLength = txtSoDienThoai.TextLength;
                txtSoDienThoai.Focus();
                if (dienthoaiLength < 10)
                    throw new Exception("Số điện thoại phải có 10 số trở lên");

                SieuThiContextDB db = new SieuThiContextDB();
                if (CheckMaKH(txtMKH.Text) == -1)
                {

                    KhachHang newKhachHang = new KhachHang();
                    newKhachHang.maKhachHang = txtMKH.Text;
                    newKhachHang.tenKhachHang = txtTKH.Text;
                    newKhachHang.soDienThoai = txtSoDienThoai.Text;
                    newKhachHang.diaChi = txtDiaChi.Text;

                    db.KhachHangs.AddOrUpdate(newKhachHang);
                    db.SaveChanges();
                    loadDgv();
                    loadForm();
                    MessageBox.Show("Thêm mới thông tin khách hàng thành công!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Thêm mới dữ liệu thất bại , mã khách hàng đã tồn tại!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private int CheckMaKH(string text)
        {
            int length = dgvKhachHang.Rows.Count;
            for (int i = 0; i < length; i++)
            {
                if (dgvKhachHang.Rows[i].Cells[0].Value != null)
                    if (dgvKhachHang.Rows[i].Cells[0].Value.ToString() == text)
                        return i;
            }
            return -1;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            txtMKH.Enabled = true;
            txtMKH.Focus();
            SieuThiContextDB db = new SieuThiContextDB();
            try
            {
                if (txtMKH.Text == "")
                    throw new Exception("Vui lòng nhập mã khách hàng cần xóa");
                {
                    KhachHang dbDelete = db.KhachHangs.FirstOrDefault(p => p.maKhachHang == txtMKH.Text);
                    if (dbDelete != null)
                    {
                        DialogResult dt = MessageBox.Show("Bạn có chắc muốn xóa thông tin khách hàng này", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dt == DialogResult.Yes)
                        {
                            db.KhachHangs.Remove(dbDelete);
                            db.SaveChanges();

                            loadDgv();
                            loadForm();

                            MessageBox.Show("Đã xóa thông tin khách hàng thành công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy thông tin khách hàng cần xóa", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            txtMKH.Enabled = true;
            txtTKH.Enabled = true;
            txtDiaChi.Enabled = true;
            txtSoDienThoai.Enabled = true;
            txtMKH.Focus();
            SieuThiContextDB db = new SieuThiContextDB();
            try
            {
                if (txtMKH.Text == "")
                    throw new Exception("Vui lòng nhập mã khách hàng cần sửa");
                {
                    KhachHang dbUpdate = db.KhachHangs.FirstOrDefault(p => p.maKhachHang == txtMKH.Text);
                    if (dbUpdate != null)
                    {
                        dbUpdate.maKhachHang = txtMKH.Text;
                        dbUpdate.tenKhachHang = txtTKH.Text;
                        dbUpdate.diaChi = txtDiaChi.Text;
                        dbUpdate.soDienThoai = txtSoDienThoai.Text;

                        db.KhachHangs.AddOrUpdate(dbUpdate);
                        db.SaveChanges();

                        loadDgv();
                        loadForm();
                        MessageBox.Show("Sửa thông tin khách hàng thành công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy thông tin khách hàng cần sửa", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgvKhachHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            SieuThiContextDB db = new SieuThiContextDB();
            List<KhachHang> listkhachHangs = db.KhachHangs.ToList();
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dgvKhachHang.Rows[e.RowIndex];

                txtMKH.Text = row.Cells[0].Value.ToString();
                txtTKH.Text = row.Cells[1].Value.ToString();
                txtDiaChi.Text = row.Cells[2].Value.ToString();
                txtSoDienThoai.Text = row.Cells[3].Value.ToString();
            }
        }
    }
}
