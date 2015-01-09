using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using SimpleLoginSystem.Models;

namespace SimpleLoginSystem
{
    public class MyRoleProvider:RoleProvider
    {
        //http://techbrij.com/custom-roleprovider-authorization-asp-net-mvc

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            using (MainDBContext obj=new MainDBContext())
            {
                var objUser = obj.SystemUsers.FirstOrDefault(x => x.Email == username);
                if(objUser==null)
                {
                    return null;
                }
                else
                {
                    return new[] {objUser.Role};
                }
            }
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
    public sealed class RolesList
    {
        public static readonly RolesList STUDENT = new RolesList("Student");
        public static readonly RolesList TEACHER = new RolesList("Teacher");
        public static readonly RolesList ADMIN = new RolesList("Admin");

        private RolesList(string value)
        {
            Value = value;
        }

        public string Value { get; private set; }
    }
}