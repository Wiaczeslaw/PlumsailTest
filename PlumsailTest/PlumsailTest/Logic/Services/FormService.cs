using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PlumsailTest.DAL.Sql;
using PlumsailTest.Domain.Entities;
using PlumsailTest.Domain.Forms;
using PlumsailTest.Domain.Models;
using PlumsailTest.Infrastructure.Exceptions;
using PlumsailTest.Infrastructure.Extensions;
using PlumsailTest.Infrastructure.Filtering;
using PlumsailTest.Logic.Services.Abstractions;

namespace PlumsailTest.Logic.Services
{
    public class FormService : IFormService
    {
        private readonly DatabaseContext _db;

        public FormService(DatabaseContext context)
        {
            _db = context;
        }
        
        public async Task<IEnumerable<FormModel>> GetListAsync(FormFilter filter)
        {
            if (filter == null)
            {
                throw new AppBadRequestException(nameof(filter), "Form cannot be empty");
            }
            
            var queryable = _db.Forms
                .Where(x => !x.IsDeleted);

            if (filter.Search.HasValue())
            {
                filter.Search = filter.Search.Replace("ё", "е");
                var pattern = $"%{Regex.Replace(filter.Search, @"\s{2,}", " ").ToLower().Split(" ").Join("%")}%";
                queryable = queryable.Where(template => EF.Functions.ILike(template.Name.ToLower().Replace("ё", "е"), pattern));
            }

            if (filter.Count > 0)
            {
                queryable = queryable.Take(filter.Count);
            }
               
            return await queryable.Select(FormModelSelector).ToArrayAsync();

        }

        public async Task<FormModel> GetAsync(Guid id)
        {
            if (id == default)
            {
                throw new AppBadRequestException(nameof(id), "Invalid id");
            }
            
            return await _db.Forms
                .Where(x => !x.IsDeleted && x.Id == id)
                .Select(FormModelSelector)
                .FirstOrDefaultAsync();
        }
        
        public async Task<FormModel> CreateAsync(CreateFormForm form)
        {
            if (form == null)
            {
                throw new AppBadRequestException(nameof(form), "Form cannot be empty");
            }
            
            var formEntity = new Form()
            {
                Name = form.Name,
                FormTemplateId = form.TemplateId,
                FormItems = form.Items.Select(i => new FormItem()
                {
                    Value = i.Value,
                    FormItemSelectValueId = i.FormItemSelectValueId,
                    FormItemTemplateId = i.FormItemTemplateId
                }).ToArray()
            };

            await _db.Forms.AddAsync(formEntity);
            await _db.SaveChangesAsync();

            return await GetAsync(formEntity.Id);
        }

        public async Task<FormModel> UpdateAsync(UpdateFormForm form)
        {
            if (form == null)
            {
                throw new AppBadRequestException(nameof(form), "Form cannot be empty");
            }
            
            var formEntity = await _db.Forms.Where(x => x.Id == form.Id).FirstOrDefaultAsync();
            if (formEntity == null)
            {
                throw new AppBadRequestException(nameof(form.Id), "Invalid id");
            }
            
            formEntity.Name = form.Name;
            _db.Forms.Update(formEntity);

            foreach (var item in form.Items)
            {
                var updatedItem = formEntity.FormItems.FirstOrDefault(x => x.Id == item.Id);
                if (updatedItem != null)
                {
                    updatedItem.Value = item.Value;
                    updatedItem.FormItemSelectValueId = item.FormItemSelectValueId;
                }
            }

            _db.Forms.Update(formEntity);
            await _db.SaveChangesAsync();

            return await GetAsync(formEntity.Id);
        }
        
        public async Task DeleteAsync(Guid id)
        {
            var formEntity = await _db.Forms
                .Include(x => x.FormItems)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            if (formEntity == null)
            {
                throw new AppBadRequestException(nameof(id), "Invalid id");
            }
            
            formEntity.IsDeleted = true;
            foreach (var itemTemplate in formEntity.FormItems)
            {
                itemTemplate.IsDeleted = true;
            }
            _db.Forms.Update(formEntity); 
            await _db.SaveChangesAsync();
        }
        
        private static readonly Expression<Func<Form, FormModel>> FormModelSelector = x =>
            new FormModel()
            {
                Id = x.Id,
                Name = x.Name,
                Items = x.FormItems.Where(i => !i.IsDeleted).Select(i => new FormItemModel
                {
                    Id = i.Id,
                    Name = i.FormItemTemplate.Name,
                    Order = i.FormItemTemplate.Order,
                    Type = i.FormItemTemplate.Type,
                    Value = i.Value,
                    SelectValue = new FormItemSelectValueModel
                    {
                        Id = i.FormItemSelectValue.Id,
                        Value = i.FormItemSelectValue.Value
                    }
                })
            };
    }
}