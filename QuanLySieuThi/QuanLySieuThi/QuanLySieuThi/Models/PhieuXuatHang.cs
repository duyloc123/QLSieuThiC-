namespace QuanLyCuaHangDienThoai.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PhieuXuatHang")]
    public partial class PhieuXuatHang
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PhieuXuatHang()
        {
            ChiTietPhieuXuatHangs = new HashSet<ChiTietPhieuXuatHang>();
        }

        [Key]
        [StringLength(20)]
        public string maPhieuXuat { get; set; }

        [Required]
        [StringLength(20)]
        public string maNV { get; set; }

        public DateTime ngayXuat { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietPhieuXuatHang> ChiTietPhieuXuatHangs { get; set; }

        public virtual NhanVien NhanVien { get; set; }
    }
}
