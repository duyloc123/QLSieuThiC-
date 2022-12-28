using QuanLyCuaHangDienThoai.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Migrations;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyCuaHangDienThoai
{
    public partial class FrmNhapHang : Form
    {

        SieuThiContextDB db = new SieuThiContextDB();
        public FrmNhapHang()
        {
            InitializeComponent();
        }
        private void FrmNhapHang_Load(object sender, EventArgs e)
        {
            SieuThiContextDB db = new SieuThiContextDB();
            List<HangHoa> listhangHoas = db.HangHoas.ToList();
            List<NhanVien> listnhanViens = db.NhanViens.ToList();
            List<NhaCungCap> listnhaCungCaps = db.NhaCungCaps.ToList();
            List<PhieuNhapHang> listphieuNhapHangs = db.PhieuNhapHangs.ToList();
            List<ChiTietPhieuNhapHang> listchiTietPhieuNhapHangs = db.ChiTietPhieuNhapHangs.ToList();
            FillNhaCungCap(listnhaCungCaps);
            FillNhanVien(listnhanViens);
            FillHangHoa(listhangHoas);
            BinGrid(listchiTietPhieuNhapHangs);

            txtMPNH.Enabled = false;
            txtgiaNhap.Enabled = false;
            txtSLN.Enabled = false;
            txtTongTien.Enabled = false;
            cbbHangHoa.Enabled = false;
            cbbHangHoa.SelectedValue = 0;
            cbbNCC.Enabled = false;
            cbbNCC.SelectedValue = 0;
            cbbNVQL.Enabled = false;
            cbbNVQL.SelectedValue = 0;
        }
        private void FillNhaCungCap(List<NhaCungCap> listnhaCungCaps)
        {
            this.cbbNCC.DataSource = listnhaCungCaps;
            this.cbbNCC.DisplayMember = "tenNCC";
            this.cbbNCC.ValueMember = "maNCC";
        }
        private void FillNhanVien(List<NhanVien> listnhanViens)
        {
            this.cbbNVQL.DataSource = listnhanViens;
            this.cbbNVQL.DisplayMember = "tenNV";
            this.cbbNVQL.ValueMember = "maNV";
        }
        private void FillHangHoa(List<HangHoa> listhangHoas)
        {
            this.cbbHangHoa.DataSource = listhangHoas;
            this.cbbHangHoa.DisplayMember = "tenHang";
            this.cbbHangHoa.ValueMember = "maHang";
        }
        private void BinGrid(List<ChiTietPhieuNhapHang> listchiTietPhieuNhapHangs )
        {
            dgvNhapHang.Rows.Clear();
            foreach (var item in listchiTietPhieuNhapHangs)
            {
                int index = dgvNhapHang.Rows.Add();
                dgvNhapHang.Rows[index].Cells[0].Value = item.PhieuNhapHang.maPhieuNhap;
                dgvNhapHang.Rows[index].Cells[1].Value = item.PhieuNhapHang.NhanVien.tenNV;
                dgvNhapHang.Rows[index].Cells[2].Value = item.PhieuNhapHang.NhaCungCap.tenNCC;
                dgvNhapHang.Rows[index].Cells[3].Value = item.HangHoa.tenHang;
                dgvNhapHang.Rows[index].Cells[4].Value = item.PhieuNhapHang.ngayNhap;
                dgvNhapHang.Rows[index].Cells[5].Value = item.giaNhap;
                dgvNhapHang.Rows[index].Cells[6].Value = item.soLuongNhap;
                dgvNhapHang.Rows[index].Cells[7].Value = item.thanhTien;
            }
        }
        private void loadDgv()
        {
            SieuThiContextDB db = new SieuThiContextDB();
            List<HangHoa> listhangHoas = db.HangHoas.ToList();
            List<NhanVien> listnhanViens = db.NhanViens.ToList();
            List<NhaCungCap> listnhaCungCaps = db.NhaCungCaps.ToList();
            List<PhieuNhapHang> listphieuNhapHangs = db.PhieuNhapHangs.ToList();
            List<ChiTietPhieuNhapHang> listchiTietPhieuNhapHangs = db.ChiTietPhieuNhapHangs.ToList();
            FillNhaCungCap(listnhaCungCaps);
            FillNhanVien(listnhanViens);
            BinGrid(listchiTietPhieuNhapHangs);
            txtMPNH.Enabled = false;
            txtgiaNhap.Enabled = false;
            txtSLN.Enabled = false;
            txtTongTien.Enabled = false;
            cbbHangHoa.Enabled = false;
            cbbHangHoa.SelectedValue = 0;
            cbbNCC.Enabled = false;
            cbbNCC.SelectedValue = 0;
            cbbNVQL.Enabled = false;
            cbbNVQL.SelectedValue = 0;
        }
        private void loadForm()
        {
            txtgiaNhap.Clear();
            txtSLN.Clear();
            txtTongTien.Clear();
            txtMPNH.Clear();
        }
        private int CheckMaPhieuNhap(string text)
        {
            int length = dgvNhapHang.Rows.Count;
            for (int i = 0; i < length; i++)
            {
                if (dgvNhapHang.Rows[i].Cells[0].Value != null)
                    if (dgvNhapHang.Rows[i].Cells[0].Value.ToString() == text)
                        return i;
            }
            return -1;
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            txtMPNH.Focus();
            txtMPNH.Enabled = true;
            txtgiaNhap.Enabled = true;
            txtSLN.Enabled = true;
            txtTongTien.Enabled = true;
            cbbHangHoa.Enabled = true;
            cbbNCC.Enabled = true;
            cbbNVQL.Enabled = true;
            try
            {
                if (txtMPNH.Text == "" || txtSLN.Text == "" || txtgiaNhap.Text == "" || txtTongTien.Text == "")
                    throw new Exception("Mời bạn nhập thông tin phiếu nhập hàng");
                 if (CheckMaPhieuNhap(txtMPNH.Text) == -1)
                 {
                    PhieuNhapHang newPhieuNhapHang = new PhieuNhapHang();
                    newPhieuNhapHang.maPhieuNhap = txtMPNH.Text;
                    newPhieuNhapHang.maNV = cbbNVQL.SelectedValue.ToString();
                    newPhieuNhapHang.maNCC = cbbNCC.SelectedValue.ToString();
                    newPhieuNhapHang.ngayNhap = Convert.ToDateTime(dtTGX.Text);
                    newPhieuNhapHang.tongTien = Convert.ToDouble(txtTongTien.Text);
                    ChiTietPhieuNhapHang newchiTietPhieuNhapHang = new ChiTietPhieuNhapHang();
                    newchiTietPhieuNhapHang.maPhieuNhap = txtMPNH.Text;
                    newchiTietPhieuNhapHang.maHang = cbbHangHoa.SelectedValue.ToString();
                    newchiTietPhieuNhapHang.maNCC = cbbNCC.SelectedValue.ToString();
                    newchiTietPhieuNhapHang.soLuongNhap = Convert.ToDouble(txtSLN.Text);
                    newchiTietPhieuNhapHang.thanhTien = Convert.ToDouble(txtTongTien.Text);
                    newchiTietPhieuNhapHang.giaNhap = Convert.ToDouble(txtgiaNhap.Text);
                    db.PhieuNhapHangs.AddOrUpdate(newPhieuNhapHang);
                    db.ChiTietPhieuNhapHangs.AddOrUpdate(newchiTietPhieuNhapHang);
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

        private void txtSLN_TextChanged(object sender, EventArgs e)
        {
            SieuThiContextDB db = new SieuThiContextDB();
            List<ChiTietPhieuNhapHang> listchiTietPhieuNhapHangs = db.ChiTietPhieuNhapHangs.ToList();
            double tt, sln, gn;
            if(txtSLN.Text == "")
            {
                sln = 0;
            }
            else
            {
                sln = Convert.ToDouble(txtSLN.Text);
            }
            if(txtgiaNhap.Text == "")
            {
                gn = 0;
            }
            else
            {
                gn = Convert.ToDouble(txtgiaNhap.Text);
            }
            tt = sln * gn;
            txtTongTien.Text = tt.ToString();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            txtMPNH.Focus();
            txtMPNH.Enabled = true;
            SieuThiContextDB db = new SieuThiContextDB();
            try
            {
                if (txtMPNH.Text == "")
                    throw new Exception("Vui lòng nhập mã phiếu nhập hàng cần xóa");
                {
                    ChiTietPhieuNhapHang dbDelete = db.ChiTietPhieuNhapHangs.FirstOrDefault(p => p.maPhieuNhap == txtMPNH.Text);
                    PhieuNhapHang dbDelete2 = db.PhieuNhapHangs.FirstOrDefault(p => p.maPhieuNhap == txtMPNH.Text);
                    if (dbDelete != null && dbDelete2 != null)
                    {
                        DialogResult dt = MessageBox.Show("Bạn có chắc muốn xóa phiếu nhập hàng này", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dt == DialogResult.Yes)
                        {
                            db.ChiTietPhieuNhapHangs.Remove(dbDelete);
                            db.PhieuNhapHangs.Remove(dbDelete2);
                            db.SaveChanges();

                            loadDgv();
                            loadForm();

                            MessageBox.Show("Đã xóa thông tin phiếu nhập hàng thành công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            txtMPNH.Focus();
            txtMPNH.Enabled = true;
            SieuThiContextDB db = new SieuThiContextDB();
            try
            {
                if (txtMPNH.Text == "")
                    throw new Exception("Vui lòng nhập mã phiếu cần sửa");
                {
                    ChiTietPhieuNhapHang dbUpdate = db.ChiTietPhieuNhapHangs.FirstOrDefault(p => p.maPhieuNhap == txtMPNH.Text);
                    PhieuNhapHang dbUpdate2 = db.PhieuNhapHangs.FirstOrDefault(p => p.maPhieuNhap == txtMPNH.Text);
                    if (dbUpdate != null && dbUpdate2 != null)
                    {
                        dbUpdate2.maPhieuNhap = txtMPNH.Text;
                        dbUpdate2.maNV = cbbNVQL.SelectedValue.ToString();
                        dbUpdate2.maNCC = cbbNCC.SelectedValue.ToString();
                        dbUpdate2.ngayNhap = Convert.ToDateTime(dtTGX.Text);
                        dbUpdate2.tongTien = Convert.ToDouble(txtTongTien.Text);

                        dbUpdate.maPhieuNhap = txtMPNH.Text;
                        dbUpdate.maHang = cbbHangHoa.SelectedValue.ToString();
                        dbUpdate.maNCC = cbbNCC.SelectedValue.ToString();
                        dbUpdate.soLuongNhap = Convert.ToDouble(txtSLN.Text);
                        dbUpdate.thanhTien = Convert.ToDouble(txtTongTien.Text);
                        dbUpdate.giaNhap = Convert.ToDouble(txtgiaNhap.Text);

                        db.PhieuNhapHangs.AddOrUpdate(dbUpdate2);
                        db.ChiTietPhieuNhapHangs.AddOrUpdate(dbUpdate);
                        db.SaveChanges();

                        loadDgv();
                        loadForm();
                        MessageBox.Show("Sửa thông tin phiếu nhập hàng thành công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void dgvNhapHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMPNH.Enabled = true;
            txtgiaNhap.Enabled = true;
            txtSLN.Enabled = true;
            txtTongTien.Enabled = true;
            cbbHangHoa.Enabled = true;
            cbbNCC.Enabled = true;
            cbbNVQL.Enabled = true;
            SieuThiContextDB db = new SieuThiContextDB();
            List<ChiTietPhieuNhapHang> listchiTietPhieuNhapHangs = db.ChiTietPhieuNhapHangs.ToList();
            List<PhieuNhapHang> listphieuNhapHangs = db.PhieuNhapHangs.ToList();
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dgvNhapHang.Rows[e.RowIndex];

                txtMPNH.Text = row.Cells[0].Value.ToString();
                cbbNVQL.Text = row.Cells[1].Value.ToString();
                cbbNCC.Text = row.Cells[2].Value.ToString();
                cbbHangHoa.Text = row.Cells[3].Value.ToString();
                dtTGX.Text = row.Cells[4].Value.ToString();
                txtgiaNhap.Text = row.Cells[5].Value.ToString();
                txtSLN.Text = row.Cells[6].Value.ToString();
                txtTongTien.Text = row.Cells[7].Value.ToString();
            }
        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            FrmReportNhapHang frm = new FrmReportNhapHang();
            frm.ShowDialog();
        }
    }
}
