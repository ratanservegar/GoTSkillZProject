using System.Collections.Generic;

namespace GoTSkillZ.Models.Membership.Data
{
    public partial class Users
    {
        public List<UserRoles> UserRoles;
        public List<Roles> CoreRoles;
        public List<Roles> AssignedRoles;
    }
}
