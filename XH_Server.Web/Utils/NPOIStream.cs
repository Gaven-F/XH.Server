namespace XH_Server.Web.Utils;

public class NPOIStream : MemoryStream
{
	public bool AllowClose { get; set; } = true;

	public override void Close()
	{
		if (AllowClose) base.Close();
	}

}
