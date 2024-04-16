using Server.Application.Entities;
using Server.Entry.Utils;
using System.Text.RegularExpressions;

namespace Server.Web.Utils;

public static partial class OrderUtils
{
    const string STR_NO_TEMPLATE_FILE = "模板文件不存在！";
    const string STR_NO_TEMPLATE_FILE_NAME = "不存在的模板文件名！";
    const string STR_ORDER_DOCX = "随工单.docx";

    private class TemplateFileInfo(string name, string path, string description = "")
    {
        public string Name { get; set; } = name;

        public string? Description { get; set; } = description;

        public string Path { get; set; } = path;

        public string FullPath
        {
            get => System.IO.Path.Combine(Path, Name);
        }
    }

    private static readonly Regex placeHolder = R_REPLACE_HOLDER();
    private static readonly Regex arrPlaceHolder = R_ARR_REPLACE_HOLDER();
    private static readonly string templateFilePath = "_template";
    private static readonly string absTemplateFilePath = Path.Combine(
        Environment.CurrentDirectory.ToString(),
        templateFilePath
    );

    private static readonly List<TemplateFileInfo> templateFiles =
    [
        new(STR_ORDER_DOCX, templateFilePath)
    ];

    private static TemplateFileInfo GetInfoByName(string name)
    {
        return templateFiles.FirstOrDefault(it => it.Name == name)
            ?? throw new(STR_NO_TEMPLATE_FILE_NAME);
    }

    static OrderUtils()
    {
        if (!Directory.Exists(absTemplateFilePath))
            throw new(STR_NO_TEMPLATE_FILE);
    }

    private static IEnumerable<N_Par> GetDocPars(N_Doc doc) => from P in doc.Paragraphs select P;

    private static IEnumerable<N_Par> GetDocTablePars(N_Doc doc) =>
        from T in doc.Tables
        from R in T.Rows
        from C in R.GetTableCells()
        from P in C.Paragraphs
        select P;

    public static Stream Demo()
    {
        var stream = new NPOIStream(false);
        using var fs = new FileStream(GetInfoByName(STR_ORDER_DOCX).FullPath, FileMode.Open);
        using var docx = new N_Doc(fs);

        var runs = new List<N_Par>();

        runs.AddRange(GetDocPars(docx));
        runs.AddRange(GetDocTablePars(docx));
        runs.ForEach(r =>
        {
            var text = r.Text;
            if (placeHolder.Matches(text) is IEnumerable<Match> ms && ms.Any())
            {
                foreach (var m in ms)
                {
                    if (
                        arrPlaceHolder.Matches(m.Groups[1].Value) is IEnumerable<Match> _arrM
                        && _arrM.Any()
                        && _arrM.ToList() is { } arrM
                    )
                    {
                        var propName = arrM[0].Groups[1].Value;
                        var index = Convert.ToInt32(arrM[0].Groups[2].Value);
                        r.ReplaceText(m.Groups[0].Value, @$"ARRAY_{propName}_{index}");
                    }
                    if (
                        placeHolder.Matches(m.Groups[1].Value) is IEnumerable<Match> _basicM
                        && _basicM.Any()
                        && _basicM.ToList() is { } basicM
                    )
                    {
                        var propName = basicM[0].Groups[0].Value;
                        r.ReplaceText(m.Groups[0].Value, @$"BASIC_{propName}");
                    }
                }
            }
        });

        docx.Write(stream);
        stream.Flush();
        stream.Seek(0, SeekOrigin.Begin);
        stream.AllowClose = true;

        return stream;
    }

    public static Stream ReplaceByEntity(EOrder e)
    {
        var stream = new NPOIStream(false);
        using var fs = new FileStream(GetInfoByName(STR_ORDER_DOCX).FullPath, FileMode.Open);
        using var docx = new N_Doc(fs);

        var runs = new List<N_Par>();

        runs.AddRange(GetDocPars(docx));
        runs.AddRange(GetDocTablePars(docx));

        var t1 = typeof(EOrder);
        var t2 = typeof(EOrderItem);

        runs.ForEach(r =>
        {
            var text = r.Text;
            if (placeHolder.Matches(text) is IEnumerable<Match> ms && ms.Any())
            {
                foreach (Match m in ms)
                {
                    string? value = null;
                    if (arrPlaceHolder.Match(m.Groups[0].Value) is Match am && am.Success)
                    {
                        var p = t2.GetProperty(am.Groups[1].Value);
                        if (p != null)
                        {
                            var index = Convert.ToInt32(am.Groups[2].Value);
                            if (e.Items != null && e.Items.Count > index)
                                value = Convert.ToString(p.GetValue(e.Items[index]));
                        }
                    }
                    else
                    {
                        var p = t1.GetProperty(m.Groups[1].Value);
                        if (p != null)
                        {
                            value = Convert.ToString(p.GetValue(e));
                        }
                    }
                    r.ReplaceText(m.Groups[0].Value, value);
                }
            }
        });

        docx.Write(stream);
        stream.Flush();
        stream.Seek(0, SeekOrigin.Begin);
        stream.AllowClose = true;

        return stream;
    }

    [GeneratedRegex(@"\{\{(.+?)\}\}")]
    private static partial Regex R_REPLACE_HOLDER();

    [GeneratedRegex(@"(\w+?)\[([0-9])+?\]")]
    private static partial Regex R_ARR_REPLACE_HOLDER();
}
