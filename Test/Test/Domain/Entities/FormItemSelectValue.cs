using System;
using Test.Domain.Entities.Abstractions;

namespace Test.Domain.Entities
{
    public class FormItemSelectValue : DeletableEntity
    {
        public FormItemTemplate FormItemTemplate { get; set; }
        public Guid FormItemTemplateId { get; set; }
        
        public string Value { get; set; }
    }
}