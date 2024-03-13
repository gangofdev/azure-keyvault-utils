namespace AzureKeyVaultUtils.Models
{
    public static class Constants
    {
        public static class Message
        {
            public static readonly string HelpMessage = "Please use the \"--h\" command for help.";
            public static readonly string ErrorMessage = "An error occurred";
            public static readonly string ExportFailedMessage = "Unable to export the secrets from Azure Key Vault.";
            //public static readonly string HelpInstructions = "Options:\n--i                                                           Description of the tool.\n--h                                                           Display this help message.\n--o Insert --f \"<Path-to-file>\" --u \"<Key-vault-url>\"         Insert the key-value pairs from the json to the key vault.\n--o Export --f \"<Path-to-file>\" --u \"<Key-vault-url>\"         Retrieve the secrets from the key vault & export it into a json file.";
            public static readonly string InfoMessage = "Azure Key Vault Manager\nThis tool inserts & exports the secrets from key vault.";
        }

        public static class Commands
        {
            public static readonly string InsertCommand = "--o Insert --f \"<Path-to-file>\" --u \"<Key-vault-url>\"";
            public static readonly string ExportCommand = "--o Export --f \"<Path-to-file>\" --u \"<Key-vault-url>\"";
            public static readonly string HelpCommand = "--h";
            public static readonly string InfoCommand = "--i";
        }

        public static class CommandDescs
        {
            public static readonly string InsertDesc = "Insert the key-value pairs from the json to the key vault.";
            public static readonly string ExportDesc = "Retrieve the secrets from the key vault & export it into a json file.";
            public static readonly string HelpDesc = "Display this help message.";
            public static readonly string InfoDesc = "Description of the tool.";
        }
    }
}


