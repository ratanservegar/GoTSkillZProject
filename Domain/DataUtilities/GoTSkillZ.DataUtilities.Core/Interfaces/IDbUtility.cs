using System.Collections.Generic;
using System.Data;
using GoTSkillZ.DataUtilities.Core.Attribute;

namespace GoTSkillZ.DataUtilities.Core.Interfaces
{
    public interface IDbUtility
    {
        int CheckIfExists(string tableName, string columnName, string value);
        DataSet RunStoredProcedure(string storeProc, List<SqlParameterDTO> parameters);

        DataTable RunSqlQuery(string query);


    }
}