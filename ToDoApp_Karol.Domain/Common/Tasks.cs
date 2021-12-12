using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp_Karol;

namespace ToDoApp_Karol.Domain.Common
{
    public class Tasks
    //usunąłem abstract

    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int TaskID { get; set; }
        public DateTime CreatedTime { get; set; }
        public bool IsCompleted { get; set; }
        //public string? TaskType { get; set; }
        public double TaskPerformanceTime { get; set; }
        public string? TaskType { get; set; }

    }
}
