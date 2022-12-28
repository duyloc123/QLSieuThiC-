using QuanLyCuaHangDienThoai.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Migrations;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyCuaHangDienThoai
{
    public partial class FrmXuatHang : Form
    {
        SieuThiContextDB db = new SieuThiContextDB();
        public FrmXuatHang()
        {
            InitializeComponent();
        }
        private void FrmXuatHang_Load(object sender, EventArgs e)
        {
            SieuThiContextDB db = new SieuThiContextDB();
            List<NhanVien> listnhanViens = db.NhanViens.ToList();
            List<HangHoa> listhangHoas = db.HangHoas.ToList();
            List<PhieuXuatHang> listphieuXuatHangs = db.PhieuXuatHangs.ToList();
            List<ChiTietPhieuXuatHang> listchiTietPhieuXuatHangs = db.ChiTietPhieuXuatHangs.ToList();
            FillNhanVien(listnhanViens);
            FillHangHoa(listhangHoas);
            BinGrid(listchiTietPhieuXuatHangs);

            txtMPXH.Enabled = false;
            txtsoLuong.Enabled = false;
            cbbHangHoa.Enabled = false;
            cbbNVKho.Enabled = false;
            cbbNVKho.SelectedValue = 0;
            dtTGX.Enabled = false;
            txtTenHang.Enabled = false;
        }

        private void FillNhanVien(List<NhanVien> listnhanViens)
        {
            this.cbbNVKho.DataSource = listnhanViens;
            this.cbbNVKho.DisplayMember = "tenNV";
            this.cbbNVKho.ValueMember = "maNV";
        }
        private void FillHangHoa(List<HangHoa> listhangHoas)
        {
            this.cbbHangHoa.DataSource = listhangHoas;
            this.cbbHangHoa.DisplayMember = "maHang";
            this.cbbHangHoa.ValueMember = "tenHang";
        }
        private void BinGrid(List<ChiTietPhieuXuatHang> listchiTietPhieuXuatHangs)
        {
            dgvXuatHang.Rows.Clear();
            foreach (var item in listchiTietPhieuXuatHangs)
            {
                int index = dgvXuatHang.Rows.Add();
                dgvXuatHang.Rows[index].Cells[0].Value = item.PhieuXuatHang.maPhieuXuat;
                dgvXuatHang.Rows[index].Cells[1].Value = item.PhieuXuatHang.NhanVien.tenNV;
                dgvXuatHang.Rows[index].Cells[2].Value = item.HangHoa.tenHang;
                dgvXuatHang.Rows[index].Cells[3].Value = item.PhieuXuatHang.ngayXuat;
                dgvXuatHang.Rows[index].Cells[4].Value = item.soLuongXuat;
            }
        }
        private void loadDgv()
        {
            SieuThiContextDB db = new SieuThiContextDB();
            List<NhanVien> listnhanViens = db.NhanViens.ToList();
            List<HangHoa> listhangHoas = db.HangHoas.ToList();
            List<PhieuXuatHang> listphieuXuatHangs = db.PhieuXuatHangs.ToList();
            List<ChiTietPhieuXuatHang> listchiTietPhieuXuatHangs = db.ChiTietPhieuXuatHangs.ToList();
            FillNhanVien(listnhanViens);
            FillHangHoa(listhangHoas);
            BinGrid(listchiTietPhieuXuatHangs);
        }
        private void loadForm()
        {
            txtMPXH.Clear();
            txtsoLuong.Clear();
            cbbHangHoa.SelectedValue = 0;
            cbbNVKho.SelectedValue = 0;
        }
        private int CheckMaPhieuXuat(string text)
        {
            int length = dgvXuatHang.Rows.Count;
            for (int i = 0; i < length; i++)
            {
                if (dgvXuatHang.Rows[i].Cells[0].Value != null)
                    if (dgvXuatHang.Rows[i].Cells[0].Value.ToString() == text)
                        return i;
            }
            return -1;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            txtMPXH.Enabled = true;
            txtsoLuong.Enabled = true;
            cbbHangHoa.Enabled = true;
            cbbNVKho.Enabled = true;
            dtTGX.Enabled = true;

            try
            {
                if (txtMPXH.Text == "" || txtsoLuong.Text == "" || dtTGX.Text == "" || cbbHangHoa.Text == "" || cbbNVKho.Text == "")
                    throw new Exception("Mời bạn nhập thông tin phiếu xuất hàng");
                SieuThiContextDB db = new SieuThiContextDB();
                if (CheckMaPhieuXuat(txtMPXH.Text) == -1)
                {
                    HangHoa hanghoa = db.HangHoas.FirstOrDefault(p => p.maHang == cbbHangHoa.Text);
                        if(hanghoa.soLuong < Convert.ToDouble(txtsoLuong.Text))
                        {
                            throw new Exception("Số lượng hàng hóa trong kho không đủ!");
                        }
                    PhieuXuatHang newPhieuXuatHang = new PhieuXuatHang();
                    newPhieuXuatHang.maPhieuXuat = txtMPXH.Text;
                    newPhieuXuatHang.maNV = cbbNVKho.SelectedValue.ToString();
                    newPhieuXuatHang.ngayXuat = Convert.ToDateTime(dtTGX.Text);
                    ChiTietPhieuXuatHang newchiTietPhieuXuatHang = new ChiTietPhieuXuatHang();
                    newchiTietPhieuXuatHang.maPhieuXuat = txtMPXH.Text;
                    newchiTietPhieuXuatHang.maHang = cbbHangHoa.Text;
                    newchiTietPhieuXuatHang.soLuongXuat = Convert.ToDouble(txtsoLuong.Text);
                    db.PhieuXuatHangs.AddOrUpdate(newPhieuXuatHang);
                    db.ChiTietPhieuXuatHangs.AddOrUpdate(newchiTietPhieuXuatHang);
                    db.SaveChanges();
                    loadDgv();

                    HangHoa timkiem = db.HangHoas.FirstOrDefault(p => p.maHang == cbbHangHoa.Text);
                    double sl;
                    sl = timkiem.soLuong - Convert.ToDouble(txtsoLuong.Text);
                    timkiem.soLuong = sl;
                    db.HangHoas.AddOrUpdate(timkiem);
                    db.SaveChanges();
                    loadDgv();
                    loadForm();

                    MessageBox.Show("Thêm mới thông tin phiếu nhập hàng thành công!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Thêm mới dữ liệu thất bại , mã phiếu nhập hàng đã tồn tại!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            txtMPXH.Enabled = true;
            txtsoLuong.Enabled = true;
            cbbHangHoa.Enabled = true;
            txtMPXH.Focus();

            SieuThiContextDB db = new SieuThiContextDB();
            try
            {
                if (txtMPXH.Text == "" || txtsoLuong.Text == "" || cbbHangHoa.Text == "")
                    throw new Exception("Vui lòng nhập mã phiếu xuất hàng , mã hàng và số lượng cần xóa");
                {
                    ChiTietPhieuXuatHang dbDelete = db.ChiTietPhieuXuatHangs.FirstOrDefault(p => p.maPhieuXuat == txtMPXH.Text);
                    PhieuXuatHang dbDelete2 = db.PhieuXuatHangs.FirstOrDefault(p => p.maPhieuXuat == txtMPXH.Text);
                    if (dbDelete != null && dbDelete2 != null)
                    {
                        DialogResult dt = MessageBox.Show("Bạn có chắc muốn xóa phiếu xuất hàng này", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dt == DialogResult.Yes)
                        {
                            db.ChiTietPhieuXuatHangs.Remove(dbDelete);
                            db.PhieuXuatHangs.Remove(dbDelete2);
                            db.SaveChanges();

                            HangHoa timkiem = db.HangHoas.FirstOrDefault(p => p.maHang == cbbHangHoa.Text);
                            double sl;
                            sl = timkiem.soLuong + Convert.ToDouble(txtsoLuong.Text);
                            timkiem.soLuong = sl;
                            db.HangHoas.AddOrUpdate(timkiem);
                            db.SaveChanges();
                            loadDgv();
                            loadForm();

                            loadDgv();
                            loadForm();

                            MessageBox.Show("Đã xóa thông tin phiếu xuất hàng thành công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy thông tin phiếu nhập hàng cần xóa", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            txtMPXH.Enabled = true;
            txtMPXH.Focus();

            SieuThiContextDB db = new SieuThiContextDB();
            try
            {
                if (txtMPXH.Text == "")
                    throw new Exception("Vui lòng nhập mã phiếu cần sửa");
                {
                    ChiTietPhieuXuatHang dbUpdate = db.ChiTietPhieuXuatHangs.FirstOrDefault(p => p.maPhieuXuat == txtMPXH.Text);
                    PhieuXuatHang dbUpdate2 = db.PhieuXuatHangs.FirstOrDefault(p => p.maPhieuXuat == txtMPXH.Text);
                    if (dbUpdate != null && dbUpdate2 != null)
                    {
                        dbUpdate2.maPhieuXuat = txtMPXH.Text;
                        dbUpdate2.maNV = cbbNVKho.SelectedValue.ToString();
                        dbUpdate2.ngayXuat = Convert.ToDateTime(dtTGX.Text);
                        dbUpdate.maPhieuXuat = txtMPXH.Text;
                        dbUpdate.maHang = cbbHangHoa.Text;
                        dbUpdate.soLuongXuat = Convert.ToDouble(txtsoLuong.Text);
                        db.PhieuXuatHangs.AddOrUpdate(dbUpdate2);
                        db.ChiTietPhieuXuatHangs.AddOrUpdate(dbUpdate);
                        db.SaveChanges();

                        HangHoa timkiem = db.HangHoas.FirstOrDefault(p => p.maHang == cbbHangHoa.Text);
                        double sl;
                        sl = timkiem.soLuong + Convert.ToDouble(txtsoLuong.Text);
                        timkiem.soLuong = sl;
                        db.HangHoas.AddOrUpdate(timkiem);
                        db.SaveChanges();
                        loadDgv();
                        loadForm();
                        MessageBox.Show("Sửa thông tin phiếu xuất hàng thành công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy thông tin phiếu cần sửa", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgvXuatHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            SieuThiContextDB db = new SieuThiContextDB();
            List<ChiTietPhieuXuatHang> listchiTietPhieuXuatHangs = db.ChiTietPhieuXuatHangs.ToList();
            List<PhieuXuatHang> listphieuXuatHangs = db.PhieuXuatHangs.ToList();
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dgvXuatHang.Rows[e.RowIndex];

                txtMPXH.Text = row.Cells[0].Value.ToString();
                cbbNVKho.Text = row.Cells[1].Value.ToString();
                cbbHangHoa.Text = row.Cells[2].Value.ToString();
                dtTGX.Text = row.Cells[3].Value.ToString();
                txtsoLuong.Text = row.Cells[4].Value.ToString();
            }
        }

        private void cbbHangHoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            SieuThiContextDB db = new SieuThiContextDB();
            HangHoa hangHoa = cbbHangHoa.SelectedItem as HangHoa;
            if (hangHoa != null)
            {
                String ho = hangHoa.maHang;
                var table = db.HangHoas.Where(p => p.maHang == ho);
                foreach (var item in table)
                {
                    txtTenHang.Text = item.tenHang.ToString();
                }
            }
        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            FrmReportPhieuXuat frm = new FrmReportPhieuXuat();
            frm.ShowDialog();
        }
    }
}
