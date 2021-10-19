using System.Collections.Generic;
using Test.Domain.Enums;

namespace Test.Domain.Forms
{
    public class CreateTemplateItemForm
    {
        public string Name { get; set; }
        public int Order { get; set; }
        public FormItemType Type { get; set; }
        public IEnumerable<string> Values { get; set; }
    }
}