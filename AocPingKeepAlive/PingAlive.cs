using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

using static System.Console;
using Net = System.Net.NetworkInformation;

namespace AocPingKeepAlive {
    
    public class PingAlive {

        const string datetimeFormat = "yyyy-MM-dd hh:mm";

        private static int timeout(int minutes) { return minutes * 60000; }

        public string Url = string.Empty;
        public int Repeat = 15;
        public string Tag = "PING";
        public int Delay = 0;
      
        public Task RunAsync() {
            await CallApi(Url, Repeat, Tag, Delay);
        }

        private async Task CallApi(string url, int repeat = 15, string tag = "Ping", int delay = 0) {
            WriteLine("****************************************************************************");
            WriteLine("* Keep Alive by Copyright (c) 2021 Doud Systems, Inc. All rights reserved. *");
            WriteLine("****************************************************************************");
            WriteLine($"CallApi {url} every {repeat} minutes as {tag} after {delay} delay. Ctrl-C to cancel.");
            var http = new HttpClient();
            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            var message = string.Empty;
            if(delay > 0)
                Thread.Sleep(timeout(delay));
            while(true) {
                var now = DateTime.Now.ToString(datetimeFormat);
                try {
                    var response = await http.GetStringAsync(url);
                    message = "Ok.";
                } catch(Exception ex) {
                    message = $"Exception: {ex.Message}";
                }
                WriteLine($"{now} [{tag}] {message}");
                Thread.Sleep(timeout(repeat));
            }
        }
    }
}
