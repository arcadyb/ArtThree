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
        [HttpGet, Route("GetAll")]
        public async Task<object> GetAll()
        {
            List<ATTrainee> users = null;
            try
            {
                users= await m_repoService.GetTrainies();

            }
            catch (Exception ex)
            {

                ex.ToString();
            }           
            return users;
        }

        // GET api/Values/GetUserByID/5
        [HttpGet, Route("GetUserByID/{id}")]
        public async Task<ATTrainee> GetUserByID(int id)
        {
            ATTrainee ATTrainee = new ATTrainee() {Name="error" };
            try
            {
                using (m_context)
                {
                    ATTrainee = await m_context.Trainees.FirstOrDefaultAsync(x => x.Id == id);
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return ATTrainee;
        }


        // POST api/Values/PostUser
        [HttpPost, Route("PostUser")]
        public async Task<ATTrainee> PostUser([FromBody]ATTrainee model)
        {
                ATTrainee ATTraineeErr = new ATTrainee() { Name = "error" };
                string message = "";
                if (model == null)
                {
                    return ATTraineeErr;
                }
 
                ATTrainee ATTrainee = new ATTrainee() { Name = "error" };
                try
                {
                        ATTrainee = await m_repoService.AddTrainee(model);
                }
                catch (Exception ex)
                {
                    ex.ToString();
                }
                return ATTrainee;
            }

        // PUT api/Values/PutUser/5
        [HttpPut, Route("PutUser/{id}")]
        public async Task<object> PutUser(int id, [FromBody]ATTrainee model)
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
                        var entityUpdate = m_context.Trainees.FirstOrDefault(x => x.Id == id);
                        if (entityUpdate != null)
                        {
                            entityUpdate.Update(model);
           
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
                        var idToRemove = m_context.Trainees.SingleOrDefault(x => x.Id == id);
                        if (idToRemove != null)
                        {
                            m_context.Trainees.Remove(idToRemove);
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
