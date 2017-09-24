using Microsoft.EntityFrameworkCore;

namespace FCM.Repositories.EF.SQLServer
{
    public class SQLServerFCMContext: FCMContext
    {
        protected readonly string connectionString;

        protected override bool SupportsTransaction
        {
            get
            {
                return true;
            }
        }

        public SQLServerFCMContext(string connectionString)
        {
            this.connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(connectionString);
        }
        
    }   
}
