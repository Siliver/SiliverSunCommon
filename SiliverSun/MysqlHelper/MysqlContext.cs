using Elastic.Apm.Api;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SiliverSun.Model;

namespace SiliverSun.MysqlHelper
{
    public class MysqlContext : DbContext
    {
        private readonly DbContextOptions<MysqlContext> _options;

        private const string DefaultMySqlConnectionString = "server=localhost;userid=root;pwd=123456;port=3306;database=test;";

        public MysqlContext() { }

        public MysqlContext(DbContextOptions<MysqlContext> options) : base(options)
        {
            _options = options;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseMySQL(DefaultMySqlConnectionString);
        }

        public DbSet<hotels> hotels { get; set; }
    }
}
