using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.NetCoreMVCCRUD.project.Models
{
    public class Employee
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "Emloyee {0} is required")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Name should be minimum 3 characters and maximum 100 characters")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [ForeignKey("Designation_ID")]
        [Required(ErrorMessage = "Designation id is required")]
        public int Designation { get; set; }

        [Required(ErrorMessage = "Date of joining is required")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd-mm-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Joining_Date { get; set; }

    }
}
