using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PlumsailTest.Domain.Forms;
using PlumsailTest.Domain.Models;
using PlumsailTest.Infrastructure.Filtering;

namespace PlumsailTest.Logic.Services.Abstractions
{
    public interface ITemplateService
    {
        /// <summary>
        /// Get FormTemplate by id
        /// </summary>
        Task<FormTemplateModel> GetAsync(Guid id);

        /// <summary>
        /// Get list FormTemplate by filter
        /// </summary>
        Task<IEnumerable<FormTemplateModel>> GetListAsync(FormFilter filter);
        
        /// <summary>
        /// Create new FormTemplate
        /// </summary>
        Task<FormTemplateModel> CreateAsync(CreateTemplateForm form);

        /// <summary>
        /// Update FormTemplate by id
        /// </summary>
        Task<FormTemplateModel> UpdateAsync(FormTemplateModel form);

        /// <summary>
        /// Delete FormTemplate by id
        /// </summary>
        Task DeleteAsync(Guid id);
    }
}