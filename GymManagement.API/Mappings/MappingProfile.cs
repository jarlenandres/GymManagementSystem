using AutoMapper;
using AutoMapper.Execution;
using GymManagement.API.DTOs.Response;

namespace GymManagement.API.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // === Member Mappings ===
        CreateMap<MemberResponseDTO, Member>();
        CreateMap<Member, MemberResponseDTO>();
    }
}
