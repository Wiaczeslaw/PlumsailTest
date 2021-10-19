using System;

namespace Test.Domain.Forms
{
    public class CreateFormItemForm
    {
        public Guid FormItemTemplateId { get; set; }
        public string Value { get; set; }
        public Guid? FormItemSelectValueId { get; set; }
    }
}