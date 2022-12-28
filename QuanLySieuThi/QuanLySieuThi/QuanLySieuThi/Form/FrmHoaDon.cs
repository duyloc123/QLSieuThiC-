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
    public partial class FrmHoaDon : Form
    {
        SieuThiContextDB db = new SieuThiContextDB();
        public FrmHoaDon()
        {
            InitializeComponent();
        }

        private void FrmHoaDon_Load(object sender, EventArgs e)
        {
            List<HangHoa> listhangHoas = db.HangHoas.ToList();
            List<NhanVien> listnhanViens = db.NhanViens.ToList();
            List<KhachHang> listkhachHangs = db.KhachHangs.ToList();
            List<HoaDon> listhoaDons = db.HoaDons.ToList();
            List<ChiTietHoaDon> listchiTietHoaDons = db.ChiTietHoaDons.ToList();
            FillHangHoa(listhangHoas);
            FillNhanVien(listnhanViens);
            FillKhachHang(listkhachHangs);
            BinGrid(listchiTietHoaDons);

            txtMHD.Enabled = false;
            cbbNVBH.SelectedValue = 0;
            cbbNVBH.Enabled = false;
            txtTenNV.Enabled = false;
            cbbMaHang.SelectedValue = 0;
            cbbMaHang.Enabled = false;
            txtTenHang.Enabled = false;
            txtGiaBan.Enabled = false;
            cbbTKH.SelectedValue = 0;
            txtAnh.Enabled = false;
            cbbTKH.Enabled = false;
            txtSL.Enabled = false;
            txtthanhTien.Enabled = false;

        }

        private void FillHangHoa(List<HangHoa> listhangHoas)
        {
            this.cbbMaHang.DataSource = listhangHoas;
            this.cbbMaHang.DisplayMember = "maHang";
            this.cbbMaHang.ValueMember = "tenHang";
        }
        private void FillNhanVien(List<NhanVien> listnhanViens)
        {
            this.cbbNVBH.DataSource = listnhanViens;
            this.cbbNVBH.DisplayMember = "maNV";
            this.cbbNVBH.ValueMember = "tenNV";
        }
        private void FillKhachHang(List<KhachHang> listkhachHangs)
        {
            this.cbbTKH.DataSource = listkhachHangs;
            this.cbbTKH.DisplayMember = "maKhachHang";
            this.cbbTKH.ValueMember = "tenKhachHang";
        }

        private void BinGrid(List<ChiTietHoaDon> listchiTietHoaDons)
        {
            dgvHoaDon.Rows.Clear();
            foreach (var item in listchiTietHoaDons)
            {
                int index = dgvHoaDon.Rows.Add();
                dgvHoaDon.Rows[index].Cells[0].Value = item.maHoaDon;
                dgvHoaDon.Rows[index].Cells[1].Value = item.HoaDon.NhanVien.tenNV;
                dgvHoaDon.Rows[index].Cells[2].Value = item.HoaDon.KhachHang.tenKhachHang;
                dgvHoaDon.Rows[index].Cells[3].Value = item.HoaDon.ngayBan;
                dgvHoaDon.Rows[index].Cells[4].Value = item.HangHoa.tenHang;
                dgvHoaDon.Rows[index].Cells[5].Value = item.soLuong;
                dgvHoaDon.Rows[index].Cells[6].Value = item.thanhTien;
                dgvHoaDon.Rows[index].Cells[7].Value = item.HangHoa.Anh;
            }
        }

        private void loadDgv()
        {
            List<HangHoa> listhangHoas = db.HangHoas.ToList();
            List<NhanVien> listnhanViens = db.NhanViens.ToList();
            List<KhachHang> listkhachHangs = db.KhachHangs.ToList();
            List<HoaDon> listhoaDons = db.HoaDons.ToList();
            List<ChiTietHoaDon> listchiTietHoaDons = db.ChiTietHoaDons.ToList();
            FillHangHoa(listhangHoas);
            FillNhanVien(listnhanViens);
            FillKhachHang(listkhachHangs);
            BinGrid(listchiTietHoaDons);
            txtMHD.Enabled = false;
            cbbNVBH.SelectedValue = 0;
            cbbNVBH.Enabled = false;
            txtTenNV.Enabled = false;
            cbbMaHang.SelectedValue = 0;
            cbbMaHang.Enabled = false;
            txtTenHang.Enabled = false;
            txtGiaBan.Enabled = false;
            txtAnh.Enabled = false;
            cbbTKH.SelectedValue = 0;
            cbbTKH.Enabled = false;
            txtSL.Enabled = false;
            txtthanhTien.Enabled = false;
        }
        private void loadForm()
        {
            txtMHD.Clear();
            txtSL.Clear();
            txtTenHang.Clear();
            txtthanhTien.Clear();
            txtGiaBan.Clear();
        }

        private void cbbMaHang_SelectedIndexChanged(object sender, EventArgs e)
        {
            SieuThiContextDB db = new SieuThiContextDB();
            HangHoa hangHoa = cbbMaHang.SelectedItem as HangHoa;
            if (hangHoa != null)
            {
                String ho = hangHoa.maHang;
                var table = db.HangHoas.Where(p => p.maHang == ho);
                foreach (var item in table)
                {
                    txtTenHang.Text = item.tenHang.ToString();
                    txtGiaBan.Text = item.giaTien.ToString();
                    txtAnh.Text = item.Anh.ToString();
                    picAnh.Image = Image.FromFile(txtAnh.Text);
                }
            }
        }
        private int CheckMaHD(string text)
        {
            int length = dgvHoaDon.Rows.Count;
            for (int i = 0; i < length; i++)
            {
                if (dgvHoaDon.Rows[i].Cells[0].Value != null)
                    if (dgvHoaDon.Rows[i].Cells[0].Value.ToString() == text)
                        return i;
            }
            return -1;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            txtMHD.Enabled = true;
            cbbNVBH.Enabled = true;
            txtTenNV.Enabled = true;
            cbbMaHang.Enabled = true;
            txtTenHang.Enabled = true;
            cbbTKH.Enabled = true;
            txtSL.Enabled = true;
            try
            {
                if (txtMHD.Text =="" || cbbNVBH.Text == "" || txtTenNV.Text == "" || cbbMaHang.Text == "" || txtTenHang.Text == "" || cbbTKH.Text == "" || txtSL.Text == "") 
                    throw new Exception("Mời bạn nhập thông tin hóa đơn");
                SieuThiContextDB db = new SieuThiContextDB();
                if (CheckMaHD(txtMHD.Text) == -1)
                {

                    HoaDon newHoaDon = new HoaDon();
                    newHoaDon.maHoaDon = txtMHD.Text;
                    newHoaDon.maKhachHang = cbbTKH.Text;
                    newHoaDon.maNV = cbbNVBH.Text;
                    newHoaDon.ngayBan = Convert.ToDateTime(dtNB.Text);
                    newHoaDon.tongTien = Convert.ToDouble(txtGiaBan.Text);
                    ChiTietHoaDon newchiTietHoaDon = new ChiTietHoaDon();
                    newchiTietHoaDon.maHoaDon = txtMHD.Text;
                    newchiTietHoaDon.maHang = cbbMaHang.Text;
                    newchiTietHoaDon.soLuong = Convert.ToDouble(txtSL.Text);
                    newchiTietHoaDon.thanhTien = Convert.ToDouble(txtthanhTien.Text);
                    db.HoaDons.AddOrUpdate(newHoaDon);
                    db.ChiTietHoaDons.AddOrUpdate(newchiTietHoaDon);
                    db.SaveChanges();
                    loadDgv();
                    loadForm();

                    MessageBox.Show("Thêm mới hóa đơn thành công!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Thêm mới dữ liệu thất bại , mã hóa đơn đã tồn tại!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cbbNVBH_SelectedIndexChanged(object sender, EventArgs e)
        {
            SieuThiContextDB db = new SieuThiContextDB();
            NhanVien nhanVien = cbbNVBH.SelectedItem as NhanVien;
            if (nhanVien != null)
            {
                String nv = nhanVien.maNV;
                var table = db.NhanViens.Where(p => p.maNV == nv);
                foreach (var item in table)
                {
                    txtTenNV.Text = item.tenNV.ToString();
                }
            }
        }

        private void cbbTKH_SelectedIndexChanged(object sender, EventArgs e)
        {
            SieuThiContextDB db = new SieuThiContextDB();
            KhachHang khachHang = cbbTKH.SelectedItem as KhachHang;
            if (khachHang != null)
            {
                String kh = khachHang.maKhachHang;
                var table = db.KhachHangs.Where(p => p.maKhachHang == kh);
                foreach (var item in table)
                {
                    txtTKH.Text = item.tenKhachHang.ToString();
                }
            }
        }
        private void txtSL_TextChanged(object sender, EventArgs e)
        {
            SieuThiContextDB db = new SieuThiContextDB();
            List<ChiTietHoaDon> listchiTietHoaDons = db.ChiTietHoaDons.ToList();
            double tt, sl, gb;
            if (txtSL.Text == "")
            {
                sl = 0;
            }
            else
            {
                sl = Convert.ToDouble(txtSL.Text);
            }
            if (txtGiaBan.Text == "")
            {
                gb = 0;
            }
            else
            {
                gb = Convert.ToDouble(txtGiaBan.Text);
            }
            tt = sl * gb;
            txtthanhTien.Text = tt.ToString();
        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            FrmReportHoaDon frm = new FrmReportHoaDon();
            frm.ShowDialog();
        }

        private void dgvHoaDon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            SieuThiContextDB db = new SieuThiContextDB();
            List<HoaDon> listhoaDons = db.HoaDons.ToList();
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dgvHoaDon.Rows[e.RowIndex];

                txtMHD.Text = row.Cells[0].Value.ToString();
                txtTenNV.Text = row.Cells[1].Value.ToString();
                txtTKH.Text = row.Cells[2].Value.ToString();
                dtNB.Text = row.Cells[3].Value.ToString();
                txtTKH.Text = row.Cells[4].Value.ToString();
                txtSL.Text = row.Cells[5].Value.ToString();
                txtthanhTien.Text = row.Cells[6].Value.ToString();
                txtAnh.Text = row.Cells[7].Value.ToString();
                picAnh.Image = Image.FromFile(txtAnh.Text);
            }
        }
    }
}
