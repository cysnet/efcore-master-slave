using mysql.ms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mysql.ms.Repository
{
    public interface IUserExtendRepository : IRespository<user_extend> { }

    public class UserExtendRepository : BaseRespository<user_extend>, IUserExtendRepository
    {
        public UserExtendRepository(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
    }


}
