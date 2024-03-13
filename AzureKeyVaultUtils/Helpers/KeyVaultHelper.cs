using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Azure;
using System.Text.Json;

namespace AzureKeyVaultUtils.Helpers
{
    public class KeyVaultHelper
    {
        private readonly SecretClient _client;

        public KeyVaultHelper(string keyVaultUrl)
        {
            _client = new SecretClient(new Uri(keyVaultUrl), new InteractiveBrowserCredential());
        }

        /// <summary>
        /// An asynchronous task that recursively inserts data into Azure Key Vault by iterating over the properties of a JsonElement data.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="prefix"></param>
        /// <returns></returns>
        public async Task Insert(JsonElement data, string prefix = "")
        {
            foreach (JsonProperty property in data.EnumerateObject())
            {
                if (property.Value.ValueKind == JsonValueKind.Object)
                {
                    string newPrefix = $"{prefix}{property.Name}--";
                    await Insert(property.Value, newPrefix);
                }
                else
                {
                    string keyWithPrefix = prefix + property.Name;
                    string value = property.Value.ToString();
                    await Insert(keyWithPrefix, value);
                }
            }
        }

        /// <summary>
        /// An asynchronous task that inserts a secret into Azure Key Vault based on the provided key and value, returning the updated secret if it was modified.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private async Task<Response<KeyVaultSecret>?> Insert(string key, string value)
        {
            try
            {
                KeyVaultSecret existingSecret = await _client.GetSecretAsync(key);
                if (!existingSecret.Value.Equals(value))
                {
                    return await _client.SetSecretAsync(key, value);
                }
            }
            catch (RequestFailedException ex)
            {
                if (ex.Status == (int)System.Net.HttpStatusCode.NotFound)
                {
                    return await _client.SetSecretAsync(key, value);
                }
            }
            return null;
        }

        /// <summary>
        /// An asynchronous task that gets all the secrets from Azure Key Vault.
        /// </summary>
        /// <returns></returns>
        public async Task<Dictionary<string, string>> GetSecrets()
        {
            var secrets = _client.GetPropertiesOfSecrets();
            Dictionary<string, string> keyValues = new Dictionary<string, string>();
            foreach (var secret in secrets)
            {
                string secretName = secret.Name;
                KeyVaultSecret secretValue = await _client.GetSecretAsync(secretName);
                keyValues.Add(secretName, secretValue.Value);
            }
            return keyValues;
        }
    }
}
