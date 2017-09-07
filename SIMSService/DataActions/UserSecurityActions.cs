using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SIMSData;
using SIMSService.Models;

namespace SIMSService.DataActions
{
    public class UserSecurityActions : IActions<UserSecurityModel>
    {
        private readonly ScreenPrintManagementEntities _dbContext = new ScreenPrintManagementEntities();

        public IEnumerable<UserSecurityModel> Get(bool showInactive)
        {
            throw new NotImplementedException();
        }

        public UserSecurityModel GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Insert(UserSecurityModel modelObj, string user)
        {
            throw new NotImplementedException();
        }

        public void Update(UserSecurityModel modelObj, string user)
        {
            throw new NotImplementedException();
        }

        public void Deactivate(int id, string user)
        {
            throw new NotImplementedException();
        }
    }
}