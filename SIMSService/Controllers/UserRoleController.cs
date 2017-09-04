using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using log4net;
using SIMSService.DataActions;
using SIMSService.Models;

namespace SIMSService.Controllers
{
    public class UserRoleController : ApiController
    {
        private static readonly ILog _log = LogManager.GetLogger(
            System.Reflection.MethodBase.GetCurrentMethod().DeclaringType
        );

        public IHttpActionResult Get(HttpRequestMessage request)
        {
            if (_log.IsDebugEnabled)
            {
                _log.DebugFormat("Executing call in debug mode");
            }

            var headers = request.Headers;
            bool showInActive = false;

            if (headers.Contains("showInactive"))
            {
                showInActive = Boolean.Parse(headers.GetValues("showInactive").First());
            }

            //Check the request object to see if they passed a userId
            if (headers.Contains("userid"))
            {
                var user = headers.GetValues("userid").First();
                _log.InfoFormat("Handling GET request from user: {0}", user);

                try
                {
                    UserRoleActions userRoleActions = new UserRoleActions();
                    _log.Debug("Getting User Roles");
                    IEnumerable<UserRoleModel> userRoleList = userRoleActions.Get(showInActive);
                    var userRoleModels = userRoleList as IList<UserRoleModel> ?? userRoleList.ToList();
                    _log.DebugFormat("User Roles retreived Count: {0}", userRoleModels.Count());
                    return Ok(userRoleModels);
                }
                catch (Exception e)
                {
                    _log.Error("An error occurred while getting user Types.", e);
                    return InternalServerError(e);
                }
            }

            return BadRequest("Header value <userid> not found.");
        }

        // GET: api/user/5
        public IHttpActionResult Get(int id, HttpRequestMessage request)
        {
            if (_log.IsDebugEnabled)
            {
                _log.DebugFormat("Executing call in debug mode");
            }

            var headers = request.Headers;
            //Check the request object to see if they passed a userId
            if (headers.Contains("userid"))
            {
                var user = headers.GetValues("userid").First();
                _log.InfoFormat("Handling GET request from user: {0}", user);

                UserRoleActions userRoleActions = new UserRoleActions();
                try
                {
                    _log.Debug("Getting userType");

                    var userRoleModel = userRoleActions.GetById(id);
                    if (userRoleModel != null)
                    {
                        _log.DebugFormat("User Role retrieved. ID: {0}", userRoleModel.Id);
                        return Ok(userRoleModel);
                    }
                    return Ok();
                }
                catch (Exception e)
                {
                    _log.Error("An error occurred while getting User Role.", e);
                    return InternalServerError(e);
                }
            }
            return BadRequest("Header value <userid> not found.");
        }

        // POST: api/user
        public IHttpActionResult Post(HttpRequestMessage request, [FromBody]UserRoleModel value)
        {
            if (_log.IsDebugEnabled)
            {
                _log.DebugFormat("Executing call in debug mode");
            }

            var headers = request.Headers;
            //Check the request object to see if they passed a userId
            if (headers.Contains("userid"))
            {
                var user = headers.GetValues("userid").First();
                _log.InfoFormat("Handling POST request from user: {0}", user);

                if (!ModelState.IsValid)
                    return BadRequest("Invalid data.");
                try
                {
                    UserRoleActions userRoleActions = new UserRoleActions();

                    userRoleActions.Insert(value, user);
                    return Ok();
                }
                catch (Exception e)
                {
                    _log.Error("An error occurred while adding User.", e);
                    return InternalServerError(e);
                }
            }

            return BadRequest("Header value <userid> not found.");
        }

        // PUT: api/user/5
        public IHttpActionResult Put(HttpRequestMessage request, UserRoleModel value)
        {
            if (_log.IsDebugEnabled)
            {
                _log.DebugFormat("Executing call in debug mode");
            }

            var headers = request.Headers;
            //Check the request object to see if they passed a userId
            if (headers.Contains("userid"))
            {
                var user = headers.GetValues("userid").First();
                _log.InfoFormat("Handling PUT request from user: {0}", user);

                if (!ModelState.IsValid)
                    return BadRequest("Invalid data.");

                try
                {
                    UserRoleActions userRoleActions = new UserRoleActions();

                    userRoleActions.Update(value, user);
                    _log.Debug("User Role Updated");
                    return Ok();
                }
                catch (Exception e)
                {
                    _log.Error("An error occurred while updating User.", e);
                    return InternalServerError(e);
                }
            }

            return BadRequest("Header value <userid> not found.");
        }

        // DELETE: api/user/5
        public IHttpActionResult Delete(int id, HttpRequestMessage request, [FromBody]UserRoleModel value)
        {
            if (_log.IsDebugEnabled)
            {
                _log.DebugFormat("Executing call in debug mode");
            }

            var headers = request.Headers;
            //Check the request object to see if they passed a userId
            if (headers.Contains("userid"))
            {
                var user = headers.GetValues("userid").First();
                _log.InfoFormat("Handling DELETE request from user: {0}", user);

                if (!ModelState.IsValid)
                    return BadRequest("Invalid data.");

                try
                {
                    UserRoleActions userRoleActions = new UserRoleActions();

                    userRoleActions.Delete(id, user);
                    return Ok();
                }
                catch (Exception e)
                {
                    _log.Error("An error occurred while DeActivating User.", e);
                    return InternalServerError(e);
                }
            }

            return BadRequest("Header value <userid> not found.");
        }
    }
}
