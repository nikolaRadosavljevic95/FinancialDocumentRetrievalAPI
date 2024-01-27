# FinancialDocumentRetrievalAPI

FinancialDocumentRetrievalAPI is a secure and efficient web API designed to retrieve and anonymize financial documents. With built-in support for product code validation, tenant and client whitelisting, and data anonymization, it is the perfect solution for fintech and financial services looking to enhance their data privacy and security.

## Features

- **Product Code Validation**: Ensure that only supported product codes are processed.
- **Tenant and Client Whitelisting**: Allows service access to pre-approved tenants and clients.
- **Data Anonymization**: Anonymizes sensitive financial information to comply with privacy regulations.

## Getting Started

To get a local copy up and running, follow these simple steps.

### Prerequisites

- .NET 6.0 SDK
- Visual Studio 2022

### Installation

1. Clone the repository using the following command:
   ```sh
   git clone https://github.com/nikolaRadosavljevic95/FinancialDocumentRetrievalAPI.git
Open the solution file (FinancialDocumentRetrievalAPI.sln) in Visual Studio.

Build the solution to restore the NuGet packages. This can be done by right-clicking on the solution name in the Solution Explorer and selecting "Restore NuGet Packages".

Set up the local database by applying Entity Framework migrations. This can be done through the Package Manager Console in Visual Studio using the command:
	Update-Database
