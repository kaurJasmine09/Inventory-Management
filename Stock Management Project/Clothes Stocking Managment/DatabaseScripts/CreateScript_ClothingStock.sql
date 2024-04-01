/*
Author: Pamela Lemus
Date: 14/02/2022
Script: CreateScript_ClothingStock.sql
*/

/*Once Database is created please comment the next line of code.*/
CREATE DATABASE ClothingStock_DB;

USE ClothingStock_DB;

CREATE TABLE "System_User" (
EmployeeID INT PRIMARY KEY, 
FName VARCHAR (20) NOT NULL,
LName VARCHAR (20),
Email VARCHAR (50) UNIQUE NOT NULL,
Role VARCHAR (10) CHECK (Role='Manager' OR Role='Operator'),
PIN INT UNIQUE NOT NULL,
Phone VARCHAR (50) NOT NULL
);

CREATE TABLE Product (
ProductID INT PRIMARY KEY, 
ProductName VARCHAR (100) NOT NULL, 
Description VARCHAR (1000) NOT NULL,
Size VARCHAR (20) CHECK (Size='XS' OR Size ='S' OR Size ='M' OR Size ='L' OR Size ='XL') NOT NULL,
Category VARCHAR (20) CHECK (Category ='Pants' OR Category ='Shirts' OR Category ='Sweaters' OR Category ='Coats' OR Category ='Jackets' OR Category ='Others') NOT NULL,
Quantity INT NOT NULL
);

CREATE TABLE Supplier (
SupplierID INT PRIMARY KEY,
SupplierName VARCHAR (50) NOT NULL ,
Email VARCHAR (50) UNIQUE NOT NULL,
Phone VARCHAR (50) NOT NULL
);

CREATE TABLE Customer (
CustomerID INT PRIMARY KEY ,
CustomerName VARCHAR (50) NOT NULL ,
Location VARCHAR (200) NOT NULL,
Email VARCHAR (50) UNIQUE NOT NULL,
Phone VARCHAR (50) NOT NULL
);

CREATE TABLE User_StockIN_OUT_Product (
TransactionNumber INT PRIMARY KEY IDENTITY(1,1),
TransactionId VARCHAR(MAX) NOT NULL,
ProductID INT NOT NULL,
Quantity INT NOT NULL,
Date DATETIME NOT NULL,
EmployeeID INT NOT NULL,
Comments VARCHAR (50),
Type VARCHAR (20) CHECK (Type ='IN' OR Type ='OUT' OR Type ='ADJUST') NOT NULL,
SupplierID INT,
CustomerID INT,
CONSTRAINT Stock_Employee FOREIGN KEY (EmployeeID) REFERENCES "System_User" (EmployeeID) ON DELETE CASCADE,
CONSTRAINT Stock_Product FOREIGN KEY (ProductID) REFERENCES Product (ProductID) ON DELETE CASCADE,
CONSTRAINT Stock_Supplier FOREIGN KEY (SupplierID) REFERENCES Supplier (SupplierID) ON DELETE CASCADE,
CONSTRAINT Stock_Customer FOREIGN KEY (CustomerID) REFERENCES Customer (CustomerID) ON DELETE CASCADE
);