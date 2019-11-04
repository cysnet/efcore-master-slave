
using mysql.ms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mysql.ms.Repository
{
    public interface IUserRepository : IRespository<user_merchant> { }

    public class UserRepository : BaseRespository<user_merchant>, IUserRepository
    {
        public UserRepository(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }
}
