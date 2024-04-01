/*
Author: Pamela Lemus
Date: 14/02/2022
Script: DropScript_ClothingStock.sql
*/

USE ClothingStock_DB;

DROP TABLE User_StockIN_OUT_Product 
DROP TABLE Customer 
DROP TABLE Supplier 
DROP TABLE Product 
DROP TABLE "System_User" 

/*If you want to delete database use the next lines of code*/
USE master ;  
GO  
ALTER DATABASE ClothingStock_DB SET SINGLE_USER WITH ROLLBACK IMMEDIATE
DROP DATABASE ClothingStock_DB ;  
GO 
