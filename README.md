# DevAssistant

DevAssistant is a powerful utility tool tailored for .NET developers, designed to simplify and automate the development process.

Dev.Assistant, built with WinForms, is a powerful tool designed to make developers' work easier. It has more than 25 features that help with everyday tasks. These tasks include creating integration guides, converting class formats to JSON, comparing classes, get PDS tables, and others.
It also works well with Azure DevOps, allowing you to look through code and repositories while making sure everything meets NIC standards. Dev.Assistant is a software helping with many different needs.

## Business Projects

### 1. Dev.Assistant.Business.Compare

Previously known as CompareService in Dev.Assistant.Common, this project provides tools for comparing different data types such as JSON, XML, and more. It's dedicated to enhancing comparison features in development processes.

#### Features

- **Data Comparison**
- **Extensibility:** Designed to incorporate additional comparison features in future updates.

### 2. Dev.Assistant.Business.Converter

This project focuses on data conversion functionalities, offering two primary services for JSON/XML and SQL conversions. Also, it can be easily adding services similar to JsonXmlConvertService

#### Services

- **JsonXmlConvertService:** Handles conversions between JSON and XML formats.
- **SqlConvertService:** Cleaning and processing SQL queries for converting code to SQL and vice versa.

### 3. Dev.Assistant.Business.Core

Serves as the foundational layer of Dev.Assistant, providing core functionalities and shared resources across modules.

#### Key Aspects

- **Core Models:** Defines essential data structures and configurations.
- **Utilities and Helpers:** Offers a collection of utility functions and helper methods for widespread use.
- **Common Services:** Hosts services used in multiple projects within Dev.Assistant, including external services like AuthServer.

### 4. Dev.Assistant.Business.Decoder

Previously known as ClassService, this library focuses on manipulating and decoding code, especially in C# and VB languages.

#### Functionalities

- **Code Analysis:** Extracts key components like methods and classes from C# and VB code.
- **Code Validation:** Ensures compliance with NIC standards and specific coding rules.

### 5. Dev.Assistant.Business.Generator

This library includes the IGService

#### Services

- **IGService:** Generates Integration Guides (IG) and Ndb JSON files for API and MicroServices documentation and resources.

### 6. Dev.Assistant.Configuration

Manages configuration settings across multiple projects within Dev.Assistant, ensuring consistency and ease of configuration.

- **ServerAppSettings and LocalAppSettings:** Manages configuration values effectively.

To make it shared configuration logic and structures across various projects like Batch and the main App.

## Publish New Update

To distribute the latest features and fixes, follow these steps to publish a new update of the Dev Assistant App. This ensures that other users can benefit from the enhancements you've made.

### Steps for Updating the App

1. **Update the App Version**  
   Open the `Dev.Assistant.App.csproj` file. Find the `<Version>` tag and update the app version.

2. **Publish the App**  
   In your project, right-click on the `Dev.Assistant.App` project and select `Publish`. Then, click **Publish**.

3. **Copy the Executable to the Server**  
   Navigate to the `bin\publish\` directory. Copy the `DevAssistant.exe` file to the server. For this application, place it in the shared folder at `\Alyahya\DevAssistant`. Ensure that the app is located under the `DevAssistant` folder.

4. **Update Configuration File**  
   In the `DevAssistant` folder, go to the `Assets` directory and open the `appsettings.json` file.

5. **Synchronize the Version Number**  
   Finally, update the `AppVersion` in `appsettings.json` to match the `<Version>` you specified in the first step.

Ensure each step is completed accurately to guarantee that all users can access and utilize the newest version of the app effectively.
