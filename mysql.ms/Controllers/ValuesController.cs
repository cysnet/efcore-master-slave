using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using mysql.ms.DBContext;
using mysql.ms.Models;
using mysql.ms.Repository;
using Newtonsoft.Json;

namespace mysql.ms.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        IUserRepository _userRepository;
        IUserExtendRepository _userExtendRepository;
        IUnitOfWork _unitOfWork;
        public ValuesController(IUserRepository userRepository, IUserExtendRepository userExtendRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _userExtendRepository = userExtendRepository;
        }

        [HttpGet("get")]
        public string Get()
        {
            var data = from user in _userRepository.Queryable()
                       join ue in _userExtendRepository.Queryable()
                       on user.id equals ue.user_id
                       select new { user, ue };
            var result = data.ToList();
            return JsonConvert.SerializeObject(result);
        }

        [HttpGet("add")]
        public string Add()
        {
            var u = new user_merchant
            {
                id = Guid.NewGuid().ToString(),
                name = "cys",
                age = 26
            };

            var ue = new user_extend
            {
                id = Guid.NewGuid().ToString(),
                user_id = u.id,
                extend_info = "this is extend_info"
            };
            _userRepository.Add(u);
            _userExtendRepository.Add(ue);
            _unitOfWork.SaveChange();

            return "ok";
        }

    }
}
