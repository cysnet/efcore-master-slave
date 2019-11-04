using mysql.ms.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mysql.ms.Repository
{
    public interface IUnitOfWork
    {
        bool SaveChange();
    }

    public class UnitOfWork : IUnitOfWork
    {

        MySqlMasterDbContext _mySqlMasterDbContext;
        public UnitOfWork(MySqlMasterDbContext mySqlMasterDbContext)
        {
            _mySqlMasterDbContext = mySqlMasterDbContext;
        }
        public bool SaveChange()
        {
            return _mySqlMasterDbContext.SaveChanges() > 0;
        }
    }

}
