using Microsoft.Data.SqlClient;
using System.Data;

namespace Razordemo.Database;

public class BaseRepository
{
    public IDbConnection _db;
    public IDbConnection _db1;


    public BaseRepository()
    {
        _db = new SqlConnection("Data Source=DESKTOP-24V1S4R\\MSSQLSERVER01;Initial Catalog=LoginDB;TrustServerCertificate=True;Integrated Security=SSPI;");
        _db1 = new SqlConnection("Server=DESKTOP-24V1S4R\\MSSQLSERVER01;Database=Profile_DB;TrustServerCertificate=True;Integrated Security=SSPI;");


    }
}
