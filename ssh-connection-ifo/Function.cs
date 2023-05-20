using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

using Amazon.Lambda.Core;
using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;

using ssh_connection_ifo.JSON;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializerAttribute(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace ssh_connection_ifo
{
    public class Function
    {
        const string _POST_URL = "https://hooks.slack.com/services/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";
        /// <summary>
        /// A simple function that takes a string and does a ToUpper
        /// </summary>
        /// <param name="input"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public string FunctionHandler(Param param, ILambdaContext context)
        {
            string message = string.Format("*{0}* Ç™ãNìÆÇµÇ‹ÇµÇΩÅB\nPub IP Address : {1}", param.Name, param.Address,param.Username);
            if (param.Username != null)
            {
                message += string.Format("\n`ssh {0}@{1}`", param.Username, param.Address);
            }
            var json = CreateJson(message);

            post_message(json).Wait();

            return "0";
        }

        string CreateJson(string markdown)
        {
            Text text = new Text
            {
                type = "mrkdwn",
                text = markdown
            };

            Block[] block = new Block[1];
            block[0] = new Block
            {
                type = "section",
                text = text
            };

            Attachments[] attachments = new Attachments[1];
            attachments[0] = new Attachments
            {
                blocks = block,
                color = "#249B12"
            };

            Payload payload = new Payload
            {
                text = "ssh-info",
                attachments=attachments
            };

            var json = JsonSerializer.Serialize(payload, new JsonSerializerOptions { IgnoreNullValues = true });
            return json;
        }

        async Task post_message(string json)
        {
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            using (var client = new HttpClient())
            {
                content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                var result = await client.PostAsync(_POST_URL, content);
            }
        }
    }
}
