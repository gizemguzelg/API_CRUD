using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_CRUD.Models.VMs
{
    public class GetCategoryVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Descripiton { get; set; }
        public string Slug { get; set; }
    }
}
