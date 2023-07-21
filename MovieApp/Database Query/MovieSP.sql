--ToGet All Movie--
create procedure spGetAllMovies
as 
begin
select Id,Name,Director,Year,Genre,MoviePhoto,AverageRating from dbo.Movies
end

--To Add Movie--
Create procedure spAddMovie
(
    @Name nvarchar(max),
	@Genre nvarchar(max),
	@Director nvarchar(max),
	@Year int,
	@MoviePhoto nvarchar(max)
)
as 
begin
Insert into Movies(Name,Genre,Director,Year,MoviePhoto)
values (@Name, @Genre,@Director, @Year, @MoviePhoto)
End 
Go

--To Delete Movie--
ALter PROCEDURE spDeleteMovie
(
	@Id int   
)
AS
BEGIN
    DELETE FROM Movies
    WHERE Id=@Id
END
GO

--To Update Movie--
Alter PROCEDURE spUpdateMovie
(
    @Id int,
    @Name nvarchar(max),
    @Genre nvarchar(max),
    @Director nvarchar(max),
    @Year int,
    @MoviePhoto nvarchar(max)
)
AS
BEGIN
    UPDATE Movies
    SET
        Name = @Name,
        Genre =@Genre,
        Director =@Director,
        Year = @Year,
        MoviePhoto = @MoviePhoto
    WHERE Id = @Id;
END
GO

--To get by id--
CREATE procedure [dbo].[spGetMovieById]
(
	@Id int
)
as 
begin
select Id,Name,Director,Description,Genre,MoviePhoto,AverageRating from dbo.Movies where id=@Id;
end

GO