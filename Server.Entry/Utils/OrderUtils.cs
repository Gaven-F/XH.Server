using System.Text.RegularExpressions;
using NPOI.XSSF.Streaming.Values;
using Server.Entry.Utils;
using Utils;

namespace Server.Web.Utils;

public static partial class OrderUtils
{
    private const string STR_NO_TEMPLATE_FILE = "模板文件不存在！";
    private const string STR_NO_TEMPLATE_FILE_NAME = "不存在的模板文件名！";
    private const string STR_ORDER_DOCX = "随工单.docx";

    private class TemplateFileInfo(string name, string path, string description = "")
    {
        public string Name { get; set; } = name;
        public string? Description { get; set; } = description;
        public string Path { get; set; } = path;

        public string FullPath => System.IO.Path.Combine(Path, Name);
    }

    private static readonly Regex placeHolder = R_REPLACE_HOLDER();
    private static readonly Regex arrPlaceHolder = R_ARR_REPLACE_HOLDER();
    private static readonly Regex specialHolder = R_SPECIAL_HOLDER();
    private static readonly Regex specialGroupHolder = R_SPECIAL_GROUP_HOLDER();
    private static readonly string templateFilePath = "_template";
    private static readonly string absTemplateFilePath = Path.Combine(
        Environment.CurrentDirectory,
        templateFilePath
    );

    private static readonly List<TemplateFileInfo> templateFiles =
        new() { new(STR_ORDER_DOCX, templateFilePath) };

    static OrderUtils()
    {
        if (!Directory.Exists(absTemplateFilePath))
        {
            throw new Exception(STR_NO_TEMPLATE_FILE);
        }
    }

    private static TemplateFileInfo GetInfoByName(string name)
    {
        return templateFiles.FirstOrDefault(it => it.Name == name)
            ?? throw new Exception(STR_NO_TEMPLATE_FILE_NAME);
    }

    private static IEnumerable<N_Par> GetDocPars(N_Doc doc) => doc.Paragraphs;

    private static IEnumerable<N_Par> GetDocTablePars(N_Doc doc) =>
        doc.Tables.SelectMany(t =>
            t.Rows.SelectMany(r => r.GetTableCells().SelectMany(c => c.Paragraphs))
        );

