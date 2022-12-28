using Microsoft.Reporting.WinForms;
using QuanLyCuaHangDienThoai.Models;
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
    public partial class FrmReportNhapHang : Form
    {
        public FrmReportNhapHang()
        {
            InitializeComponent();
        }

        private void FrmReportNhapHang_Load(object sender, EventArgs e)
        {
            SieuThiContextDB db = new SieuThiContextDB();
            List<PhieuNhapHang> listphieuNhapHangs = db.PhieuNhapHangs.ToList();
            List<ChiTietPhieuNhapHang> listchiTietPhieuNhapHangs = db.ChiTietPhieuNhapHangs.ToList();
            List<ReportNhapHang> listreportPN = new List<ReportNhapHang>();

            foreach (var item in listchiTietPhieuNhapHangs)
            {
                ReportNhapHang rp = new ReportNhapHang();
                rp.maPhieu = item.maPhieuNhap;
                rp.maNCC = item.PhieuNhapHang.NhaCungCap.tenNCC;
                rp.ngayNhap = item.PhieuNhapHang.ngayNhap;
                rp.giaNhap = item.giaNhap;
                rp.soLuong = item.soLuongNhap;
                rp.tongTien = item.PhieuNhapHang.tongTien;
                rp.maHang = item.HangHoa.tenHang;
                listreportPN.Add(rp);
            }
            this.reportViewer1.LocalReport.ReportPath = "./Report/ReportPhieuNhap.rdlc";
            var reportDataSource = new ReportDataSource("DataSet1", listreportPN);
            this.reportViewer1.LocalReport.DataSources.Clear();

            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource);
            this.reportViewer1.RefreshReport();
        }
    }
}
