# FinancialDocumentRetrievalAPI

FinancialDocumentRetrievalAPI is a secure and efficient web API designed to retrieve and anonymize financial documents. With built-in support for product code validation, tenant and client whitelisting, and data anonymization, it is the perfect solution for fintech and financial services looking to enhance their data privacy and security.

## Features

- **Product Code Validation**: Ensure that only supported product codes are processed.
- **Tenant and Client Whitelisting**: Allows service access to pre-approved tenants and clients.
- **Data Anonymization**: Anonymizes sensitive financial information to comply with privacy regulations.

## Getting Started

To get a local copy up and running, follow these simple steps.

### Prerequisites

- .NET 8.0 SDK
- Visual Studio 2022

### Installation

Clone the repository using the following command:
   ```sh
   git clone https://github.com/nikolaRadosavljevic95/FinancialDocumentRetrievalAPI.git
Open the solution file (FinancialDocumentRetrievalAPI.sln) in Visual Studio.

Build the solution to restore the NuGet packages. This can be done by right-clicking on the solution name in the Solution Explorer and selecting "Restore NuGet Packages".

The SQLite database will be automatically created when you run the project for the first time. There is no need for manual database setup.