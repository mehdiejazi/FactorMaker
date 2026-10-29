using Data;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FactorMaker.Infrastructure
{
    public static class DefaultAccessBootstrap
    {
        private static readonly HashSet<string> AdminControllers = new HashSet<string>
        {
            "RoleController",
            "RoleActionPermissionController",
            "ActionPermissionController",
            "UserController"
        };

        public static void EnsureStoreOwnerRole(DatabaseContext database)
        {
            var storeOwner = database.Roles
                .FirstOrDefault(role => !role.IsDeleted && role.Name == "StoreOwner");

            if (storeOwner == null)
            {
                var hasDefaultRole = database.Roles.Any(role => !role.IsDeleted && role.IsDefault);
                storeOwner = new Role
                {
                    Name = "StoreOwner",
                    Description = "نقش پیش‌فرض مالک فروشگاه برای ثبت‌نام",
                    IsDefault = !hasDefaultRole
                };
                database.Roles.Add(storeOwner);
                database.SaveChanges();
            }
            else if (!storeOwner.IsDefault && !database.Roles.Any(role => !role.IsDeleted && role.IsDefault))
            {
                storeOwner.IsDefault = true;
                database.SaveChanges();
            }

            // Admin-only controllers (users, roles, permissions) must never be granted to store owners.
            var adminGrants = database.RoleActionPermission
                .Where(rap => rap.RoleId == storeOwner.Id)
                .Join(database.ActionPermissions, rap => rap.ActionPermissionId, ap => ap.Id, (rap, ap) => new { rap, ap.ControllerName })
                .ToList()
                .Where(x => AdminControllers.Contains(x.ControllerName))
                .Select(x => x.rap)
                .ToList();
            if (adminGrants.Count > 0)
            {
                database.RoleActionPermission.RemoveRange(adminGrants);
                database.SaveChanges();
            }
            var hasPermissions = database.RoleActionPermission
                .Any(permission => permission.RoleId == storeOwner.Id && !permission.IsDeleted);
            if (hasPermissions)
            {
                return;
            }

            var operationalPermissions = database.ActionPermissions
                .Where(permission => !permission.IsDeleted && !AdminControllers.Contains(permission.ControllerName))
                .ToList();

            foreach (var permission in operationalPermissions)
            {
                database.RoleActionPermission.Add(new RoleActionPermission
                {
                    RoleId = storeOwner.Id,
                    ActionPermissionId = permission.Id,
                    Id = Guid.NewGuid()
                });
            }

            if (operationalPermissions.Count > 0)
            {
                database.SaveChanges();
            }
        }
    }
}
