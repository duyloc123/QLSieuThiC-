using QuanLyCuaHangDienThoai.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Migrations;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyCuaHangDienThoai
{
    public partial class FrmHangHoa : Form
    {

        public FrmHangHoa()
        {
            InitializeComponent();
        }

        private void btnAnh_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlgOpen = new OpenFileDialog();
            dlgOpen.Filter = "Bitmap|*.bmp|JPEG|*.jpg|GIF|*.gif|PNG|*.png";
            if (dlgOpen.ShowDialog() == DialogResult.OK)
            {
                picAnh.Image = Image.FromFile(dlgOpen.FileName);
                txtAnh.Text = dlgOpen.FileName;
            }
        }

        private void FrmHangHoa_Load_1(object sender, EventArgs e)
        {
            SieuThiContextDB db = new SieuThiContextDB();
            List<LoaiHangHoa> listloaiHangHoas = db.LoaiHangHoas.ToList();
            List<HangHoa> listhangHoas = db.HangHoas.ToList();
            List<NhaCungCap> listnhaCungCaps = db.NhaCungCaps.ToList();
            FillLoaiHangHoa(listloaiHangHoas);
            FillNhaCungCap(listnhaCungCaps);
            BinGrid(listhangHoas);
            txtMaHang.Enabled = false;
            txtTenHang.Enabled = false;
            cbbLoaiHang.Enabled = false;
            cbbLoaiHang.SelectedValue = 0;
            cbbNCC.Enabled = false;
            cbbNCC.SelectedValue = 0;
            txtSoLuong.Enabled = false;
            txtGiaTien.Enabled = false;
            txtAnh.Enabled = false;
        }

        private void FillNhaCungCap(List<NhaCungCap> listnhaCungCaps)
        {
            this.cbbNCC.DataSource = listnhaCungCaps;
            this.cbbNCC.DisplayMember = "tenNCC";
            this.cbbNCC.ValueMember = "maNCC";
        }
        private void FillLoaiHangHoa(List<LoaiHangHoa> listloaiHangHoas)
        {
            this.cbbLoaiHang.DataSource = listloaiHangHoas;
            this.cbbLoaiHang.DisplayMember = "tenLoai";
            this.cbbLoaiHang.ValueMember = "maLoai";
        }

        private void BinGrid(List<HangHoa> listhangHoas)
        {
            dgvHangHoa.Rows.Clear();
            foreach(var item in listhangHoas)
            {
                int index = dgvHangHoa.Rows.Add();
                dgvHangHoa.Rows[index].Cells[0].Value = item.maHang;
                dgvHangHoa.Rows[index].Cells[1].Value = item.tenHang;
                dgvHangHoa.Rows[index].Cells[3].Value = item.LoaiHangHoa.tenLoai;
                dgvHangHoa.Rows[index].Cells[2].Value = item.NhaCungCap.tenNCC;
                dgvHangHoa.Rows[index].Cells[4].Value = item.ngaySanXuat;
                dgvHangHoa.Rows[index].Cells[5].Value = item.ngayHetHan;
                dgvHangHoa.Rows[index].Cells[6].Value = item.soLuong;
                dgvHangHoa.Rows[index].Cells[7].Value = item.giaTien;
                dgvHangHoa.Rows[index].Cells[8].Value = item.Anh;
            }
        }
        private void loadDgv()
        {
            SieuThiContextDB db = new SieuThiContextDB();

            List<HangHoa> listhangHoas = db.HangHoas.ToList();
            List<LoaiHangHoa> listloaiHangs = db.LoaiHangHoas.ToList();
            List<NhaCungCap> listnhaCungCaps = db.NhaCungCaps.ToList();
            FillLoaiHangHoa(listloaiHangs);
            FillNhaCungCap(listnhaCungCaps);
            BinGrid(listhangHoas);
            txtMaHang.Enabled = false;
            txtTenHang.Enabled = false;
            cbbLoaiHang.Enabled = false;
            cbbLoaiHang.SelectedValue = 0;
            cbbNCC.Enabled = false;
            cbbNCC.SelectedValue = 0;
            txtSoLuong.Enabled = false;
            txtGiaTien.Enabled = false;
            txtAnh.Enabled = false;
        }
        private void loadForm()
        {
            txtMaHang.Clear();
            txtTenHang.Clear();
            txtGiaTien.Clear();
            txtAnh.Clear();
            txtSoLuong.Clear();
        }

        private int CheckMaHang(string text)
        {
            int length = dgvHangHoa.Rows.Count;
            for (int i = 0; i < length; i++)
            {
                if (dgvHangHoa.Rows[i].Cells[0].Value != null)
                    if (dgvHangHoa.Rows[i].Cells[0].Value.ToString() == text)
                        return i;
            }
            return -1;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            txtMaHang.Enabled = true;
            txtTenHang.Enabled = true;
            cbbLoaiHang.Enabled = true;
            cbbNCC.Enabled = true;
            txtSoLuong.Enabled = true;
            txtGiaTien.Enabled = true;
            txtAnh.Enabled = true;
            txtMaHang.Focus();

            try
            {
                if (txtMaHang.Text == "" || txtTenHang.Text == "" || txtSoLuong.Text == "" || txtGiaTien.Text == "" || txtAnh.Text == "" )
                    throw new Exception("Mời bạn nhập thông tin hàng hóa");

                SieuThiContextDB db = new SieuThiContextDB();
                if (CheckMaHang(txtMaHang.Text) == -1)
                {

                    HangHoa newHangHoa = new HangHoa();
                    newHangHoa.maHang = txtMaHang.Text;
                    newHangHoa.tenHang = txtTenHang.Text;
                    newHangHoa.maNCC = cbbNCC.SelectedValue.ToString();
                    newHangHoa.maLoai = cbbLoaiHang.SelectedValue.ToString();
                    newHangHoa.ngaySanXuat = dtNSX.Value;
                    newHangHoa.ngayHetHan = dtNHH.Value;
                    newHangHoa.soLuong = Convert.ToInt16(txtSoLuong.Text);
                    newHangHoa.giaTien = Convert.ToDouble(txtGiaTien.Text);
                    newHangHoa.Anh = txtAnh.Text;
                    
                    

                    db.HangHoas.AddOrUpdate(newHangHoa);
                    db.SaveChanges();
                    loadDgv();
                    loadForm();
                    MessageBox.Show("Thêm mới thông tin hàng hóa thành công!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Thêm mới dữ liệu thất bại , mã hàng hóa đã tồn tại!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            txtMaHang.Enabled = true;
            txtMaHang.Focus();
            SieuThiContextDB db = new SieuThiContextDB();
            try
            {
                if (txtMaHang.Text == "")
                    throw new Exception("Vui lòng nhập mã hàng hóa cần xóa");
                {
                    HangHoa dbDelete = db.HangHoas.FirstOrDefault(p => p.maHang == txtMaHang.Text);
                    if (dbDelete != null)
                    {
                        DialogResult dt = MessageBox.Show("Bạn có chắc muốn xóa thông tin hàng hóa này", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dt == DialogResult.Yes)
                        {
                            db.HangHoas.Remove(dbDelete);
                            db.SaveChanges();

                            loadDgv();
                            loadForm();

                            MessageBox.Show("Đã xóa thông tin hàng hóa thành công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy thông tin hàng hóa cần xóa", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            txtMaHang.Enabled = true;
            txtMaHang.Focus();
            txtTenHang.Enabled = true;
            cbbLoaiHang.Enabled = true;
            cbbNCC.Enabled = true;
            txtSoLuong.Enabled = true;
            txtGiaTien.Enabled = true;
            txtAnh.Enabled = true;
            SieuThiContextDB db = new SieuThiContextDB();
            try
            {
                if (txtMaHang.Text == "")
                    throw new Exception("Vui lòng nhập mã hàng hóa cần sửa");
                {
                    HangHoa dbUpdate = db.HangHoas.FirstOrDefault(p => p.maHang == txtMaHang.Text);
                    if (dbUpdate != null)
                    {
                        dbUpdate.maHang = txtMaHang.Text;
                        dbUpdate.maLoai = cbbLoaiHang.SelectedValue.ToString();
                        dbUpdate.maNCC = cbbNCC.SelectedValue.ToString();
                        dbUpdate.ngaySanXuat = Convert.ToDateTime(dtNSX.Text);
                        dbUpdate.ngayHetHan = Convert.ToDateTime(dtNHH.Text);
                        dbUpdate.soLuong = Convert.ToInt16(txtSoLuong.Text);
                        dbUpdate.giaTien = Convert.ToDouble(txtGiaTien.Text);
                        dbUpdate.Anh = txtAnh.Text;

                        db.HangHoas.AddOrUpdate(dbUpdate);
                        db.SaveChanges();

                        loadDgv();
                        loadForm();
                        MessageBox.Show("Sửa thông tin hàng hóa thành công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy thông tin hàng hóa cần sửa", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgvHangHoa_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaHang.Enabled = true;
            txtTenHang.Enabled = true;
            cbbLoaiHang.Enabled = true;
            cbbNCC.Enabled = true;
            txtSoLuong.Enabled = true;
            txtGiaTien.Enabled = true;
            txtAnh.Enabled = true;

            SieuThiContextDB db = new SieuThiContextDB();
            List<HangHoa> listhangHoas = db.HangHoas.ToList();
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dgvHangHoa.Rows[e.RowIndex];

                txtMaHang.Text = row.Cells[0].Value.ToString();
                txtTenHang.Text = row.Cells[1].Value.ToString();
                cbbLoaiHang.Text = row.Cells[3].Value.ToString();
                cbbNCC.Text = row.Cells[2].Value.ToString();
                txtSoLuong.Text = row.Cells[6].Value.ToString();
                txtGiaTien.Text = row.Cells[7].Value.ToString();
                txtAnh.Text = row.Cells[8].Value.ToString();
                picAnh.Image = Image.FromFile(txtAnh.Text);
            }
        }
    }
}
