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

        void Run(string[] args) {
            var tasks = new Dictionary<string, Task>();
            if (args.Length == 0) {
                tasks = StartConfig();
            } else {
                StartOnce(args);
            }
        }
        // start by config if no cmd line parms else start by config.json
        static void Main(string[] args) {
            (new Program()).Run(args);
        }



        static Dictionary<string, Task> StartConfig() {
            LoadConfig();
            var config = LoadConfig();
            var tasks = new Dictionary<string, Task>();
            foreach (var system in config.Systems) {
                var pa = new PingAlive() {
                    Url = system.Url, Repeat = system.Repeat, Tag = system.Tag, Delay = system.Delay
                };
                var t = Task.Run(async () => {
                    await pa.RunAsync();
                });
                tasks.Add(system.Tag, t);
            }
            return tasks;
        }

        private void StartOnce(string[] args) {
            var parms = ParseArgs(args);
            var url = parms["--url"].ToString();
            var repeat = Convert.ToInt32(parms["--repeat"]);
            var tag = parms["--tag"].ToString();
            var delay = Convert.ToInt32(parms["--delay"]);
            var pa = new PingAlive() {
                Url = url, Repeat = repeat, Tag = tag, Delay = delay
            };
            var t = Task.Run(async () => { await pa.RunAsync(); });
        }

        static Config LoadConfig() {
            var configPath = "config.json";
#if DEBUG
            configPath = "../../../config.json";
#endif
            var configStr = File.ReadAllText(configPath);
            var jsonOptions = new JsonSerializerOptions {
                PropertyNameCaseInsensitive = true
            };
            var config = JsonSerializer.Deserialize<Config>(configStr, jsonOptions);
            return config;
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
