using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SuperBotManagerBase.Utils
{
    public class ConsumersUtils
    {
        public static string BuildMessage(string messageKey, Dictionary<string, string> input)
        {
            var msg = input[messageKey];

            msg = Regex.Replace(msg, @"\{\{\{\s*(.+?)\s*\}\}\}", match =>
            {
                string key = match.Groups[1].Value;

                if(input.TryGetValue(key, out string value))
                {
                    return value;
                }

                return match.Value;
            });


            return msg;
        }
    }
}
