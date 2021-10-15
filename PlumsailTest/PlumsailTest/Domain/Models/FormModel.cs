using System;
using System.Collections;
using System.Collections.Generic;

namespace PlumsailTest.Domain.Models
{
    public class FormModel : ShortFormModel
    {
        public IEnumerable<FormItemModel> Items { get; set; }
    }
}