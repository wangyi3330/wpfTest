using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Carrot.DBUtility
{
    public class AccountDAL : DALHelper
    {
        public static DataTable GetAccount_Colum(int Account_ColumTypeID, string AccountID)
        {
            string sqlStr = @"declare @sql varchar(8000) set @sql = 'select AccountColumRelationGUID ' select @sql = @sql + ' , max(case AccountColumnName when ''' + AccountColumnName + ''' then AccountValue else ''0'' end) [' + AccountColumnName + ']' FROM (SELECT distinct TOP 1000000  T_Account_Colum.AccountColumnName,AccountColumnOrder from T_Account_Colum_Relation LEFT JOIN T_Account_Colum 
on T_Account_Colum.AccountColumnID=T_Account_Colum_Relation.AccountColumnID  where AccountColumTypeID=" + Account_ColumTypeID + @" ORDER BY AccountColumnOrder ASC) AS A SET @sql = @sql + ' from  T_Account_Colum_Relation left join T_Account_Colum ON T_Account_Colum.AccountColumnID=T_Account_Colum_Relation.AccountColumnID WHERE AccountColumTypeID=" + Account_ColumTypeID + @" AND T_Account_Colum_Relation.AccountID=''" + AccountID + @"'' group by AccountColumRelationGUID,AccountColumOrder Order By AccountColumOrder Asc' exec(@sql)";
            return ExecuteDataSet(connString, CommandType.Text, sqlStr).Tables[0];
        }
    }
}
