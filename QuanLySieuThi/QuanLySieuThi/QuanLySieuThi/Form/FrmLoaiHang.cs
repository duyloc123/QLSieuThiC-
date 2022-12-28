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
    public partial class FrmLoaiHang : Form
    {
        public FrmLoaiHang()
        {
            InitializeComponent();
        }


        private void BindGrid(List<LoaiHangHoa> listloaiHang)
        {
            dgvLoaiHang.Rows.Clear();
            foreach (var item in listloaiHang)
            {
                int index = dgvLoaiHang.Rows.Add();
                dgvLoaiHang.Rows[index].Cells[0].Value = item.maLoai;
                dgvLoaiHang.Rows[index].Cells[1].Value = item.tenLoai;
            }
        }

        private int CheckMaLoai(string text)
        {
            int length = dgvLoaiHang.Rows.Count;
            for (int i = 0; i < length; i++)
            {
                if (dgvLoaiHang.Rows[i].Cells[0].Value != null)
                    if (dgvLoaiHang.Rows[i].Cells[0].Value.ToString() == text)
                        return i;
            }
            return -1;
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            txtmaLoai.Focus();
            try
            {
                if (txtmaLoai.Text == "" || txttenLoai.Text == "")
                    throw new Exception("Vui lòng nhập đầy đủ loại hàng");
                SieuThiContextDB db = new SieuThiContextDB();
                if(CheckMaLoai(txtmaLoai.Text) == -1)
                {
                    LoaiHangHoa newLoai = new LoaiHangHoa();
                    newLoai.maLoai = txtmaLoai.Text;
                    newLoai.tenLoai = txttenLoai.Text;
                    db.LoaiHangHoas.Add(newLoai);
                    db.SaveChanges();

                    loadDgv();
                    loadForm();
                    MessageBox.Show("Cập nhập loại hàng thành công","Thông Báo",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Cập nhập loại hàng thất bại , mã loại đã có","Thông Báo",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FrmLoaiHang_Load(object sender, EventArgs e)
        {
            SieuThiContextDB db = new SieuThiContextDB();
            List<LoaiHangHoa> listloaiHang = db.LoaiHangHoas.ToList();
            BindGrid(listloaiHang);
        }
        private void loadDgv()
        {
            SieuThiContextDB db = new SieuThiContextDB();
            List<LoaiHangHoa> listloaiHang = db.LoaiHangHoas.ToList();
            BindGrid(listloaiHang);
        }
        private void loadForm()
        {
            txtmaLoai.Clear();
            txttenLoai.Clear();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            SieuThiContextDB db = new SieuThiContextDB();
            try
            {
                if(txtmaLoai.Text == "")
                    throw new Exception("Vui lòng nhập mã loại cần xóa");
                {
                    LoaiHangHoa dbDelete = db.LoaiHangHoas.FirstOrDefault(p => p.maLoai == txtmaLoai.Text);
                    if (dbDelete != null)
                    {
                        DialogResult dt = MessageBox.Show("Bạn có chắc muốn xóa thông tin loại hàng này", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dt == DialogResult.Yes)
                        {
                            db.LoaiHangHoas.Remove(dbDelete);
                            db.SaveChanges();

                            loadDgv();
                            loadForm();

                            MessageBox.Show("Đã xóa thông tin loại hàng thành công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy thông tin loại hàng cần xóa", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            SieuThiContextDB db = new SieuThiContextDB();
            try
            {
                if (txtmaLoai.Text == "")
                    throw new Exception("Vui lòng nhập mã loại cần sửa");
                {
                    LoaiHangHoa dbUpdate = db.LoaiHangHoas.FirstOrDefault(p => p.maLoai == txtmaLoai.Text);
                    if (dbUpdate != null)
                    {
                        dbUpdate.maLoai = txtmaLoai.Text;
                        dbUpdate.tenLoai = txttenLoai.Text;

                        db.LoaiHangHoas.AddOrUpdate(dbUpdate);
                        db.SaveChanges();

                        loadDgv();
                        loadForm();
                        MessageBox.Show("Sửa thông tin loại hàng thành công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy thông tin loại hàng cần sửa", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
