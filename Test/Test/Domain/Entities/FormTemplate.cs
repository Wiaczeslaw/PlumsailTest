using System.Collections;
using System.Collections.Generic;
using Test.Domain.Entities.Abstractions;

namespace Test.Domain.Entities
{
    public class FormTemplate : DeletableEntity
    {
        public string Name { get; set; }
        public ICollection<FormItemTemplate> ItemTemplates { get; set; }
    }
}