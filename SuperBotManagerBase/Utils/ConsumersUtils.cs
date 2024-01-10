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
        public static string BuildMessage(string messageKey, Dictionary<string, string> input, List<string> ignoreFields)
        {
            var msg = input[messageKey];
            ignoreFields.Add(messageKey);

            msg = Regex.Replace(msg, @"\{\{\{\s*(.+?)\s*\}\}\}", match =>
            {
                string key = match.Groups[1].Value;

                ignoreFields.Add(key);
                if(input.TryGetValue(key, out string value))
                {
                    return value;
                }

                return match.Value;
            });

            //var remainingField = input.Where(x => !ignoreFields.Contains(x.Key)).ToList();
            //foreach(var field in remainingField)
            //{
            //    msg += $"\n{field.Key}: {field.Value}";
            //}

            return msg;
        }
    }
}
