using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PlumsailTest.Domain.Forms;
using PlumsailTest.Domain.Models;
using PlumsailTest.Infrastructure.Filtering;

namespace PlumsailTest.Logic.Services.Abstractions
{
    public interface IFormService
    {
        /// <summary>
        /// Get Form by id
        /// </summary>
        Task<FormModel> GetAsync(Guid id);

        /// <summary>
        /// Get list Form by filter
        /// </summary>
        Task<IEnumerable<ShortFormModel>> GetListAsync(FormFilter filter);
        
        /// <summary>
        /// Create new Form
        /// </summary>
        Task<FormModel> CreateAsync(CreateFormForm form);

        /// <summary>
        /// Update Form by id
        /// </summary>
        Task<FormModel> UpdateAsync(UpdateFormForm form);

        /// <summary>
        /// Delete Form by id
        /// </summary>
        Task DeleteAsync(Guid id);
    }
}