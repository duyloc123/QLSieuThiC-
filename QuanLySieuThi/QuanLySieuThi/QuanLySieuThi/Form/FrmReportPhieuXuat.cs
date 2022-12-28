using Microsoft.Reporting.WinForms;
using QuanLyCuaHangDienThoai.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyCuaHangDienThoai
{
    public partial class FrmReportPhieuXuat : Form
    {
        public FrmReportPhieuXuat()
        {
            InitializeComponent();
        }

        private void FrmReportPhieuXuat_Load(object sender, EventArgs e)
        {
            SieuThiContextDB db = new SieuThiContextDB();
            List<NhanVien> listnhanViens = db.NhanViens.ToList();
            List<ChiTietPhieuXuatHang> listchiTietPhieuXuatHang = db.ChiTietPhieuXuatHangs.ToList();
            List<PhieuXuatHang> listphieuXuatHangs = db.PhieuXuatHangs.ToList();
            List<ReportPhieuXuat> listreportPX = new List<ReportPhieuXuat>();

            foreach (var item in listchiTietPhieuXuatHang)
            {
                ReportPhieuXuat rp = new ReportPhieuXuat();
                rp.maPhieu = item.maPhieuXuat;
                rp.maNV = item.PhieuXuatHang.NhanVien.tenNV;
                rp.ngayXuat = item.PhieuXuatHang.ngayXuat;
                rp.maHang = item.HangHoa.tenHang;
                rp.soLuong = Convert.ToDouble(item.soLuongXuat);

                listreportPX.Add(rp);
            }
            this.reportViewer1.LocalReport.ReportPath = "./Report/ReportPhieuXuat.rdlc";
            var reportDataSource = new ReportDataSource("DataSet1", listreportPX);
            this.reportViewer1.LocalReport.DataSources.Clear();

            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource);
            this.reportViewer1.RefreshReport();
        }
    }
}
