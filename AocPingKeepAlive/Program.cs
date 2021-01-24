using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

using static System.Console;

namespace AocPingKeepAlive {

    class Program {

        async static Task Main(string[] args) {

            var parms = ParseArgs(args);
            var url = parms["--url"].ToString();
            var delay = Convert.ToInt32(parms["--delay"]);
            var tag = parms["--tag"].ToString();
            LoadConfig();
            await PingAlive.CallApi(url, delay, tag);
        }

        static void LoadConfig() {
            var configPath = "config.json";
#if DEBUG
            configPath = "../../../config.json";
#endif
            var configStr = File.ReadAllText(configPath);
            var jsonOptions = new JsonSerializerOptions {
                PropertyNameCaseInsensitive = true
            };
            var configJson = JsonSerializer.Deserialize<Config>(configStr, jsonOptions);
        }

        static Dictionary<string, object> ParseArgs(string[] args) {
            var parms = new Dictionary<string, object>();
            string lastArg = null;
            foreach(var arg in args) {

                if(arg.Length > 1 && arg.Substring(0,2) == "--") {
                    parms.Add(arg, null);
                    lastArg = arg; // strip double dash
                    continue;
                }

                parms[lastArg] = arg;
            }
            return parms;
        }
    }
}
