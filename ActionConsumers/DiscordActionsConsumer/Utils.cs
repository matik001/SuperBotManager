using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DiscordActionsConsumer
{
    internal class Utils
    { 
        public static string BuildMessage(Dictionary<string, string> input, List<string> ignoreFields)
        {
            var msg = input["Message"];
            ignoreFields.Add("Message");

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

            var remainingField = input.Where(x => !ignoreFields.Contains(x.Key)).ToList();
            foreach(var field in remainingField)
            {
                msg += $"\n{field.Key}: {field.Value}";
            }

            return msg; 
        }
    }
}


