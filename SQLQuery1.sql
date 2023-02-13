create database ShoppingDB;

use ShoppingDB;

create table Countries (
	Id int,
	[Name] varchar(40) not null,
	IsActive bit not null,

	constraint Countries_PK primary key (Id),
);

create table Accounts (
	Id int identity(1,1),
	[Name] varchar(80) not null,
	BirthDate date not null,
	CountryId int not null,
	ImageURL varchar(255),
	CreationDate datetime not null,
	LastModifiedDate datetime not null,
	NationalId varchar(14) not null,
	IsDeleted bit not null default '0',
	Username varchar(20) not null Unique,
	Email varchar(255) not null Unique,
	[Password] varchar(20) not null,
	AccountType varchar(20) not null,

	constraint Accounts_PK primary key (Id),
	constraint Accounts_CountryId_FK foreign key (CountryId) references Countries(Id),
);

create table [Admins] (
	AccountId int not null,

	constraint Admins_PK primary key (AccountId), 
	constraint Admins_AccountId foreign key (AccountId) references Accounts(Id)
);

create table ContactTypes (
	Id int identity(1,1),
	[Name] varchar(25) not null,
	ValidationExpression varchar(255) not null,

	constraint ContactTypes_PK primary key (Id),
);

create table Traders (
	AccountId int not null,

	constraint Traders_PK primary key (AccountId),
	constraint Traders_AccountId_FK foreign key (AccountId) references Accounts(Id)
);

create table Categories (
	Id int identity(1,1),
	[Name] varchar(50) not null,
	[Desc] text,

	constraint Categories_PK primary key (Id)
);

create table Products (
	Id int identity(1,1),
	[Name] varchar(100) not null,
	[Desc] text not null,
	Price decimal not null,
	Quantity int not null,
	categoryId int,
	IsDeleted bit not null default '0',
	TraderId int not null,
	CreationDate datetime not null,
	LastModifiedDate datetime not null,

	constraint Products_PK primary key (Id),
	constraint Products_CategoryId_FK foreign key (CategoryId) references Categories(Id),
	constraint Products_TraderId_FK foreign key (TraderId) references Traders(AccountId)
);

create table ProductImages (
	Id int identity(1,1),
	ProductId int not null,
	ImageURL varchar(500) not null,

	constraint ProductImages_PK primary key (Id),
	constraint ProductImages_ProductId_FK foreign key (ProductId) references Products(Id)
);

create table ShoppingCarts (
	Id int identity(1,1),
	Total decimal not null default '0',

	constraint ShoopingCarts_PK primary key (Id),
);

create table CartItems (
	Id int identity(1,1),
	CartId int not null,
	ProductId int not null,
	Quantity int not null,
	CreationDate datetime not null,
	LastModifiedDate datetime not null, 

	constraint CartItems_PK primary key (Id),
	constraint CartItems_CartId_FK foreign key (CartId) references ShoppingCarts(Id),
	constraint CartItems_ProductId_FK foreign key (ProductId) references Products(Id)
);

create table Customers (
	AccountId int not null,
	CartId int not null,

	constraint Customers_PK primary key (AccountId),
	constraint Customers_AccountId_FK foreign key (AccountId) references Accounts(Id),
	constraint Customers_CartId_FK foreign key (CartId) references ShoppingCarts(Id)
);

create table CustomerContacts (
	Id int identity(1,1),
	CustomerId int not null,
	ContactTypeId int not null,
	Contact varchar(255) not null unique,
	CreationDate datetime not null,
	LastModifiedDate datetime not null,
	
	constraint CustomerContacts_PK primary key (Id),
	constraint CustomerContacts_CustomerId_FK foreign key (CustomerId) references Customers(AccountId),
	constraint CustomerContacts_ContactTypeId_FK foreign key (ContactTypeId) references ContactTypes(Id),
);

create table CreditCards (
	Id int identity(1,1),
	CustomerId int not null,
	CardNumber varchar(16) not null unique,
	[Provider] varchar(50) not null,

	constraint CreditCards_PK primary key (Id),
	constraint CreditCards_CustomerId foreign key (CustomerId) references Customers(AccountId)
);

create table Orders (
	Id int identity(1,1),
	CustomerId int not null,
	CardId int not null,
	Total decimal not null,
	[Status] varchar(50) not null, 
	OrderDate datetime not null,
	ReceiptDate datetime not null,

	constraint Orders_PK primary key (Id),
	constraint Orders_CustomerId_FK foreign key (CustomerId) references Customers(AccountId),
	constraint Orders_CardId_FK foreign key (CardId) references CreditCards(Id)
);

create table OrderItems (
	Id int identity(1,1),
	ProductId int not null,
	OrderId int not null,
	Quantity int not null,

	constraint OrderItems_PK primary key (Id),
	constraint OrderItems_ProductId_FK foreign key (ProductId) references Products(Id),
	constraint OrderItems_OrderId_FK foreign key (OrderId) references Orders(Id)
);