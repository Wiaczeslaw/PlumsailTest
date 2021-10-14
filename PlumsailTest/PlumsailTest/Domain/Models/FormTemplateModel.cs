using System;
using System.Collections;
using System.Collections.Generic;

namespace PlumsailTest.Domain.Models
{
    public class FormTemplateModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<FormItemTemplateModel> Items { get; set; }
    }
}