using Microsoft.EntityFrameworkCore;

namespace FCM.Repositories.EF.InMemory
{
    public class EFInMemoryFCMContext: FCMContext
    {
        protected override bool SupportsTransaction
        {
            get
            {
                return false;
            }
        }

        public EFInMemoryFCMContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseInMemoryDatabase();
        }
        
    }   
}
