﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Banka.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class BankaEntities1 : DbContext
    {
        public BankaEntities1()
            : base("name=BankaEntities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Banka> Banka { get; set; }
        public virtual DbSet<Banka_Muduru> Banka_Muduru { get; set; }
        public virtual DbSet<Banka_Toplam> Banka_Toplam { get; set; }
        public virtual DbSet<Hesaplar> Hesaplar { get; set; }
        public virtual DbSet<İslemler> İslemler { get; set; }
        public virtual DbSet<Kredi_Borclari> Kredi_Borclari { get; set; }
        public virtual DbSet<Kullanıcılar> Kullanıcılar { get; set; }
        public virtual DbSet<Kur> Kur { get; set; }
        public virtual DbSet<Makbuz> Makbuz { get; set; }
        public virtual DbSet<Musteri> Musteri { get; set; }
        public virtual DbSet<Musteri_Temsilcisi> Musteri_Temsilcisi { get; set; }
    
        public virtual ObjectResult<İslemler> son5islem()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<İslemler>("son5islem");
        }
    
        public virtual ObjectResult<İslemler> son5islem(MergeOption mergeOption)
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<İslemler>("son5islem", mergeOption);
        }
    
        public virtual ObjectResult<Makbuz> son5makbuz()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Makbuz>("son5makbuz");
        }
    
        public virtual ObjectResult<Makbuz> son5makbuz(MergeOption mergeOption)
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Makbuz>("son5makbuz", mergeOption);
        }
    }
}
