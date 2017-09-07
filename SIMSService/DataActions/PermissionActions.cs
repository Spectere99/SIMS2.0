using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SIMSData;
using SIMSService.Models;

namespace SIMSService.DataActions
{
    public class PermissionActions : IActions<PermissionModel>
    {
        private readonly ScreenPrintManagementEntities _dbContext = new ScreenPrintManagementEntities();

        public PermissionActions()
        { }

        public IEnumerable<PermissionModel> Get(bool showInactive)
        {
            try
            {
                var permissions = showInactive ? _dbContext.Permissions.ToList() :
                    _dbContext.Permissions.Where(p => p.IsActive).ToList();
                var permissionModels = permissions.Select(permission => new PermissionModel
                {
                    Id = permission.Id,
                    Permission = permission.Permission1,
                    PermissionModuleKey = permission.PermissionModuleKey,
                    CanAccess = permission.CanAccess,
                    CanUpdate = permission.CanUpdate,
                    CanDelete = permission.CanDelete,
                    IsActive = permission.IsActive,
                    Created = permission.Created,
                    CreatedBy = permission.CreatedBy,
                    LastUpdated = permission.LastUpdated,
                    LastUpdatedBy = permission.LastUpdatedBy
                }).ToList();

                return permissionModels;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                _dbContext.Dispose();
            }

        }
        public PermissionModel GetById(int id)
        {
            try
            {
                var permission = _dbContext.Permissions.Find(id);
                if (permission == null)
                {
                    return null;
                }
                var permissionModel = new PermissionModel
                {
                    Id = permission.Id,
                    Permission = permission.Permission1,
                    PermissionModuleKey = permission.PermissionModuleKey,
                    CanAccess = permission.CanAccess,
                    CanUpdate = permission.CanUpdate,
                    CanDelete = permission.CanDelete,
                    IsActive = permission.IsActive,
                    Created = permission.Created,
                    CreatedBy = permission.CreatedBy,
                    LastUpdated = permission.LastUpdated,
                    LastUpdatedBy = permission.LastUpdatedBy
                };

                return permissionModel;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                _dbContext.Dispose();
            }
        }
        public void Insert(PermissionModel permissionModel, string user)
        {
            Permission newPermission = new Permission
            {
                Id = permissionModel.Id,
                Permission1 = permissionModel.Permission,
                PermissionModuleKey = permissionModel.PermissionModuleKey,
                CanAccess = permissionModel.CanAccess,
                CanUpdate = permissionModel.CanUpdate,
                CanDelete = permissionModel.CanDelete,
                IsActive = permissionModel.IsActive,
                Created = DateTime.Now,
                CreatedBy = user,
                LastUpdated = DateTime.Now,
                LastUpdatedBy = user
            };

            _dbContext.Permissions.Add(newPermission);
            _dbContext.SaveChanges();
        }
        public void Update(PermissionModel permissionModel, string user)
        {
            Permission updPermission = _dbContext.Permissions.Find(permissionModel.Id);
            if (updPermission == null)
            {
                return;
            }

            updPermission.Id = permissionModel.Id;
            updPermission.Permission1 = permissionModel.Permission;
            updPermission.PermissionModuleKey = permissionModel.PermissionModuleKey;
            updPermission.CanAccess = permissionModel.CanAccess;
            updPermission.CanUpdate = permissionModel.CanUpdate;
            updPermission.CanDelete = permissionModel.CanDelete;
            updPermission.IsActive = permissionModel.IsActive;
            updPermission.Created = DateTime.Now;
            updPermission.CreatedBy = user;
            updPermission.LastUpdated = DateTime.Now;
            updPermission.LastUpdatedBy = user;
            _dbContext.SaveChanges();
        }
        public void Deactivate(int id, string user)
        {
            Permission delPermission = _dbContext.Permissions.Find(id);

            if (delPermission == null)
            {
                return;
            }

            delPermission.IsActive = false;
            delPermission.LastUpdated = DateTime.Now;
            delPermission.LastUpdatedBy = user;
            _dbContext.SaveChanges();

        }
    }
}