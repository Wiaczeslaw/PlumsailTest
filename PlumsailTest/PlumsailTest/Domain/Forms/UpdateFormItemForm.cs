using System;

namespace PlumsailTest.Domain.Forms
{
    public class UpdateFormItemForm
    {
        public Guid Id { get; set; }
        public string Value { get; set; }
        public Guid? FormItemSelectValueId { get; set; }
    }
}