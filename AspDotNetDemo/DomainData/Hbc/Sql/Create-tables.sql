
SET DATEFORMAT mdy
GO
use "Hbc"
go

if exists (select * from sysobjects where id = object_id('dbo.Order') and sysstat & 0xf = 3)
	drop table "dbo"."Order"
GO
if exists (select * from sysobjects where id = object_id('dbo.Product') and sysstat & 0xf = 3)
	drop table "dbo"."Product"
GO
if exists (select * from sysobjects where id = object_id('dbo.Customer') and sysstat & 0xf = 3)
	drop table "dbo"."Customer"
GO
if exists (select * from sysobjects where id = object_id('dbo.Employee') and sysstat & 0xf = 3)
	drop table "dbo"."Employee"
GO
CREATE TABLE "Product" (
	"Id" "int" IDENTITY (1, 1) NOT NULL ,
	"Name"   nvarchar (40) NOT NULL ,
	"Price" "money" NOT NULL CONSTRAINT "DF_Product_UnitPrice" DEFAULT (0),
	CONSTRAINT "PK_Product" PRIMARY KEY  CLUSTERED 
	(
		"Id"
	)
)
CREATE TABLE "Customer" (
	"Id" "int" IDENTITY (1, 1) NOT NULL ,
	"FirstName"  nvarchar (64) NOT NULL ,
	"MiddleName" nvarchar (64) NOT NULL ,
	"LastName"   nvarchar (64) NOT NULL ,
	CONSTRAINT "PK_Customer" PRIMARY KEY  CLUSTERED 
	(
		"Id"
	)
)
CREATE TABLE "Employee" (
	"Id" "int" IDENTITY (1, 1) NOT NULL ,
	"FirstName"  nvarchar (64) NOT NULL ,
	"MiddleName" nvarchar (64) NOT NULL ,
	"LastName"   nvarchar (64) NOT NULL ,
	CONSTRAINT "PK_Employee" PRIMARY KEY  CLUSTERED 
	(
		"Id"
	)
)
CREATE TABLE "Order" (
	"Id" "int" IDENTITY (1, 1) NOT NULL ,
	"SalesPersonId" "int" NOT NULL ,
	"CustomerId"    "int" NOT NULL ,
	"ProductId"     "int" NOT NULL ,
	"Quantity"      "int" NOT NULL CONSTRAINT "DF_Order_Quantity" DEFAULT (0),
	CONSTRAINT "PK_Order" PRIMARY KEY  CLUSTERED 
	(
		"Id"
	),
	CONSTRAINT "FK_Order_Customer" FOREIGN KEY 
	(
		"CustomerId"
	) REFERENCES "dbo"."Customer" ( "Id" ),
	CONSTRAINT "FK_Order_Employee" FOREIGN KEY 
	(
		"SalesPersonId"
	) REFERENCES "dbo"."Employee" (	"Id" ),
	CONSTRAINT "FK_Order_Product" FOREIGN KEY 
	(
		"SalesPersonId"
	) REFERENCES "dbo"."Product" (	"Id" ),
)

