using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SoftLineTestApp.Data.Entity
{
    public class Status
    {
        public int Id { get; set; }
        public string StatusName { get; set; }
    }
}
