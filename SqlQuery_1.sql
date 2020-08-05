/* 
 * SQL Server Script
 * 
 * In a local environment (for example, with the SQLServerExpress instance 
 * included in the VStudio installation) it will be necessary to create the 
 * database and the user required by the connection string. So, the following
 * steps are needed:
 *
 *     Configure the @Default_DB_Path variable with the path where 
 *     database and log files will be created  
 *
 * This script can be executed from MS Sql Server Management Studio Express,
 * but also it is possible to use a command Line syntax:
 *
 *    > sqlcmd.exe -U [user] -P [password] -I -i SqlServerCreateTables.sql
 *
 */


 /******************************************************************************/
 /*** PATH to store the db files. This path must exists in the local system. ***/
 /******************************************************************************/
 DECLARE @Default_DB_Path as VARCHAR(64)  
 SET @Default_DB_Path = N'C:\bd\'
 
USE [master]


/***** Drop database if already exists  ******/
IF  EXISTS (SELECT name FROM sys.databases WHERE name = 'practica_db')
DROP DATABASE [practica_db]


USE [master]


/* DataBase Creation */

	                              
DECLARE @sql nvarchar(500)

SET @sql = 
  N'CREATE DATABASE [practica_db] 
    ON PRIMARY ( NAME = practica_db, FILENAME = "' + @Default_DB_Path + N'practica_db.mdf")
    LOG ON ( NAME = practica_db_log, FILENAME = "' + @Default_DB_Path + N'practica_db_log.ldf")'

EXEC(@sql)
PRINT N'Database created.'
GO


/* 
 * Drop tables.                                                             
 * NOTE: before dropping a table (when re-executing the script), the tables 
 * having columns acting as foreign keys of the table to be dropped must be 
 * dropped first (otherwise, the corresponding checks on those tables could 
 * not be done).                                                            
 */

USE practica_db



/* Drop Table Category if already exists */

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[Category]') 
AND type in ('U')) DROP TABLE [Category]
GO


/* Drop Table RecommendationGroup if already exists */

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[RecommendationGroup]') 
AND type in ('U')) DROP TABLE [RecommendationGroup]
GO


/* Drop Table Recommendation if already exists */

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[Recommendation]') 
AND type in ('U')) DROP TABLE [Recommendation]
GO


/* Drop Table Comment if already exists */

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[Comment]') 
AND type in ('U')) DROP TABLE [Comment]
GO

/* Drop Table TagComment if already exists */

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[TagComment]') 
AND type in ('U')) DROP TABLE [TagComment]
GO


/* Drop Table Tag if already exists */

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[Tag]') 
AND type in ('U')) DROP TABLE [Tag]
GO



/* Drop Table GroupUsersUserProfile if already exists */

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[GroupUsersUserProfile]') 
AND type in ('U')) DROP TABLE [GroupUsersUserProfile]
GO


/* Drop Table GroupUsers if already exists */

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[GroupUsers]') 
AND type in ('U')) DROP TABLE [GroupUsers]
GO


/*  Drop Table UserProfile if already exists  */

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[UserProfile]') AND type in ('U'))
DROP TABLE [UserProfile]
GO


/* Drop Table SportEvent if already exists */

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[SportEvent]') 
AND type in ('U')) DROP TABLE [SportEvent]
GO



 /***
 * Create tables.
 * UserProfile table is created. Indexes required for the 
 * most common operations are also defined.
 ****/

   
/* TABLE: UserProfile 
   Table which stores User's personal data
*/

CREATE TABLE UserProfile (
	userId bigint IDENTITY(1,1) NOT NULL,
	loginName varchar(30) NOT NULL,
	enPassword varchar(50) NOT NULL,
	firstName varchar(30) NOT NULL,
	lastName varchar(40) NOT NULL,
	email varchar(60) NOT NULL,
	language varchar(2) NOT NULL,
	country varchar(2) NOT NULL,

	CONSTRAINT [PK_UserProfile] PRIMARY KEY (userId),
	CONSTRAINT [UniqueKey_Login] UNIQUE (loginName)
)
CREATE NONCLUSTERED INDEX [IX_UserProfileIndexByLoginName]
ON [UserProfile] ([loginName] ASC)

PRINT N'Table UserProfile created.'
GO



/* TABLE: Category 
   Table which storesCategories about events
*/

CREATE TABLE Category (
	categoryName varchar(25) NOT NULL,
	

    CONSTRAINT [PK_Category] PRIMARY KEY (categoryName),
) 

PRINT N'Table Category created.'
GO

/* TABLE: SportEvent 
   Table which stores Event's info
*/

CREATE TABLE SportEvent (
	eventId bigint IDENTITY(1,1) NOT NULL,
	ev_name varchar(25) NOT NULL,
	ev_date datetime NOT NULL,
	ev_description varchar(150),
	ev_category varchar(25) NOT NULL,
	

    CONSTRAINT [PK_SportEvent] PRIMARY KEY (eventId ASC),
	CONSTRAINT [FK_SportEventCategory] FOREIGN KEY (ev_category)
	 REFERENCES Category (categoryName) ON DELETE NO ACTION,
) 

