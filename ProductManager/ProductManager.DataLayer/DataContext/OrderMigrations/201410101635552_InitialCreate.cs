namespace ProductManager.DataLayer.DataContext.OrderMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderID = c.Int(nullable: false, identity: true),
                        Author = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        TotalPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Discount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.OrderID);
            
            CreateTable(
                "dbo.OrderLines",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ItemPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Width = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Height = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NumberOfItems = c.Int(nullable: false),
                        UnitDiscount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Order_OrderID = c.Int(),
                        Product_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Orders", t => t.Order_OrderID)
                .ForeignKey("dbo.Products", t => t.Product_Id)
                .Index(t => t.Order_OrderID)
                .Index(t => t.Product_Id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductCode = c.String(nullable: false),
                        Width = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Height = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ImageUrl = c.String(),
                        CurrentDiscount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        UnitPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsNewProduct = c.Boolean(nullable: false),
                        ColorName = c.String(),
                        ColoCode = c.String(),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProductCatagories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        Name = c.String(nullable: false),
                        ParentCategory_Id = c.Int(),
                        Product_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProductCatagories", t => t.ParentCategory_Id)
                .ForeignKey("dbo.Products", t => t.Product_Id)
                .Index(t => t.ParentCategory_Id)
                .Index(t => t.Product_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderLines", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.ProductCatagories", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.ProductCatagories", "ParentCategory_Id", "dbo.ProductCatagories");
            DropForeignKey("dbo.OrderLines", "Order_OrderID", "dbo.Orders");
            DropIndex("dbo.ProductCatagories", new[] { "Product_Id" });
            DropIndex("dbo.ProductCatagories", new[] { "ParentCategory_Id" });
            DropIndex("dbo.OrderLines", new[] { "Product_Id" });
            DropIndex("dbo.OrderLines", new[] { "Order_OrderID" });
            DropTable("dbo.ProductCatagories");
            DropTable("dbo.Products");
            DropTable("dbo.OrderLines");
            DropTable("dbo.Orders");
        }
    }
}
