using EC2_1601226.Data;
using EC2_1601226.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EC2_1601226.Repository
{
   
    public class BagRepository : IBagRepository
    { 
        private readonly EC2_1601226Context _context;
        public BagRepository(EC2_1601226Context context)
        {
            _context = context;
        }

        public IEnumerable<Bag> Bags => _context.Bag.ToList();

        public Bag GetBagById(int bagId) => _context.Bag.FirstOrDefault(p => p.Id == bagId);

    }
}
