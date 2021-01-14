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

        public async static Task CallApi(string url = "http://doudsystems.com/servant/api/users", int delay = 15) {
            WriteLine("****************************************************************************");
            WriteLine("* Keep Alive by Copyright (c) 2021 Doud Systems, Inc. All rights reserved. *");
            WriteLine("****************************************************************************");
            WriteLine($"CallApi {url} every {delay} minutes. Ctrl-C to cancel.");
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
                WriteLine($"{now} {message}");
                Thread.Sleep(timeout(delay));
            }
        }

            public static void Ping(string host = "doudsystems.com", int minutes = 15) {
            WriteLine("Keep Alive by Doud Systems, Inc.");
            WriteLine($"Ping {host} every {minutes} minutes. Ctrl-C to cancel.");
            Net.Ping ping = new Net.Ping();
            Net.PingOptions options = new Net.PingOptions();
            options.DontFragment = true;
            string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            while(true) {
                Net.PingReply reply = ping.Send(host, timeout(minutes), buffer, options);
                if(reply.Status != Net.IPStatus.Success) {
                    throw new Exception($"Exception: status is {reply.Status}");
                }
                var now = DateTime.Now.ToString(datetimeFormat);
                WriteLine($"{now} {host} ({reply.Address.ToString()}), RT: {reply.RoundtripTime}, TTL: {reply.Options.Ttl}");
                Thread.Sleep(timeout(minutes));
            }
        }
    }
}
