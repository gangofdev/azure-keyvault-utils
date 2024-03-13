using System.Text.Json;

namespace AzureKeyVaultUtils.Helpers
{
    public class JsonHelper
    {
        /// <summary>
        /// An asynchronous task that stores key-value pairs to a JSON file.
        /// </summary>
        /// <param name="keyValues"></param>
        /// <returns></returns>
        public static async Task<bool> WriteToFile(Dictionary<string, string> keyValues, string filePath)
        {
            Dictionary<string, object> serializedData = new Dictionary<string, object>();

            foreach (var kvp in keyValues)
            {
                string[] parts = kvp.Key.Split("--");
                var current = serializedData;

                for (int i = 0; i < parts.Length - 1; i++)
                {
                    if (!current.ContainsKey(parts[i]))
                    {
                        current[parts[i]] = new Dictionary<string, object>();
                    }

                    current = (Dictionary<string, object>)current[parts[i]];
                }

                current[parts[^1]] = kvp.Value;
            }

            JsonSerializerOptions options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };
        
            string formattedJson = JsonSerializer.Serialize(serializedData, options);
            await File.WriteAllTextAsync(filePath, formattedJson);
            return true;
        }
    }
}
