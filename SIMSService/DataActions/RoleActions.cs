using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SIMSData;
using SIMSService.Models;

namespace SIMSService.DataActions
{
    public class RoleActions : IActions<RoleModel>
    {
        private readonly ScreenPrintManagementEntities _dbContext = new ScreenPrintManagementEntities();

        public RoleActions()
        { }

        public IEnumerable<RoleModel> Get(bool showInactive)
        {
            try
            {
                var roles = showInactive ? _dbContext.Roles.ToList() :
                    _dbContext.Roles.Where(p => p.IsActive).ToList();
                var roleModels = roles.Select(role => new RoleModel
                {
                    Id = role.Id,
                    Role = role.Role1,
                    PermissionId = role.PermissionId,
                    Permission = new PermissionModel
                    {
                      Id = role.Permission.Id,
                      Permission  = role.Permission.Permission1,
                      PermissionModuleKey = role.Permission.PermissionModuleKey,
                      CanAccess = role.Permission.CanAccess,
                      CanUpdate = role.Permission.CanUpdate,
                      CanDelete = role.Permission.CanDelete,
                      IsActive = role.Permission.IsActive,
                      Created = role.Permission.Created,
                      CreatedBy = role.Permission.CreatedBy,
                      LastUpdated = role.Permission.LastUpdated,
                      LastUpdatedBy = role.Permission.LastUpdatedBy
                    },
                    IsActive = role.IsActive,
                    Created = role.Created,
                    CreatedBy = role.CreatedBy,
                    LastUpdated = role.LastUpdated,
                    LastUpdatedBy = role.LastUpdatedBy
                }).ToList();

                return roleModels;
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
        public RoleModel GetById(int id)
        {
            try
            {
                var role = _dbContext.Roles.Find(id);
                if (role == null)
                {
                    return null;
                }
                var roleModel = new RoleModel
                {
                    Id = role.Id,
                    Role = role.Role1,
                    PermissionId = role.PermissionId,
                    Permission = new PermissionModel
                    {
                        Id = role.Permission.Id,
                        Permission = role.Permission.Permission1,
                        PermissionModuleKey = role.Permission.PermissionModuleKey,
                        CanAccess = role.Permission.CanAccess,
                        CanUpdate = role.Permission.CanUpdate,
                        CanDelete = role.Permission.CanDelete,
                        IsActive = role.Permission.IsActive,
                        Created = role.Permission.Created,
                        CreatedBy = role.Permission.CreatedBy,
                        LastUpdated = role.Permission.LastUpdated,
                        LastUpdatedBy = role.Permission.LastUpdatedBy
                    },
                    IsActive = role.IsActive,
                    Created = role.Created,
                    CreatedBy = role.CreatedBy,
                    LastUpdated = role.LastUpdated,
                    LastUpdatedBy = role.LastUpdatedBy
                };

                return roleModel;
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
        public void Insert(RoleModel roleModel, string user)
        {
            try
            {
                Role newRole = new Role
                {
                    Id = roleModel.Id,
                    Role1 = roleModel.Role,
                    PermissionId = roleModel.PermissionId,
                    IsActive = roleModel.IsActive,
                    Created = DateTime.Now,
                    CreatedBy = user,
                    LastUpdated = DateTime.Now,
                    LastUpdatedBy = user
                };

                _dbContext.Roles.Add(newRole);
                _dbContext.SaveChanges();
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
        public void Update(RoleModel roleModel, string user)
        {
            try
            {
                Role updRole = _dbContext.Roles.Find(roleModel.Id);
                if (updRole == null)
                {
                    return;
                }

                updRole.Id = roleModel.Id;
                updRole.Role1 = roleModel.Role;
                updRole.PermissionId = roleModel.PermissionId;
                updRole.IsActive = roleModel.IsActive;
                updRole.Created = DateTime.Now;
                updRole.CreatedBy = user;
                updRole.LastUpdated = DateTime.Now;
                updRole.LastUpdatedBy = user;
                _dbContext.SaveChanges();
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
        public void Deactivate(int id, string user)
        {
            try
            {
                Role delRole = _dbContext.Roles.Find(id);

                if (delRole == null)
                {
                    return;
                }

                delRole.IsActive = false;
                delRole.LastUpdated = DateTime.Now;
                delRole.LastUpdatedBy = user;
                _dbContext.SaveChanges();
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
    }
}