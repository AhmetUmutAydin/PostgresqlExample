﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostgresqlExample.Models
{
    public class Categories
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Byte[] Picture { get; set; }

    }
}
