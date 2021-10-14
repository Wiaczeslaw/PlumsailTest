using System;
using PlumsailTest.Domain.Entities.Abstractions;

namespace PlumsailTest.Domain.Entities
{
    public class FormItem : DeletableEntity
    {
        public FormItemTemplate FormItemTemplate { get; set; }
        public Guid FormItemTemplateId { get; set; }
        
        public Form Form { get; set; }
        public Guid FormId { get; set; }
        
        public string Value { get; set; }
        
        public FormItemSelectValue FormItemSelectValue { get; set; }
        public Guid? FormItemSelectValueId { get; set; }
    }
}