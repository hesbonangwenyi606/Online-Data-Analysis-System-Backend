using FOS.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOS.Shared
{
    public class RolesData
    {

        public int RoleID { get; set; }
        public string RoleName { get; set; }
        public List<RolePage> RolePages { get; set; }

    }
}
