using System.Text.RegularExpressions;

namespace XH_Server.Web.Utils;

public static partial class OrderUtils
{
    class TemplateFileInfo(string name, string path, string description = "")
    {
        public string Name { get; set; } = name;
        public string? Description { get; set; } = description;
        public string Path { get; set; } = path;
        public string FullPath { get => System.IO.Path.Combine(Path, Name); }
    }

    private static readonly Regex placeHolder = R_REPLACE_HOLDER();
    private static readonly string templateFilePath = "_template";
    private static readonly string absTemplateFilePath = Path.Combine(Environment.CurrentDirectory.ToString(), templateFilePath);

    private static readonly List<TemplateFileInfo> templateFiles = [
        new ("随工单.docx", templateFilePath)
    ];

    private static TemplateFileInfo GetInfoByName(string name)
    {
        return templateFiles.FirstOrDefault(it => it.Name == name) ?? throw new("不存在的模板文件名！");
    }

    static OrderUtils()
    {
        if (!Directory.Exists(absTemplateFilePath)) throw new("模板文件不存在！");
    }

    private static IEnumerable<N_Run> GetDocRuns(N_Doc doc) => from P in doc.Paragraphs
                                                               from r in P.Runs
                                                               select r;

    private static IEnumerable<N_Run> GetDocTableRuns(N_Doc doc) => from T in doc.Tables
                                                                    from R in T.Rows
                                                                    from C in R.GetTableCells()
                                                                    from P in C.Paragraphs
                                                                    from r in P.Runs
                                                                    select r;

    public static Stream Demo()
    {
        var stream = new NPOIStream(false);
        using var fs = new FileStream(GetInfoByName("随工单.docx").FullPath, FileMode.Open);
        using var docx = new N_Doc(fs);

        var runs = new List<N_Run>();

        runs.AddRange(GetDocRuns(docx));
        runs.AddRange(GetDocTableRuns(docx));
        runs.ForEach(r =>
        {
            var text = r.Text;
            if (placeHolder.Matches(text) is IEnumerable<Match> ms && ms.Any())
            {
                foreach (Match m in ms)
                {
                    // TODO 根据不同占位符获取不同的值
                    r.ReplaceText(m.Groups[0].Value, "REPLACE");
                }
            }
        });

        docx.Write(stream);
        stream.Flush();
        stream.Seek(0, SeekOrigin.Begin);
        stream.AllowClose = true;

        return stream;
    }

    [GeneratedRegex(@"\{\{(.*?)\}\}")]
    private static partial Regex R_REPLACE_HOLDER();
}


