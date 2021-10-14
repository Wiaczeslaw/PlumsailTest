using System;
using System.Collections.Generic;
using PlumsailTest.Domain.Enums;

namespace PlumsailTest.Domain.Models
{
    public class FormItemTemplateModel
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
        public FormItemType Type { get; set; }

        public IEnumerable<FormItemSelectValueModel> Values { get; set; }
    }
}