using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArtThree.Models
{
    public class ATDbContext: DbContext
    {
        private readonly IConfiguration _config;

        public ATDbContext([FromServices] IConfiguration config)
            : base()
        {
            _config = config;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connection = _config["Database:SqlServerConnection"];
            PlatformID platform = Environment.OSVersion.Platform;
            if (platform != PlatformID.Win32NT)
            {
                //connection = _config["Database:SqliteFilename"];
                //optionsBuilder.UseSqlite($"Filename={connection}");
            }
            else
            {
                connection = _config["Database:SqlServerConnection"];
                optionsBuilder.UseSqlServer(connection);
            }
        }

        public DbSet<ATTrainee> Trainees { get; set; }
 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
