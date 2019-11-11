using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatingCriteria.Models
{
    public class criteria
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AutoId { get; set; }

        //public int Mod_Id { get; set; }
        public string Topic { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public DateTime DateCreated { get; set; }

        public bool Active { get; set; }




    }
}
