using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecretSanta.DAL.Entities
{
    public class Pair
    {
        public int Id { get; set; }

        public int PresenterId { get; set; }
        public Person Presenter { get; set; }

        public int ReceiverId { get; set; }
        public Person Receiver { get; set; }
    }
}
