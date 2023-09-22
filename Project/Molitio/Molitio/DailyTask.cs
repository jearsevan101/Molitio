using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Molitio
{
    class DailyTask
    {
        private int _idDailyTask;
        private int _taskTime;
        private string _taskName;
        private string _taskDescription;
        private bool _isDone;

        public int idDailyTask
        {
            get { return _idDailyTask; }
            set { _idDailyTask = value; }
        }
        public int taskTime
        {
            get { return _taskTime; }
            set { _taskTime = value; }
        }
        public string taskName
        {
            get { return _taskName; }
            set { _taskName = value; }
        }
        public string taskDescription
        {
            get { return _taskDescription; }
            set { _taskDescription = value; }
        }
        public bool isDone
        {
            get { return _isDone; }
            set { _isDone = value; }
        }

        int idNext = 0;

        public void CreateDailyTask(string Name, string Description, int Time)
        {
            idDailyTask = idNext;
            taskTime = Time;
            taskName = Name;
            taskDescription = Description;
            isDone = false;
            idNext++;
        }

        public void EditDailyTask(int id, string Name, string Description, int Time)
        {
            if (idDailyTask == id)
            {
                taskTime = Time;
                taskName = Name;
                taskDescription = Description;
            }
        }

        public void DeleteDailyTask(int id)
        {
            if (idDailyTask == id)
            {
                idDailyTask = 0;
                taskTime = 0;
                taskName = null;
                taskDescription = null;
            }
        }

        public bool CheckList()
        {
            if (isDone == true)
            {
                isDone = false;
                return false;
            }
            else
            {
                isDone = true;
                return true;
            }
        }
    }
}