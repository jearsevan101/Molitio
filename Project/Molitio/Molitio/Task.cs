using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Molitio
{
    class Task
    {
        public int _idTask;
        public string _taskName;
        public DateTime _taskDeadlineDate;
        public int _taskDeadlineTime;
        public string _taskDescription;

        public int idTask
        {
            get { return _idTask; }
            set { _idTask = value; }
        }

        public string taskName
        {
            get { return _taskName; }
            set { _taskName = value; }
        }

        public DateTime taskDeadlineDate
        {
            get { return _taskDeadlineDate; }
            set { _taskDeadlineDate = value; }
        }

        public int taskDeadlineTime
        {
            get { return _taskDeadlineTime; }
            set { _taskDeadlineTime = value; }
        }

        public string taskDescription
        {
            get { return _taskDescription; }
            set { _taskDescription = value; }
        }
    }
}