using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Test.Domain.Entities;
using Test.Domain.Forms;
using Test.Domain.Models;
using Test.Infrastructure.Filtering;
using Test.Logic.Services.Abstractions;

namespace Test.Controllers
{
    [Route("api/template")]
    public class TemplateController : ApiBaseController
    {
        private readonly ITemplateService _templateService;
        
        public TemplateController(ITemplateService templateService)
        {
            _templateService = templateService;
        }

        [HttpGet("get")]
        public async Task<IActionResult> GetFormTemplate([FromQuery] Guid id)
            => Ok( await _templateService.GetAsync(id));

        [HttpGet("get/list")]
        public async Task<IActionResult> GetListFormTemplate([FromQuery] FormFilter form)
            => Ok(await _templateService.GetListAsync(form));
        
        [HttpPost("create")]
        public async Task<IActionResult> CreateFormTemplate([FromBody] CreateTemplateForm form)
            => Ok(await _templateService.CreateAsync(form));
        
        [HttpPost("update")]
        public async Task<IActionResult> UpdateFormTemplate([FromBody] FormTemplateModel form)
            => Ok(await _templateService.UpdateAsync(form));
        
        [HttpPost("delete")]
        public async Task<IActionResult> DeleteFormTemplate(Guid id)
        {
            await _templateService.DeleteAsync(id);
            return NoContent();
        }
    }
}