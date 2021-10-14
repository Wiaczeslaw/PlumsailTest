using System.Collections;
using System.Collections.Generic;
using PlumsailTest.Domain.Entities.Abstractions;

namespace PlumsailTest.Domain.Entities
{
    public class FormTemplate : DeletableEntity
    {
        public string Name { get; set; }
        public ICollection<FormItemTemplate> ItemTemplates { get; set; }
    }
}