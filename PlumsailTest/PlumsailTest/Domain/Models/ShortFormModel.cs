using System;

namespace PlumsailTest.Domain.Models
{
    public class ShortFormModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid TemplateId { get; set; }
        public string TemplateName { get; set; }
    }
}