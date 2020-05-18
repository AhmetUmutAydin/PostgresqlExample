using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostgresqlExample.Models
{
    public class CategoryPosts
    {
        public List<Posts> Post { get; set; }
        public Categories Categories { get; set; }
    }
}
