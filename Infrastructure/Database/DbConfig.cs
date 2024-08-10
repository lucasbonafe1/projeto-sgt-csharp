using Microsoft.Extensions.Configuration;

namespace SGT.Infrastructure.Data
{
    public class DbConfig 
    {
        public static string ConnectionString {  get; set; }
        
        public static void Initialize(IConfiguration configuration)
        {
            ConnectionString = configuration.GetConnectionString("SqlConnection");
        }
    }
}
