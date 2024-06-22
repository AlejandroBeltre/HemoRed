using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using backend.Models;

namespace backend.Context
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
            {
            
        }
        public DbSet<tblAddress> Address { get; set; }
        public DbSet<tblBloodBag> BloodBag { get; set; }
        public DbSet<tblBloodBank> BloodBank { get; set; }
        public DbSet<tblBloodType> BloodType { get; set; }
        public DbSet<tblCampaign> Campaign { get; set; }
        public DbSet<tblCampaignParticipation> CampaignParticipation { get; set; }
        public DbSet<tblDonation> Donation { get; set; }
        public DbSet<tblEula> Eula { get; set; }
        public DbSet<tblMunicipality> Municipality { get; set; }
        public DbSet<tblOrganizer> Organizer { get; set; }
        public DbSet<tblProvince> Province { get; set; }
        public DbSet<tblRequest> Request { get; set; }
        public DbSet<tblUser> User { get; set; }
        public DbSet<tblUserCampaign> UserCampaign { get; set; }
        public DbSet<tblUserEula> UserEula { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<tblAddress>(entity =>
            {
                entity.HasKey(e => e.AddressID);
            });
            modelBuilder.Entity<tblBloodBag>(entity =>
            {
                entity.HasKey(e => new { e.BagID, e.BloodBankID, e.BloodTypeID });
            });
            modelBuilder.Entity<tblBloodBank>(entity =>
            {
                entity.HasKey(e => e.BloodBankID);
            });
            modelBuilder.Entity<tblBloodType>(entity =>
            {
                entity.HasKey(e => e.BloodTypeID);
            });
            modelBuilder.Entity<tblCampaign>(entity =>
            {
                entity.HasKey(e => e.CampaignID);
            });
            modelBuilder.Entity<tblCampaignParticipation>(entity =>
            {
                entity.HasKey(e => new { e.CampaignID, e.OrganizerID });
            });
            modelBuilder.Entity<tblDonation>(entity =>
            {
                entity.HasKey(e => e.DonationID);
            });
            modelBuilder.Entity<tblEula>(entity =>
            {
                entity.HasKey(e => e.EulaID);
            });
            modelBuilder.Entity<tblMunicipality>(entity =>
            {
                entity.HasKey(e => e.MunicipalityID);
            });
            modelBuilder.Entity<tblOrganizer>(entity =>
            {
                entity.HasKey(e => e.OrganizerID);
            });
            modelBuilder.Entity<tblProvince>(entity =>
            {
                entity.HasKey(e => e.ProvinceID);
            });
            modelBuilder.Entity<tblRequest>(entity =>
            {
                entity.HasKey(e => e.RequestID);
            });
            modelBuilder.Entity<tblUser>(entity =>
            {
                entity.HasKey(e => e.DocumentNumber);
            });
            modelBuilder.Entity<tblUserCampaign>(entity =>
            {
                entity.HasKey(e => new { e.CampaignID, e.UserDocument });
            });
            modelBuilder.Entity<tblUserEula>(entity =>
            {
                entity.HasKey(e => new { e.EulaID, e.UserDocument });
            });
        }
    }
}