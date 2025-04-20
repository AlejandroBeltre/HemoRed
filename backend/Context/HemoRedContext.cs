using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using backend.Models;

namespace backend.Context;

public partial class HemoRedContext : DbContext
{
    public HemoRedContext()
    {
    }

    public HemoRedContext(DbContextOptions<HemoRedContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblAddress> TblAddresses { get; set; }

    public virtual DbSet<TblBloodBag> TblBloodBags { get; set; }

    public virtual DbSet<TblBloodBank> TblBloodBanks { get; set; }

    public virtual DbSet<TblBloodType> TblBloodTypes { get; set; }

    public virtual DbSet<TblCampaign> TblCampaigns { get; set; }

    public virtual DbSet<TblCampaignParticipation> TblCampaignParticipations { get; set; }

    public virtual DbSet<TblDonation> TblDonations { get; set; }

    public virtual DbSet<TblEula> TblEulas { get; set; }

    public virtual DbSet<TblMunicipality> TblMunicipalities { get; set; }

    public virtual DbSet<TblOrganizer> TblOrganizers { get; set; }

    public virtual DbSet<TblProvince> TblProvinces { get; set; }

    public virtual DbSet<TblRequest> TblRequests { get; set; }

    public virtual DbSet<TblUser> TblUsers { get; set; }

    public virtual DbSet<TblUserEula> TblUserEulas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseMySQL("Server=shortline.proxy.rlwy.net;Port=16308;UserID=root;Password=aMBlbokRwNYfMpIzOMovRkNbQhlEjdeL;Database=HemoRedDB;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblAddress>(entity =>
        {
            entity.HasKey(e => e.AddressId).HasName("PRIMARY");

            entity.ToTable("tblAddress");

            entity.HasIndex(e => e.MunicipalityId, "municipalityID");

            entity.HasIndex(e => e.ProvinceId, "provinceID");

            entity.Property(e => e.AddressId).HasColumnName("addressID");
            entity.Property(e => e.BuildingNumber).HasColumnName("buildingNumber");
            entity.Property(e => e.MunicipalityId).HasColumnName("municipalityID");
            entity.Property(e => e.ProvinceId).HasColumnName("provinceID");
            entity.Property(e => e.Street)
                .HasMaxLength(50)
                .HasColumnName("street");

            entity.HasOne(d => d.Municipality).WithMany(p => p.TblAddresses)
                .HasForeignKey(d => d.MunicipalityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("tblAddress_ibfk_1");

            entity.HasOne(d => d.Province).WithMany(p => p.TblAddresses)
                .HasForeignKey(d => d.ProvinceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("tblAddress_ibfk_2");
        });

        modelBuilder.Entity<TblBloodBag>(entity =>
        {
            entity.HasKey(e => new { e.BagId, e.BloodTypeId, e.BloodBankId }).HasName("PRIMARY");

            entity.ToTable("tblBloodBag");

            entity.HasIndex(e => e.BloodBankId, "bloodBankID");

            entity.HasIndex(e => e.BloodTypeId, "bloodTypeID");

            entity.HasIndex(e => e.DonationId, "donationID");

            entity.Property(e => e.BagId)
                .ValueGeneratedOnAdd()
                .HasColumnName("bagID");
            entity.Property(e => e.BloodTypeId).HasColumnName("bloodTypeID");
            entity.Property(e => e.BloodBankId).HasColumnName("bloodBankID");
            entity.Property(e => e.DonationId).HasColumnName("donationID");
            entity.Property(e => e.ExpirationDate)
                .HasColumnType("date")
                .HasColumnName("expirationDate");
            entity.Property(e => e.IsReserved).HasColumnName("isReserved");

            entity.HasOne(d => d.BloodBank).WithMany(p => p.TblBloodBags)
                .HasForeignKey(d => d.BloodBankId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("tblBloodBag_ibfk_2");

            entity.HasOne(d => d.BloodType).WithMany(p => p.TblBloodBags)
                .HasForeignKey(d => d.BloodTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("tblBloodBag_ibfk_1");

            entity.HasOne(d => d.Donation).WithMany(p => p.TblBloodBags)
                .HasForeignKey(d => d.DonationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("tblBloodBag_ibfk_3");
        });

        modelBuilder.Entity<TblBloodBank>(entity =>
        {
            entity.HasKey(e => e.BloodBankId).HasName("PRIMARY");

            entity.ToTable("tblBloodBank");

            entity.HasIndex(e => e.AddressId, "addressID");

            entity.Property(e => e.BloodBankId).HasColumnName("bloodBankID");
            entity.Property(e => e.AddressId).HasColumnName("addressID");
            entity.Property(e => e.AvailableHours)
                .HasMaxLength(255)
                .HasColumnName("availableHours");
            entity.Property(e => e.BloodBankName)
                .HasMaxLength(100)
                .HasColumnName("bloodBankName");
            entity.Property(e => e.Image)
                .HasMaxLength(4096)
                .HasColumnName("image");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .HasColumnName("phone");

            entity.HasOne(d => d.Address).WithMany(p => p.TblBloodBanks)
                .HasForeignKey(d => d.AddressId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("tblBloodBank_ibfk_1");
        });

        modelBuilder.Entity<TblBloodType>(entity =>
        {
            entity.HasKey(e => e.BloodTypeId).HasName("PRIMARY");

            entity.ToTable("tblBloodType");

            entity.Property(e => e.BloodTypeId).HasColumnName("bloodTypeID");
            entity.Property(e => e.BloodType)
                .HasColumnType("enum('A+','A-','B+','B-','AB+','AB-','O+','O-')")
                .HasColumnName("bloodType");
        });

        modelBuilder.Entity<TblCampaign>(entity =>
        {
            entity.HasKey(e => e.CampaignId).HasName("PRIMARY");

            entity.ToTable("tblCampaign");

            entity.HasIndex(e => e.AddressId, "addressID");

            entity.HasIndex(e => e.OrganizerId, "organizerID");

            entity.Property(e => e.CampaignId).HasColumnName("campaignID");
            entity.Property(e => e.AddressId).HasColumnName("addressID");
            entity.Property(e => e.CampaignName)
                .HasMaxLength(100)
                .HasColumnName("campaignName");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.EndTimestamp)
                .HasColumnType("datetime")
                .HasColumnName("endTimestamp");
            entity.Property(e => e.Image)
                .HasMaxLength(4096)
                .HasColumnName("image");
            entity.Property(e => e.OrganizerId).HasColumnName("organizerID");
            entity.Property(e => e.StartTimestamp)
                .HasColumnType("datetime")
                .HasColumnName("startTimestamp");

            entity.HasOne(d => d.Address).WithMany(p => p.TblCampaigns)
                .HasForeignKey(d => d.AddressId)
                .HasConstraintName("tblCampaign_ibfk_1");

            entity.HasOne(d => d.Organizer).WithMany(p => p.TblCampaigns)
                .HasForeignKey(d => d.OrganizerId)
                .HasConstraintName("tblCampaign_ibfk_2");

            entity.HasMany(d => d.UserDocuments).WithMany(p => p.Campaigns)
                .UsingEntity<Dictionary<string, object>>(
                    "TblUserCampaign",
                    r => r.HasOne<TblUser>().WithMany()
                        .HasForeignKey("UserDocument")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("tblUserCampaign_ibfk_2"),
                    l => l.HasOne<TblCampaign>().WithMany()
                        .HasForeignKey("CampaignId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("tblUserCampaign_ibfk_1"),
                    j =>
                    {
                        j.HasKey("CampaignId", "UserDocument").HasName("PRIMARY");
                        j.ToTable("tblUserCampaign");
                        j.HasIndex(new[] { "UserDocument" }, "userDocument");
                        j.IndexerProperty<int>("CampaignId").HasColumnName("campaignID");
                        j.IndexerProperty<string>("UserDocument")
                            .HasMaxLength(20)
                            .HasColumnName("userDocument");
                    });
        });

        modelBuilder.Entity<TblCampaignParticipation>(entity =>
        {
            entity.HasKey(e => new { e.CampaignId, e.OrganizerId }).HasName("PRIMARY");

            entity.ToTable("tblCampaignParticipation");

            entity.HasIndex(e => e.DonationId, "donationID");

            entity.HasIndex(e => e.OrganizerId, "organizerID");

            entity.Property(e => e.CampaignId).HasColumnName("campaignID");
            entity.Property(e => e.OrganizerId).HasColumnName("organizerID");
            entity.Property(e => e.DonationId).HasColumnName("donationID");

            entity.HasOne(d => d.Campaign).WithMany(p => p.TblCampaignParticipations)
                .HasForeignKey(d => d.CampaignId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("tblCampaignParticipation_ibfk_1");

            entity.HasOne(d => d.Donation).WithMany(p => p.TblCampaignParticipations)
                .HasForeignKey(d => d.DonationId)
                .HasConstraintName("tblCampaignParticipation_ibfk_3");

            entity.HasOne(d => d.Organizer).WithMany(p => p.TblCampaignParticipations)
                .HasForeignKey(d => d.OrganizerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("tblCampaignParticipation_ibfk_2");
        });

        modelBuilder.Entity<TblDonation>(entity =>
        {
            entity.HasKey(e => e.DonationId).HasName("PRIMARY");

            entity.ToTable("tblDonation");

            entity.HasIndex(e => e.BloodBankId, "bloodBankID");

            entity.HasIndex(e => e.BloodTypeId, "bloodTypeID");

            entity.HasIndex(e => e.UserDocument, "userDocument");

            entity.Property(e => e.DonationId).HasColumnName("donationID");
            entity.Property(e => e.BloodBankId).HasColumnName("bloodBankID");
            entity.Property(e => e.BloodTypeId).HasColumnName("bloodTypeID");
            entity.Property(e => e.DonationTimestamp)
                .HasColumnType("datetime")
                .HasColumnName("donationTimestamp");
            entity.Property(e => e.Status)
                .HasColumnType("enum('D','P')")
                .HasColumnName("status");
            entity.Property(e => e.UserDocument)
                .HasMaxLength(20)
                .HasColumnName("userDocument");

            entity.HasOne(d => d.BloodBank).WithMany(p => p.TblDonations)
                .HasForeignKey(d => d.BloodBankId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("tblDonation_ibfk_3");

            entity.HasOne(d => d.BloodType).WithMany(p => p.TblDonations)
                .HasForeignKey(d => d.BloodTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("tblDonation_ibfk_2");

            entity.HasOne(d => d.UserDocumentNavigation).WithMany(p => p.TblDonations)
                .HasForeignKey(d => d.UserDocument)
                .HasConstraintName("tblDonation_ibfk_1");
        });

        modelBuilder.Entity<TblEula>(entity =>
        {
            entity.HasKey(e => e.EulaId).HasName("PRIMARY");

            entity.ToTable("tblEula");

            entity.Property(e => e.EulaId).HasColumnName("eulaID");
            entity.Property(e => e.Content)
                .HasColumnType("text")
                .HasColumnName("content");
            entity.Property(e => e.UpdateDate)
                .HasColumnType("date")
                .HasColumnName("updateDate");
            entity.Property(e => e.Version)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("version");
        });

        modelBuilder.Entity<TblMunicipality>(entity =>
        {
            entity.HasKey(e => e.MunicipalityId).HasName("PRIMARY");

            entity.ToTable("tblMunicipality");

            entity.HasIndex(e => e.ProvinceId, "provinceID");

            entity.Property(e => e.MunicipalityId).HasColumnName("municipalityID");
            entity.Property(e => e.MunicipalityName)
                .HasMaxLength(100)
                .HasColumnName("municipalityName");
            entity.Property(e => e.ProvinceId).HasColumnName("provinceID");

            entity.HasOne(d => d.Province).WithMany(p => p.TblMunicipalities)
                .HasForeignKey(d => d.ProvinceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("tblMunicipality_ibfk_1");
        });

        modelBuilder.Entity<TblOrganizer>(entity =>
        {
            entity.HasKey(e => e.OrganizerId).HasName("PRIMARY");

            entity.ToTable("tblOrganizer");

            entity.HasIndex(e => e.AddressId, "addressID");

            entity.Property(e => e.OrganizerId).HasColumnName("organizerID");
            entity.Property(e => e.AddressId).HasColumnName("addressID");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.OrganizerName)
                .HasMaxLength(100)
                .HasColumnName("organizerName");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .HasColumnName("phone");

            entity.HasOne(d => d.Address).WithMany(p => p.TblOrganizers)
                .HasForeignKey(d => d.AddressId)
                .HasConstraintName("tblOrganizer_ibfk_1");
        });

        modelBuilder.Entity<TblProvince>(entity =>
        {
            entity.HasKey(e => e.ProvinceId).HasName("PRIMARY");

            entity.ToTable("tblProvince");

            entity.Property(e => e.ProvinceId).HasColumnName("provinceID");
            entity.Property(e => e.ProvinceName)
                .HasMaxLength(100)
                .HasColumnName("provinceName");
        });

        modelBuilder.Entity<TblRequest>(entity =>
        {
            entity.HasKey(e => e.RequestId).HasName("PRIMARY");

            entity.ToTable("tblRequest");

            entity.HasIndex(e => e.BloodTypeId, "bloodTypeID");

            entity.HasIndex(e => e.BloodBankId, "tblRequest___fk_3");

            entity.HasIndex(e => e.UserDocument, "userDocument");

            entity.Property(e => e.RequestId).HasColumnName("requestID");
            entity.Property(e => e.BloodBankId).HasColumnName("bloodBankID");
            entity.Property(e => e.BloodTypeId).HasColumnName("bloodTypeID");
            entity.Property(e => e.RequestReason)
                .HasColumnType("text")
                .HasColumnName("requestReason");
            entity.Property(e => e.RequestTimestamp)
                .HasColumnType("datetime")
                .HasColumnName("requestTimestamp");
            entity.Property(e => e.RequestedAmount).HasColumnName("requestedAmount");
            entity.Property(e => e.Status)
                .HasColumnType("enum('D','P')")
                .HasColumnName("status");
            entity.Property(e => e.UserDocument)
                .HasMaxLength(20)
                .HasColumnName("userDocument");

            entity.HasOne(d => d.BloodBank).WithMany(p => p.TblRequests)
                .HasForeignKey(d => d.BloodBankId)
                .HasConstraintName("tblRequest___fk_3");

            entity.HasOne(d => d.BloodType).WithMany(p => p.TblRequests)
                .HasForeignKey(d => d.BloodTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("tblRequest_ibfk_2");

            entity.HasOne(d => d.UserDocumentNavigation).WithMany(p => p.TblRequests)
                .HasForeignKey(d => d.UserDocument)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("tblRequest_ibfk_1");
        });

        modelBuilder.Entity<TblUser>(entity =>
        {
            entity.HasKey(e => e.DocumentNumber).HasName("PRIMARY");

            entity.ToTable("tblUser");

            entity.HasIndex(e => e.AddressId, "addressID");

            entity.HasIndex(e => e.BloodTypeId, "bloodTypeID");

            entity.Property(e => e.DocumentNumber)
                .HasMaxLength(20)
                .HasColumnName("documentNumber");
            entity.Property(e => e.AddressId).HasColumnName("addressID");
            entity.Property(e => e.BirthDate)
                .HasColumnType("date")
                .HasColumnName("birthDate");
            entity.Property(e => e.BloodTypeId).HasColumnName("bloodTypeID");
            entity.Property(e => e.DocumentType)
                .HasDefaultValueSql("'C'")
                .HasColumnType("enum('P','C')")
                .HasColumnName("documentType");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.FullName)
                .HasMaxLength(255)
                .HasColumnName("fullName");
            entity.Property(e => e.Gender)
                .HasColumnType("enum('M','F')")
                .HasColumnName("gender");
            entity.Property(e => e.Image)
                .HasMaxLength(4096)
                .HasColumnName("image");
            entity.Property(e => e.LastDonationDate)
                .HasColumnType("date")
                .HasColumnName("lastDonationDate");
            entity.Property(e => e.Password)
                .HasMaxLength(128)
                .IsFixedLength()
                .HasColumnName("password");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .HasColumnName("phone");
            entity.Property(e => e.UserRole)
                .HasColumnType("enum('A','U')")
                .HasColumnName("userRole");

            entity.HasOne(d => d.Address).WithMany(p => p.TblUsers)
                .HasForeignKey(d => d.AddressId)
                .HasConstraintName("tblUser_ibfk_2");

            entity.HasOne(d => d.BloodType).WithMany(p => p.TblUsers)
                .HasForeignKey(d => d.BloodTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("tblUser_ibfk_1");
        });

        modelBuilder.Entity<TblUserEula>(entity =>
        {
            entity.HasKey(e => new { e.EulaId, e.UserDocument }).HasName("PRIMARY");

            entity.ToTable("tblUserEula");

            entity.HasIndex(e => e.UserDocument, "userDocument");

            entity.Property(e => e.EulaId).HasColumnName("eulaID");
            entity.Property(e => e.UserDocument)
                .HasMaxLength(20)
                .HasColumnName("userDocument");
            entity.Property(e => e.AcceptedStatus).HasColumnName("acceptedStatus");

            entity.HasOne(d => d.Eula).WithMany(p => p.TblUserEulas)
                .HasForeignKey(d => d.EulaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("tblUserEula_ibfk_1");

            entity.HasOne(d => d.UserDocumentNavigation).WithMany(p => p.TblUserEulas)
                .HasForeignKey(d => d.UserDocument)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("tblUserEula_ibfk_2");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
