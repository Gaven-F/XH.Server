using Furion.DatabaseAccessor;
using Furion.DynamicApiController;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Server.Web.Controllers.Entity;

/// <summary>
/// 流片
/// </summary>


public class ChipPayment() : BasicApplicationApi<EChipPayment>, IDynamicApiController
{
    public Results<Ok<IEnumerable<string>>, BadRequest> GetReceivingUnits()
    {
        return TypedResults.Ok(
            BasicEntityService.GetEntities().Select(it => it.ReceivingUnit).ToList().Distinct()
        );
    }

    public Results<Ok<IEnumerable<string>>, BadRequest> GetBanks()
    {
        return TypedResults.Ok(
            BasicEntityService.GetEntities().Select(it => it.Bank).ToList().Distinct()
        );
    }

    public Results<Ok<IEnumerable<string>>, BadRequest> GetAccount()
    {
        return TypedResults.Ok(
            BasicEntityService.GetEntities().Select(it => it.Account).ToList().Distinct()
        );
    }

    public Results<Ok<string>, BadRequest> GetNextId()
    {
        return TypedResults.Ok($"XH_CP_{BasicEntityService.GetEntities().Count():000000}");
    }
}
