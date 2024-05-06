using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Azure;

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
        /// An asynchronous task that inserts a secret into Azure Key Vault based on the provided key and value, returning the updated secret if it was modified.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task<Response<KeyVaultSecret>?> Insert(string key, string value)
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
            var secrets = _client.GetPropertiesOfSecretsAsync();
            Dictionary<string, string> keyValues = new Dictionary<string, string>();
            await foreach (var secret in secrets)
            {
                string secretName = secret.Name;
                KeyVaultSecret secretValue = await _client.GetSecretAsync(secretName);
                keyValues.Add(secretName, secretValue.Value);
            }
            return keyValues;
        }
    }
}
