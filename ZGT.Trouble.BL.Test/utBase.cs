using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ZGT.Trouble.BL.Test
{
    [TestClass]
    public class utBase
    {
        protected TroubleEntities dc;  //Declare the DataContext
        protected IDbContextTransaction transaction;
        private IConfigurationRoot _configuration;
        protected DbContextOptions<TroubleEntities> options;

        public utBase()
        {
            var builder = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile("appsettings.json");

            _configuration = builder.Build();
            options = new DbContextOptionsBuilder<TroubleEntities>()
                .UseSqlServer(_configuration.GetConnectionString("TroubleGameConnection"))
                //.UseLazyLoadingProxies()
                .Options;

            dc = new TroubleEntities(options);
        }

        [TestInitialize]
        public void Initialize()
        {
            transaction = dc.Database.BeginTransaction();
        }
        [TestCleanup]
        public void Cleanup()
        {
            transaction.Rollback();
            transaction.Dispose();
            dc = null;
        }
    }
}