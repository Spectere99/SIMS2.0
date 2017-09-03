using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SIMSData;
using SIMSService.Models;

namespace SIMSService.DataActions
{
    public class LookupTypeActions
    {
        private readonly ScreenPrintManagementEntities _dbContext = new ScreenPrintManagementEntities();

        public LookupTypeActions()
        {
            
        }

        public IEnumerable<LookupTypeModel> Get(bool showInactive)
        {
            try
            {
                var lookupTypes = showInactive ? _dbContext.LookupTypes.ToList() :
                                                 _dbContext.LookupTypes.Where(p => p.IsActive).ToList();
                
                var lookupTypeModels = lookupTypes.Select(lookupType => new LookupTypeModel
                {
                    Id = lookupType.Id,
                    TypeDescription = lookupType.TypeDescription,
                    IsActive = lookupType.IsActive
                }).ToList();

                return lookupTypeModels;
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
        public LookupTypeModel GetById(int id)
        {
            try
            {
                var lookupType = _dbContext.LookupTypes.Find(id);
                var lookupTypeModel = new LookupTypeModel
                {
                    Id = lookupType.Id,
                    TypeDescription = lookupType.TypeDescription
                };

                return lookupTypeModel;
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
        public void Insert(LookupTypeModel lookupTypeModel, string user)
        {
            LookupType lookupType = new LookupType
            {
                Id = 0,
                TypeDescription = lookupTypeModel.TypeDescription,
                IsActive = lookupTypeModel.IsActive,
                Created = DateTime.Now,
                CreatedBy = user,
                LastUpdated = DateTime.Now,
                LastUpdatedBy = user
            };

            _dbContext.LookupTypes.Add(lookupType);
            _dbContext.SaveChanges();

        }
        public void Update(LookupTypeModel lookupTypeModel, string user)
        {
            LookupType lookupType = _dbContext.LookupTypes.Find(lookupTypeModel.Id);
            if (lookupType == null)
            {
                return;
            }

            lookupType.TypeDescription = lookupTypeModel.TypeDescription;
            lookupType.IsActive = lookupTypeModel.IsActive;
            lookupType.LastUpdated = DateTime.Now;
            lookupType.LastUpdatedBy = user;
            _dbContext.SaveChanges();
        }
        public void Deactivate(int id, string user)
        {
            LookupType lookupType = _dbContext.LookupTypes.Find(id);

            if (lookupType == null)
            {
                return;
            }

            lookupType.IsActive = false;
            lookupType.LastUpdated = DateTime.Now;
            lookupType.LastUpdatedBy = user;
            _dbContext.SaveChanges();

        }
    }
}