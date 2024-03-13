# Introduction
This tool will be helpful for performing the following operations related to Azure Key Vault:

1. Inserts values from a given JSON file to Azure Key Vault.
2. Saves all the secrets from Azure Key Vault to a JSON file in the desired location.

# Prerequisites

Before running this application, ensure that you have the following prerequisites:
1. .NET Core SDK installed.
2. Azure subscription with access to Azure Key Vault.

# Installation
1. Run the command to create a nuget package from the project. A new folder called 'nupkg' is created with the new nuget package.
   >`dotnet pack`
2. Run command inside 'nupkg' folder to install the nuget package globally
   >`dotnet tool install -g AzureKeyVaultUtils --add-source ./`
   
# Running the Application
1. Open a terminal or command prompt and navigate to the project directory.
2. For inserting values from a given JSON file to Azure Key Vault:
   > `azurekeyvaultutils --o Insert --f "<Path-to-file>" --u "<Key-vault-url>"`
3. For saving all the secrets from Azure Key Vault to a JSON file:
   > `azurekeyvaultutils --o Export --f "<Path-to-file>" --u "<Key-vault-url>"`
4. For displaying help message:
   > `azurekeyvaultutils --h`
