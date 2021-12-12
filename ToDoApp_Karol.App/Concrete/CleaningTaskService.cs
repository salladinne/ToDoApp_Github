using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.App.Abstract;
using ToDoApp_Karol;
using ToDoApp_Karol.Domain.Entity;

namespace ToDoApp.App.Concrete
{
    public class CleaningTaskService : TaskService<CleaningTasks, FamilyMember>
        //dlaczego tak nie mozna?
    {


        private int id = 0;

        public CleaningTaskService()
        {

        }


        

        //przeniosłem CheckTaskTime też do TaskSerwisu bo to też jest ogólna właściwość
    }
}
