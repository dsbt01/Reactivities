using BulkyBook.DataAccess.Repository.iRepository;
using BulkyBook.Models;
using BulkyBooks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository
{
    internal class CoverTypeRepository : Repository<CoverType>, ICoverTypeRepository
    {
        private ApplicationDbContext _db;

        public CoverTypeRepository(ApplicationDbContext Db) : base(Db)
        {
            _db = Db;
        }

        public void Update(CoverType coverType)
        {
            _db.CoverTypes.Update(coverType);
        }
    }
}
