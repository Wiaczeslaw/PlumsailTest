using System;
using System.Collections;
using System.Collections.Generic;
using PlumsailTest.Domain.Entities.Abstractions;

namespace PlumsailTest.Domain.Entities
{
    public class Form : DeletableEntity
    {
        public FormTemplate FormTemplate { get; set; }
        public Guid FormTemplateId { get; set; }
        
        public string Name { get; set; }
        public ICollection<FormItem> FormItems { get; set; }
    }
}