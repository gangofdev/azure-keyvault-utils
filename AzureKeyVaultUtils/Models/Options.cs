using CommandLine;

namespace AzureKeyVaultUtils.Models
{
    public class Options
    {
        [Option('o', "operation", Required = false, HelpText = "Operation (insert or export)")]
        public OperationEnum Operation { get; set; }

        [Option('f', "filepath", Required = false, HelpText = "Json file path")]
        public string? FilePath { get; set; }

        [Option('u', "url", Required = false, HelpText = "Key Vault URL")]
        public string? Url { get; set; }

        [Option('h', HelpText = "Display help information")]
        public bool Help { get; set; }

        [Option('i', HelpText = "Display application information")]
        public bool Info { get; set; }
    }
}
