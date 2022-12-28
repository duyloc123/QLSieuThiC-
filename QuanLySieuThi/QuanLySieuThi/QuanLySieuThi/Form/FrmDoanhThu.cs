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
    public partial class FrmDoanhThu : Form
    {
        public FrmDoanhThu()
        {
            InitializeComponent();
        }

        private void FrmDoanhThu_Load(object sender, EventArgs e)
        {
            SieuThiContextDB db = new SieuThiContextDB();
            List<HoaDon> listhoaDons = db.HoaDons.ToList();
            List<ChiTietHoaDon> listchiTietHoaDons = db.ChiTietHoaDons.ToList();
            List<ReportHoaDon> listreportHD = new List<ReportHoaDon>();

            foreach (var item in listchiTietHoaDons)
            {
                ReportHoaDon rp = new ReportHoaDon();
                rp.ngayBan = item.HoaDon.ngayBan;
                rp.maHang = item.HangHoa.tenHang;
                rp.soLuong = item.soLuong;
                rp.tongTien = item.HoaDon.tongTien;
                listreportHD.Add(rp);
            }
            this.reportViewer1.LocalReport.ReportPath = "./Report/Report1.rdlc";
            var reportDataSource = new ReportDataSource("DataSet1", listreportHD);
            this.reportViewer1.LocalReport.DataSources.Clear();

            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource);
            this.reportViewer1.RefreshReport();
            this.reportViewer1.RefreshReport();
        }
    }
}
