using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //  Storing only path | guid of project code files
        public string Code { get; set; }
    }
}
