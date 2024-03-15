using NetCoreIdentity_1.Models.Admins.AppRoles.ResponceModels;

namespace NetCoreIdentity_1.Models.Admins.AppRoles.PageVms
{
    public class AssignRolePageVM
    {
        public List<AppRoleResponceModel> Roles { get; set; }
        public int UserID { get; set; }
    }
}
