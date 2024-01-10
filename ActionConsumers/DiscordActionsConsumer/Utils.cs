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

            Regex.Replace(msg, @"\{\{\{\s*(.+?)\s*\}\}\}", match =>
            {
                string key = match.Groups[1].Value;

                ignoreFields.Add(key);
                if(input.TryGetValue(key, out string value))
                {
                    return value;
                }

                // Jeśli klucz nie istnieje w słowniku, pozostaw oryginalny tekst
                return match.Value;
            });

            input.Where(x => !ignoreFields.Contains(x.Key)).ToList().ForEach(x => msg += $"\n{x.Key}: {x.Value}");

            return msg; 
        }
    }
}


