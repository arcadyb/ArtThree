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

        public ATTrainee AddOrUpdateTrainee(ATTrainee atTrainee)
        {
            if (atTrainee.Id == 0)
            {
                _context.Trainees.Add(atTrainee);
                return atTrainee;
            }
            var originalTrainee = GetTraineeById(atTrainee.Id);
            originalTrainee.Update(atTrainee);
            _context.Update(originalTrainee);
            return originalTrainee;
        }

        public bool Delete(int traineeId)
        {
            throw new NotImplementedException();
        }
    }


}
