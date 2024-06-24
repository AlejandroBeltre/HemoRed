using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tblBloodType",
                columns: table => new
                {
                    bloodTypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    bloodType = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblBloodType", x => x.bloodTypeID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tblEula",
                columns: table => new
                {
                    eulaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    version = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    updateDate = table.Column<DateOnly>(type: "date", nullable: false),
                    content = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblEula", x => x.eulaID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tblProvince",
                columns: table => new
                {
                    provinceID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    provinceName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblProvince", x => x.provinceID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tblMunicipality",
                columns: table => new
                {
                    municipalityID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    provinceID = table.Column<int>(type: "int", nullable: false),
                    municipalityName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblMunicipality", x => x.municipalityID);
                    table.ForeignKey(
                        name: "FK_tblMunicipality_tblProvince_provinceID",
                        column: x => x.provinceID,
                        principalTable: "tblProvince",
                        principalColumn: "provinceID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tblAddress",
                columns: table => new
                {
                    addressID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    municipalityID = table.Column<int>(type: "int", nullable: false),
                    provinceID = table.Column<int>(type: "int", nullable: false),
                    street = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    buildingNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblAddress", x => x.addressID);
                    table.ForeignKey(
                        name: "FK_tblAddress_tblMunicipality_municipalityID",
                        column: x => x.municipalityID,
                        principalTable: "tblMunicipality",
                        principalColumn: "municipalityID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblAddress_tblProvince_provinceID",
                        column: x => x.provinceID,
                        principalTable: "tblProvince",
                        principalColumn: "provinceID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tblBloodBank",
                columns: table => new
                {
                    bloodBankID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    addressID = table.Column<int>(type: "int", nullable: false),
                    bloodBankName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    availableHours = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    phone = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    image = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblBloodBank", x => x.bloodBankID);
                    table.ForeignKey(
                        name: "FK_tblBloodBank_tblAddress_addressID",
                        column: x => x.addressID,
                        principalTable: "tblAddress",
                        principalColumn: "addressID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tblOrganizer",
                columns: table => new
                {
                    organizerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    addressID = table.Column<int>(type: "int", nullable: true),
                    organizerName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    email = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    phone = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblOrganizer", x => x.organizerID);
                    table.ForeignKey(
                        name: "FK_tblOrganizer_tblAddress_addressID",
                        column: x => x.addressID,
                        principalTable: "tblAddress",
                        principalColumn: "addressID");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tblUser",
                columns: table => new
                {
                    documentNumber = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    documentType = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    bloodTypeID = table.Column<int>(type: "int", nullable: false),
                    addressID = table.Column<int>(type: "int", nullable: true),
                    fullName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    email = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    password = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    birthDate = table.Column<DateOnly>(type: "date", nullable: false),
                    gender = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    phone = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    userRole = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    lastDonationDate = table.Column<DateOnly>(type: "date", nullable: true),
                    image = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblUser", x => x.documentNumber);
                    table.ForeignKey(
                        name: "FK_tblUser_tblAddress_addressID",
                        column: x => x.addressID,
                        principalTable: "tblAddress",
                        principalColumn: "addressID");
                    table.ForeignKey(
                        name: "FK_tblUser_tblBloodType_bloodTypeID",
                        column: x => x.bloodTypeID,
                        principalTable: "tblBloodType",
                        principalColumn: "bloodTypeID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tblCampaign",
                columns: table => new
                {
                    campaignID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    addressID = table.Column<int>(type: "int", nullable: true),
                    organizerID = table.Column<int>(type: "int", nullable: false),
                    campaignName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    startTimestamp = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    endTimestamp = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    image = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblCampaign", x => x.campaignID);
                    table.ForeignKey(
                        name: "FK_tblCampaign_tblAddress_addressID",
                        column: x => x.addressID,
                        principalTable: "tblAddress",
                        principalColumn: "addressID");
                    table.ForeignKey(
                        name: "FK_tblCampaign_tblOrganizer_organizerID",
                        column: x => x.organizerID,
                        principalTable: "tblOrganizer",
                        principalColumn: "organizerID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tblDonation",
                columns: table => new
                {
                    donationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    userDocument = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    bloodTypeID = table.Column<int>(type: "int", nullable: false),
                    bloodBankID = table.Column<int>(type: "int", nullable: false),
                    donationTimestamp = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    status = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblDonation", x => x.donationID);
                    table.ForeignKey(
                        name: "FK_tblDonation_tblBloodBank_bloodBankID",
                        column: x => x.bloodBankID,
                        principalTable: "tblBloodBank",
                        principalColumn: "bloodBankID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblDonation_tblBloodType_bloodTypeID",
                        column: x => x.bloodTypeID,
                        principalTable: "tblBloodType",
                        principalColumn: "bloodTypeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblDonation_tblUser_userDocument",
                        column: x => x.userDocument,
                        principalTable: "tblUser",
                        principalColumn: "documentNumber");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tblRequest",
                columns: table => new
                {
                    requestID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    userDocument = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    bloodTypeID = table.Column<int>(type: "int", nullable: false),
                    requestTimeStamp = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    requestReason = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    requestedAmount = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblRequest", x => x.requestID);
                    table.ForeignKey(
                        name: "FK_tblRequest_tblBloodBank_bloodTypeID",
                        column: x => x.bloodTypeID,
                        principalTable: "tblBloodBank",
                        principalColumn: "bloodBankID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblRequest_tblUser_userDocument",
                        column: x => x.userDocument,
                        principalTable: "tblUser",
                        principalColumn: "documentNumber",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tblUserEula",
                columns: table => new
                {
                    eulaID = table.Column<int>(type: "int", nullable: false),
                    userDocument = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    acceptedStatus = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblUserEula", x => new { x.eulaID, x.userDocument });
                    table.ForeignKey(
                        name: "FK_tblUserEula_tblEula_eulaID",
                        column: x => x.eulaID,
                        principalTable: "tblEula",
                        principalColumn: "eulaID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblUserEula_tblUser_userDocument",
                        column: x => x.userDocument,
                        principalTable: "tblUser",
                        principalColumn: "documentNumber",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tblUserCampaign",
                columns: table => new
                {
                    campaignID = table.Column<int>(type: "int", nullable: false),
                    userDocument = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblUserCampaign", x => new { x.campaignID, x.userDocument });
                    table.ForeignKey(
                        name: "FK_tblUserCampaign_tblCampaign_campaignID",
                        column: x => x.campaignID,
                        principalTable: "tblCampaign",
                        principalColumn: "campaignID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblUserCampaign_tblUser_userDocument",
                        column: x => x.userDocument,
                        principalTable: "tblUser",
                        principalColumn: "documentNumber",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tblBloodBag",
                columns: table => new
                {
                    bagID = table.Column<int>(type: "int", nullable: false),
                    bloodTypeID = table.Column<int>(type: "int", nullable: false),
                    bloodBankID = table.Column<int>(type: "int", nullable: false),
                    donationID = table.Column<int>(type: "int", nullable: false),
                    expirationDate = table.Column<DateOnly>(type: "date", nullable: false),
                    isReserved = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblBloodBag", x => new { x.bagID, x.bloodTypeID, x.bloodBankID });
                    table.ForeignKey(
                        name: "FK_tblBloodBag_tblBloodBank_bloodBankID",
                        column: x => x.bloodBankID,
                        principalTable: "tblBloodBank",
                        principalColumn: "bloodBankID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblBloodBag_tblBloodType_bloodTypeID",
                        column: x => x.bloodTypeID,
                        principalTable: "tblBloodType",
                        principalColumn: "bloodTypeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblBloodBag_tblDonation_donationID",
                        column: x => x.donationID,
                        principalTable: "tblDonation",
                        principalColumn: "donationID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tblCampaignParticipation",
                columns: table => new
                {
                    campaignID = table.Column<int>(type: "int", nullable: false),
                    organizerID = table.Column<int>(type: "int", nullable: false),
                    donationID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblCampaignParticipation", x => new { x.campaignID, x.organizerID });
                    table.ForeignKey(
                        name: "FK_tblCampaignParticipation_tblCampaign_campaignID",
                        column: x => x.campaignID,
                        principalTable: "tblCampaign",
                        principalColumn: "campaignID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblCampaignParticipation_tblDonation_donationID",
                        column: x => x.donationID,
                        principalTable: "tblDonation",
                        principalColumn: "donationID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblCampaignParticipation_tblOrganizer_organizerID",
                        column: x => x.organizerID,
                        principalTable: "tblOrganizer",
                        principalColumn: "organizerID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_tblAddress_municipalityID",
                table: "tblAddress",
                column: "municipalityID");

            migrationBuilder.CreateIndex(
                name: "IX_tblAddress_provinceID",
                table: "tblAddress",
                column: "provinceID");

            migrationBuilder.CreateIndex(
                name: "IX_tblBloodBag_bloodBankID",
                table: "tblBloodBag",
                column: "bloodBankID");

            migrationBuilder.CreateIndex(
                name: "IX_tblBloodBag_bloodTypeID",
                table: "tblBloodBag",
                column: "bloodTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_tblBloodBag_donationID",
                table: "tblBloodBag",
                column: "donationID");

            migrationBuilder.CreateIndex(
                name: "IX_tblBloodBank_addressID",
                table: "tblBloodBank",
                column: "addressID");

            migrationBuilder.CreateIndex(
                name: "IX_tblCampaign_addressID",
                table: "tblCampaign",
                column: "addressID");

            migrationBuilder.CreateIndex(
                name: "IX_tblCampaign_organizerID",
                table: "tblCampaign",
                column: "organizerID");

            migrationBuilder.CreateIndex(
                name: "IX_tblCampaignParticipation_donationID",
                table: "tblCampaignParticipation",
                column: "donationID");

            migrationBuilder.CreateIndex(
                name: "IX_tblCampaignParticipation_organizerID",
                table: "tblCampaignParticipation",
                column: "organizerID");

            migrationBuilder.CreateIndex(
                name: "IX_tblDonation_bloodBankID",
                table: "tblDonation",
                column: "bloodBankID");

            migrationBuilder.CreateIndex(
                name: "IX_tblDonation_bloodTypeID",
                table: "tblDonation",
                column: "bloodTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_tblDonation_userDocument",
                table: "tblDonation",
                column: "userDocument");

            migrationBuilder.CreateIndex(
                name: "IX_tblMunicipality_provinceID",
                table: "tblMunicipality",
                column: "provinceID");

            migrationBuilder.CreateIndex(
                name: "IX_tblOrganizer_addressID",
                table: "tblOrganizer",
                column: "addressID");

            migrationBuilder.CreateIndex(
                name: "IX_tblRequest_bloodTypeID",
                table: "tblRequest",
                column: "bloodTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_tblRequest_userDocument",
                table: "tblRequest",
                column: "userDocument");

            migrationBuilder.CreateIndex(
                name: "IX_tblUser_addressID",
                table: "tblUser",
                column: "addressID");

            migrationBuilder.CreateIndex(
                name: "IX_tblUser_bloodTypeID",
                table: "tblUser",
                column: "bloodTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_tblUserCampaign_userDocument",
                table: "tblUserCampaign",
                column: "userDocument");

            migrationBuilder.CreateIndex(
                name: "IX_tblUserEula_userDocument",
                table: "tblUserEula",
                column: "userDocument");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblBloodBag");

            migrationBuilder.DropTable(
                name: "tblCampaignParticipation");

            migrationBuilder.DropTable(
                name: "tblRequest");

            migrationBuilder.DropTable(
                name: "tblUserCampaign");

            migrationBuilder.DropTable(
                name: "tblUserEula");

            migrationBuilder.DropTable(
                name: "tblDonation");

            migrationBuilder.DropTable(
                name: "tblCampaign");

            migrationBuilder.DropTable(
                name: "tblEula");

            migrationBuilder.DropTable(
                name: "tblBloodBank");

            migrationBuilder.DropTable(
                name: "tblUser");

            migrationBuilder.DropTable(
                name: "tblOrganizer");

            migrationBuilder.DropTable(
                name: "tblBloodType");

            migrationBuilder.DropTable(
                name: "tblAddress");

            migrationBuilder.DropTable(
                name: "tblMunicipality");

            migrationBuilder.DropTable(
                name: "tblProvince");
        }
    }
}
