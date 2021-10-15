using System;
using System.Collections.Generic;
using PlumsailTest.Domain.Enums;

namespace PlumsailTest.Domain.Models
{
    public class FormItemModel
    {
        public Guid Id { get; set; }
        public Guid TemplateId { get; set; }
        public string Value { get; set; }
        public Guid? SelectValueId { get; set; }
    }
}