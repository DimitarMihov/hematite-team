using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GarageManagementSystem
{
    public interface iTask
    {
        List<ToDo> Tasks { get; }
        ToDo GetTaskByIndex(int toDoIndex);
        void AddTask(ToDo task);
        void RemoveTask(ToDo task);
        string Alarm(ToDo task);
    }
}
