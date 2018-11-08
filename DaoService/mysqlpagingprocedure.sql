DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sp_pager`(
 in tablename varchar(8000) ,         #-表名

 in fields longtext ,                 #-字段名(全部字段为*)

 in orderfield varchar(1000) ,        #-排序字段(必须!支持多字段)

 in sqlwhere longtext ,               #-条件语句(不用加where)

 in groupfield varchar(1000) ,        #-分组

 in pagesize int ,                    #-每页多少条记录

 in pageindex int,                    #-指定当前为第几页

 in sqlcounttable varchar(1000) ,     #-指定查询总记录数

 in paging bit ,			          #-是否分页

 out totalpage int)
    COMMENT '分页查询存储过程'
BEGIN

	declare exSql longtext;

	declare totalrecord int;   #总记录数

    declare grouptablename varchar(2500);

	#group条件是否存在

	IF(groupfield <> '' and groupfield is not null) then

		

			#where条件是否存在

			IF ( sqlwhere  = '' or sqlwhere is null) then

				set grouptablename  = concat('(SELECT count(*) num from ' , tablename  , ' group by ' , groupfield , ' ) records');

			else 

				set grouptablename  = concat('(SELECT count(*) num from ' , tablename  , ' WHERE ' , sqlwhere  , ' group by ' , groupfield ,' ) records');

			end if;



			set @exSql  = concat( 'select count(*) totalrecord into @totalrecord from ' , grouptablename );



	elseif (sqlwhere = '' or sqlwhere is null) then

			#where条件是否存在

			#计算总记录数

			set @exSql  = concat( 'select count(*) totalrecord into @totalrecord from ' , tablename );

	else 

			set @exSql  = concat( 'select count(*) totalrecord into @totalrecord from ' , tablename  , ' WHERE ' , sqlwhere); 

	end if;



	#计算总记录数 

	PREPARE stmt1 FROM @exSql;

	EXECUTE stmt1;



	#计算总数量

    SET totalpage = @totalrecord;

	#SELECT @totalpage ; #ceiling(( totalrecord,0.0)/pagesize),1



	set @exSql = '';

    SET @exSql = concat('SELECT ', fields, ' FROM ' , tablename);



	#追加查询条件

	IF ( sqlwhere IS NOT NULL AND sqlwhere<>'') THEN

		SET @exSql = concat(@exSql , ' WHERE ' , sqlwhere);

	END IF;



	#追加Group By

	IF ( groupfield IS NOT NULL AND groupfield<>'') THEN

		SET @exSql = concat(@exSql , ' GROUP BY ' , groupfield);

	END IF;



	#追加排序字段

	IF ( orderfield IS NOT NULL AND orderfield<>'') THEN

		SET @exSql = concat(@exSql , ' ORDER BY ' , orderfield);

	END IF;



	#处理页数超出范围情况

	IF (pagesize IS NULL OR pagesize  <= 0) THEN

		SET pagesize = 10;

	END IF;

	IF (pageindex IS NULL OR pageindex  <= 1) THEN 

		SET pageindex = 0; #mysql分页从0开始

	ELSEIF (pageindex > totalpage) THEN

		SET pageindex = totalpage;

	ELSE

		SET pageindex = pagesize * (pageindex - 1) ;

	END IF;


	IF paging = 1 THEN

		SET @exSql = concat(@exSql , ' LIMIT ' , pageindex , ',' , pagesize);
	END IF;


	#SELECT @exSql;

	PREPARE stmt2 FROM @exSql;

	EXECUTE stmt2;



END$$
DELIMITER ;
