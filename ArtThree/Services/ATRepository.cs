using System;
using System.Collections.Generic;
using System.Linq;
using ArtThree.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ArtThree
{
    public class ATRepository : IATRepository
    {
        private readonly ATDbContext _context;
        private readonly IConfiguration _config;


        public List<ATTrainee> GetTrainies()
        {
            return new List<ATTrainee>();
        }
        public ATRepository([FromServices] ATDbContext dbctx,
                            IConfiguration config)
        {
            _context = dbctx;
            _config = config;
        }
        public ATTrainee GetTraineeById(Guid traineeId)
        {
            //return p_context.Contacts.Include(c => c.Company).OrderBy(c => c.Name);
            return _context.Trainees.FirstOrDefault(c => c.Id == traineeId);
        }

        public ATTrainee AddOrUpdateContact(ATTrainee atTrainee)
        {
            if (atTrainee.Id == Guid.Empty)
            {
                _context.Trainees.Add(atTrainee);
                return atTrainee;
            }
            var originalTrainee = GetTraineeById(atTrainee.Id);
            originalTrainee.Update(atTrainee);
            _context.Update(originalTrainee);
            return originalTrainee;
        }
    }


}
