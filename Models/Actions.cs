using Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Action:EntityBase
    {
        public string ControllerName { get; set; }
        public string  ActionName { get; set; }
        public string Url { get; set; }
    }
}
