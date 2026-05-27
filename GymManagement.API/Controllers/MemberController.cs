using AutoMapper;
using GymManagement.API.DTOs.Response;
using GymManagement.Domain.Entities;
using GymManagement.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace GymManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MemberController : ControllerBase
{
    private readonly IMemberService _memberService;
    private readonly IMapper _mapper;
    private readonly ILogger<MemberController> _logger;

    public MemberController(IMemberService memberService, IMapper mapper, ILogger<MemberController> logger)
    {
        _memberService = memberService;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MemberResponseDTO>>> GetAll()
    {
        var members = await _memberService.GetAllAsync();
        var memberDTO = _mapper.Map<IEnumerable<MemberResponseDTO>>(members);
        return Ok(memberDTO);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<MemberResponseDTO>> GetById(int id)
    {
        var member = await _memberService.GetByIdAsync(id);
        if (member == null)
        {
            return NotFound(new {message = $"Usuario con ID: {id} no encontrado"});
        }
        var memberDTO = _mapper.Map<MemberResponseDTO>(member);
        return Ok(memberDTO);
    }

    [HttpGet("{document}")]
    public async Task<ActionResult<MemberResponseDTO>> GetByDocument(int document)
    {
        var member = await _memberService.GetByDocumentAsync(document);
        if (member == null)
        {
            return NotFound(new { message = $"Usuario con documento: {document} no encontrado" });
        }
        var memberDTO = _mapper.Map<MemberResponseDTO>(member);
        return Ok(memberDTO);
    }

    [HttpPost]
    public async Task<ActionResult<MemberResponseDTO>> Create(MemberResponseDTO dto)
    {
        try
        {
            var member = _mapper.Map<Member>(dto);
            var createdMember = await _memberService.CreateAsync(member);
            var responseDTO = _mapper.Map<MemberResponseDTO>(createdMember);
            
            return CreatedAtAction(nameof(GetById), new { id = responseDTO.Id }, responseDTO);
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogWarning(ex, "Error creating member with document: {Document}", dto.Document);
            return Conflict(new { message = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, MemberResponseDTO dto)
    {
        try
        {
            var member = _mapper.Map<Member>(dto);
            await _memberService.UpdateAsync(id, member);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new {message = ex.Message});
        }
    }

    [HttpPut("{document}")]
    public async Task<ActionResult> UpdateDocument(int document, MemberResponseDTO dto)
    {
        try
        {
            var member = _mapper.Map<Member>(dto);
            await _memberService.UpdateAsync(document, member);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new {message = ex.Message});
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            await _memberService.DeleteAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new {message = ex.Message});
        }
    }

    [HttpDelete("{document}")]
    public async Task<ActionResult> DeleteForDocumento(int document)
    {
        try
        {
            await _memberService.DeleteAsync(document);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
}
