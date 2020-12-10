using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyForms.Data;
using MyForms.Data.Models;

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

        [HttpGet("{formId}")]
        public ActionResult<Form> GetForm(int formId)
        {
            return _dataRepository.GetForm(formId);
        }

        [HttpGet("{formId}/results")]
        public IEnumerable<Result> GetFormResults(int formId)
        {
            return _dataRepository.GetFormResults(formId);
        }

    }
}
