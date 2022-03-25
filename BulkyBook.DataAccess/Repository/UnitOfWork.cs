using BulkyBook.DataAccess.Repository.iRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public ICategoryRepository Category { get; private set; }

        public ICoverTypeRepository CoverType { get; private set; }

        public IProductRepository Product { get; private set; }

        public ICompanyRepository Company { get; }

        public IApplicationUserRepository ApplicationUser { get; }

        public IShoppingCartRepository ShoppingCart { get; }

        private ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext Db)
        {
            _db = Db;
            Category = new CategoryRepository(_db);
            CoverType = new CoverTypeRepository(_db);
            Product = new ProductRepository(_db);
            Company = new CompanyRepository(_db);
            ApplicationUser = new ApplicationUserRepository(_db);
            ShoppingCart = new ShoppingCartRepository(_db);

        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
