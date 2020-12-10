using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyForms.Data.Models
{
    public class Form
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }

        public IEnumerable<Question> Questions { get; set; }
    }
}
