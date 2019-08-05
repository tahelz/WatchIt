namespace WatchIt.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Branch",
                c => new
                    {
                        BranchID = c.Int(nullable: false, identity: true),
                        BranchName = c.String(nullable: false, maxLength: 60),
                        BranchCity = c.String(nullable: false, maxLength: 60),
                        BranchStreet = c.String(nullable: false, maxLength: 60),
                        BranchLat = c.Double(nullable: false),
                        BranchLng = c.Double(nullable: false),
                        BranchsPhoneNumber = c.String(),
                    })
                .PrimaryKey(t => t.BranchID);
            
            CreateTable(
                "dbo.Customer",
                c => new
                    {
                        CustomerID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 60),
                        LastName = c.String(nullable: false, maxLength: 60),
                        BirthDate = c.DateTime(nullable: false),
                        Gender = c.Int(nullable: false),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false, maxLength: 100),
                        City = c.String(maxLength: 60),
                        Street = c.String(maxLength: 60),
                        PhoneNumber = c.String(),
                        IsAdmin = c.Boolean(nullable: false),
                        Movie_ID = c.Int(),
                    })
                .PrimaryKey(t => t.CustomerID)
                .ForeignKey("dbo.Movie", t => t.Movie_ID)
                .Index(t => t.Movie_ID);
            
            CreateTable(
                "dbo.Order",
                c => new
                    {
                        OrderID = c.Int(nullable: false, identity: true),
                        CustomerId = c.Int(nullable: false),
                        BranchID = c.Int(nullable: false),
                        OrderDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.OrderID)
                .ForeignKey("dbo.Branch", t => t.BranchID, cascadeDelete: true)
                .ForeignKey("dbo.Customer", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId)
                .Index(t => t.BranchID);
            
            CreateTable(
                "dbo.Movie",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 45),
                        Description = c.String(nullable: false, maxLength: 400),
                        Genre = c.Int(nullable: false),
                        Price = c.Double(nullable: false),
                        Image = c.String(),
                        DirectorID = c.Int(nullable: false),
                        Length = c.Double(nullable: false),
                        Rating = c.Double(nullable: false),
                        ReleaseDate = c.DateTime(nullable: false),
                        IsCart = c.Boolean(nullable: false),
                        Trailer = c.String(),
                        Order_OrderID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Director", t => t.DirectorID, cascadeDelete: true)
                .ForeignKey("dbo.Order", t => t.Order_OrderID)
                .Index(t => t.DirectorID)
                .Index(t => t.Order_OrderID);
            
            CreateTable(
                "dbo.Director",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 60),
                        Description = c.String(nullable: false, maxLength: 500),
                        PlaceOfBirth = c.String(maxLength: 45),
                        NominatedNum = c.Int(nullable: false),
                        Image = c.String(),
                        BirthDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Movie", "Order_OrderID", "dbo.Order");
            DropForeignKey("dbo.Movie", "DirectorID", "dbo.Director");
            DropForeignKey("dbo.Customer", "Movie_ID", "dbo.Movie");
            DropForeignKey("dbo.Order", "CustomerId", "dbo.Customer");
            DropForeignKey("dbo.Order", "BranchID", "dbo.Branch");
            DropIndex("dbo.Movie", new[] { "Order_OrderID" });
            DropIndex("dbo.Movie", new[] { "DirectorID" });
            DropIndex("dbo.Order", new[] { "BranchID" });
            DropIndex("dbo.Order", new[] { "CustomerId" });
            DropIndex("dbo.Customer", new[] { "Movie_ID" });
            DropTable("dbo.Director");
            DropTable("dbo.Movie");
            DropTable("dbo.Order");
            DropTable("dbo.Customer");
            DropTable("dbo.Branch");
        }
    }
}
