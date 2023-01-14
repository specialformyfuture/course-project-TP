using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Course_Project_TP_6.DAO;

namespace Course_Project_TP_6.Models
{
    public class RoleCS : RoleProvider
    {
        UsersDAO usersDAO = new UsersDAO();
        RoleDAO roleDAO = new RoleDAO();
        public override string ApplicationName { get; set; }

        public override void AddUsersToRoles(string[] UserNames, string[] Roles)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string Name)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string Name, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string Name, string UserNameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string UserName)
        {
            List<Role> role = roleDAO.GetAllRecords();
            string[] roleArray;
            List<string> roles = new List<string>();
            int count = 0;
            foreach (Role i in role)
            {
                roles.Add(i.Name);
                count += 1;
            }

            roleArray = new string[count];

            count = 0;
            foreach (string str in roles)
            {
                roleArray[count] = str;
                count += 1;
            }

            return roleArray;
        }

        public override string[] GetUsersInRole(string Name)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string UserName, string Name)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] UserNames, string[] Roles)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string Name)
        {
            throw new NotImplementedException();
        }
    }
}