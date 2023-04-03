using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for IDCreator
/// </summary>

public class IDCreator
{
    public IDCreator()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public string GenerateIDs(string _prefix)
    {
        string ID = string.Empty;
        using (SqlConnection _objConnection = DataConnection.GetConnection())
        {
            using (SqlCommand _objCommand = new SqlCommand())
            {

                _objCommand.CommandType = CommandType.StoredProcedure;
                _objCommand.CommandText = "Genrate_IDs";
                _objCommand.Connection = _objConnection;
                _objCommand.Parameters.Add("@STRBOOKREFNO", SqlDbType.NVarChar, 25).Direction = ParameterDirection.Output;
                _objCommand.Parameters.Add("@INTBOOKREFSuffix", SqlDbType.Decimal).Direction = ParameterDirection.Output;
                _objCommand.Parameters.Add("@STRPREFIX", SqlDbType.NVarChar, 25).Value = _prefix;
                _objConnection.Open();
                _objCommand.ExecuteNonQuery();

                ID = (_objCommand.Parameters["@STRBOOKREFNO"].Value.ToString() + _objCommand.Parameters["@INTBOOKREFSuffix"].Value.ToString());

                _objConnection.Close();
                return ID;

            }
        }
    }
}