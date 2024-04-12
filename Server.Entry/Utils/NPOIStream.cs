namespace Server.Web.Utils;

public class NPOIStream(bool allowClose = true) : MemoryStream
{
    public bool AllowClose { get; set; } = allowClose;

    public override void Close()
    {
        if (AllowClose)
        {
            base.Close();
        }
    }
}
