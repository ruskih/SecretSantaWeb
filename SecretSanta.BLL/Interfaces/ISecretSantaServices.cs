using SecretSanta.BLL.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SecretSanta.BLL.Interfaces
{
    public interface ISecretSantaServices
    {
        Task<IEnumerable<PairViewModel>> GetAllPairs();

        Task<PersonViewModel> Create(PersonCreateModel item);
    }
}
