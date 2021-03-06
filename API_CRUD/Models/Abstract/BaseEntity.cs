using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_CRUD.Model.Abstract
{
    public enum Status { Active = 1, Modified = 2, Passive = 3 }
    public class BaseEntity
    {
        public int Id { get; set; }

        private DateTime _createDate = DateTime.Now;
        public DateTime CreateDate { get => _createDate; set => _createDate = value; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }

        private Status _status = Status.Active;
        public Status Status { get => _status; set => _status = value; }
    }
}
