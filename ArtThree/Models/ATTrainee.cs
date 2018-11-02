using System;
using System.ComponentModel.DataAnnotations;

namespace ArtThree.Models
{
    public class ATTrainee
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Fax { get; set; } = string.Empty;
        public string Email { get; set; }
        public string Mobile { get; set; }

        public override string ToString()
        {
            return GetActualName();
        }

        public string GetActualName()
        {
            return $"{FirstName} {LastName}";
        }
        public Guid Id { get; set; }

        public DateTime DateCreated { get; set; } = DateTime.Now;
        public bool IsActive { get; set; } = true;
        public string Name { get; set; } = string.Empty;
        public string Tags { get; set; } = string.Empty;
        [MaxLength(5000)] public string Description { get; set; } = string.Empty;

        public void Update(ATTrainee traineeUpdated)
        {
            Name = traineeUpdated.Name;
            Description = traineeUpdated.Description;
            
            IsActive = traineeUpdated.IsActive;
            LastName = traineeUpdated.LastName;
            FirstName = traineeUpdated.FirstName;
            Mobile = traineeUpdated.Mobile;
            Phone = traineeUpdated.Phone;
            Mobile = traineeUpdated.Mobile;
            Position = traineeUpdated.Position;
            
            Email = traineeUpdated.Email;
            Fax = traineeUpdated.Fax;
        }
    }
}