using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecretSanta.BLL.Manager
{
    public static class CustomListExtensions
    {
        public static IEnumerable<LinkedListNode<T>> Nodes<T>(this LinkedList<T> list)
        {
            for (var n = list.First; n != null; n = n.Next)
                yield return n;
        }
    }
}
