using Furion.DatabaseAccessor;
using Furion.DynamicApiController;
using Microsoft.AspNetCore.Http.HttpResults;
using Server.Application;
using Server.Application.Entities;
using Server.Application.Entities.Dto;
using Server.Domain.ApprocedPolicy;
using Server.Domain.Basic;

namespace Server.Web.Controllers.Entity;

/// <summary>
/// 流片
/// </summary>
/// <param name="bes"></param>
/// <param name="aps"></param>
public class ChipPayment(
    IBasicEntityService<EChipPayment> bes,
    ApprovedPolicyService aps)
    : BasicApplicationApi<EChipPayment, Vo.ChipPayment>(
        bes, aps)
    , IDynamicApiController
{
    public IBasicEntityService<EChipPayment> Bes { get; set; } = bes;

    public Results<Ok<IEnumerable<string>>, BadRequest> GetReceivingUnits()
    {
        return TypedResults.Ok(Bes.GetEntities().Select(it => it.ReceivingUnit).ToList().Distinct());
    }

    public Results<Ok<IEnumerable<string>>, BadRequest> GetBanks()
    {
        return TypedResults.Ok(Bes.GetEntities().Select(it => it.Bank).ToList().Distinct());
    }

    public Results<Ok<IEnumerable<string>>, BadRequest> GetAccount()
    {
        return TypedResults.Ok(Bes.GetEntities().Select(it => it.Account).ToList().Distinct());
    }

    public Results<Ok<string>, BadRequest> GetNextId()
    {
        return TypedResults.Ok($"XH_CP_{Bes.GetEntities().Count():000000}");
    }
}