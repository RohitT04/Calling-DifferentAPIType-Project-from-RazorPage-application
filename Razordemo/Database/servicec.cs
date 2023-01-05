using Dapper;
using System.Data;

namespace Razordemo.Database;

public class servicec : BaseRepository
{
    public servicec()
            : base()
    {
    }
    public LoginModel1 hi(string username, string password)
    {
        return _db.Query<LoginModel1>("sp_login", new { username, password },
                  commandType: CommandType.StoredProcedure).First();
    }
}
