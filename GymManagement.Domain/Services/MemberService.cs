using GymManagement.Domain.Entities;
using GymManagement.Domain.Interfaces.Repositories;
using GymManagement.Domain.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace GymManagement.Domain.Services;

public class MemberService : IMemberService
{
    private readonly IMemberRepository _memberRepository;
    private readonly ILogger<MemberService> _logger;

    public MemberService(IMemberRepository memberRepository, ILogger<MemberService> logger)
    {
        _memberRepository = memberRepository;
        _logger = logger;
    }

    public async Task<IEnumerable<Member>> GetAllAsync()
    {
        _logger.LogInformation("Getting all members");
        return await _memberRepository.GetAllAsync();
    }

    public async Task<Member?> GetByIdAsync(int id)
    {
        _logger.LogInformation("Getting member by id: {Id}", id);
        var member = await _memberRepository.GetByIdAsync(id);

        if (member == null)
        {
            _logger.LogWarning("Member with id {Id} not found", id);
        }
        return member;
    }

    public async Task<Member?> CreateAsync(Member member)
    {
        // Validación de negocio, que el documento no se repita
        var existingMember = await _memberRepository.GetByDocument(member.Document);
        if (existingMember != null)
        {
            _logger.LogWarning("Member with document {Document} already exists", member.Document);
            throw new InvalidOperationException($"Member with document {member.Document} already exists");
        }

        _logger.LogInformation("Creating member with document: {Document}", member.Document);
        return await _memberRepository.CreateAsync(member);
    }

    public async Task UpdateAsync(int id, Member member)
    {
        var existingMember = await _memberRepository.GetByIdAsync(id);
        if (existingMember == null)
        {
            _logger.LogWarning("Member with id {Id} not found for update", id);
            throw new KeyNotFoundException($"No se encontró el usuario con ID {id}");
        }

        if (existingMember.Document != member.Document)
        {
            var document = await _memberRepository.GetByDocument(member.Document);
            if (document != null)
            {
                throw new InvalidOperationException($"Ya existe un usario con el Dcomuento: {member.Document}");
            }
        }
        existingMember.FirstName = member.FirstName;
        existingMember.LastName = member.LastName;
        existingMember.Document = member.Document;
        existingMember.Email = member.Email;
        existingMember.PhoneNumber = member.PhoneNumber;
        existingMember.Birthdate = member.Birthdate;
        existingMember.Address = member.Address;
        existingMember.RegistrateDate = member.RegistrateDate;
        existingMember.IsActive = member.IsActive;

        _logger.LogInformation("Updating member with id: {Id}", id);
        await _memberRepository.UpdateAsync(existingMember);
    }
    
    public async Task DeleteAsync(int id)
    {
        var existingMember = await _memberRepository.ExistsAsync(id);
        if (!existingMember)
        {
            _logger.LogWarning("Member with id {Id} not found for deletion", id);
            throw new KeyNotFoundException($"No se encontró el usuario con ID {id}");
        }
        _logger.LogInformation("Deleting member with id: {Id}", id);
        await _memberRepository.DeleteAsync(id);
    }
}
