﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyForms.Data.Models
{
    public class FormPostRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }
        public QuestionPostRequest[] Questions { get; set; }
    }
}
