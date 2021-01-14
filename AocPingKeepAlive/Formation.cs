using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace AocPingKeepAlive {
    class Formation {

        [JsonInclude]
        public int Id { get; set; }
        [JsonInclude]
        public string Name { get; set; }
        [JsonInclude]
        public int StartYear { get; set; }

        [JsonInclude]
        public bool Active { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }

        public Formation() { }

        public override string ToString() {
            return $"Name:[{this.Name}]";
        }

    }
}
