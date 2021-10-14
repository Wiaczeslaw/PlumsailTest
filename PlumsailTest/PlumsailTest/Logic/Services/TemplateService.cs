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
using PlumsailTest.Infrastructure.Filtering;
using PlumsailTest.Infrastructure.Extensions;
using PlumsailTest.Logic.Services.Abstractions;

namespace PlumsailTest.Logic.Services
{
    public class TemplateService : ITemplateService
    {
        private readonly DatabaseContext _db;

        public TemplateService(DatabaseContext context)
        {
            _db = context;
        }
        
        public async Task<IEnumerable<FormTemplateModel>> GetListAsync(FormFilter filter)
        {
            if (filter == null)
            {
                throw new AppBadRequestException(nameof(filter), "Form cannot be empty");
            }
            
            var queryable = _db.FormTemplates
                .Include(x => x.ItemTemplates)
                .ThenInclude(x => x.Values)
                .Where(x => !x.IsDeleted);

            if (filter.Search.HasValue())
            {
                filter.Search = filter.Search.Replace("ё", "е");
                var pattern = $"%{Regex.Replace(filter.Search, @"\s{2,}", " ").ToLower().Split(" ").Join("%")}%";
                queryable = queryable.Where(template => EF.Functions.ILike(template.Name.ToLower().Replace("ё", "е"), pattern));
            }

            if (filter.Count > 0)
                queryable = queryable.Take(filter.Count);

            return await queryable.Select(FormTemplateSelector).ToArrayAsync();

        }

        public async Task<FormTemplateModel> GetAsync(Guid id)
        {
            if (id == default)
            {
                throw new AppBadRequestException(nameof(id), "Invalid id");  
            }
            
            return await _db.FormTemplates
                .Include(x => x.ItemTemplates)
                .ThenInclude(x => x.Values)
                .Where(x => !x.IsDeleted && x.Id == id)
                .Select(FormTemplateSelector)
                .FirstOrDefaultAsync();
        }
        
        public async Task<FormTemplateModel> CreateAsync(CreateTemplateForm form)
        {
            if (form == null)
            {
                throw new AppBadRequestException(nameof(form), "Form cannot be empty");
            }
            
            var template = new FormTemplate()
            {
                Name = form.Name,
                ItemTemplates = form.Items.Select(i => new FormItemTemplate()
                {
                    Name = i.Name,
                    Order = i.Order,
                    Type = i.Type,
                    Values = i.Values.Select(sv => new FormItemSelectValue() {Value = sv}).ToArray()
                }).ToArray()
            };

            await _db.FormTemplates.AddAsync(template);
            await _db.SaveChangesAsync();

            return await GetAsync(template.Id);
        }

        public async Task<FormTemplateModel> UpdateAsync(FormTemplateModel form)
        {
            if (form == null)
            {
                throw new AppBadRequestException(nameof(form), "Form cannot be empty");
            }
            
            var template = await _db.FormTemplates.Where(x => x.Id == form.Id)
                .Include(x => x.ItemTemplates).ThenInclude(x => x.Values)
                .FirstOrDefaultAsync();
            if (template == null)
            {
                throw new AppBadRequestException(nameof(form.Id), "Invalid id"); 
            }
            
            var formsByTemplate = await _db.Forms.Where(x => x.FormTemplateId == form.Id).ToListAsync();
            
            template.Name = form.Name;
            _db.FormTemplates.Update(template);
            
            foreach (var deleteItem in template.ItemTemplates.Where(x => !form.Items.Select(fi => fi.Id).Contains(x.Id)))
            {
                deleteItem.IsDeleted = true;

                var formItemsForDelete = _db.FormItems.Where(x => x.FormItemTemplateId == deleteItem.Id).ToList();
                if (formItemsForDelete.Count > 0)
                {
                    foreach (var deleteFormItem in formItemsForDelete)
                    {
                        deleteFormItem.IsDeleted = true;
                        _db.FormItems.Update(deleteFormItem);
                    }

                    await _db.SaveChangesAsync();
                }
            }
            
            foreach (var item in form.Items)
            {
                var updatedItem = template.ItemTemplates.FirstOrDefault(x => x.Id == item.Id);
                if (updatedItem == null)
                {
                    updatedItem = new FormItemTemplate();
                    template.ItemTemplates.Add(updatedItem);
                    
                    if (formsByTemplate.Count > 0)
                    {
                        foreach (var formEntity in formsByTemplate)
                        {
                            formEntity.FormItems.Add(new FormItem{FormItemTemplate = updatedItem});
                            _db.Forms.Update(formEntity);
                        }

                        await _db.SaveChangesAsync();
                    }
                }
                
                updatedItem.Name = item.Name;
                updatedItem.Type = item.Type;
                updatedItem.Order = item.Order;
                
                updatedItem.Values ??= new List<FormItemSelectValue>();
                foreach (var formValue in item.Values)
                {
                    var updatedValue = updatedItem.Values?.FirstOrDefault(x => x.Id == formValue.Id);
                    if (updatedValue == null)
                    {
                        updatedValue = new FormItemSelectValue();
                        updatedItem.Values.Add(updatedValue);
                    } 
                    updatedValue.Value = formValue.Value;
                }
                
                foreach (var deleteValues in updatedItem.Values?.Where(x => item.Values.Select(fv => fv.Id).Contains(x.Id)))
                {
                    deleteValues.IsDeleted = true;
                }
            }

            _db.FormTemplates.Update(template);
            await _db.SaveChangesAsync();

            return await GetAsync(template.Id);
        }
        
        public async Task DeleteAsync(Guid id)
        {
            var template = await _db.FormTemplates
                .Include(x => x.ItemTemplates)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            if (template == null)
            {
                throw new AppBadRequestException(nameof(id), "Invalid id"); 
            }
            
            template.IsDeleted = true;
            foreach (var itemTemplate in template.ItemTemplates)
            {
                itemTemplate.IsDeleted = true;
            }
            _db.FormTemplates.Update(template); 
            await _db.SaveChangesAsync();
        }
        
        private static readonly Expression<Func<FormTemplate, FormTemplateModel>> FormTemplateSelector = x =>
            new FormTemplateModel()
            {
                Id = x.Id,
                Name = x.Name,
                Items = x.ItemTemplates.Where(i => !i.IsDeleted).Select(i => new FormItemTemplateModel()
                {
                    Id = i.Id,
                    Name = i.Name,
                    Order = i.Order,
                    Type = i.Type,
                    Values = i.Values.Where(sv => !sv.IsDeleted).Select(sv => new FormItemSelectValueModel()
                    {
                        Id = sv.Id,
                        Value = sv.Value
                    })
                })
            };
    }
}