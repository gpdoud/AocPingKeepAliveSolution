using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AocPingKeepAlive {
    
    public record Config {
        public IEnumerable<System> Systems { get; set; }
    }
    public record System {
        public string Tag { get; set; }
        public string Url { get; set; }
        public int Repeat { get; set; }
        public int Delay { get; set; }
    }
}
