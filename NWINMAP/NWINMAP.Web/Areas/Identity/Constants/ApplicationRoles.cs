using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NWINMAP.Web.Areas.Identity.Constants
{
    public sealed class ApplicationRoles
    {
        private ApplicationRoles(string id, string name)
        {
            Id = id;
            Name = name;
        }

        public string Id { get; }
        public string Name { get; }

        public const string SystemAdministratorRoleName = "SystemAdministrator";
        public static ApplicationRoles SystemAdministrator = new ApplicationRoles(SystemAdministratorRoleName.ToLower(), SystemAdministratorRoleName);

        public const string AdministratorRoleName = "Administrator";
        public static ApplicationRoles Administrator = new ApplicationRoles(AdministratorRoleName.ToLower(), AdministratorRoleName);

        //public const string OfficerRoleName = "Officer";
        //public static ApplicationRoles Officer = new ApplicationRoles(OfficerRoleName.ToLower(), OfficerRoleName);

        //public const string ResidentRoleName = "Resident";
        //public static ApplicationRoles Resident = new ApplicationRoles(ResidentRoleName.ToLower(), ResidentRoleName);

        public static List<ApplicationRoles> Items
        {
            get
            {
                var items = new List<ApplicationRoles>
                {
                    SystemAdministrator, Administrator//, Officer, Resident
                };

                return items;
            }
        }
    }
}
