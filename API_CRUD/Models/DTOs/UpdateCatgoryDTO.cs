using API_CRUD.Model.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API_CRUD.Models.DTOs
{
    public class UpdateCatgoryDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Type to category name")]
        [MinLength(3, ErrorMessage = "Minimum lenght is 3")]
        [RegularExpression(@"^[a-zA-Z- ]+$", ErrorMessage = "Only letters ar allowed")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Type to category descripiton")]
        [MinLength(3, ErrorMessage = "Minimum lenght is 3")]
        public string Description { get; set; }

        public string Slug => Name.ToLower().Replace(' ', '-');

        public DateTime UpdateDate => DateTime.Now;

        [NotMapped]
        public Status Status => Status.Modified;
    }
}
