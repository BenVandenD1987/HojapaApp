namespace HojapaApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ReservatieForms",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CategorieId = c.Int(nullable: false),
                        ReservatieFormName = c.String(unicode: false),
                        Datum = c.DateTime(nullable: false, precision: 0),
                        Prijs = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Productie_Id = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Categories", t => t.CategorieId, cascadeDelete: true)
                .ForeignKey("dbo.Producties", t => t.Productie_Id)
                .Index(t => t.CategorieId)
                .Index(t => t.Productie_Id);
            
            CreateTable(
                "dbo.Producties",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductieNaam = c.String(nullable: false, unicode: false),
                        Auteur = c.String(unicode: false),
                        Regisseur = c.String(unicode: false),
                        Datum = c.DateTime(nullable: false, precision: 0),
                        AantalToeschouwers = c.Int(nullable: false),
                        ProductieStatusId = c.Int(nullable: false),
                        Leden_ID = c.Int(),
                        Leden_ID1 = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Ledens", t => t.Leden_ID)
                .ForeignKey("dbo.Ledens", t => t.Leden_ID1)
                .ForeignKey("dbo.ProductieStatus", t => t.ProductieStatusId, cascadeDelete: true)
                .Index(t => t.ProductieStatusId)
                .Index(t => t.Leden_ID)
                .Index(t => t.Leden_ID1);
            
            CreateTable(
                "dbo.Ledens",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SoortLid = c.String(unicode: false),
                        Naam = c.String(nullable: false, unicode: false),
                        Voornaam = c.String(unicode: false),
                        Geslacht = c.Int(nullable: false),
                        Geboortedatum = c.DateTime(nullable: false, precision: 0),
                        Adres = c.String(unicode: false),
                        Postcode = c.Int(nullable: false),
                        Gemeente = c.String(unicode: false),
                        Nummer = c.Int(nullable: false),
                        BusNummer = c.String(unicode: false),
                        Telefoon = c.String(unicode: false),
                        Gsm = c.String(unicode: false),
                        Email = c.String(unicode: false),
                        Website = c.String(unicode: false),
                        Productie_Id = c.Int(),
                        Productie_Id1 = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Producties", t => t.Productie_Id)
                .ForeignKey("dbo.Producties", t => t.Productie_Id1)
                .Index(t => t.Productie_Id)
                .Index(t => t.Productie_Id1);
            
            CreateTable(
                "dbo.Files",
                c => new
                    {
                        FileId = c.Int(nullable: false, identity: true),
                        FileName = c.String(maxLength: 255, storeType: "nvarchar"),
                        ContentType = c.String(maxLength: 100, storeType: "nvarchar"),
                        Content = c.Binary(),
                        FileType = c.Int(nullable: false),
                        LidId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FileId)
                .ForeignKey("dbo.Ledens", t => t.LidId, cascadeDelete: true)
                .Index(t => t.LidId);
            
            CreateTable(
                "dbo.LidTypes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Naam = c.String(unicode: false),
                        isActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ProductieStatus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Naam = c.String(nullable: false, unicode: false),
                        Productie_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Producties", t => t.Productie_Id)
                .Index(t => t.Productie_Id);
            
            CreateTable(
                "dbo.ReservatieDetails",
                c => new
                    {
                        ReservatieDetailsId = c.Int(nullable: false, identity: true),
                        ReservatieId = c.Int(nullable: false),
                        ReservatieFormId = c.Int(nullable: false),
                        Hoeveelheid = c.Int(nullable: false),
                        UnitPrijs = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ReservatieDetailsId)
                .ForeignKey("dbo.Reservaties", t => t.ReservatieId, cascadeDelete: true)
                .ForeignKey("dbo.ReservatieForms", t => t.ReservatieFormId, cascadeDelete: true)
                .Index(t => t.ReservatieId)
                .Index(t => t.ReservatieFormId);
            
            CreateTable(
                "dbo.Reservaties",
                c => new
                    {
                        ReservatieId = c.Int(nullable: false, identity: true),
                        ReservatieDatum = c.DateTime(nullable: false, precision: 0),
                        Gebruikersnaam = c.String(unicode: false),
                        Voornaam = c.String(maxLength: 160, storeType: "nvarchar"),
                        Achternaam = c.String(maxLength: 160, storeType: "nvarchar"),
                        Email = c.String(unicode: false),
                        Totaal = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ReservatieId);
            
            CreateTable(
                "dbo.Kaarts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        KaartId = c.String(unicode: false),
                        ReservatieFormId = c.Int(nullable: false),
                        Optellen = c.Int(nullable: false),
                        CreateDatum = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ReservatieForms", t => t.ReservatieFormId, cascadeDelete: true)
                .Index(t => t.ReservatieFormId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        Name = c.String(nullable: false, maxLength: 255, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        RoleId = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        FirstName = c.String(unicode: false),
                        LastName = c.String(unicode: false),
                        Address = c.String(unicode: false),
                        City = c.String(unicode: false),
                        State = c.String(unicode: false),
                        PostalCode = c.String(unicode: false),
                        Country = c.String(unicode: false),
                        Phone = c.String(unicode: false),
                        Email = c.String(maxLength: 255, storeType: "nvarchar"),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(unicode: false),
                        SecurityStamp = c.String(unicode: false),
                        PhoneNumber = c.String(unicode: false),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(precision: 0),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 255, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        ClaimType = c.String(unicode: false),
                        ClaimValue = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        ProviderKey = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        UserId = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.LidTypeLedens",
                c => new
                    {
                        LidType_ID = c.Int(nullable: false),
                        Leden_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.LidType_ID, t.Leden_ID })
                .ForeignKey("dbo.LidTypes", t => t.LidType_ID, cascadeDelete: true)
                .ForeignKey("dbo.Ledens", t => t.Leden_ID, cascadeDelete: true)
                .Index(t => t.LidType_ID)
                .Index(t => t.Leden_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Kaarts", "ReservatieFormId", "dbo.ReservatieForms");
            DropForeignKey("dbo.ReservatieDetails", "ReservatieFormId", "dbo.ReservatieForms");
            DropForeignKey("dbo.ReservatieDetails", "ReservatieId", "dbo.Reservaties");
            DropForeignKey("dbo.ReservatieForms", "Productie_Id", "dbo.Producties");
            DropForeignKey("dbo.ProductieStatus", "Productie_Id", "dbo.Producties");
            DropForeignKey("dbo.Producties", "ProductieStatusId", "dbo.ProductieStatus");
            DropForeignKey("dbo.Ledens", "Productie_Id1", "dbo.Producties");
            DropForeignKey("dbo.Ledens", "Productie_Id", "dbo.Producties");
            DropForeignKey("dbo.Producties", "Leden_ID1", "dbo.Ledens");
            DropForeignKey("dbo.Producties", "Leden_ID", "dbo.Ledens");
            DropForeignKey("dbo.LidTypeLedens", "Leden_ID", "dbo.Ledens");
            DropForeignKey("dbo.LidTypeLedens", "LidType_ID", "dbo.LidTypes");
            DropForeignKey("dbo.Files", "LidId", "dbo.Ledens");
            DropForeignKey("dbo.ReservatieForms", "CategorieId", "dbo.Categories");
            DropIndex("dbo.LidTypeLedens", new[] { "Leden_ID" });
            DropIndex("dbo.LidTypeLedens", new[] { "LidType_ID" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Kaarts", new[] { "ReservatieFormId" });
            DropIndex("dbo.ReservatieDetails", new[] { "ReservatieFormId" });
            DropIndex("dbo.ReservatieDetails", new[] { "ReservatieId" });
            DropIndex("dbo.ProductieStatus", new[] { "Productie_Id" });
            DropIndex("dbo.Files", new[] { "LidId" });
            DropIndex("dbo.Ledens", new[] { "Productie_Id1" });
            DropIndex("dbo.Ledens", new[] { "Productie_Id" });
            DropIndex("dbo.Producties", new[] { "Leden_ID1" });
            DropIndex("dbo.Producties", new[] { "Leden_ID" });
            DropIndex("dbo.Producties", new[] { "ProductieStatusId" });
            DropIndex("dbo.ReservatieForms", new[] { "Productie_Id" });
            DropIndex("dbo.ReservatieForms", new[] { "CategorieId" });
            DropTable("dbo.LidTypeLedens");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Kaarts");
            DropTable("dbo.Reservaties");
            DropTable("dbo.ReservatieDetails");
            DropTable("dbo.ProductieStatus");
            DropTable("dbo.LidTypes");
            DropTable("dbo.Files");
            DropTable("dbo.Ledens");
            DropTable("dbo.Producties");
            DropTable("dbo.ReservatieForms");
            DropTable("dbo.Categories");
        }
    }
}