    /// <summary>
    /// 示例方法：演示占位符替换功能
    /// </summary>
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
                    )
                    {
                        var arrM = _arrM.ToList();
                        var propName = arrM[0].Groups[1].Value;
                        var index = Convert.ToInt32(arrM[0].Groups[2].Value);
                        r.ReplaceText(m.Groups[0].Value, @$"ARRAY_{propName}_{index}");
                    }
                    if (
                        placeHolder.Matches(m.Groups[1].Value) is IEnumerable<Match> _basicM
                        && _basicM.Any()
                    )
                    {
                        var basicM = _basicM.ToList();
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

    /// <summary>
    /// 根据实体替换文档中的占位符
    /// </summary>
    /// <param name="e">订单实体</param>
    /// <param name="baseFunc">基础功能对象</param>
    public static Stream ReplaceByEntity(EOrder e, BaseFunc baseFunc)
    {
        var stream = new NPOIStream(false);
        using var fs = new FileStream(GetInfoByName(STR_ORDER_DOCX).FullPath, FileMode.Open);
        using var docx = new N_Doc(fs);

        var pars = new List<N_Par>();
        pars.AddRange(GetDocPars(docx));
        pars.AddRange(GetDocTablePars(docx));

        var orderType = typeof(EOrder);
        var orderItemType = typeof(EOrderItem);

        pars.ForEach(par =>
        {
            var text = par.Text;
            ReplaceSpecialPlaceholders(par, e, baseFunc);
            ReplacePlaceholders(par, e);
        });

        docx.Write(stream);
        stream.Flush();
        stream.Seek(0, SeekOrigin.Begin);
        stream.AllowClose = true;

        return stream;
    }

    /// <summary>
    /// 特殊占位符替换
    /// </summary>
    /// <param name="par">段落对象</param>
    /// <param name="e">订单实体</param>
    /// <param name="baseFunc">基础功能对象</param>
    /// <exception cref="ArgumentNullException"></exception>
    private static void ReplaceSpecialPlaceholders(N_Par par, EOrder e, BaseFunc baseFunc)
    {
        var orderType = typeof(EOrder);
        var orderItemType = typeof(EOrderItem);

        if (specialHolder.Matches(par.Text) is IEnumerable<Match> specialMs && specialMs.Any())
        {
            foreach (Match m in specialMs)
            {
                string? value = null;

                if (specialGroupHolder.Match(m.Groups[1].Value) is Match mg && mg.Success)
                {
                    var pName = mg.Groups[1].Value;
                    var p = orderType.GetProperty(pName) ?? throw new ArgumentNullException();

                    var mgValue = mg.Groups[2].Value;

                    if (pName.Contains("Scene") || pName.Contains("Laboratory"))
                    {
                        var v = Convert.ToString(p.GetValue(e)) == "是" ? "0" : "1";
                        value = (v == mgValue) ? "■" : "□";
                    }
                    else if (pName.Contains("Urgency"))
                    {
                        var v = Convert.ToString(p.GetValue(e)) == "正常" ? "0" : "1";
                        value = (v == mgValue) ? "■" : "□";
                    }
                }
                else if (arrPlaceHolder.Match(m.Groups[1].Value) is Match am && am.Success)
                {
                    var p =
                        orderItemType.GetProperty(am.Groups[1].Value)
                        ?? throw new ArgumentNullException();
                    var index = Convert.ToInt32(am.Groups[2].Value);
                    if (e.Items != null && e.Items.Count > index)
                    {
                        var v =
                            Convert.ToString(p.GetValue(e.Items[index]))
                            ?? throw new ArgumentNullException();
                        value = UTILS.GetUserNameById(baseFunc, v);
                    }
                }
                par.ReplaceText(m.Groups[0].Value, value);
            }
        }
    }

    /// <summary>
    /// 普通占位符替换
    /// </summary>
    /// <param name="par">段落对象</param>
    /// <param name="e">订单实体</param>
    private static void ReplacePlaceholders(N_Par par, EOrder e)
    {
        var orderType = typeof(EOrder);
        var orderItemType = typeof(EOrderItem);
        if (placeHolder.Matches(par.Text) is IEnumerable<Match> ms && ms.Any())
        {
            foreach (Match m in ms)
            {
                string? value = GetPropertyValue(m, orderType, orderItemType, e);
                par.ReplaceText(m.Groups[0].Value, value);
            }
        }
    }

    /// <summary>
    /// 获取属性值
    /// </summary>
    /// <param name="m">匹配的正则表达式结果</param>
    /// <param name="t1">订单类型</param>
    /// <param name="t2">订单项类型</param>
    /// <param name="e">订单实体</param>
    /// <returns>属性值</returns>
    private static string? GetPropertyValue(Match m, Type t1, Type t2, EOrder e)
    {
        if (arrPlaceHolder.Match(m.Groups[0].Value) is Match am && am.Success)
        {
            var p = t2.GetProperty(am.Groups[1].Value);
            if (p != null)
            {
                var index = Convert.ToInt32(am.Groups[2].Value);
                if (e.Items != null && e.Items.Count > index)
                {
                    if (
                        p.Name.Contains("Time")
                        && !p.Name.Equals("trialtime", StringComparison.OrdinalIgnoreCase)
                    )
                    {
                        return DateTime
                            .Parse(Convert.ToString(p.GetValue(e.Items[index]))!)
                            .ToString("G");
                    }
                    else
                    {
                        return Convert.ToString(p.GetValue(e.Items[index]));
                    }
                }
            }
        }
        else
        {
            var p = t1.GetProperty(m.Groups[1].Value);
            if (p != null)
            {
                if (
                    p.Name.Contains("Time")
                    && !p.Name.Equals("trialtime", StringComparison.OrdinalIgnoreCase)
                )
                {
                    return DateTime.Parse(Convert.ToString(p.GetValue(e))!).ToString("G");
                }
                else
                {
                    return Convert.ToString(p.GetValue(e));
                }
            }
        }
        return null;
    }

    [GeneratedRegex(@"\{{2}(.+?)\}{2}")]
    private static partial Regex R_REPLACE_HOLDER();

    [GeneratedRegex(@"(\w+?)\[{1}(\d+?)\]{1}")]
    private static partial Regex R_ARR_REPLACE_HOLDER();

    [GeneratedRegex(@"\|{2}(.*?)\|{2}")]
    private static partial Regex R_SPECIAL_HOLDER();

    [GeneratedRegex(@"(\w+?)_(\d+)")]
    private static partial Regex R_SPECIAL_GROUP_HOLDER();
}
