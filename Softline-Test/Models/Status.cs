using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Softline_Test.Models
{
    public class Status
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [JsonIgnore]
        public List<Objective> Objectives { get; set; }

        public Status()
        {
            Objectives = new List<Objective>();
        }
    }
}
