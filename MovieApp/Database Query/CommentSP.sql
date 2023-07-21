--To Get All Comment--
CREATE procedure [dbo].[spGetAllComment]
@MovieId int
as 
begin
select CommentId,CommentDesc,MovieId,UserId,UserName,TimeStamp from dbo.Comments where MovieId=@MovieID
end
GO

--To Add Comment--
ALter PROCEDURE spAddComment
    @UserName nvarchar(MAX),
    @CommentDesc NVARCHAR(MAX),
    @UserId NVARCHAR(MAX),   -- Assuming UserId is a foreign key to some Users table
    @MovieId INT,  -- Assuming MovieId is a foreign key to some Movies table
    @CreatedAt DATETIME
AS
BEGIN
    INSERT INTO Comments ( CommentDesc,UserName, UserId, MovieId, TimeStamp)
    VALUES ( @CommentDesc,@UserName, @UserId, @MovieId, @CreatedAt)
END

--To Get Comment BY CommentID--
CREATE procedure [dbo].[spGetCommentById]
(
	@CommentId int
)
as 
begin
select CommentId,CommentDesc,UserId,UserName,MovieId from dbo.Comments where CommentId=@CommentId;
end

GO

--To Delete Comment--
Alter PROCEDURE [dbo].[spDeleteComment]
(
	@CommentId int   
)
AS
BEGIN
    DELETE FROM Comments
    WHERE CommentId=@CommentId
END
GO

--To Update Comment--
Create PROCEDURE [dbo].[spUpdateComment]
    @CommentId INT,
    @CommentDesc NVARCHAR(100),
    @UserID NVARCHAR(50),
    @UserName NVARCHAR(100),
    @MovieId NVARCHAR(MAX),
	@CreatedAt DATETIME
AS
BEGIN
    UPDATE Comments
    SET
	@CommentId=@CommentId,
       CommentDesc= @CommentDesc,
        UserId = @UserId,
        MovieId = @MovieId,
        UserName = @UserName, 
        TimeStamp=@CreatedAt
    WHERE CommentId = @CommentId;
END

GO
