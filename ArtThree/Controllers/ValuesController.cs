using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ArtThree.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ArtThree.Controllers
{
    [Route("api/Values"), Produces("application/json"), EnableCors("AppPolicy")]
    public class ValuesController : Controller
    {
        
        private readonly ATDbContext m_context;
        private readonly IATRepository m_repoService;
        private readonly IViewRenderService m_viewEngine;

        public ValuesController([FromServices] ATDbContext context,
            [FromServices] IViewRenderService viewEngine, IATRepository repoService)
        {
            m_context = context;
            m_repoService = repoService;
            m_viewEngine = viewEngine;
        }

 

        // GET: api/Values/GetUser
        [HttpGet, Route("GetUser")]
        public async Task<object> GetUser()
        {
            List<ATUser> users = null;
            object result = null;
            try
            {
                using (m_context)
                {
                    users = await m_context.User.ToListAsync();
                    result = new
                    {
                        User
                    };
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return users;
        }

        // GET api/Values/GetUserByID/5
        [HttpGet, Route("GetUserByID/{id}")]
        public async Task<ATUser> GetUserByID(int id)
        {
            ATUser ATUser = null;
            try
            {
                using (m_context)
                {
                    ATUser = await m_context.User.FirstOrDefaultAsync(x => x.Id == id);
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return ATUser;
        }


        // POST api/Values/PostUser
        [HttpPost, Route("PostUser")]
        public async Task<object> PostUser([FromBody]ATUser model)
        {
            object result = null; string message = "";
            if (model == null)
            {
                return BadRequest();
            }
            using (m_context)
            {
                using (var _ctxTransaction = m_context.Database.BeginTransaction())
                {
                    try
                    {
                        m_context.User.Add(model);
                        await m_context.SaveChangesAsync();
                        _ctxTransaction.Commit();
                        message = "Saved Successfully";
                    }
                    catch (Exception e)
                    {
                        _ctxTransaction.Rollback();
                        e.ToString();
                        message = "Saved Error";
                    }

                    result = new
                    {
                        message
                    };
                }
            }
            return result;
        }

        // PUT api/Values/PutUser/5
        [HttpPut, Route("PutUser/{id}")]
        public async Task<object> PutUser(int id, [FromBody]ATUser model)
        {
            object result = null; string message = "";
            if (model == null)
            {
                return BadRequest();
            }
            using (m_context)
            {
                using (var _ctxTransaction = m_context.Database.BeginTransaction())
                {
                    try
                    {
                        var entityUpdate = m_context.User.FirstOrDefault(x => x.Id == id);
                        if (entityUpdate != null)
                        {
                            entityUpdate.Name = model.Name;
                            entityUpdate.Phone = model.Phone;
                            entityUpdate.Email = model.Email;

                            await m_context.SaveChangesAsync();
                        }
                        _ctxTransaction.Commit();
                        message = "Entry Updated";
                    }
                    catch (Exception e)
                    {
                        _ctxTransaction.Rollback(); e.ToString();
                        message = "Entry Update Failed!!";
                    }

                    result = new
                    {
                        message
                    };
                }
            }
            return result;
        }

        // DELETE api/Values/DeleteUserByID/5
        [HttpDelete, Route("DeleteUserByID/{id}")]
        public async Task<object> DeleteUserByID(int id)
        {
            object result = null; string message = "";
            using (m_context)
            {
                using (var _ctxTransaction = m_context.Database.BeginTransaction())
                {
                    try
                    {
                        var idToRemove = m_context.User.SingleOrDefault(x => x.Id == id);
                        if (idToRemove != null)
                        {
                            m_context.User.Remove(idToRemove);
                            await m_context.SaveChangesAsync();
                        }
                        _ctxTransaction.Commit();
                        message = "Deleted Successfully";
                    }
                    catch (Exception e)
                    {
                        _ctxTransaction.Rollback(); e.ToString();
                        message = "Error on Deleting!!";
                    }

                    result = new
                    {
                        message
                    };
                }
            }
            return result;
        }
    }
}
