using System;
using Microsoft.EntityFrameworkCore;
using backend.Models;
using backend.Enums;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace backend.Context
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        public DbSet<tblAddress> tblAddress { get; set; }
        public DbSet<tblBloodBag> tblBloodBag { get; set; }
        public DbSet<tblBloodBank> tblBloodBank { get; set; }
        public DbSet<tblBloodType> tblBloodType { get; set; }
        public DbSet<tblCampaign> tblCampaign { get; set; }
        public DbSet<tblCampaignParticipation> tblCampaignParticipation { get; set; }
        public DbSet<tblDonation> tblDonation { get; set; }
        public DbSet<tblEula> tblEula { get; set; }
        public DbSet<tblMunicipality> tblMunicipality { get; set; }
        public DbSet<tblOrganizer> tblOrganizer { get; set; }
        public DbSet<tblProvince> tblProvince { get; set; }
        public DbSet<tblRequest> tblRequest { get; set; }
        public DbSet<tblUser> tblUser { get; set; }
        public DbSet<tblUserCampaign> tblUserCampaign { get; set; }
        public DbSet<tblUserEula> tblUserEula { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<tblAddress>(entity =>
            {
                entity.HasKey(e => e.addressID);
                entity.HasOne(e => e.tblMunicipality).WithMany().HasForeignKey(e => e.municipalityID);
                entity.HasOne(e => e.tblProvince).WithMany().HasForeignKey(e => e.provinceID);
            });

            modelBuilder.Entity<tblBloodBag>(entity =>
            {
                entity.HasKey(e => new { e.bagID, e.bloodTypeID, e.bloodBankID });
                entity.HasOne(e => e.tblBloodType).WithMany().HasForeignKey(e => e.bloodTypeID);
                entity.HasOne(e => e.tblBloodBank).WithMany().HasForeignKey(e => e.bloodBankID);
                entity.HasOne(e => e.tblDonation).WithMany().HasForeignKey(e => e.donationID);
            });

            modelBuilder.Entity<tblBloodBank>(entity =>
            {
                entity.HasKey(e => e.bloodBankID);
                entity.HasOne(e => e.tblAddress).WithMany().HasForeignKey(e => e.addressID);
            });

            modelBuilder.Entity<tblBloodType>(entity =>
            {
                entity.HasKey(e => e.bloodTypeID);
                entity.Property(e => e.bloodType).HasConversion<string>();
            });

            modelBuilder.Entity<tblCampaign>(entity =>
            {
                entity.HasKey(e => e.campaignID);
                entity.HasOne(e => e.tblAddress).WithMany().HasForeignKey(e => e.addressID);
                entity.HasOne(e => e.tblOrganizer).WithMany().HasForeignKey(e => e.organizerID);
            });

            modelBuilder.Entity<tblCampaignParticipation>(entity =>
            {
                entity.HasKey(e => new { e.campaignID, e.organizerID });
                entity.HasOne(e => e.tblCampaign).WithMany().HasForeignKey(e => e.campaignID);
                entity.HasOne(e => e.tblOrganizer).WithMany().HasForeignKey(e => e.organizerID);
                entity.HasOne(e => e.tblDonation).WithMany().HasForeignKey(e => e.donationID);
            });

            modelBuilder.Entity<tblDonation>(entity =>
            {
                entity.HasKey(e => e.donationID);
                entity.HasOne(e => e.tblUser).WithMany().HasForeignKey(e => e.userDocument);
                entity.HasOne(e => e.tblBloodType).WithMany().HasForeignKey(e => e.bloodTypeID);
                entity.HasOne(e => e.tblBloodBank).WithMany().HasForeignKey(e => e.bloodBankID);
                entity.Property(e => e.status).HasConversion<string>();
            });

            modelBuilder.Entity<tblEula>(entity =>
            {
                entity.HasKey(e => e.eulaID);
            });

            modelBuilder.Entity<tblMunicipality>(entity =>
            {
                entity.HasKey(e => e.municipalityID);
                entity.HasOne(e => e.tblProvince).WithMany().HasForeignKey(e => e.provinceID);
            });

            modelBuilder.Entity<tblOrganizer>(entity =>
            {
                entity.HasKey(e => e.organizerID);
                entity.HasOne(e => e.address).WithMany().HasForeignKey(e => e.addressID);
            });

            modelBuilder.Entity<tblProvince>(entity =>
            {
                entity.HasKey(e => e.provinceID);
            });

            modelBuilder.Entity<tblRequest>(entity =>
            {
                entity.HasKey(e => e.requestID);
                entity.HasOne(e => e.tblUser).WithMany().HasForeignKey(e => e.userDocument);
                entity.HasOne(e => e.tblBloodBank).WithMany().HasForeignKey(e => e.bloodTypeID);
                entity.Property(e => e.status).HasConversion<string>();
            });

            modelBuilder.Entity<tblUser>(entity =>
            {
                entity.HasKey(e => e.documentNumber);
                entity.HasOne(e => e.tblBloodType).WithMany().HasForeignKey(e => e.bloodTypeID);
                entity.HasOne(e => e.tblAddress).WithMany().HasForeignKey(e => e.addressID);
                entity.Property(e => e.documentType).HasConversion<string>();
                entity.Property(e => e.gender).HasConversion<string>();
                entity.Property(e => e.userRole).HasConversion<string>();
            });

            modelBuilder.Entity<tblUserCampaign>(entity =>
            {
                entity.HasKey(e => new { e.campaignID, e.userDocument });
                entity.HasOne(e => e.tblCampaign).WithMany().HasForeignKey(e => e.campaignID);
                entity.HasOne(e => e.tblUser).WithMany().HasForeignKey(e => e.userDocument);
            });

            modelBuilder.Entity<tblUserEula>(entity =>
            {
                entity.HasKey(e => new { e.eulaID, e.userDocument });
                entity.HasOne(e => e.tblEula).WithMany().HasForeignKey(e => e.eulaID);
                entity.HasOne(e => e.tblUser).WithMany().HasForeignKey(e => e.userDocument);
            });
            base.OnModelCreating(modelBuilder);

            var converter = new ValueConverter<BloodType, string>(
                v => v.ToDatabaseString(),
                v => BloodTypeExtensions.FromDatabaseString(v));

            modelBuilder.Entity<tblBloodType>()
                .Property(e => e.bloodType)
                .HasConversion(converter);
        }
    }
}