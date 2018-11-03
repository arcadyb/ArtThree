using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArtThree.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ArtThree
{
    public class ATRepository : IATRepository
    {
        private readonly ATDbContext _context;
        private readonly IConfiguration _config;


        public async Task<List<ATTrainee>> GetTrainies()
        {
            List<ATTrainee> users = null;
           
            try
            {
                using (_context)
                {
                    users = await _context.Trainees.ToListAsync();

                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return users;
        }
        public ATRepository([FromServices] ATDbContext dbctx,
                            IConfiguration config)
        {
            _context = dbctx;
            _config = config;
        }
        public ATTrainee GetTraineeById(int traineeId)
        {
            //return p_context.Contacts.Include(c => c.Company).OrderBy(c => c.Name);
            return _context.Trainees.FirstOrDefault(c => c.Id == traineeId);
        }

        public async Task<ATTrainee> AddTrainee(ATTrainee atTrainee)
        {
            object result = null; string message = "";
            if (atTrainee == null)
            {
                return null;
            }
 
                using (var _ctxTransaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _context.Trainees.Add(atTrainee);
                        await _context.SaveChangesAsync();
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
          
            return GetTraineeById(atTrainee.Id);
        }

        public bool Delete(int traineeId)
        {
            throw new NotImplementedException();
        }

        public Task<ATTrainee> UpdateTrainee(ATTrainee atTrainee)
        {
            throw new NotImplementedException();
        }
    }


}
