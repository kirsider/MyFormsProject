using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyForms.Data.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string Qname { get; set; }
        public string Type { get; set; }
        public dynamic Options { get; set; }
    }
}
