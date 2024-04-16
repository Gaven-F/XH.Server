namespace Server.Entry.Utils;

public class CellMsg(string colName, int colNumber)
{
    public string ColName { get; set; } = colName;
    public int ColNumber { get; set; } = colNumber;
}