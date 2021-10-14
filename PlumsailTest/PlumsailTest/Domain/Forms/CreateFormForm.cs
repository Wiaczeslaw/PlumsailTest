using System;
using System.Collections;
using System.Collections.Generic;

namespace PlumsailTest.Domain.Forms
{
    public class CreateFormForm
    {
        public string Name { get; set; }
        public Guid TemplateId { get; set; }
        public IEnumerable<CreateFormItemForm> Items { get; set; }
    }
}