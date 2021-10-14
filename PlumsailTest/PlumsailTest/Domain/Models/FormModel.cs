using System;
using System.Collections;
using System.Collections.Generic;

namespace PlumsailTest.Domain.Models
{
    public class FormModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<FormItemModel> Items { get; set; }
    }
}