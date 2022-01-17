using API_CRUD.Infrastructure.Context;
using API_CRUD.Infrastructure.Repositories.Interfaces;
using API_CRUD.Model.Abstract;
using API_CRUD.Model.Concrete;
using API_CRUD.Models.DTOs;
using API_CRUD.Models.VMs;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace API_CRUD.Infrastructure.Repositories.Concrete
{
    public class CategoryRepository:ICategoryRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CategoryRepository(ApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _context = applicationDbContext;
            _mapper = mapper;
        }


        public async Task<bool> CategoryExists(string slug) => await _context.Categories.AnyAsync(x => x.Slug.ToLower().Trim() == slug.ToLower().Trim());


        public async Task<bool> CategoryExists(int id) => await _context.Categories.AnyAsync(x => x.Id == id);


        public async Task<bool> Create(CreateCategoryDTO model)
        {
            var category = _mapper.Map<Category>(model);
            await _context.Categories.AddAsync(category);
            return await Save();
        }

        public async Task<bool> Delete(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            category.Status = Status.Passive;
            category.DeleteDate = DateTime.Now;
            return await Save();
        }

        public async Task<ICollection<TResult>> GetCategories<TResult>(Expression<Func<Category, TResult>> selector,
                                                                       Expression<Func<Category, bool>> where = null,
                                                                       Func<IQueryable<Category>, IOrderedQueryable<Category>> orderBy = null)
        {

            IQueryable<Category> query = _context.Categories;

            if (where != null)
            {
                query = query.Where(where);
            }

            if (orderBy != null)
            {
                return await orderBy(query).Select(selector).ToListAsync();
            }
            else
            {
                return await query.Select(selector).ToListAsync();
            }
        }

        public async Task<GetCategoryVM> GetCategory(int id)
        {
            var categoryObj = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);

            var category = _mapper.Map<GetCategoryVM>(categoryObj);

            return category;
        }

        public async Task<GetCategoryVM> GetCategory(string slug)
        {
            var categoryObj = await _context.Categories.FirstOrDefaultAsync(x => x.Slug == slug);

            var category = _mapper.Map<GetCategoryVM>(categoryObj);

            return category;
        }

        public async Task<bool> Save() => await _context.SaveChangesAsync() >= 0 ? true : false;


        public async Task<bool> Update(UpdateCatgoryDTO model)
        {
            var category = _mapper.Map<Category>(model);
            _context.Categories.Update(category);
            return await Save();
        }

    }
}
