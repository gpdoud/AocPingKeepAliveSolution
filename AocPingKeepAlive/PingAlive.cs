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
    
    static class PingAlive {

        const string datetimeFormat = "yyyy-MM-dd hh:mm";

        private static int timeout(int minutes) { return minutes * 60000; }

        public async static Task CallApi(string url, int delay = 15, string tag = "Ping") {
            WriteLine("****************************************************************************");
            WriteLine("* Keep Alive by Copyright (c) 2021 Doud Systems, Inc. All rights reserved. *");
            WriteLine("****************************************************************************");
            WriteLine($"CallApi {url} every {delay} minutes as {tag}. Ctrl-C to cancel.");
            var http = new HttpClient();
            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            var message = string.Empty;
            while(true) {
                var now = DateTime.Now.ToString(datetimeFormat);
                try {
                    var response = await http.GetStringAsync(url);
                    message = "Ok.";
                } catch(Exception ex) {
                    message = $"Exception: {ex.Message}";
                }
                WriteLine($"{now} [{tag}] {message}");
                Thread.Sleep(timeout(delay));
            }
        }
    }
}
