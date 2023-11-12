using FastJSON;
using Mapster;
using StackExchange.Profiling.Internal;
using XH.Core.Database.Tables;

namespace XH.Application.Mapper;

public class Mapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.ForType<FBusinessTrip, BusinessTrip>()
            .Map(it => it.Members, it => it.Members.ToJson())
            .Map(it => it.Annex, it => it.Annex.ToJson());

        config.ForType<BusinessTrip, FBusinessTrip>()
          .Map(it => it.Members, it => JSON.ToObject<List<string>>(it.Members))
          .Map(it => it.Annex, it => JSON.ToObject<List<string>>(it.Annex));
    }
}
