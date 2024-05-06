using AzureKeyVaultUtils.Helpers;
using AzureKeyVaultUtils.Models;
using CommandLine;
using Serilog;

namespace AzureKeyVaultUtils
{
    class Program
    {
        static void Main(string[] args)
        {
            // Configuring logger
            Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .CreateLogger();

            // Handle input arguments
            HandleCommandLineArguments(args);

            // Flush the logger
            Log.CloseAndFlush();
        }

        private static void HandleCommandLineArguments(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args)
            .WithParsed(options =>
            {
                if (options.Help)
                {
                    DisplayHelpDesc();
                }
                else if (options.Info)
                {
                    Log.Information(Constants.Message.InfoMessage);
                }
                else
                {
                    Log.Information($"Process started.");
                    KeyVaultHelper vaultHelper = new KeyVaultHelper(options.Url!);
                    switch (options.Operation)
                    {
                        case OperationEnum.Insert:
                            //Performs insertion of key-value pairs into the Azure Key Vault
                            Log.Information($"Reading the json file.");
                            Dictionary<string, string> secrets = JsonHelper.ReadSecrets(options.FilePath!);
                            foreach(KeyValuePair<string, string> kvp in secrets)
                            {
                                Task.Run(async () => await vaultHelper.Insert(kvp.Key, kvp.Value)).Wait();
                            }
                            Log.Information($"Finished inserting secrets.");
                            break;
                        case OperationEnum.Export:  //Performs export of key-value pairs from Azure Key Vault to a json file
                            Log.Information($"Obtaining keys from key vault.");
                            Dictionary<string, string> keyValues = vaultHelper.GetSecrets().GetAwaiter().GetResult();
                            Log.Information($"Writing to json file.");
                            Task.Run(async () => await JsonHelper.WriteToFile(keyValues, options.FilePath!)).Wait();
                            break;
                        default:
                            Log.Information(Constants.Message.HelpMessage);
                            break;
                    }
                    Log.Information($"Process completed.");
                }
            })
            .WithNotParsed(errors =>
            {
                Log.Information(Constants.Message.HelpMessage);
            });
        }

        private static void DisplayHelpDesc()
        {
            int maxCommandWidth = Constants.CommandInfo.Commands.Select(x => x.Name).Max(key => key.Length);

            foreach (Command command in Constants.CommandInfo.Commands)
            {
                string formattedCommand = $"{command.Name.PadRight(maxCommandWidth)}  {command.Description}";
                Log.Information(formattedCommand);
            }
        }
    }
}