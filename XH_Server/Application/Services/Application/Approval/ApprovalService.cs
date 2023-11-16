using Mapster;
using XH_Server.Core;
using XH_Server.Domain.Dto;
using XH_Server.Domain.Entities;

namespace XH_Server.Application.Services.Application.Approval;

public class ApprovalService
{

    private readonly Repository<ApprovalConfig> _repository;

    public ApprovalService(Repository<ApprovalConfig> repository)
    {
        _repository = repository;
    }

    public IEnumerable<long> SetConfig(IEnumerable<ApprovalConfigDto> dto)
    {
        return _repository.InsertReturnSnowflakeId(dto.Adapt<List<ApprovalConfig>>());
    }
}