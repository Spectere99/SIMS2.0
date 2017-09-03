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
    public class PermissionController : ApiController
    {
        static ILog _log = log4net.LogManager.GetLogger(
            System.Reflection.MethodBase.GetCurrentMethod().DeclaringType
        );
        // GET: api/user
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
                    PermissionActions permissionActions = new PermissionActions();
                    _log.Debug("Getting Permissions");
                    IEnumerable<PermissionModel> permissionList = permissionActions.Get(showInActive);
                    var permissionModels = permissionList as IList<PermissionModel> ?? permissionList.ToList();
                    _log.DebugFormat("Permissions retreived Count: {0}", permissionModels.Count());
                    return Ok(permissionModels);
                }
                catch (Exception e)
                {
                    _log.Error("An error occurred while getting Permission.", e);
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

                PermissionActions permissionActions = new PermissionActions();
                try
                {
                    _log.Debug("Getting Permission");

                    var permissionModel = permissionActions.GetById(id);
                    if (permissionModel != null)
                    {
                        _log.DebugFormat("Permission retrieved. ID: {0}", permissionModel.Id);
                        return Ok(permissionModel);
                    }
                    return Ok();
                }
                catch (Exception e)
                {
                    _log.Error("An error occurred while getting Permission.", e);
                    return InternalServerError(e);
                }
            }
            return BadRequest("Header value <userid> not found.");
        }

        // POST: api/user
        public IHttpActionResult Post(HttpRequestMessage request, [FromBody]PermissionModel value)
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
                    PermissionActions permissionActions = new PermissionActions();

                    permissionActions.Insert(value, user);
                    return Ok();
                }
                catch (Exception e)
                {
                    _log.Error("An error occurred while adding Permission.", e);
                    return InternalServerError(e);
                }
            }

            return BadRequest("Header value <userid> not found.");
        }

        // PUT: api/user/5
        public IHttpActionResult Put(HttpRequestMessage request, PermissionModel value)
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
                    PermissionActions permissionActions = new PermissionActions();

                    permissionActions.Update(value, user);
                    _log.Debug("Permission Updated");
                    return Ok();
                }
                catch (Exception e)
                {
                    _log.Error("An error occurred while updating Permission.", e);
                    return InternalServerError(e);
                }
            }

            return BadRequest("Header value <userid> not found.");
        }

        // DELETE: api/user/5
        public IHttpActionResult Delete(int id, HttpRequestMessage request)
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
                _log.InfoFormat("Handling  DELETE request from user: {0}", user);

                if (!ModelState.IsValid)
                    return BadRequest("Invalid data.");

                try
                {
                    PermissionActions permissionActions = new PermissionActions();

                    permissionActions.Deactivate(id, user);
                    return Ok();
                }
                catch (Exception e)
                {
                    _log.Error("An error occurred while DeActivating Permission.", e);
                    return InternalServerError(e);
                }
            }

            return BadRequest("Header value <userid> not found.");
        }
    }
}
