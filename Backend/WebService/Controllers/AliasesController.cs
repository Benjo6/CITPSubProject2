﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common;
using Common.DataTransferObjects;
using DataLayer.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
namespace WebService.Controllers;

[Route("[controller]")]
[ApiController]
public class AliasesController : ControllerBase
{
    private readonly IAliasesService _service;

    public AliasesController(IAliasesService service)
    {
        _service = service;
    }

    // GET: Aliases
    [HttpGet]
    public async Task<IActionResult> GetAliases(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] Dictionary<string,string>? conditions = null,
        [FromQuery] string sortBy = "Id",
        [FromQuery] bool asc = true)
    {
        try
        {
            var aliases = await _service.GetAllAliases(new Filter(page,pageSize,sortBy,asc,conditions));
            return Ok(aliases);
        }
        catch(Exception ex)
        {
            return BadRequest(new {message = ex.Message});
        }
    }

    // GET: Aliases/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetAlias(string id)
    {
        try
        {
            var alias = await _service.GetOneAlias(id);
            return Ok(alias);

        }
        catch (Exception ex)
        {
            return BadRequest(new {message = ex.Message});
        }
    }

    // PUT: api/Aliases/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutAlias(string id, AlterAliasDTO alias)
    {
        try
        {
            var putAlias = await _service.UpdateAlias(id, alias);
            return Ok(putAlias);

        }
        catch (Exception ex)
        {
            return BadRequest(new {message = ex.Message});
        }
    }

    // POST: api/Aliases
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<IActionResult> PostAlias(AlterAliasDTO alias)
    {
        try
        {
            var postAlias = await _service.AddAlias(alias);
            return Ok(postAlias);

        }
        catch (Exception ex)
        {
            return BadRequest(new {message = ex.Message});
        }
    }

    // DELETE: Aliases/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAlias(string id)
    {
        try
        {
            var result = await _service.DeleteAlias(id);
            return Ok(result);

        }
        catch (Exception ex)
        {
            return BadRequest(new {message = ex.Message});
        }
    }
}