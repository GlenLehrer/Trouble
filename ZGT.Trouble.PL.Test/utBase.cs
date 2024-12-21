using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ZGT.Trouble.PL.Test
{
    [TestClass]
    public class utBase<T> where T : class
    {
        protected TroubleEntities dc;
        protected IDbContextTransaction transaction;
        private IConfigurationRoot _configuration;
        private DbContextOptions<TroubleEntities> options;

        public utBase()
        {
            var builder = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile("appsettings.json");

            _configuration = builder.Build();
            options = new DbContextOptionsBuilder<TroubleEntities>()
                .UseSqlServer(_configuration.GetConnectionString("TroubleGameConnection"))
                .UseLazyLoadingProxies()
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

        public virtual List<T> LoadTest()
        {
            return dc.Set<T>().ToList();

        }

        public int InsertTest(T row)
        {
            dc.Set<T>().Add(row);
            return dc.SaveChanges();
        }
        public int UpdateTest(T row)
        {
            dc.Entry(row).State = EntityState.Modified;
            return dc.SaveChanges();
        }
        public int DeleteTest(T row)
        {
            dc.Set<T>().Remove(row);
            return dc.SaveChanges();
        }


    }
}
