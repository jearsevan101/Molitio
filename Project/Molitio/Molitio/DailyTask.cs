using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Molitio
{
    public class DailyTask
    {
        public int Id { get; set; }
        public string Time { get; set; }
        public string Title { get; set; }
        public bool isDone { get; set; }
    }
}
