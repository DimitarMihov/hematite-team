﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GarageManagementSystem
{
    public class DescriptionAttribute : Attribute
    {
        public DescriptionAttribute(string name)
        {
            this.Name = name;
        }

        public string Name { get; set; }
    }
}