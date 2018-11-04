using System.Text.RegularExpressions;

namespace TsqlLintPlugin
{
    public static class ParseString
    {
        public static (string schema, string objName) GetNames(string pattern, string source, ErrorNameType errorNameType)
        {
            var result = Regex.Match(source, pattern, RegexOptions.IgnoreCase);
            if (result.Success)
            {
                var FullName = result.Groups.Count >= 4 ? result.Groups[3].Value : errorNameType.ToString();
                FullName = FullName.Replace("[", "").Replace("]", "");
                var Schema = FullName?.Substring(0, 4).ToLower().StartsWith("dbo.") == false ? "" : "dbo";
                var ObjName = Schema?.Length == 0 ? FullName : FullName.Substring(4);
                return (
                    schema: Schema,
                    objName: ObjName
                    );
            }
            return (schema: "", objName: "");
        }
    }

    public enum ErrorNameType : short
    {
        sp_ = 1,
        uf_ = 2,
        ubt_ = 3,
        uvv_ = 4,
        idx_ = 5,
        trr_ = 6,
        seqq_ = 7
    }
}
