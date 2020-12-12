using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyForms.Data.Models
{
    public class Form
    {
        public int Id { get; set; }
        public string Fname { get; set; }
        public string Description { get; set; }
        public string Uid { get; set; }

        public Question[] Questions { get; set; }
    }
}
