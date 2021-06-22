using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QulixCore
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public OrganizationalForm OrganizationalForm { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"company id = {Id}\n");
            sb.Append($"company Name = {Name}\n");
            sb.Append($"organizational form = {OrganizationalForm.Name}\n");
            return sb.ToString();
        }
    }
}
