using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostgresqlExample.Models
{
    public class Posts
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public DateTime OperationDate { get; set; }
    }
}
