using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyForms.Data.Models
{
    public class QuestionPostRequest
    {
        public string QName { get; set; }
        public string Type { get; set; }
        public string[] Options { get; set; }
    }
}
