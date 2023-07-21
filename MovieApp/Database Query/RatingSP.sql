--To Get All Rating--
Create procedure [dbo].[spGetAllRatings]
as 
begin
select RatingId,Ratings,MovieId,UserId from dbo.Ratings
end
GO

--To Add Rating--
Create PROCEDURE spAddRating
    @RatingId  int,
    @Ratings int,
    @UserId NVARCHAR(MAX),   -- Assuming UserId is a foreign key to some Users table
    @MovieId INT  -- Assuming MovieId is a foreign key to some Movies table
AS
BEGIN
    INSERT INTO Ratings ( RatingId,Ratings, UserId, MovieId)
    VALUES ( @RatingId,@Ratings, @UserId, @MovieId)
END

--To get Rating by Id And Movie--
Create Procedure spGetRatingOnIdAndMovie
@UserId NVarChar(MAX),
@MovieId Int
As 
BEGIN
 Select Ratings from dbo.Ratings where UserId=@UserId and MovieId=@MovieId
 end 
 Go

--To Ipdate Rating--
 Create PROCEDURE [dbo].[spUpdateRating]  
    @Ratings int,
    @UserID NVARCHAR(MAX),
    @MovieId int
AS
BEGIN
    UPDATE Ratings
    SET
		Ratings=@Ratings
    WHERE MovieId=@MovieId and  UserId=@UserId
END

GO

--Get average rating--
Create PROCEDURE [dbo].[spGetAverageRating]  
    @MovieId int
AS
BEGIN
		SELECT CAST(SUM(Ratings) AS DECIMAL(10, 2)) / COUNT(Ratings) AS AverageRating
	FROM Ratings
	WHERE MovieId = @MovieId
END

GO