USE ClothingStock_DB;

declare @DataPath varchar(1000)
--Set the path to the data folder in your system. Ex: 'C:\Data'
set @DataPath = 'C:\data\'
--Insert product data
declare @sql_product varchar(1000)
set @sql_product = 'Bulk insert Product
	from ''' + @DataPath + 'Product.psv ' +  
	'''with 
	(
		fieldterminator = ''|'',
		rowterminator = ''\n''
	)'
exec(@sql_product)

--Insert System User data

declare @sql_system_user varchar(1000)
set @sql_system_user = 'Bulk insert "System_User"
	from ''' + @DataPath + 'System_User.psv ' +  
	'''with 
	(
		fieldterminator = ''|'',
		rowterminator = ''\n''
	)'
exec(@sql_system_user)

--Insert Supplier data

declare @sql_supplier varchar(1000)
set @sql_supplier = 'Bulk insert "Supplier"
	from ''' + @DataPath + 'Supplier.psv ' +  
	'''with 
	(
		fieldterminator = ''|'',
		rowterminator = ''\n''
	)'
exec(@sql_supplier)

--Insert Customer data

declare @sql_customer varchar(1000)
set @sql_customer = 'Bulk insert "Customer"
	from ''' + @DataPath + 'Customer.psv ' +  
	'''with 
	(
		fieldterminator = ''|'',
		rowterminator = ''\n''
	)'
exec(@sql_customer)