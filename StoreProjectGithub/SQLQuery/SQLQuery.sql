--Create database
create database StoreManager
on primary(
	Name = StoreManager_data,
	FileName = 'D:\StoreProject\DataBase\StoreManager_data.mdf',
	Size = 20MB,
	MaxSize = 80MB,
	FileGrowth = 5MB
)
log on(
	Name = StoreManager_Log,
	FileName = 'D:\StoreProject\DataBase\StoreManager_Log.ldf',
	Size = 20MB,
	MaxSize = 80MB,
	FileGrowth = 5MB
)
go

use StoreManager
go

--Create tables
--Products
--Customers
--Employees
--Orders
--OrderDetails
--Account

create table Products(
	ProductID int not null identity(1,1) primary key,
	ProductName nvarchar(50) not null,
	Unit nvarchar(20) not null,
	Price float not null check(Price > 0),
	ImportPrice float not null check(ImportPrice > 0),
	QuantityOnHand int not null check(QuantityOnHand > 0)
)
go

create table Customers(
	CustomerID int not null identity(1,1) primary key,
	CustomerName nvarchar(50) not null,
	Phone nvarchar(11),
	TypeCustomer nvarchar(10) check(TypeCustomer in ('VIP', 'TV', 'VL')) default 'VL',
	Debt float check(Debt >= 0) default 0
)
go

create table Employees(
	EmployeeID int not null identity(1,1) primary key,
	EmployeeName nvarchar(50) not null,
	Phone nvarchar(11) not null,
	TypeEmployee nvarchar(10) check(TypeEmployee in ('BH', 'QL')) default 'BH'
)
go

create table Orders(
	OrderID int not null identity(1,1) primary key,
	CustomerID int not null,
	EmployeeID int not null,
	OrderDate DateTime default getDate(),
	Amount float not null,
	PaymentMethod nvarchar(20) check(PaymentMethod in (N'Tiền mặt', N'Momo', N'Ngân hàng', N'Ghi nợ'))

	foreign key (CustomerID) references Customers(CustomerID),
	foreign key (EmployeeID) references Employees(EmployeeID)
)
go

create table OrderDetails(
	OrderDetailID int not null identity(1,1) primary key,
	OrderID int not null,
	ProductID int not null,
	Quantity int not null,

	foreign key (OrderID) references Orders(OrderID),
	foreign key (ProductID) references Products(ProductID)
)

go

create table Accounts(
	LoginName nvarchar(50) not null primary key,
	AccountName nvarchar(50) not null,
	PasswordAcc nvarchar(1000) not null,
	TypeAcc nvarchar(20) not null check(TypeAcc in('Admin', 'User'))
)

go

--Create stored procedures
--Stored Procedure, Find account by name
create proc USP_GetAccountByAccountName
@AccountName nvarchar(50)
as
begin
	select * from Accounts where  AccountName = @AccountName
end
go 

--Stored Procedure, login
go
create proc USP_Login
@LoginName nvarchar(50),
@PasswordAcc nvarchar(1000)
as
begin
	select * from Accounts where LoginName = @LoginName and PasswordAcc = @PasswordAcc
end
go

--Stored procedure, get products list
create proc USP_GetProductsList
as
begin
	select * from Products
end
go

create proc USP_GetCustomerList
as
begin
	select * from Customers
end
go

--Stored procedure, DeleteProductById
create proc USP_DeleteProductById
@ProductID int
as
begin
	delete from Products where ProductID = @ProductID
end
go
--Stored procedure, insert value into Products

create proc USP_InsertValueIntoProducts
@ProductName nvarchar(50) ,
@Unit nvarchar(20),
@Price float,
@ImportPrice float,
@QuantityOnHand int
as
begin
	insert into Products (
	ProductName,
	Unit,
	Price,
	ImportPrice,
	QuantityOnHand)
	values(
	@ProductName,
	@Unit,
	@Price,
	@ImportPrice,
	@QuantityOnHand)
end
go
--insert value into Customers
create proc USP_InsertValueIntoCustomers
@CustomerName nvarchar(50),
@Phone nvarchar(11),
@TypeCustomer nvarchar(10),
@Debt float 
as
begin
	insert into Customers (
	CustomerName,
	Phone,
	TypeCustomer,
	Debt)
	values(
	@CustomerName,
	@Phone,
	@TypeCustomer,
	@Debt)
end
go

--Update value form Product
create proc USP_UpdateValueFormProduct
@ProductID int,
@ProductName nvarchar(50) ,
@Unit nvarchar(20),
@Price float,
@ImportPrice float,
@QuantityOnHand int
as
begin
	update Products set 
	ProductName = @ProductName,
	Unit = @Unit,
	Price = @Price,
	ImportPrice = @ImportPrice,
	QuantityOnHand = @QuantityOnHand
	where ProductID = @ProductID
end
go
select * from Products
exec USP_UpdateValueFormProduct 
@ProductID = 15,
@ProductName = 'Hello' ,
@Unit = 'abc',
@Price = 10010,
@ImportPrice = 1010,
@QuantityOnHand = 10

--Sort table Products by ProductName
go
create proc USP_SortTableProductByProductName
as
begin
	select * from Products order by ProductName
end
go
--Sort Table Products By ProductID
go
create proc USP_SortTableProductByProductID
as
begin
	select * from Products order by ProductID
end
go
--Sort Table Products By Unit
go
create proc USP_SortTableProductByUnit
as
begin
	select * from Products order by Unit
end
go
--Sort Table Products By Price
go
create proc USP_SortTableProductByPrice
as
begin
	select * from Products order by Price
end
go
--Sort Table Products By ImportPrice
go
create proc USP_SortTableProductByImportPrice
as
begin
	select * from Products order by ImportPrice
end
go
--Sort Table Products By QuantityOnHand
go
create proc USP_SortTableProductByQuantityOnHand
as
begin
	select * from Products order by QuantityOnHand
end
go

exec USP_SortTableProductBy

--Insert values into tables
--Insert values into Accounts
insert into Accounts (
	LoginName,
	AccountName,
	PasswordAcc,
	TypeAcc)
values (
	N'Khang',
	N'Khang',
	N'123',
	N'Admin')
go
insert into Accounts (
	LoginName,
	AccountName,
	PasswordAcc,
	TypeAcc)
values (
	N'Hung',
	N'Hung',
	N'123',
	N'User')
go

--Insert values into Products
insert into Products(
	ProductName,
	Unit,
	Price,
	ImportPrice,
	QuantityOnHand)
values (
	N'Sting',
	N'Chai',
	10000,
	7700,
	20)

insert into Products(
	ProductName,
	Unit,
	Price,
	ImportPrice,
	QuantityOnHand)
values (
	N'Mì ba miền',
	N'Gói',
	3000,
	2700,
	20)

go

--Insert values into Customer
insert into Customers(
	CustomerName,
	Phone,
	TypeCustomer,
	Debt)
values (
	N'Phạm Văn Khang',
	N'0339165536',
	N'VIP',
	0)
go


select AccountName, LoginName, TypeAcc from Accounts

go


exec USP_GetAccountByAccountName @AccountName = N'Khang'

select * from Accounts where LoginName = N'Khang' and PasswordAcc = N'123'

exec USP_Login @LoginName = N'Khang', @PasswordAcc = '123'

exec USP_DeleteProductById @ProductID = 7

select * from Customers 

select * from Accounts where LoginName = '' or 1=1--' and PasswordAcc = '123' 



select * from Products order by ProductName