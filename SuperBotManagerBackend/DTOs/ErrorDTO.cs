using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SuperBotManagerBackend.DTOs {
    public class ErrorDTO {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public override string ToString() => JsonConvert.SerializeObject(this);
    }
}
