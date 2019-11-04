using Microsoft.EntityFrameworkCore;
using mysql.ms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mysql.ms.DBContext
{
    /// <summary>
    /// 主数据库
    /// </summary>
    public class MySqlMasterDbContext : DbContext
    {
        public MySqlMasterDbContext(DbContextOptions<MySqlMasterDbContext> options) : base(options)
        {
            Console.WriteLine(" Master Init ");
        }


        public override void Dispose()
        {
            Console.WriteLine(" Master Dispose ");
            base.Dispose();
        }

        public DbSet<user_merchant> user_merchant { get; set; }

        public DbSet<user_extend> user_extend { get; set; }
    }

    /// <summary>
    /// 从数据库
    /// </summary>
    public class MySqlSlaveDbContext : MySqlMasterDbContext
    {

        private string _conn;
        public MySqlSlaveDbContext(DbContextOptions<MySqlMasterDbContext> options) : base(options)
        {
            Console.WriteLine(" Slave Init ");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //TODO 算法mysql串获取可以随机
            if (!string.IsNullOrEmpty(_conn))
            {
                optionsBuilder.UseMySql(_conn);
            }
            base.OnConfiguring(optionsBuilder);
        }

        public override void Dispose()
        {
            Console.WriteLine(" Slave Dispose ");
            base.Dispose();
        }
    }
}
