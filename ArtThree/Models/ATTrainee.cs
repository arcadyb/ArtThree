using System;
using System.ComponentModel.DataAnnotations;

namespace ArtThree.Models
{
    public class ATTrainee
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Grade { get; set; } = 0;
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string Email { get; set; }
        public string Zip { get; set; }
        public DateTime DateJoined { get; set; } = DateTime.Now;

        [MaxLength(5000)] public string Subject { get; set; } = string.Empty;

        public void Update(ATTrainee traineeUpdated)
        {
            Name = traineeUpdated.Name;
            Subject = traineeUpdated.Subject;
            Address = traineeUpdated.Address;
            City = traineeUpdated.City;
            Country = traineeUpdated.Country; 
            Email = traineeUpdated.Email;
            Zip = traineeUpdated.Zip;
            DateJoined  = traineeUpdated.DateJoined;
            Grade = traineeUpdated.Grade;
         }
        public override string ToString()
        {
            return Name;
        }
    }
}