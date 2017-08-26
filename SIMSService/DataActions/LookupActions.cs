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

        public IEnumerable<LookupTypeModel> GetLookupTypes()
        {
            try
            {
                var lookupTypes = _dbContext.LookupTypes.ToList();
                var lookupTypeModels = lookupTypes.Select(lookupType => new LookupTypeModel
                {
                    Id = lookupType.Id,
                    TypeDescription = lookupType.TypeDescription
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

        public IEnumerable<LookupModel> GetLookups()
        {
            try
            {
                var lookups = _dbContext.Lookups.ToList();
                var lookupModels = lookups.Select(lookup => new LookupModel
                {
                    Id = lookup.Id,
                    Value = lookup.Value,
                    LookupType = new LookupTypeModel
                    {
                        Id = lookup.LookupType.Id,
                        TypeDescription = lookup.LookupType.TypeDescription
                    },
                    LookupTypeId = lookup.LookupTypeId
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
    }
}