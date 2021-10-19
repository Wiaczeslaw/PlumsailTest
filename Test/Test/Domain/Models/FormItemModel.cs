using System;
using System.Collections.Generic;
using Test.Domain.Enums;

namespace Test.Domain.Models
{
    public class FormItemModel
    {
        public Guid Id { get; set; }
        public Guid TemplateId { get; set; }
        public string Value { get; set; }
        public Guid? SelectValueId { get; set; }
    }
}