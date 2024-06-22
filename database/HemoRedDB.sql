DROP DATABASE IF EXISTS HemoRedDB;
CREATE DATABASE HemoRedDB;
USE HemoRedDB;
 
-- BloodType Table
CREATE TABLE tblBloodType (
    bloodTypeID     INT PRIMARY KEY AUTO_INCREMENT,
    bloodType       ENUM('A+', 'A-', 'B+', 'B-', 'AB+', 'AB-', 'O+', 'O-') NOT NULL
);

-- Province Table
CREATE TABLE tblProvince (
    provinceID      INT PRIMARY KEY AUTO_INCREMENT,
    provinceName    VARCHAR(100) NOT NULL
);

-- Municipality Table
CREATE TABLE tblMunicipality (
    municipalityID      INT PRIMARY KEY AUTO_INCREMENT,
    provinceID          INT NOT NULL,
    municipalityName    VARCHAR(100) NOT NULL,
    FOREIGN KEY (provinceID) REFERENCES tblProvince(provinceID)
);

-- Address Table
CREATE TABLE tblAddress (
    addressID       INT PRIMARY KEY AUTO_INCREMENT,
    municipalityID  INT NOT NULL,
    provinceID      INT NOT NULL,
    street          VARCHAR(50) NOT NULL,
    buildingNumber  INT NOT NULL,
    FOREIGN KEY (municipalityID) REFERENCES tblMunicipality(municipalityID),
    FOREIGN KEY (provinceID) REFERENCES tblProvince(provinceID)
);

-- BloodBank Table
CREATE TABLE tblBloodBank (
    bloodBankID     INT PRIMARY KEY AUTO_INCREMENT,
    addressID       INT NOT NULL,
    bloodBankName   VARCHAR(100) NOT NULL,
    availableHours  VARCHAR(255) NOT NULL,
    phone           VARCHAR(20) NOT NULL,
    image           VARCHAR(4096),
    FOREIGN KEY (addressID) REFERENCES tblAddress(addressID)
);

-- User Table
CREATE TABLE tblUser (
    documentNumber      VARCHAR(20) PRIMARY KEY,
    documentType        ENUM('P', 'C') NOT NULL DEFAULT 'C',
    bloodTypeID         INT NOT NULL,
    addressID           INT,
    fullName            VARCHAR(255) NOT NULL,
    email               VARCHAR(100) NOT NULL,
    password            CHAR(128) NOT NULL,
    birthDate           DATE NOT NULL,
    gender              ENUM('M', 'F') NOT NULL, -- Male, Female
    phone               VARCHAR(20),
    userRole            ENUM('A', 'U') NOT NULL, -- Admin, User
    lastDonationDate    DATE,
    image               VARCHAR(4096),
    FOREIGN KEY (bloodTypeID) REFERENCES tblBloodType(bloodTypeID),
    FOREIGN KEY (addressID) REFERENCES tblAddress(addressID)
);

-- Request Table
CREATE TABLE tblRequest (
    requestID           INT PRIMARY KEY AUTO_INCREMENT,
    userDocument        VARCHAR(20) NOT NULL,
    bloodTypeID         INT NOT NULL,
    requestTimestamp    DATETIME NOT NULL,
    requestReason       TEXT NOT NULL,
    requestedAmount     INT NOT NULL,
    status              ENUM('D', 'P') NOT NULL, -- Done, Pending
    FOREIGN KEY (userDocument) REFERENCES tblUser(documentNumber),
    FOREIGN KEY (bloodTypeID) REFERENCES tblBloodType(bloodTypeID)
);

-- Donation Table
CREATE TABLE tblDonation (
    donationID          INT PRIMARY KEY AUTO_INCREMENT,
    userDocument        VARCHAR(20),
    bloodTypeID         INT NOT NULL,
    bloodBankID         INT NOT NULL,
    donationTimestamp   DATETIME NOT NULL,
    status              ENUM('D', 'P') NOT NULL, -- Done, Pending
    FOREIGN KEY (userDocument) REFERENCES tblUser(documentNumber),
    FOREIGN KEY (bloodTypeID) REFERENCES tblBloodType(bloodTypeID),
    FOREIGN KEY (bloodBankID) REFERENCES tblBloodBank(bloodBankID)
);