PRINT N'Table SportEvent created.'
GO


/* TABLE: GroupUsers 
   Table which stores information about Groups of Users
*/

CREATE TABLE GroupUsers (
	group_usersId bigint IDENTITY(1,1) NOT NULL,
	gr_name varchar(25) NOT NULL,
	gr_description varchar(150),
	gr_owner bigint NOT NULL,

    CONSTRAINT [PK_GroupUsers] PRIMARY KEY (group_usersId ASC),
	
	CONSTRAINT [FK_GroupUsersOwner] FOREIGN KEY(gr_owner)
        REFERENCES UserProfile (userId) ON DELETE CASCADE,
) 

PRINT N'Table GroupUsers created.'
GO


/* TABLE: GroupUsersUserProfile 
   Table used in Relation between Group of Users and User Profile to Store who belongs to each group.
   One user could be in some groups.
*/

CREATE TABLE GroupUsersUserProfile(
	
	user_profileId bigint NOT NULL,
	groupId bigint NOT NULL,

    CONSTRAINT [PK_GroupUsersUserProfile] PRIMARY KEY (user_profileId,groupId ),

	CONSTRAINT [FK_GroupUsersUserProfileUserId] FOREIGN KEY(user_profileId)
        REFERENCES UserProfile (userId) ON DELETE NO ACTION,

	CONSTRAINT [FK_GroupUsersUserProfileGroupId] FOREIGN KEY(groupId)
        REFERENCES GroupUsers (group_usersId) ON DELETE CASCADE,
) 

PRINT N'Table GroupUsersUserProfile created.'
GO


/* TABLE: Comment 
   Table which stores information about Comment
*/

CREATE TABLE Comment(
	commentId bigint IDENTITY(1,1) NOT NULL,
	ownerId bigint NOT NULL,
	eventId bigint NOT NULL,
	publishDate datetime2 NOT NULL,
	comment_description varchar(200) NOT NULL,


    CONSTRAINT [PK_Comment] PRIMARY KEY (commentId ASC),

	CONSTRAINT [FK_CommentUserId] FOREIGN KEY(ownerId)
        REFERENCES UserProfile (userId) ON DELETE CASCADE,

	CONSTRAINT [FK_CommentSportEventId] FOREIGN KEY(eventId)
        REFERENCES SportEvent (eventId) ON DELETE CASCADE,
) 

PRINT N'Table Comment created.'
GO


/* TABLE: Tag 
   Table which stores information about Tag
*/

CREATE TABLE Tag (
	
	tagName varchar(30) NOT NULL,
	

    CONSTRAINT [PK_Tag] PRIMARY KEY (tagName),

) 

PRINT N'Table Tag created.'
GO


/* TABLE: TagComment 
   Table which stores Relation between Tags and Comments.
   One comment can have some Tags.
*/

CREATE TABLE TagComment (
	
	tagName varchar(30) NOT NULL,
	commentId bigint NOT NULL,

    CONSTRAINT [PK_TagComment] PRIMARY KEY (tagName, commentId),
	
	CONSTRAINT [FK_TagCommentTagId] FOREIGN KEY(tagName)
        REFERENCES Tag (tagName) ON DELETE NO ACTION,
	
	CONSTRAINT [FK_TagCommentcommentId] FOREIGN KEY(commentId)
        REFERENCES Comment (commentId) ON DELETE CASCADE,
) 



PRINT N'Table TagComment created.'
GO


/* TABLE: Recommendation 
   Table which stores information about Recommendation from User to one Event.
*/

CREATE TABLE Recommendation(
	recommendationId bigint IDENTITY(1,1) NOT NULL,
	userId bigint NOT NULL,
	eventId bigint NOT NULL,
	recommendation_text varchar(300) NOT NULL,


    CONSTRAINT [PK_Recommendation] PRIMARY KEY (recommendationId ASC),

	CONSTRAINT [FK_RecommendationUserId] FOREIGN KEY(userId)
        REFERENCES UserProfile (userId) ON DELETE CASCADE,

	CONSTRAINT [FK_Recommendation_EventId] FOREIGN KEY(eventId)
        REFERENCES SportEvent (eventId) ON DELETE CASCADE,

	
)

PRINT N'Table Recommendation created.'
GO


/* TABLE: RecommendationGroup 
   Table which stores information about Relation between Recomendation and Group.
   Users can sent Recommendation in several groups.
*/

CREATE TABLE RecommendationGroup(
	
	recommendationId bigint NOT NULL,
	group_usersId bigint NOT NULL,

    CONSTRAINT [PK_RecommendationGroup] PRIMARY KEY (recommendationId, group_usersId),

	CONSTRAINT [FK_RecommendationGroupRecomendationId] FOREIGN KEY(recommendationId)
        REFERENCES Recommendation (recommendationId) ON DELETE CASCADE,

    CONSTRAINT [FK_RecommendationGroupGroupUsersId] FOREIGN KEY(group_usersId)
        REFERENCES GroupUsers (group_usersId) ON DELETE NO ACTION, 
) 

PRINT N'Table RecommendationGroup created.'
GO









PRINT N'Done'