﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Quanlynhansu.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class QLNSEntities : DbContext
    {
        public QLNSEntities()
            : base("name=QLNSEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<BANGCONG> BANGCONGs { get; set; }
        public virtual DbSet<BAOHIEM> BAOHIEMs { get; set; }
        public virtual DbSet<BOPHAN> BOPHANs { get; set; }
        public virtual DbSet<CHAMCONG> CHAMCONGs { get; set; }
        public virtual DbSet<CHUCVU> CHUCVUs { get; set; }
        public virtual DbSet<DANTOC> DANTOCs { get; set; }
        public virtual DbSet<HESOLUONG> HESOLUONGs { get; set; }
        public virtual DbSet<HOPDONG> HOPDONGs { get; set; }
        public virtual DbSet<HOSOTD> HOSOTDs { get; set; }
        public virtual DbSet<KHENTHUONG> KHENTHUONGs { get; set; }
        public virtual DbSet<KQTD> KQTDs { get; set; }
        public virtual DbSet<KYLUAT> KYLUATs { get; set; }
        public virtual DbSet<LUONG1> LUONG1 { get; set; }
        public virtual DbSet<NGUOIDUNG> NGUOIDUNGs { get; set; }
        public virtual DbSet<NHANVIEN> NHANVIENs { get; set; }
        public virtual DbSet<NV_PHUCAP> NV_PHUCAP { get; set; }
        public virtual DbSet<PHONGBAN> PHONGBANs { get; set; }
        public virtual DbSet<PHUCAP> PHUCAPs { get; set; }
        public virtual DbSet<QUATRINHCONGTAC> QUATRINHCONGTACs { get; set; }
        public virtual DbSet<QUATRINHLENLUONG> QUATRINHLENLUONGs { get; set; }
        public virtual DbSet<TaiKhoan> TaiKhoans { get; set; }
        public virtual DbSet<THOIVIEC> THOIVIECs { get; set; }
        public virtual DbSet<TONGIAO> TONGIAOs { get; set; }
        public virtual DbSet<TRINHDO> TRINHDOes { get; set; }
        public virtual DbSet<VITRITUYEN> VITRITUYENs { get; set; }
    }
}
