set quoted_identifier on
go
use "Hbc"
go
--set identity_insert "Categories" off
--go
--ALTER TABLE "Categories" CHECK CONSTRAINT ALL
--go
set quoted_identifier on
go
INSERT "Customer" VALUES('Maria','Teresa','Anders')
INSERT "Customer" VALUES('Ana','Gracia','Trujillo')
INSERT "Customer" VALUES('Antonio','Julio','Moreno')
INSERT "Customer" VALUES('Thomas','Robert','Hardy')
INSERT "Customer" VALUES('Christina','Nicole','Berglund')
INSERT "Customer" VALUES('Hanna','','Moos')
INSERT "Customer" VALUES('Frédérique','','Citeaux')
INSERT "Customer" VALUES('Martín','','Sommer')
INSERT "Customer" VALUES('Laurence','Thomas','Lebihan')
INSERT "Customer" VALUES('Elizabeth','Grace','Lincoln')
INSERT "Customer" VALUES('Elizabeth','','Brown')
INSERT "Customer" VALUES('Sven','Otto','Ottlieb')
INSERT "Customer" VALUES('Janine','Gale','Labrune')
INSERT "Customer" VALUES('Ann','','Devon')
INSERT "Customer" VALUES('Roland','Samuel','Mendel')
go
INSERT "Employee" VALUES('Victoria','Jane','Ashworth')
INSERT "Employee" VALUES('Patricio','Fred','Simpson')
INSERT "Employee" VALUES('Francisco','','Chang')
INSERT "Employee" VALUES('Yang','','Wang')
INSERT "Employee" VALUES('Pedro','Ricardo','Afonso')
go
INSERT "Product" VALUES ('Chai',10.60)
INSERT "Product" VALUES ('Chang',16.30)
INSERT "Product" VALUES ('Aniseed Syrup',24.60)
INSERT "Product" VALUES ('Chef Anton''s Cajun Seasoning',1.60)
INSERT "Product" VALUES ('Chef Anton''s Gumbo Mix',10.70)
INSERT "Product" VALUES ('Grandma''s Boysenberry Spread',19.50)
INSERT "Product" VALUES ('Uncle Bob''s Organic Dried Pears',2.10)
INSERT "Product" VALUES ('Northwoods Cranberry Sauce',1.60)
INSERT "Product" VALUES ('Mishi Kobe Niku',11.60)
INSERT "Product" VALUES ('Ikura',25.30)
INSERT "Product" VALUES ('Queso Cabrales',5.70)
INSERT "Product" VALUES ('Queso Manchego La Pastora',15.40)
INSERT "Product" VALUES ('Konbu',13.30)
INSERT "Product" VALUES ('Tofu',14.70)
INSERT "Product" VALUES ('Genen Shouyu',4.70)
INSERT "Product" VALUES ('Pavlova',1.60)
INSERT "Product" VALUES ('Alice Mutton',16.60)
INSERT "Product" VALUES ('Carnarvon Tigers',16.70)
INSERT "Product" VALUES ('Teatime Chocolate Biscuits',15.30)
INSERT "Product" VALUES ('Sir Rodney''s Marmalade',17.60)
go

set quoted_identifier on
go


