using EC2_1601226.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EC2_1601226.Repository
{
    public interface IBagRepository
    {
        IEnumerable<Bag> Bags { get; }
        Bag GetBagById(int bagId);
    }
}
