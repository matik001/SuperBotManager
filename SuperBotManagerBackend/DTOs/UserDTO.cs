using SuperBotManagerBackend.DB.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SuperBotManagerBackend.DTOs {
    public class RoleDTO
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
    }
    public class UserDTO {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        //public virtual ICollection<RoleDTO> Roles { get; set; }
    }

}
