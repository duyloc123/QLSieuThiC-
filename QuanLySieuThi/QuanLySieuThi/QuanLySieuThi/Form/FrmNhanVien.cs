using Microsoft.SqlServer.Server;
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
    public partial class FrmNhanVien : Form
    {
        public FrmNhanVien()
        {
            InitializeComponent();
        }

        private void FrmNhanVien_Load(object sender, EventArgs e)
        {
            try
            {
                SieuThiContextDB db = new SieuThiContextDB();
                List<NhanVien> listnhanViens = db.NhanViens.ToList();
                List<LoaiNV> listloaiNVs = db.LoaiNVs.ToList();
                FillLoaiNhanVien(listloaiNVs);
                BindGrid(listnhanViens);
                txtMNV.Enabled = false;
                txtTenNV.Enabled = false;
                txtDiaChi.Enabled = false;
                txtSoDienThoai.Enabled = false;
                txtLuong.Enabled = false;
                cbbChucVu.Enabled = false;
                cbbChucVu.SelectedValue = 0;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FillLoaiNhanVien(List<LoaiNV> listloaiNVs)
        {
            this.cbbChucVu.DataSource = listloaiNVs;
            this.cbbChucVu.DisplayMember = "chucVu";
            this.cbbChucVu.ValueMember = "maLoai";
        }

        private void BindGrid(List<NhanVien> listnhanViens)
        {
            dgvNhanVien.Rows.Clear();
            foreach(var item in listnhanViens)
            {
                int index = dgvNhanVien.Rows.Add();
                dgvNhanVien.Rows[index].Cells[0].Value = item.maNV;
                dgvNhanVien.Rows[index].Cells[1].Value = item.tenNV;
                dgvNhanVien.Rows[index].Cells[2].Value = item.diaChi;
                dgvNhanVien.Rows[index].Cells[3].Value = item.soDienThoai;
                dgvNhanVien.Rows[index].Cells[4].Value = item.LoaiNV.chucVu;
                dgvNhanVien.Rows[index].Cells[5].Value = item.luongCoBan;
            }
        }

        private void loadDgv()
        {
            SieuThiContextDB db = new SieuThiContextDB();

            List<NhanVien> listnhanViens = db.NhanViens.ToList();
            List<LoaiNV> listloaiNVs = db.LoaiNVs.ToList();
            FillLoaiNhanVien(listloaiNVs);
            BindGrid(listnhanViens);
            txtMNV.Enabled = false;
            txtTenNV.Enabled = false;
            txtDiaChi.Enabled = false;
            txtSoDienThoai.Enabled = false;
            txtLuong.Enabled = false;
            cbbChucVu.Enabled = false;
            cbbChucVu.SelectedValue = 0;
        }
        private void loadForm()
        {
            txtMNV.Clear();
            txtTenNV.Clear();
            txtDiaChi.Clear();
            txtSoDienThoai.Clear();
            txtLuong.Clear();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            txtMNV.Enabled = true;
            txtTenNV.Enabled = true;
            txtDiaChi.Enabled = true;
            txtSoDienThoai.Enabled = true;
            txtLuong.Enabled = true;
            cbbChucVu.Enabled = true;
            txtMNV.Focus();
            try
            {
                if (txtMNV.Text == "" || txtTenNV.Text == "" || txtDiaChi.Text == "" || txtSoDienThoai.Text == "" || txtLuong.Text == "")
                    throw new Exception("Mời bạn nhập  thông tin nhân viên");
                int dienthoaiLength = txtSoDienThoai.TextLength;
                txtSoDienThoai.Focus();
                if (dienthoaiLength < 10)
                    throw new Exception("Số điện thoại phải có 10 số trở lên");

                SieuThiContextDB db = new SieuThiContextDB();
                if (CheckMaNV(txtMNV.Text) == -1)
                {

                    NhanVien newNhanVien = new NhanVien();
                    newNhanVien.maNV = txtMNV.Text;
                    newNhanVien.tenNV = txtTenNV.Text;
                    newNhanVien.diaChi = txtDiaChi.Text;
                    newNhanVien.soDienThoai = txtSoDienThoai.Text;
                    newNhanVien.luongCoBan = Convert.ToDouble(txtLuong.Text);
                    newNhanVien.maLoai = cbbChucVu.SelectedValue.ToString();

                    db.NhanViens.AddOrUpdate(newNhanVien);
                    db.SaveChanges();
                    loadDgv();
                    loadForm();
                    MessageBox.Show("Thêm mới thông tin nhân viên thành công!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Thêm mới dữ liệu thất bại , mã nhân viên đã tồn tại!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private int CheckMaNV(string text)
        {
            int length = dgvNhanVien.Rows.Count;
            for (int i = 0; i < length; i++)
            {
                if (dgvNhanVien.Rows[i].Cells[0].Value != null)
                    if (dgvNhanVien.Rows[i].Cells[0].Value.ToString() == text)
                        return i;
            }
            return -1;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            txtMNV.Enabled = true;
            txtTenNV.Enabled = false;
            txtDiaChi.Enabled = false;
            txtSoDienThoai.Enabled = false;
            txtLuong.Enabled = false;
            cbbChucVu.Enabled = false;
            txtMNV.Focus();
            SieuThiContextDB db = new SieuThiContextDB();
            try
            {
                if (txtMNV.Text == "")
                    throw new Exception("Vui lòng nhập mã nhân viên cần xóa");
                {
                    NhanVien dbDelete = db.NhanViens.FirstOrDefault(p => p.maNV == txtMNV.Text);
                    if (dbDelete != null)
                    {
                        DialogResult dt = MessageBox.Show("Bạn có chắc muốn xóa thông tin nhân viên này", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dt == DialogResult.Yes)
                        {
                            db.NhanViens.Remove(dbDelete);
                            db.SaveChanges();

                            loadDgv();
                            loadForm();

                            MessageBox.Show("Đã xóa thông tin nhân viên thành công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy thông tin nhân viên", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            txtMNV.Enabled = true;
            txtTenNV.Enabled = true;
            txtDiaChi.Enabled = true;
            txtSoDienThoai.Enabled = true;
            txtLuong.Enabled = true;
            cbbChucVu.Enabled = true;
            txtMNV.Focus();
            SieuThiContextDB db = new SieuThiContextDB();
            try
            {
                if (txtMNV.Text == "")
                    throw new Exception("Vui lòng nhập mã nhân viên cần sửa");
                {
                    NhanVien dbUpdate = db.NhanViens.FirstOrDefault(p => p.maNV == txtMNV.Text);
                    if (dbUpdate != null)
                    {
                        dbUpdate.maNV = txtMNV.Text;
                        dbUpdate.tenNV = txtTenNV.Text;
                        dbUpdate.diaChi = txtDiaChi.Text;
                        dbUpdate.soDienThoai = txtSoDienThoai.Text;
                        dbUpdate.luongCoBan = Convert.ToDouble(txtLuong.Text);
                        dbUpdate.maLoai = cbbChucVu.SelectedValue.ToString();

                        db.NhanViens.AddOrUpdate(dbUpdate);
                        db.SaveChanges();

                        loadDgv();
                        loadForm();
                        MessageBox.Show("Sửa thông tin nhân viên thành công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy thông tin nhân viên cần sửa", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgvNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMNV.Enabled = true;
            txtTenNV.Enabled = true;
            txtDiaChi.Enabled = true;
            txtSoDienThoai.Enabled = true;
            txtLuong.Enabled = true;
            cbbChucVu.Enabled = true;

            SieuThiContextDB db = new SieuThiContextDB();
            List<NhanVien> listnhanViens = db.NhanViens.ToList();
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dgvNhanVien.Rows[e.RowIndex];

                txtMNV.Text = row.Cells[0].Value.ToString();
                txtTenNV.Text = row.Cells[1].Value.ToString();
                txtDiaChi.Text = row.Cells[2].Value.ToString();
                txtSoDienThoai.Text = row.Cells[3].Value.ToString();
                cbbChucVu.Text = row.Cells[4].Value.ToString();
                txtLuong.Text = row.Cells[5].Value.ToString();
            }
        }
    }
}
