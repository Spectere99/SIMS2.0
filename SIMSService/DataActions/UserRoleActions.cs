﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using SIMSData;
using SIMSService.Models;

namespace SIMSService.DataActions
{
    public class UserRoleActions : IActions<UserRoleModel>
    {
        private readonly ScreenPrintManagementEntities _dbContext = new ScreenPrintManagementEntities();

        public UserRoleActions()
        { }
        public IEnumerable<UserRoleModel> Get(bool showInactive)
        {
            try
            {
                var userRoles = showInactive ? _dbContext.UserRoles.ToList() :
                    _dbContext.UserRoles.Where(p => p.IsActive).ToList();
                var userRoleModels = userRoles.Select(userRole => new UserRoleModel
                {
                    Id = userRole.Id,
                    RoleId = userRole.RoleID,
                    Role = new RoleModel
                    {
                        Id = userRole.Role.Id,
                        Role = userRole.Role.Role1,
                        PermissionId = userRole.Role.PermissionId,
                        Permission = new PermissionModel
                        {
                            Id = userRole.Role.Permission.Id,
                            Permission = userRole.Role.Permission.Permission1,
                            PermissionModuleKey = userRole.Role.Permission.PermissionModuleKey,
                            CanAccess = userRole.Role.Permission.CanAccess,
                            CanUpdate = userRole.Role.Permission.CanUpdate,
                            CanDelete = userRole.Role.Permission.CanDelete,
                            IsActive = userRole.Role.Permission.IsActive,
                            Created = userRole.Role.Permission.Created,
                            CreatedBy = userRole.Role.Permission.CreatedBy,
                            LastUpdated = userRole.Role.Permission.LastUpdated,
                            LastUpdatedBy = userRole.Role.Permission.LastUpdatedBy
                        },
                        IsActive = userRole.Role.IsActive,
                        Created = userRole.Role.Created,
                        CreatedBy = userRole.Role.CreatedBy,
                        LastUpdated = userRole.Role.LastUpdated,
                        LastUpdatedBy = userRole.Role.LastUpdatedBy
                    },
                    UserId = userRole.UserID,
                    IsActive = userRole.IsActive,
                    Created = userRole.Created,
                    CreatedBy = userRole.CreatedBy,
                    LastUpdated = userRole.LastUpdated,
                    LastUpdatedBy = userRole.LastUpdatedBy
                }).ToList();

                return userRoleModels;
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
        public UserRoleModel GetById(int id)
        {
            try
            {
                var userRole = _dbContext.UserRoles.Find(id);
                if (userRole == null)
                {
                    return null;
                }
                var userRoleModel = new UserRoleModel
                {
                    Id = userRole.Id,
                    RoleId = userRole.RoleID,
                    Role = new RoleModel
                    {
                        Id = userRole.Role.Id,
                        Role = userRole.Role.Role1,
                        PermissionId = userRole.Role.PermissionId,
                        Permission = new PermissionModel
                        {
                            Id = userRole.Role.Permission.Id,
                            Permission = userRole.Role.Permission.Permission1,
                            PermissionModuleKey = userRole.Role.Permission.PermissionModuleKey,
                            CanAccess = userRole.Role.Permission.CanAccess,
                            CanUpdate = userRole.Role.Permission.CanUpdate,
                            CanDelete = userRole.Role.Permission.CanDelete,
                            IsActive = userRole.Role.Permission.IsActive,
                            Created = userRole.Role.Permission.Created,
                            CreatedBy = userRole.Role.Permission.CreatedBy,
                            LastUpdated = userRole.Role.Permission.LastUpdated,
                            LastUpdatedBy = userRole.Role.Permission.LastUpdatedBy
                        },
                        IsActive = userRole.Role.IsActive,
                        Created = userRole.Role.Created,
                        CreatedBy = userRole.Role.CreatedBy,
                        LastUpdated = userRole.Role.LastUpdated,
                        LastUpdatedBy = userRole.Role.LastUpdatedBy
                    },
                    UserId = userRole.UserID,
                    IsActive = userRole.IsActive,
                    Created = userRole.Created,
                    CreatedBy = userRole.CreatedBy,
                    LastUpdated = userRole.LastUpdated,
                    LastUpdatedBy = userRole.LastUpdatedBy
                };

                return userRoleModel;
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
        public void Insert(UserRoleModel userModel, string user)
        {
            try
            {
                UserRole newUserRole = new UserRole
                {
                    Id = userModel.Id,
                    RoleID = userModel.RoleId,
                    UserID = userModel.UserId,
                    IsActive = userModel.IsActive,
                    Created = DateTime.Now,
                    CreatedBy = user,
                    LastUpdated = DateTime.Now,
                    LastUpdatedBy = user
                };

                _dbContext.UserRoles.Add(newUserRole);
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
        public void Update(UserRoleModel userModel, string user)
        {
            try
            {
                UserRole updUserRole = _dbContext.UserRoles.Find(userModel.Id);
                if (updUserRole == null)
                {
                    return;
                }

                updUserRole.Id = userModel.Id;
                updUserRole.RoleID = userModel.RoleId;
                updUserRole.UserID = userModel.UserId;
                updUserRole.IsActive = userModel.IsActive;
                updUserRole.Created = DateTime.Now;
                updUserRole.CreatedBy = user;
                updUserRole.LastUpdated = DateTime.Now;
                updUserRole.LastUpdatedBy = user;
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
                UserRole delUser = _dbContext.UserRoles.FirstOrDefault(p => p.RoleID == id);

                if (delUser == null)
                {
                    return;
                }

                delUser.IsActive = false;
                delUser.LastUpdated = DateTime.Now;
                delUser.LastUpdatedBy = user;
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

        public void Delete(int id, string user)
        {
            UserRole delUserRole = _dbContext.UserRoles.FirstOrDefault(p => p.Id == id);

            if (delUserRole == null)
            {
                return;
            }

            _dbContext.Entry(delUserRole).State = EntityState.Deleted;
            _dbContext.SaveChanges();
        }
    }
}