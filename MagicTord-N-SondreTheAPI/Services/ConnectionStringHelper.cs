using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicTord_N_SondreTheAPI.Services
{
    public class ConnectionStringHelper
    {

        public static string getConnectionString()
        {
            string connection = "Server=N-NO-01-01-4694\\SQLEXPRESS;Database=THENCU;Trusted_Connection=True;TrustServerCertificate=True;";
            return connection;
        }
    }
}
