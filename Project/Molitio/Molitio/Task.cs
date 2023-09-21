using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Molitio
{
    class Task
    {
        public int idTask
        {
            get { return idTask; }
        }

        public string taskName
        {
            get { return taskName; }
        }

        public DateTime taskDeadlineDate
        {
            get { return taskDeadlineDate; }
        }

        public int taskDeadlineTime
        {
            get { return taskDeadlineTime; }
        }

        public string taskDescription
        {
            get { return taskDescription; }
        }
    }
}
