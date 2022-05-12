using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SecretSanta.DAL.Entities
{
    public class Person
    {
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public IList<Pair> Presenters { get; set; }
        public IList<Pair> Receivers { get; set; }
    }
}
