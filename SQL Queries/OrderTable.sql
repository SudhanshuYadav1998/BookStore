create table Orders(
	OrderId int identity (1,1) primary key,
	OrderDate Date not null,
	BooksQty int not null,
	OrderPrice float not null,
	ActualPrice float not null,
	BookId int not null foreign key (BookId) references Books(BookId),
	UserId int not null foreign key (UserId) references UserRegistration(UserId),
	AddressId int not null foreign key (AddressId) references AddressTable(AddressId)
	)


create procedure spAddOrders
(
	@BookId int,
	@UserId int,
	@AddressId int
)
as
	declare @OrderPrice float;
	declare @ActualPrice float;
	declare @BookQuantity int;
begin
	begin
	if(exists(select*from Books where BookId=@BookId))
		begin
			if(exists(select*from AddressTable where AddressId=@AddressId))
				begin transaction
					select @BookQuantity=BooksQty from cart where BookId=@BookId and UserId=@UserId;
					set @OrderPrice=(select DiscountPrice from Books where BookId=@BookId);
					set @ActualPrice=(select ActualPrice from Books where BookId=@BookId);
					if((select Quantity from Books where BookId=@BookId)>=@BookQuantity)
						begin
						insert into Orders
						values(GETDATE(),@BookQuantity,@OrderPrice*@BookQuantity,@ActualPrice*@BookQuantity,@BookId,@UserId,@AddressId);
						update Books set Quantity=Quantity-@BookQuantity where BookId=BookId;
						delete from cart where BookId=@BookId and UserId=@UserId;
						end
					else
					begin
					select 2
					end
				commit transaction
				end
			else
			begin
			select 3
			end

		end	
end
go

create procedure spGetAllOrders
(
	@UserId int
)
as
BEGIN 
	SELECT	Books.BookName,
			Books.BookImage,
			Books.Author, 
		    Orders.ActualPrice,
			Orders.OrderPrice,
			Orders.OrderDate,
			Orders.BooksQty,
			Orders.BookId,
			Orders.OrderId,
			Orders.UserId,
			Orders.AddressId
	FROM Orders 
	INNER JOIN Books
	ON Orders.BookId = Books.BookId
	where Orders.UserId = @UserId

END

update Books set Quantity=13 where bookid=8
select*from Books
select*from Orders


create procedure spRemoveFromOrder
(
	@OrderId int,
	@UserId int

)
as
declare @BookQuantity int,
		@Bookid int
begin
		if(exists(select*from Orders where OrderId=@OrderId))
			begin
			select @Bookid=BookId from Orders where OrderId=@OrderId and UserId=@UserId
			select @BookQuantity=BooksQty from Orders where OrderId=@OrderId and UserId=@UserId
			update Books set Quantity=Quantity+@BookQuantity where BookId=@Bookid 
			delete from Orders where OrderId=@OrderId
			end
end
go