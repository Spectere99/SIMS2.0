using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SIMSData;
using SIMSService.Models;

namespace SIMSService.DataActions
{
    public class UserActions : IActions<UserModel>
    {
        private readonly ScreenPrintManagementEntities _dbContext = new ScreenPrintManagementEntities();

        public UserActions()
        { }

        public IEnumerable<UserModel> Get(bool showInactive)
        {
            try
            {
                var users = showInactive ? _dbContext.Users.ToList() :
                    _dbContext.Users.Where(p => p.IsActive).ToList();
                var userModels = users.Select(user => new UserModel
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    ContactNumber = user.ContactNumber,
                    UserRoles = user.UserRoles.Select(userRole => new UserRoleModel
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
                    }).ToList(),
                    IsActive = user.IsActive,
                    Created = user.Created,
                    CreatedBy = user.CreatedBy,
                    LastUpdated = user.LastUpdated,
                    LastUpdatedBy = user.LastUpdatedBy
                }).ToList();

                return userModels;
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
        public UserModel GetById(int id)
        {
            try
            {
                var user = _dbContext.Users.Find(id);
                var userModel = new UserModel
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    ContactNumber = user.ContactNumber,
                    IsActive = user.IsActive,
                    Created = user.Created,
                    CreatedBy = user.CreatedBy,
                    LastUpdated = user.LastUpdated,
                    LastUpdatedBy = user.LastUpdatedBy
                };

                return userModel;
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
        public void Insert(UserModel userModel, string user)
        {
            User newUser = new User
            {
                Id = userModel.Id,
                UserName = userModel.UserName,
                Email = userModel.Email,
                FirstName = userModel.FirstName,
                LastName = userModel.LastName,
                ContactNumber = userModel.ContactNumber,
                IsActive = userModel.IsActive,
                Created = DateTime.Now,
                CreatedBy = user,
                LastUpdated = DateTime.Now,
                LastUpdatedBy = user
            };

            _dbContext.Users.Add(newUser);
            _dbContext.SaveChanges();
        }
        public void Update(UserModel userModel, string user)
        {
            User updUser = _dbContext.Users.Find(userModel.Id);
            if (updUser == null)
            {
                return;
            }

            updUser.Id = userModel.Id;
            updUser.UserName = userModel.UserName;
            updUser.Email = userModel.Email;
            updUser.FirstName = userModel.FirstName;
            updUser.LastName = userModel.LastName;
            updUser.ContactNumber = userModel.ContactNumber;
            updUser.IsActive = userModel.IsActive;
            updUser.Created = DateTime.Now;
            updUser.CreatedBy = user;
            updUser.LastUpdated = DateTime.Now;
            updUser.LastUpdatedBy = user;
            _dbContext.SaveChanges();
        }
        public void Deactivate(int id, string user)
        {
            User delUser = _dbContext.Users.Find(id);

            if (delUser == null)
            {
                return;
            }

            delUser.IsActive = false;
            delUser.LastUpdated = DateTime.Now;
            delUser.LastUpdatedBy = user;
            _dbContext.SaveChanges();

        }
    }
}