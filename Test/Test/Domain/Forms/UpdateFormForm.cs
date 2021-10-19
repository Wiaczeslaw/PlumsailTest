using System;
using System.Collections;
using System.Collections.Generic;

namespace Test.Domain.Forms
{
    public class UpdateFormForm
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<UpdateFormItemForm> Items { get; set; }
    }
}