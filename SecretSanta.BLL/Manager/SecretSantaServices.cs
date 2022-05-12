using Mapster;
using SecretSanta.BLL.Interfaces;
using SecretSanta.BLL.Model;
using SecretSanta.DAL.Entities;
using SecretSanta.DAL.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecretSanta.BLL.Manager
{
    public class SecretSantaServices : ISecretSantaServices
    {
        private readonly SecretSantaDBContext _context;
        private static bool isChanges = false;

        public SecretSantaServices(SecretSantaDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PairViewModel>> GetAllPairs()
        {
            var users = _context.People.ProjectToType<PersonViewModel>().ToList();

            if (users.Count < 2)
                throw new Exception("Please add more Persons");

            if (isChanges)
                await shuffleListOfUsers(users);

            var usersList = new LinkedList<PersonViewModel>(users);

            var pairs = usersList
                .Nodes()
                .Select(p => new PairViewModel
                {
                    Presenter = p.Value,
                    Receiver = p?.Next?.Value ?? usersList.First.Value
                })
                .ToList();

            isChanges = false;

            return pairs;
        }
        
        public async Task<PersonViewModel> Create(PersonCreateModel person)
        {
            var personModel = person.Adapt<Person>();

            _context.People.Add(personModel);
            await _context.SaveChangesAsync();

            isChanges = true;

            return personModel.Adapt<PersonViewModel>();
        }

        private async Task shuffleListOfUsers(IList<PersonViewModel> users)
        {
            var rng = new Random();
            int countOfUsers = users.Count;
            while (countOfUsers > 1)
            {
                countOfUsers--;
                int nextUserItem = rng.Next(countOfUsers + 1);
                var value = users[nextUserItem];
                users[nextUserItem] = users[countOfUsers];
                users[countOfUsers] = value;
            }
            
            isChanges = false;
        }
    }
}
