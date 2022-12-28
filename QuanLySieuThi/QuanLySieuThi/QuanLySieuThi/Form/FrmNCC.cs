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
    public partial class FrmNCC : Form
    {
        SieuThiContextDB db = new SieuThiContextDB();
        public FrmNCC()
        {
            InitializeComponent();
        }

        private void FrmNCC_Load(object sender, EventArgs e)
        {
            List<NhaCungCap> listnhaCungCaps = db.NhaCungCaps.ToList();
            BinGrid(listnhaCungCaps);

            txtMNCC.Enabled = false;
            txtTNCC.Enabled = false;
            txtDiaChi.Enabled = false;
            txtSoDienThoai.Enabled = false;
        }

        private void BinGrid(List<NhaCungCap> listnhaCungCaps)
        {
            dgvNhaCungCap.Rows.Clear();
            foreach(var item in listnhaCungCaps)
            {
                int index = dgvNhaCungCap.Rows.Add();
                dgvNhaCungCap.Rows[index].Cells[0].Value = item.maNCC;
                dgvNhaCungCap.Rows[index].Cells[1].Value = item.tenNCC;
                dgvNhaCungCap.Rows[index].Cells[2].Value = item.diaChi;
                dgvNhaCungCap.Rows[index].Cells[3].Value = item.soDienThoai;
            }
        }

        private void loadDgv()
        {
            List<NhaCungCap> listnhaCungCaps = db.NhaCungCaps.ToList();
            BinGrid(listnhaCungCaps);
            txtMNCC.Enabled = false;
            txtTNCC.Enabled = false;
            txtDiaChi.Enabled = false;
            txtSoDienThoai.Enabled = false;
        }
        private void loadForm()
        {
            txtMNCC.Clear();
            txtTNCC.Clear();
            txtDiaChi.Clear();
            txtSoDienThoai.Clear();
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            SieuThiContextDB db = new SieuThiContextDB();
            txtMNCC.Enabled = true;
            txtTNCC.Enabled = true;
            txtSoDienThoai.Enabled = true;
            txtDiaChi.Enabled = true;
            txtMNCC.Focus();
            try
            {
                if(txtMNCC.Text == "" || txtTNCC.Text == "" || txtSoDienThoai.Text == "" ||txtDiaChi.Text =="")
                MessageBox.Show("Mời bạn nhập thông tin nhà cung cấp");
                else if (txtMNCC.Text == "")
                    MessageBox.Show("Vui lòng nhập mã nhà cung cấp");
                else if (txtTNCC.Text == "")
                    MessageBox.Show("Vui lòng nhập tên nhà cung cấp");
                else if (txtDiaChi.Text == "")
                    MessageBox.Show("Vui lòng nhập địa chỉ nhà cung cấp");
                else if (txtSoDienThoai.TextLength < 10 || txtSoDienThoai.Text == "")
                    MessageBox.Show("Vui lòng kiểm tra lại số điện thoại phải hơn 10 số");
                else if (CheckMaNCC(txtMNCC.Text) == -1)
                    {
                        NhaCungCap nhaCungCap = new NhaCungCap();
                        nhaCungCap.maNCC = txtMNCC.Text;
                        nhaCungCap.tenNCC = txtTNCC.Text;
                        nhaCungCap.diaChi = txtDiaChi.Text;
                        nhaCungCap.soDienThoai = txtSoDienThoai.Text;

                        db.NhaCungCaps.Add(nhaCungCap);
                        db.SaveChanges();

                        loadDgv();
                        loadForm();
                        MessageBox.Show("Thêm mới thông tin nhà cung cấp thành công!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Thêm mới dữ liệu thất bại . mã nhà cung cấp đã tồn tại", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private int CheckMaNCC(string text)
        {
            int length = dgvNhaCungCap.Rows.Count;
            for (int i = 0; i < length; i++)
            {
                if (dgvNhaCungCap.Rows[i].Cells[0].Value != null)
                    if (dgvNhaCungCap.Rows[i].Cells[0].Value.ToString() == text)
                        return i;
            }
            return -1;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            txtMNCC.Enabled = true;
            txtMNCC.Focus();
            try
            {
                if (txtMNCC.Text == "")
                    throw new Exception("Vui lòng nhập mã nhà cung cấp cần xóa");
                {
                    NhaCungCap dbDelete = db.NhaCungCaps.FirstOrDefault(p => p.maNCC == txtMNCC.Text);
                    if (dbDelete != null)
                    {
                        DialogResult dt = MessageBox.Show("Bạn có muốn xóa thông tin nhà cung cấp này", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dt == DialogResult.Yes)
                        {
                            db.NhaCungCaps.Remove(dbDelete);
                            db.SaveChanges();

                            loadDgv();
                            loadForm();
                            MessageBox.Show("Xóa thông tin nhà cung cấp thành công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy thông tin nhà cung cấp cần xóa", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            txtMNCC.Enabled = true;
            txtTNCC.Enabled = true;
            txtDiaChi.Enabled = true;
            txtSoDienThoai.Enabled = true;
            txtMNCC.Focus();

            try
            {
                if (txtMNCC.Text == "")
                    throw new Exception("Vui lòng nhập mã nhà cung cấp cần sửa");
                {
                    NhaCungCap dbUpdate = db.NhaCungCaps.FirstOrDefault(p => p.maNCC ==txtMNCC.Text);
                    if (dbUpdate != null)
                    {
                        dbUpdate.maNCC = txtMNCC.Text;
                        dbUpdate.tenNCC = txtTNCC.Text;
                        dbUpdate.diaChi = txtDiaChi.Text;
                        dbUpdate.soDienThoai = txtSoDienThoai.Text;

                        db.NhaCungCaps.AddOrUpdate(dbUpdate);
                        db.SaveChanges();

                        loadDgv();
                        loadForm();

                        MessageBox.Show("Sửa thông tin nhà cung cấp thành công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy thông tin nhà cung cấp cần sửa", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgvNhaCungCap_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            List<NhaCungCap> listnhaCungCap = db.NhaCungCaps.ToList();
            if(e.RowIndex >= 0)
            {
                DataGridViewRow dgv = this.dgvNhaCungCap.Rows[e.RowIndex];

                txtMNCC.Text = dgv.Cells[0].Value.ToString();
                txtTNCC.Text = dgv.Cells[1].Value.ToString();
                txtDiaChi.Text = dgv.Cells[2].Value.ToString();
                txtSoDienThoai.Text = dgv.Cells[3].Value.ToString();
            }
        }
    }
}
