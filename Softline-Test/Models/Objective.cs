using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Softline_Test.Models
{
    public class Objective
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Укажите наименование задачи")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Укажите описание задачи")]
        public string Description { get; set; }

        //[Required(ErrorMessage = "Укажите статус задачи")]
        public int StatusId { get; set; }

        public Status Status { get; set; }
    }
}
