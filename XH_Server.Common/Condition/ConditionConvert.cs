using SqlSugar;
using System.Data;
using XH_Server.Common.Condition;

namespace XH_Server.Domain.Approve;
public class ConditionConvert : ISugarDataConverter
{
	public SugarParameter ParameterConverter<T>(object columnValue, int columnIndex)
	{
		return new SugarParameter($"@myn{columnIndex}", (columnValue as Condition)!.ToString());
	}

	public T QueryConverter<T>(IDataRecord dataRecord, int dataRecordIndex)
	{
		var str = dataRecord[dataRecordIndex].ToString()!;
		return (T)(object)(Condition.Parse(str));
	}
}