-- BloodBag Table
CREATE TABLE tblBloodBag (
    bagID           INT AUTO_INCREMENT,
    bloodTypeID     INT NOT NULL,
    bloodBankID     INT NOT NULL,
    donationID      INT NOT NULL,
    expirationDate  DATE NOT NULL,
    isReserved      BOOL NOT NULL DEFAULT FALSE,
    PRIMARY KEY (bagID, bloodTypeID, bloodBankID),
    FOREIGN KEY (bloodTypeID) REFERENCES tblBloodType(bloodTypeID),
    FOREIGN KEY (bloodBankID) REFERENCES tblBloodBank(bloodBankID),
    FOREIGN KEY (donationID) REFERENCES tblDonation(donationID)
);

-- Eula Table
CREATE TABLE tblEula (
    eulaID      INT PRIMARY KEY AUTO_INCREMENT,
    version     CHAR(10) NOT NULL,
    updateDate  DATE NOT NULL,
    content     TEXT NOT NULL
);

-- UserEula Table
CREATE TABLE tblUserEula (
    eulaID          INT,
    userDocument    VARCHAR(20),
    acceptedStatus  BOOL NOT NULL DEFAULT FALSE,
    PRIMARY KEY (eulaID, userDocument),
    FOREIGN KEY (eulaID) REFERENCES tblEula(eulaID),
    FOREIGN KEY (userDocument) REFERENCES tblUser(documentNumber)
);

-- Organizer Table
CREATE TABLE tblOrganizer (
    organizerID     INT PRIMARY KEY AUTO_INCREMENT,
    addressID       INT,
    organizerName   VARCHAR(100) NOT NULL,
    email           VARCHAR(255) NOT NULL,
    phone           VARCHAR(20) NOT NULL,
    FOREIGN KEY (addressID) REFERENCES tblAddress(addressID)
);

-- Campaign Table
CREATE TABLE tblCampaign (
    campaignID      INT PRIMARY KEY AUTO_INCREMENT,
    addressID       INT,
    organizerID     INT,
    campaignName    VARCHAR(100) NOT NULL,
    description     TEXT NOT NULL,
    startTimestamp  DATETIME NOT NULL,
    endTimestamp    DATETIME NOT NULL,
    image           VARCHAR(4096),
    FOREIGN KEY (addressID) REFERENCES tblAddress(addressID),
    FOREIGN KEY (organizerID) REFERENCES tblOrganizer(organizerID)
);

-- UserCampaign Table
CREATE TABLE tblUserCampaign (
    campaignID      INT,
    userDocument    VARCHAR(20),
    PRIMARY KEY (campaignID, userDocument),
    FOREIGN KEY (campaignID) REFERENCES tblCampaign(campaignID),
    FOREIGN KEY (userDocument) REFERENCES tblUser(documentNumber)
);

-- CampaignParticipation Table
CREATE TABLE tblCampaignParticipation (
    campaignID  INT,
    organizerID INT,
    donationID  INT,
    PRIMARY KEY (campaignID, organizerID),
    FOREIGN KEY (campaignID) REFERENCES tblCampaign(campaignID),
    FOREIGN KEY (organizerID) REFERENCES tblOrganizer(organizerID),
    FOREIGN KEY (donationID) REFERENCES tblDonation(donationID)
);

DELIMITER //
CREATE TRIGGER trg_after_donation_insert
AFTER INSERT ON tblDonation
FOR EACH ROW
BEGIN
    IF NEW.userDocument IS NOT NULL THEN
       UPDATE tblUser
       SET lastDonationDate = NEW.donationTimestamp
       WHERE documentNumber = NEW.userDocument;
    END IF;
END//
DELIMITER ;
