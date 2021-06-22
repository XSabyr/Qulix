using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QulixCore
{
    public class OrganizationalForm
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return $"of id = {Id}\nof name = {Name}\n";
        }
    }
}
