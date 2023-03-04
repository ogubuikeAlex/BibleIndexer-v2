using BibleIndexerV2.Services.Implementations;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace BibleIndexerV2.Data
{
    internal class ApiV2
    {
        /// <Summary>Get bible books, chapters and verses in JSON format</Summary>:
        public static async Task<List<dynamic>> GetBibleBlob()
        {
            var path = (new ConfigurationBuilder().AddUserSecrets<BibleService>()).Build().GetSection("file_path").Value;
            Directory.SetCurrentDirectory(path);
            string text = File.ReadAllText(Directory.GetCurrentDirectory() + @"./books.json");
            
            return JsonConvert.DeserializeObject<List<dynamic>>(text);
        }
    }
}
