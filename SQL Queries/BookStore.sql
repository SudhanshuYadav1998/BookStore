create database BookStore

use BookStore
create table UserRegistration(
UserId int not null identity(1,1) primary key,
FullName varchar(50),
Email varchar(50),
Password varchar(50),
Mobile_Number varchar(50)
)

select*from UserRegistration

alter proc spAddUser(
@fullname varchar(50),
@email varchar(50) ,
@password varchar(50) ,
@mobilenumber varchar(50) 
)
as
begin
insert into UserRegistration
values(@fullname,@email,@password,@mobilenumber)
end
go

use BookStore
create table UserLogin(
Email varchar(50),
Password varchar(50)
)

select*from UserLogin

alter proc spUserLogin

(
@email varchar(50),
@password varchar(50)
)

as
begin 
     select * from UserRegistration where (Email = @email and Password = @password)
end
go

create proc ForgotPassword

(
@EmailId varchar(50)
)

as
begin 
     update UserRegistration set Password=null where Email=@EmailId
end 
go

alter proc spResetPassword

(
@EmailId varchar(50),
@Password varchar(50)
)

as
begin 
     update UserRegistration set Password=@Password where Email=@EmailId
end 
go

create table Admin(
	AdminId int identity (1,1) primary key,
	FullName varchar(200) not null,
	Email varchar(200) not null,
	Password varchar(200) not null,
	MobileNumber varchar(200) not null
	)

create procedure spAdminLogin
(
	@email varchar(200),
    @password varchar(200)
)
as
BEGIN
	select * from Admin where Email = @email and Password = @password;
END
go

insert into Admin values('Admin','sy7040@gmail.com','123','7275707070')

select*from Admin

create procedure spGetAllBooks
as
BEGIN
	select * from Books;
END
Go

create procedure spGetBookById
(
	@BookId int
)
as
BEGIN
	select * from Books where BookId = @BookId;
END
go

create procedure spDeleteBook
(
	@BookId int
)
as
BEGIN 
	delete from Books where BookId = @BookId;
END 
go

create procedure spUpdateBook
(
	@BookId int,
	@BookName varchar(200),
	@Author varchar(200),
	@BookImage varchar(200),
	@BookDetail varchar(max),
	@DiscountPrice float,
	@ActualPrice float,
	@Quantity int,
	@Rating float,
	@RatingCount int
)
as
BEGIN 
	update Books 
	set BookName = @BookName, Author = @Author, BookImage = @BookImage, BookDetail = @BookDetail, DiscountPrice = @DiscountPrice, ActualPrice = @ActualPrice, Quantity = @Quantity, Rating = @Rating, RatingCount = @RatingCount where BookId = @BookId;
END
go

create table WishList(
	WishListId int identity (1,1) primary key,
	UserId int not null foreign key (UserId) references UserRegistration(UserId),
	BookId int not null foreign key (BookId) references Books(BookId)
	)

	create procedure spAddToWishList
(
	@UserId int,
	@BookId int
)
as
begin
insert into WishList
values( @UserId, @BookId);
end
go


create procedure spGetAllWishList
(
	@UserId int
)
as
BEGIN
	select 
		w.WishListId,
		w.BookId,
		w.UserId,
		b.BookName,
		b.BookImage,
		b.Author,
		b.DiscountPrice,
		b.ActualPrice		
	from WishList w
	inner join Books b
	on w.BookId = b.BookId
	where w.UserId = @UserId;
END
go 

create procedure spRemoveFromWishList
(
	@WishListId int
)
as
BEGIN 
	delete from WishList where WishListId = @WishListId;
END 
go

create table Cart(
	CartId int identity (1,1) primary key,
	BooksQty int default 1,
	UserId int not null foreign key (UserId) references UserRegistration(UserId),
	BookId int not null foreign key (BookId) references Books(BookId)
	)

alter procedure spAddToCart
(
    @BooksQty int,
	@UserId int,
	@BookId int
)
as
BEGIN
IF (NOT EXISTS(SELECT * FROM Cart WHERE BookId = @BookId and UserId=@UserId))
		begin
		insert into Cart
		values(@BooksQty, @UserId, @BookId);
		end
end
go

create procedure spRemoveFromCart
(
	@CartId int
)
as
BEGIN
	delete from Cart where CartId = @CartId;
END
go

create procedure spGetAllCart
(
	@UserId int
)
as
BEGIN
	select 
		c.CartId,
		c.BookId,
		c.UserId,
		c.BooksQty,
		b.BookName,
		b.BookImage,
		b.Author,
		b.DiscountPrice,
		b.ActualPrice,
		b.Quantity
	from Cart c
	inner join Books b
	on c.BookId = b.BookId
	where c.UserId = @UserId;
END
go

create procedure spUpdateQtyInCart
(
	@CartId int,
	@BooksQty int
)
as
BEGIN
	update Cart set BooksQty = @BooksQty where CartId = @CartId;
END 
go

create table AddressType(
	TypeId int identity (1,1) primary key,
	Type varchar(200)
	)

insert into AddressType values ('Home');
insert into AddressType values ('Work');
insert into AddressType values ('Others');


create table AddressTable(
	AddressId int identity (1,1) primary key,
	Address varchar(200) not null,
	City varchar(200) not null,
	State varchar(200) not null,
	TypeId int not null foreign key (TypeId) references AddressType(TypeId),
	UserId int not null foreign key (UserId) references UserRegistration(UserId)
	)

	
alter procedure spAddAddress
(
    @Address varchar(200),
	@City varchar(200),
	@State varchar(200),
	@TypeId int,
	@UserId int
)
as
BEGIN 
	insert into AddressTable
	values(@Address, @City, @State, @TypeId, @UserId);
END
go

alter procedure spDeleteAddress
(
	@AddressId int,
	@UserId int
)
as
BEGIN
	delete from AddressTable where AddressId = @AddressId and UserId = @UserId;
END
go 

select*from AddressType

alter procedure spGetAllAddress
(
	@UserId int
)
as
BEGIN 
	select*from AddressTable where UserId=@UserId
	
	
END
go
