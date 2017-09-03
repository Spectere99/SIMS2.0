using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SIMSData;
using SIMSService.Models;

namespace SIMSService.DataActions
{
    public class LookupActions
    {
        private readonly ScreenPrintManagementEntities _dbContext = new ScreenPrintManagementEntities();

        public LookupActions()
        {
            
        }

        public IEnumerable<LookupModel> Get(bool showInactive)
        {
            try
            {
                var lookups = showInactive ? _dbContext.Lookups.ToList() :
                                                 _dbContext.Lookups.Where(p => p.IsActive).ToList();
                var lookupModels = lookups.Select(lookup => new LookupModel
                {
                    Id = lookup.Id,
                    Value = lookup.Value,
                    LookupType = new LookupTypeModel
                    {
                        Id = lookup.LookupType.Id,
                        TypeDescription = lookup.LookupType.TypeDescription,
                        IsActive = lookup.LookupType.IsActive
                    },
                    LookupTypeId = lookup.LookupTypeId,
                    IsActive = lookup.IsActive
                }).ToList();

                return lookupModels;
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
        public LookupModel GetById(int id)
        {
            try
            {
                var lookup = _dbContext.Lookups.Find(id);
                var lookupModel = new LookupModel
                {
                    Id = lookup.Id,
                    Value = lookup.Value,
                    LookupTypeId = lookup.LookupTypeId,
                    LookupType = new LookupTypeModel()
                    {
                        Id = lookup.LookupType.Id,
                        TypeDescription = lookup.LookupType.TypeDescription
                    }
                    
                };

                return lookupModel;
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
        public void Insert(LookupModel lookupModel, string user)
        {
            Lookup lookup = new Lookup
            {
                Id = 0,
                Value = lookupModel.Value,
                LookupTypeId = lookupModel.LookupTypeId,
                IsActive = lookupModel.IsActive,
                Created = DateTime.Now,
                CreatedBy = user,
                LastUpdated = DateTime.Now,
                LastUpdatedBy = user
            };

            _dbContext.Lookups.Add(lookup);
            _dbContext.SaveChanges();
        }
        public void Update(LookupModel lookupModel, string user)
        {
            Lookup lookup = _dbContext.Lookups.Find(lookupModel.Id);
            if (lookup == null)
            {
                return;
            }

            lookup.Value = lookupModel.Value;
            lookup.LookupTypeId = lookupModel.LookupTypeId;
            lookup.IsActive = lookupModel.IsActive;
            lookup.LastUpdated = DateTime.Now;
            lookup.LastUpdatedBy = user;
            _dbContext.SaveChanges();
        }
        public void Deactivate(int id, string user)
        {
            Lookup lookup = _dbContext.Lookups.Find(id);

            if (lookup == null)
            {
                return;
            }

            lookup.IsActive = false;
            lookup.LastUpdated = DateTime.Now;
            lookup.LastUpdatedBy = user;
            _dbContext.SaveChanges();
        }
    }
}