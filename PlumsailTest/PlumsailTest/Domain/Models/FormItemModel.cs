using System;
using PlumsailTest.Domain.Enums;

namespace PlumsailTest.Domain.Models
{
    public class FormItemModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
        public FormItemType Type { get; set; }
        public string Value { get; set; }
        public FormItemSelectValueModel SelectValue { get; set; }
    }
}