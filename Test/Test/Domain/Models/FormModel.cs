using System;
using System.Collections;
using System.Collections.Generic;

namespace Test.Domain.Models
{
    public class FormModel : ShortFormModel
    {
        public IEnumerable<FormItemModel> Items { get; set; }
    }
}