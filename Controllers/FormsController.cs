using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyForms.Data;

namespace MyForms.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormsController : ControllerBase
    {
        private readonly IDataRepository _dataRepository;

        public FormsController(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }

        [HttpGet("count")]
        public IEnumerable<dynamic> GetFormsCount()
        {
            return _dataRepository.GetFormsNumber();
        }
    }
}
