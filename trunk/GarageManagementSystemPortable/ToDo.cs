using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GarageManagementSystem
{
    public struct ToDo
    {
        public DateTime DueDate { get; set; }
        public string TaskContent { get; set; }
        public bool Completed { get; private set; }
    }
}
