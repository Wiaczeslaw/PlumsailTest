using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PlumsailTest.Domain.Forms;
using PlumsailTest.Domain.Models;
using PlumsailTest.Infrastructure.Filtering;
using PlumsailTest.Logic.Services.Abstractions;

namespace PlumsailTest.Controllers
{
    [Route("api/form")]
    public class FormController : ApiBaseController
    {
        private readonly IFormService _formService;
        
        public FormController(IFormService formService)
        {
            _formService = formService;
        }

        [HttpGet("get")]
        public async Task<IActionResult> GetFormTemplate([FromQuery] Guid id)
            => Ok( await _formService.GetAsync(id));

        [HttpGet("get/list")]
        public async Task<IActionResult> GetListFormTemplate([FromBody] FormFilter form)
            => Ok(await _formService.GetListAsync(form));
        
        [HttpPost("create")]
        public async Task<IActionResult> CreateFormTemplate([FromBody] CreateFormForm form)
            => Ok(await _formService.CreateAsync(form));
        
        [HttpPost("update")]
        public async Task<IActionResult> UpdateFormTemplate([FromBody] UpdateFormForm form)
            => Ok(await _formService.UpdateAsync(form));
        
        [HttpPost("delete")]
        public async Task<IActionResult> DeleteFormTemplate(Guid id)
        {
            await _formService.DeleteAsync(id);
            return NoContent();
        }
    }
}