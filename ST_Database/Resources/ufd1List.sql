SELECT [TableID]
      ,[FieldID]
      ,[IndexID]
      ,[FldValue]
      ,[Descr]
      ,[FldDate]
  FROM [UFD1] 
  where tableid = '{0}' 
        and fieldid = (select FieldID from CUFD where TableID = '{0}' and AliasID = '{1}')