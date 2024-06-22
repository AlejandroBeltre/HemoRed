using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "BloodType",
                columns: table => new
                {
                    BloodTypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    BloodType = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BloodType", x => x.BloodTypeID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Eula",
                columns: table => new
                {
                    EulaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Version = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UpdateDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Content = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Eula", x => x.EulaID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Province",
                columns: table => new
                {
                    ProvinceID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Province = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Province", x => x.ProvinceID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Municipality",
                columns: table => new
                {
                    MunicipalityID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ProvinceID = table.Column<int>(type: "int", nullable: false),
                    MunicipalityName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Municipality", x => x.MunicipalityID);
                    table.ForeignKey(
                        name: "FK_Municipality_Province_ProvinceID",
                        column: x => x.ProvinceID,
                        principalTable: "Province",
                        principalColumn: "ProvinceID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    AddressID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    MunicipalityID = table.Column<int>(type: "int", nullable: false),
                    ProvinceID = table.Column<int>(type: "int", nullable: false),
                    Street = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BuildingNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.AddressID);
                    table.ForeignKey(
                        name: "FK_Address_Municipality_MunicipalityID",
                        column: x => x.MunicipalityID,
                        principalTable: "Municipality",
                        principalColumn: "MunicipalityID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Address_Province_ProvinceID",
                        column: x => x.ProvinceID,
                        principalTable: "Province",
                        principalColumn: "ProvinceID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "BloodBank",
                columns: table => new
                {
                    BloodBankID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AddressID = table.Column<int>(type: "int", nullable: false),
                    BloodBankName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AvailableHours = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Phone = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Image = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BloodBank", x => x.BloodBankID);
                    table.ForeignKey(
                        name: "FK_BloodBank_Address_AddressID",
                        column: x => x.AddressID,
                        principalTable: "Address",
                        principalColumn: "AddressID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Organizer",
                columns: table => new
                {
                    OrganizerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AddressID = table.Column<int>(type: "int", nullable: true),
                    OrganizerName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Phone = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizer", x => x.OrganizerID);
                    table.ForeignKey(
                        name: "FK_Organizer_Address_AddressID",
                        column: x => x.AddressID,
                        principalTable: "Address",
                        principalColumn: "AddressID");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    DocumentNumber = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DocumentType = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BloodTypeID = table.Column<int>(type: "int", nullable: false),
                    AddressID = table.Column<int>(type: "int", nullable: true),
                    FullName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Password = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BirthDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Gender = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Phone = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserRole = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastDonationDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Image = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.DocumentNumber);
                    table.ForeignKey(
                        name: "FK_User_Address_AddressID",
                        column: x => x.AddressID,
                        principalTable: "Address",
                        principalColumn: "AddressID");
                    table.ForeignKey(
                        name: "FK_User_BloodType_BloodTypeID",
                        column: x => x.BloodTypeID,
                        principalTable: "BloodType",
                        principalColumn: "BloodTypeID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Campaign",
                columns: table => new
                {
                    CampaignID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AddressID = table.Column<int>(type: "int", nullable: true),
                    OrganizerID = table.Column<int>(type: "int", nullable: false),
                    CampaignName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StartTimestamp = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    EndTimestamp = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Image = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Campaign", x => x.CampaignID);
                    table.ForeignKey(
                        name: "FK_Campaign_Address_AddressID",
                        column: x => x.AddressID,
                        principalTable: "Address",
                        principalColumn: "AddressID");
                    table.ForeignKey(
                        name: "FK_Campaign_Organizer_OrganizerID",
                        column: x => x.OrganizerID,
                        principalTable: "Organizer",
                        principalColumn: "OrganizerID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Donation",
                columns: table => new
                {
                    DonationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserDocument = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BloodTypeID = table.Column<int>(type: "int", nullable: false),
                    BloodBankID = table.Column<int>(type: "int", nullable: false),
                    DonationTimestamp = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Status = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserDocumentNumber = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Donation", x => x.DonationID);
                    table.ForeignKey(
                        name: "FK_Donation_BloodBank_BloodBankID",
                        column: x => x.BloodBankID,
                        principalTable: "BloodBank",
                        principalColumn: "BloodBankID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Donation_BloodType_BloodTypeID",
                        column: x => x.BloodTypeID,
                        principalTable: "BloodType",
                        principalColumn: "BloodTypeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Donation_User_UserDocumentNumber",
                        column: x => x.UserDocumentNumber,
                        principalTable: "User",
                        principalColumn: "DocumentNumber");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Request",
                columns: table => new
                {
                    RequestID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    BloodBankID = table.Column<int>(type: "int", nullable: false),
                    RequestDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Status = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserDocumentNumber = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Request", x => x.RequestID);
                    table.ForeignKey(
                        name: "FK_Request_BloodBank_BloodBankID",
                        column: x => x.BloodBankID,
                        principalTable: "BloodBank",
                        principalColumn: "BloodBankID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Request_User_UserDocumentNumber",
                        column: x => x.UserDocumentNumber,
                        principalTable: "User",
                        principalColumn: "DocumentNumber",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "UserEula",
                columns: table => new
                {
                    EulaID = table.Column<int>(type: "int", nullable: false),
                    UserDocument = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AcceptedStatus = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    UserDocumentNumber = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserEula", x => new { x.EulaID, x.UserDocument });
                    table.ForeignKey(
                        name: "FK_UserEula_Eula_EulaID",
                        column: x => x.EulaID,
                        principalTable: "Eula",
                        principalColumn: "EulaID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserEula_User_UserDocumentNumber",
                        column: x => x.UserDocumentNumber,
                        principalTable: "User",
                        principalColumn: "DocumentNumber",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "UserCampaign",
                columns: table => new
                {
                    CampaignID = table.Column<int>(type: "int", nullable: false),
                    UserDocument = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserDocumentNumber = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCampaign", x => new { x.CampaignID, x.UserDocument });
                    table.ForeignKey(
                        name: "FK_UserCampaign_Campaign_CampaignID",
                        column: x => x.CampaignID,
                        principalTable: "Campaign",
                        principalColumn: "CampaignID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserCampaign_User_UserDocumentNumber",
                        column: x => x.UserDocumentNumber,
                        principalTable: "User",
                        principalColumn: "DocumentNumber",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "BloodBag",
                columns: table => new
                {
                    BagID = table.Column<int>(type: "int", nullable: false),
                    BloodTypeID = table.Column<int>(type: "int", nullable: false),
                    BloodBankID = table.Column<int>(type: "int", nullable: false),
                    DonationID = table.Column<int>(type: "int", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    IsReserved = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BloodBag", x => new { x.BagID, x.BloodBankID, x.BloodTypeID });
                    table.ForeignKey(
                        name: "FK_BloodBag_BloodBank_BloodBankID",
                        column: x => x.BloodBankID,
                        principalTable: "BloodBank",
                        principalColumn: "BloodBankID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BloodBag_BloodType_BloodTypeID",
                        column: x => x.BloodTypeID,
                        principalTable: "BloodType",
                        principalColumn: "BloodTypeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BloodBag_Donation_DonationID",
                        column: x => x.DonationID,
                        principalTable: "Donation",
                        principalColumn: "DonationID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CampaignParticipation",
                columns: table => new
                {
                    CampaignID = table.Column<int>(type: "int", nullable: false),
                    OrganizerID = table.Column<int>(type: "int", nullable: false),
                    DonationID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CampaignParticipation", x => new { x.CampaignID, x.OrganizerID });
                    table.ForeignKey(
                        name: "FK_CampaignParticipation_Campaign_CampaignID",
                        column: x => x.CampaignID,
                        principalTable: "Campaign",
                        principalColumn: "CampaignID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CampaignParticipation_Donation_DonationID",
                        column: x => x.DonationID,
                        principalTable: "Donation",
                        principalColumn: "DonationID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CampaignParticipation_Organizer_OrganizerID",
                        column: x => x.OrganizerID,
                        principalTable: "Organizer",
                        principalColumn: "OrganizerID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Address_MunicipalityID",
                table: "Address",
                column: "MunicipalityID");

            migrationBuilder.CreateIndex(
                name: "IX_Address_ProvinceID",
                table: "Address",
                column: "ProvinceID");

            migrationBuilder.CreateIndex(
                name: "IX_BloodBag_BloodBankID",
                table: "BloodBag",
                column: "BloodBankID");

            migrationBuilder.CreateIndex(
                name: "IX_BloodBag_BloodTypeID",
                table: "BloodBag",
                column: "BloodTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_BloodBag_DonationID",
                table: "BloodBag",
                column: "DonationID");

            migrationBuilder.CreateIndex(
                name: "IX_BloodBank_AddressID",
                table: "BloodBank",
                column: "AddressID");

            migrationBuilder.CreateIndex(
                name: "IX_Campaign_AddressID",
                table: "Campaign",
                column: "AddressID");

            migrationBuilder.CreateIndex(
                name: "IX_Campaign_OrganizerID",
                table: "Campaign",
                column: "OrganizerID");

            migrationBuilder.CreateIndex(
                name: "IX_CampaignParticipation_DonationID",
                table: "CampaignParticipation",
                column: "DonationID");

            migrationBuilder.CreateIndex(
                name: "IX_CampaignParticipation_OrganizerID",
                table: "CampaignParticipation",
                column: "OrganizerID");

            migrationBuilder.CreateIndex(
                name: "IX_Donation_BloodBankID",
                table: "Donation",
                column: "BloodBankID");

            migrationBuilder.CreateIndex(
                name: "IX_Donation_BloodTypeID",
                table: "Donation",
                column: "BloodTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Donation_UserDocumentNumber",
                table: "Donation",
                column: "UserDocumentNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Municipality_ProvinceID",
                table: "Municipality",
                column: "ProvinceID");

            migrationBuilder.CreateIndex(
                name: "IX_Organizer_AddressID",
                table: "Organizer",
                column: "AddressID");

            migrationBuilder.CreateIndex(
                name: "IX_Request_BloodBankID",
                table: "Request",
                column: "BloodBankID");

            migrationBuilder.CreateIndex(
                name: "IX_Request_UserDocumentNumber",
                table: "Request",
                column: "UserDocumentNumber");

            migrationBuilder.CreateIndex(
                name: "IX_User_AddressID",
                table: "User",
                column: "AddressID");

            migrationBuilder.CreateIndex(
                name: "IX_User_BloodTypeID",
                table: "User",
                column: "BloodTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_UserCampaign_UserDocumentNumber",
                table: "UserCampaign",
                column: "UserDocumentNumber");

            migrationBuilder.CreateIndex(
                name: "IX_UserEula_UserDocumentNumber",
                table: "UserEula",
                column: "UserDocumentNumber");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BloodBag");

            migrationBuilder.DropTable(
                name: "CampaignParticipation");

            migrationBuilder.DropTable(
                name: "Request");

            migrationBuilder.DropTable(
                name: "UserCampaign");

            migrationBuilder.DropTable(
                name: "UserEula");

            migrationBuilder.DropTable(
                name: "Donation");

            migrationBuilder.DropTable(
                name: "Campaign");

            migrationBuilder.DropTable(
                name: "Eula");

            migrationBuilder.DropTable(
                name: "BloodBank");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Organizer");

            migrationBuilder.DropTable(
                name: "BloodType");

            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "Municipality");

            migrationBuilder.DropTable(
                name: "Province");
        }
    }
}
