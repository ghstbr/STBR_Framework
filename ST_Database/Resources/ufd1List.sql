SELECT "TableID"
      ,"FieldID"
      ,"IndexID"
      ,"FldValue"
      ,"Descr"
      ,"FldDate"
  FROM UFD1 
  where "TableID" = '{0}' 
        and "FieldID" = (select "FieldID" from CUFD where "TableID" = '{0}' and "AliasID" = '{1}')