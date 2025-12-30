using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace FOS.Shared
{
    public class UserData
    {
        public int ID { get; set; }

        [DisplayName("User Name *")]
        [Required(ErrorMessage = "* Required")]
        public string UserName { get; set; }

        [DisplayName("Password *")]
        [Required(ErrorMessage = "* Required")]
        public string Password { get; set; }

        [DisplayName("Role *")]
        [Required(ErrorMessage = "* Required")]
        public Nullable<int> RoleID { get; set; }
        public string RoleName { get; set; }

        public string EmailID { get; set; }
        public string PhoneNo { get; set; }
        public string Address { get; set; }
        public Nullable<System.DateTime> DOB { get; set; }

        public System.DateTime CreatedDate { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }

        public List<FOS.Shared.Role> Roles { get; set; }
    }
}
