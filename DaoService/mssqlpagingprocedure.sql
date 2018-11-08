USE [TAS]
GO
/****** Object:  StoredProcedure [dbo].[Com_Pagination]    Script Date: 2018-09-11 15:44:30 ******/
/****** Limeng created *******/
/****** For MSSQL *******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[Com_Pagination]
@TotalCount INT OUTPUT, --总记录数
@TotalPage INT OUTPUT, --总页数
@Table NVARCHAR(1000), --查询的表名（可多表，例如：Person p LEFT JOIN TE a ON a.PID=p.Id ）
@Column NVARCHAR(1000), --查询的字段，可多列或者为*
@OrderColumn NVARCHAR(100), --排序字段
@GroupColumn NVARCHAR(150), --分组字段
@PageSize INT, --每页记录数
@CurrentPage INT, --当前页数
@Group TINYINT, --是否使用分组，否是
@Condition NVARCHAR(4000) --查询条件（注意：若这时候为多表查询，这里也可以跟条件，例如：a.pid=2）
AS
DECLARE @PageCount   INT, --总页数
    @strSql    NVARCHAR(4000), --主查询语句
    @strTemp    NVARCHAR(2000), --临时变量
    @strCount   NVARCHAR(1000), --统计语句
    @strOrderType NVARCHAR(1000) --排序语句
BEGIN
SET @PageCount = @PageSize * (@CurrentPage -1)
SET @strOrderType = ' ORDER BY ' + @OrderColumn + ' '
IF @Condition != ''
BEGIN
  IF @CurrentPage = 1
  BEGIN
    IF @GROUP = 1
    BEGIN
      SET @strCount = 'SELECT @TotalCount=COUNT(*) FROM ' + @Table
        + ' WHERE ' + @Condition + ' GROUP BY ' + @GroupColumn
      SET @strCount = @strCount + ' SET @TotalCount=@@ROWCOUNT'
      SET @strSql = 'SELECT TOP ' + STR(@PageSize) + ' ' + @Column
        + ' FROM ' + @Table + ' WHERE ' + @Condition + 
        ' GROUP BY ' + @GroupColumn + ' ' + @strOrderType
    END
    ELSE
    BEGIN
      SET @strCount = 'SELECT @TotalCount=COUNT(*) FROM ' + @Table
        + ' WHERE ' + @Condition
      SET @strSql = 'SELECT TOP ' + STR(@PageSize) + ' ' + @Column
        + ' FROM ' + @Table + ' WHERE ' + @Condition + ' ' + @strOrderType
    END
  END
  ELSE
  BEGIN
    IF @GROUP = 1
    BEGIN
      SET @strCount = 'SELECT @TotalCount=COUNT(*) FROM ' + @Table
        + ' WHERE ' + @Condition + ' GROUP BY ' + @GroupColumn
      SET @strCount = @strCount + ' SET @TotalCount=@@ROWCOUNT'
      SET @strSql = 'SELECT * FROM (SELECT TOP (2000) ' + @Column
        + ',ROW_NUMBER() OVER(' + @strOrderType + 
        ') AS NUM FROM ' + @Table + ' WHERE ' + @Condition + 
        ' GROUP BY ' + @GroupColumn + 
        ') AS T WHERE NUM BETWEEN ' + STR(@PageCount + 1) + 
        ' AND ' + STR(@PageCount + @PageSize)
    END
    ELSE
    BEGIN
      SET @strCount = 'SELECT @TotalCount=COUNT(*) FROM ' + @Table
        + ' WHERE ' + @Condition
      SET @strSql = 'SELECT * FROM (SELECT TOP (2000) ' + @Column
        + ',ROW_NUMBER() OVER(' + @strOrderType + 
        ') AS NUM FROM ' + @Table + ' WHERE ' + @Condition + 
        ') AS T WHERE NUM BETWEEN ' + STR(@PageCount + 1) + 
        ' AND ' + STR(@PageCount + @PageSize)
    END
  END
END
ELSE
  --没有查询条件
BEGIN
  IF @CurrentPage = 1
  BEGIN
    IF @GROUP = 1
    BEGIN
      SET @strCount = 'SELECT @TotalCount=COUNT(*) FROM ' + @Table
        + ' GROUP BY ' + @GroupColumn
      SET @strCount = @strCount + 'SET @TotalCount=@@ROWCOUNT'
      SET @strSql = 'SELECT TOP ' + STR(@PageSize) + ' ' + @Column
        + ' FROM ' + @Table + ' GROUP BY ' + @GroupColumn + ' ' + 
        @strOrderType
    END
    ELSE
    BEGIN
      SET @strCount = 'SELECT @TotalCount=COUNT(*) FROM ' + @Table
      SET @strSql = 'SELECT TOP ' + STR(@PageSize) + ' ' + @Column
        + ' FROM ' + @Table + ' ' + @strOrderType
    END
  END
  ELSE
  BEGIN
    IF @GROUP = 1
    BEGIN
      SET @strCount = 'SELECT @TotalCount=COUNT(*) FROM ' + @Table
        + ' GROUP BY ' + @GroupColumn
      SET @strCount = @strCount + 'SET @TotalCount=@@ROWCOUNT'
      SET @strSql = 'SELECT * FROM (SELECT TOP (2000) ' + @Column
        + ',ROW_NUMBER() OVER(' + @strOrderType + 
        ') AS NUM FROM ' + @Table + ' GROUP BY ' + @GroupColumn + 
        ') AS T WHERE NUM BETWEEN ' + STR(@PageCount + 1) + 
        ' AND ' + STR(@PageCount + @PageSize)
    END
    ELSE
    BEGIN
      SET @strCount = 'SELECT @TotalCount=COUNT(*) FROM ' + @Table
      SET @strSql = 'SELECT * FROM (SELECT TOP (2000) ' + @Column
        + ',ROW_NUMBER() OVER(' + @strOrderType + 
        ') AS NUM FROM ' + @Table + ') AS T WHERE NUM BETWEEN ' + 
        STR(@PageCount + 1) + ' AND ' + STR(@PageCount + @PageSize)
    END
  END
END
EXEC sp_executesql @strCount,
   N'@TotalCount INT OUTPUT',
   @TotalCount OUTPUT
IF @TotalCount > 2000
BEGIN
  SET @TotalCount = 2000
END
IF @TotalCount%@PageSize = 0
BEGIN
  SET @TotalPage = @TotalCount / @PageSize
END
ELSE
BEGIN
  SET @TotalPage = @TotalCount / @PageSize + 1
END
SET NOCOUNT ON
EXEC (@strSql)
END
SET NOCOUNT OFF