using System;
using System.Collections;
using System.Collections.Generic;
using Test.Domain.Entities.Abstractions;
using Test.Domain.Enums;

namespace Test.Domain.Entities
{
    public class FormItemTemplate : DeletableEntity
    {
        public FormTemplate FormTemplate { get; set; }
        public Guid FormTemplateId { get; set; }
        
        public string Name { get; set; }
        public int Order { get; set; }
        public FormItemType Type { get; set; }
        
        public ICollection<FormItemSelectValue> Values { get; set; }
    }
}