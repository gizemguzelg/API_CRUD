using API_CRUD.Model.Concrete;
using API_CRUD.Models.DTOs;
using API_CRUD.Models.VMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace API_CRUD.Infrastructure.Repositories.Interfaces
{
        public interface ICategoryRepository
        {
            Task<ICollection<TResult>> GetCategories<TResult>(Expression<Func<Category, TResult>> selector,
                                                              Expression<Func<Category, bool>> where = null,
                                                              Func<IQueryable<Category>, IOrderedQueryable<Category>> orderBy = null);

            Task<GetCategoryVM> GetCategory(int id);
            Task<GetCategoryVM> GetCategory(string slug);

            Task<bool> CategoryExists(string slug);
            Task<bool> CategoryExists(int id);

            Task<bool> Create(CreateCategoryDTO model);
            Task<bool> Update(UpdateCatgoryDTO model);
            Task<bool> Delete(int id);

            Task<bool> Save();
        }
    
}
