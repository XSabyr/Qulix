﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QulixCore
{
    public class Position
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return $"position id = {Id}\nposition name = {Name}\n";
        }
    }
}
