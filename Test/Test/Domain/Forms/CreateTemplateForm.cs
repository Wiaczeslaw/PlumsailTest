using System.Collections;
using System.Collections.Generic;

namespace Test.Domain.Forms
{
    public class CreateTemplateForm
    {
        public string Name { get; set; }
        public IEnumerable<CreateTemplateItemForm> Items { get; set; }
    }
}