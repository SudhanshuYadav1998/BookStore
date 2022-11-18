create table Books(
	BookId int identity (1,1) primary key,
	BookName varchar(200) not null,
	Author varchar(200) not null,
	BookImage varchar(max) not null,
	BookDetail varchar(max) not null,
	DiscountPrice float not null,
	ActualPrice float not null,
	Quantity int not null,
	Rating float,
	RatingCount int
	)
create procedure spAddBook
(
    @BookName varchar(200),
	@Author varchar(200),
	@BookImage varchar(200),
	@BookDetail varchar(max),
	@DiscountPrice float,
	@ActualPrice float,
	@Quantity int,
	@Rating float,
	@RatingCount int,
	@BookId int output
)
as
BEGIN
	insert into Books
	values(@BookName, @Author, @BookImage, @BookDetail, @DiscountPrice, @ActualPrice, @Quantity, @Rating, @RatingCount);
	set @BookId = SCOPE_IDENTITY()
	return @BookId;
END
go

create table Feedback(
	FeedbackId int identity (1,1) primary key,
	Rating int not null,
	Comment varchar(max) not null,
	BookId int not null foreign key (BookId) references Books(BookId),
	UserId int not null foreign key (UserId) references UserRegistration(UserId)
	)


create procedure spAddFeedback
(
    @Rating int,
	@Comment varchar(max),
	@BookId int,
	@UserId int
)
as
BEGIN
insert into Feedback
values(@Rating, @Comment, @BookId, @UserId);
END
go

create procedure spGetAllFeedback
(
	@BookId int
)
as
BEGIN
	SELECT Feedback.FeedbackId,
		   Feedback.UserId,
		   Feedback.BookId,
		   Feedback.Comment,
		   Feedback.Rating, 
		   UserRegistration.FullName 
	FROM UserRegistration 
	INNER JOIN Feedback 
	ON Feedback.UserId = UserRegistration.UserId WHERE BookId=@BookId

END
go