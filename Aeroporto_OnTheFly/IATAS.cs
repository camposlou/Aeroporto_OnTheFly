using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aeroporto_OnTheFly
{
    internal class IATAS
    {
        private static InternalControlDB Conn = new InternalControlDB();
        private static SqlConnection Conecxaosql = new SqlConnection(Conn.AbrirConexao());




    }
}
