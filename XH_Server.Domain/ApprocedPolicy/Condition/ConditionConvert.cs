using SqlSugar;
using System.Data;

namespace XH_Server.Domain.ApprocedPolicy.Condition;
public class ConditionConvert : ISugarDataConverter
{
    public SugarParameter ParameterConverter<T>(object columnValue, int columnIndex)
    {
        return new SugarParameter($"@myn{columnIndex}", (columnValue as ECondition)!.ToString());
    }

    public T QueryConverter<T>(IDataRecord dataRecord, int dataRecordIndex)
    {
        var str = dataRecord[dataRecordIndex].ToString()!;
        return (T)(object)ECondition.Parse(str);
    }
}
