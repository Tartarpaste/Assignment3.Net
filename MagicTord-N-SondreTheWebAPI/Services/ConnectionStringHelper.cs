
namespace MagicTord_N_SondreTheWebAPI.Services
{
    public class ConnectionStringHelper
    {

        public static string getConnectionString()
        {
            string connection = "Server=N-NO-01-03-8890\\SQLEXPRESS;Database=THENCU;Trusted_Connection=True;TrustServerCertificate=True;";
            return connection;
        }
    }
}
