using System.Collections.Generic;
using PlumsailTest.Domain.Enums;

namespace PlumsailTest.Domain.Forms
{
    public class CreateTemplateItemForm
    {
        public string Name { get; set; }
        public int Order { get; set; }
        public FormItemType Type { get; set; }
        public IEnumerable<string> Values { get; set; }
    }
}