using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyCuaHangDienThoai.Models
{
     class ReportHoaDon
    {
        public string maKhach { get; set; }
        public string maNV { get; set; }
        public string maHang { get; set; }
        public double soLuong { get; set; }
        public double tongTien { get; set; }
        public double giaTien { get; set; }
        public DateTime ngayBan { get; set; }
    }
}
