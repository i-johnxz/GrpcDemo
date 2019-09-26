﻿using System;
using System.Collections.Generic;
using System.Text;
using SampleProject.Domain.SeedWork;

namespace SampleProject.Domain.Customers
{
    public class CustomerId : TypedIdValueBase
    {
        public CustomerId(Guid value) : base(value)
        {
        }
    }
}
