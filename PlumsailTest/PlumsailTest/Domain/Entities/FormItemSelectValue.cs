using System;
using PlumsailTest.Domain.Entities.Abstractions;

namespace PlumsailTest.Domain.Entities
{
    public class FormItemSelectValue : DeletableEntity
    {
        public FormItemTemplate FormItemTemplate { get; set; }
        public Guid FormItemTemplateId { get; set; }
        
        public string Value { get; set; }
    }
}