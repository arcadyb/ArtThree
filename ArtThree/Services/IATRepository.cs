using ArtThree.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArtThree
{
    public interface IATRepository
    {
        Task<List<ATTrainee>> GetTrainies();
        ATTrainee GetTraineeById(int traineeId);
        Task<ATTrainee> UpdateTrainee(ATTrainee atTrainee);
        Task<ATTrainee> AddTrainee(ATTrainee atTrainee);
        bool Delete(int traineeId);
    }
}