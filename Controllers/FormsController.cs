using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyForms.Data;
using MyForms.Data.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

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
            Form form = _dataRepository.GetForm(formId);
            return form;
        }

        [HttpGet("userforms/{uid}")]
        public IEnumerable<Form> GetUserForms(string uid)
        {
            return _dataRepository.GetUserForms(uid);
        }

        [HttpGet("{formId}/results")]
        public IEnumerable<Result> GetFormResults(int formId)
        {
            return _dataRepository.GetFormResults(formId);
        }

        [HttpPost]  
        public ActionResult<Form> PostForm([FromBody] FormPostRequest form)
        {
            var savedForm = _dataRepository.PostForm(form);
            return CreatedAtAction("PostForm", savedForm);
        }

        [HttpPost("results")] 
        public ActionResult<Result> PostResult([FromBody] ResultPostRequest result)
        {
            var savedResult = _dataRepository.PostResult(result);
            return CreatedAtAction("PostResult", savedResult);
        }

    }
}
