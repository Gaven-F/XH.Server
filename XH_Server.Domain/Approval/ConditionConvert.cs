using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XH_Server.Common.Condition;
internal class ConditionConvert : ISugarDataConverter
{
	public SugarParameter ParameterConverter<T>(object columnValue, int columnIndex)
	{
		var name = "@myp" + columnIndex;
		return new SugarParameter(name, columnValue.ToString());
	}

	public T QueryConverter<T>(IDataRecord dataRecord, int dataRecordIndex)
	{
		return (T)(object)Condition.Parse((dataRecord.GetValue(dataRecordIndex) as string)!);
	}
}
