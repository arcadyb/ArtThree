using ArtThree.Models;
using System.Collections.Generic;

namespace ArtThree
{
    public interface IATRepository
    {
        List<ATTrainee> GetTrainies();

    }
}