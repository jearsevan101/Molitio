using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Molitio
{
    class ToDoList
    {
        private List<Task> tasks = new List<Task>();

        public int _idToDoList;

        public int idToDoList
        {
            get { return _idToDoList; }
            set { _idToDoList = value; }
        }

        int idNext = 0;

        public void createTask(Task newtask)
        {
            newtask.idTask = idNext;
            tasks.Add(newtask);
            idNext++;
        }

        public void editTask(int id, Task editedtask)
        {
            var task = tasks.Find(p => p.idTask == id);
            if (task != null)
            {
                task = editedtask;
            }
        }

        public void deleteTask(int id)
        {
            var task = tasks.Find(p => p.idTask == id);
            if (task != null)
            {
                tasks.Remove(task);
            }
        }
    }
}