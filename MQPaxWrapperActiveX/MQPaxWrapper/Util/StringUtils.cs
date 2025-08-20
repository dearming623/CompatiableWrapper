using System.Text.RegularExpressions;
namespace MQPaxWrapper
{

    class StringUtils
    {
        public static string GetXmlTagValue(string input, string tag)
        {
            string pattern = "<" + tag + ">(.*?)</" + tag + ">";
            Match match = Regex.Match(input, pattern);
            if (match.Success)
            {
                return match.Groups[1].Value;
            }
            return string.Empty;
        }


        public static string GetPaxExtData(string input)
        {
            string pattern = "<ExtData>(.*?)</ExtData>";
            Match match = Regex.Match(input, pattern);
            if (match.Success)
            {
                return match.Groups[1].Value;
            }
            return string.Empty;
        }
        public static bool IsNumeric(string str)
        {
            Regex regex = new Regex(@"^\d+(\.\d+)?$");
            return regex.IsMatch(str);
        }
    }
}