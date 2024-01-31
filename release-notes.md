# DevAssistant: Release Notes

## v4.0.5 (Current Release)

31/01/2024

This release bring new features, bug fixes, and enhancements:

### New

- **[GenerateIG()]** Add the ability to generate integration guide for MicroServices project.
- **[PullRequests]**:
  - Add new option to get **PDS Tables**, this will read the code from your PR to get all PDS tables and Micro names, then read Micro code from server to get all needed tables.
  - Now if you select a PR, a summary will apear to view your CR status, and to check your CR if it match the Rules such as if CR contains a Link to your Changeset for the IG file, Test Data attached or if the CM tab empty.
  
<br/>
<br/>

# Release History

## v4.0.4  

25/01/2024

This release bring new features, bug fixes, and enhancements:

### New

- **[PullRequests]** New app added to `DevAssistant` it's gives you the ability to browse your active API's Pull Requests or any active PR that assigned to you (joined as reviewer).
  By browsing your PR(s) now you can **Generate IG** for the service or even **Check Spelling and Rules** from TFS.

## v4.0.2

18/01/2024

This release bring bug fixe for `Object reference not set to an instance of an object.` when a model have `List` of Primitive Type such as `List<string>` or `List<int>`.

## v4.0.1

15/01/2024

This release bring bug fixes, and enhancements

## v4.0.0

02/01/2024

In this release, I've focused on major refactoring code to enhance the structure and functionality of DevAssistant app. These changes are aimed to simplify the development process, improving code maintainability, and preparing for future enhancements by others 😉.

### Updates

- **Code Refactoring**: Split the "Dev.Assistant.Common" project into business projects for better organization and efficiency.
  - **Dev.Assistant.Business.Compare**: Previously CompareService, now offering enhanced data comparison tools for JSON, XML, and more.
  - **Dev.Assistant.Business.Converter**: Includes services like JsonXmlConvertService and SqlConvertService for versatile data conversion functionalities.
  - **Dev.Assistant.Business.Core**: The central component providing core models, utilities, helpers, and common services across different projects.
  - **Dev.Assistant.Business.Decoder**: Specialized in reads code and picks out important parts like methods and classes and validate code and business for API code to helps make sure the code follows NIC standards and certain rules
  - **Dev.Assistant.Business.Generator**: Comprises FileService for file-related operations and IGService for API `IG` generation.
  - **Dev.Assistant.Configuration**: Manages configuration settings across multiple Dev.Assistant projects, ensuring share the same configuration logic and structures.
- **New Source Folder Structure**: Add a `src` folder to holds all code folders, enhancing organization.
- Add the ability to bypass NIC standards in **Get props info for IG**, now the process will not stop if there is any business error.
- Add new feature **VB to C# Class** with the ability to add `suffix` after each class name to avoid duplicate class within the same contract as `ServiceBus` services in `Sbc`.
- Update server's `appsettings`.
- Enhance and Update `Dev.Assistant.Batch` **v1.8**

### Fixes

- A lot of bug fixes, and enhancements :)

## v3.4.2

10/08/2023

This release bring bug fixes, and enhancements:

### Update

- **[UtilitiesOps]**:
  - Add ability to get all PDS table from a controller such as Npv controllers
  - Enhance `AuthServer` comment for `Npv` contract by adding needed keys in `appsettings.json`
  - Add Excluded tables such as `ZoneDetails`

## v3.4.1

03/08/2023

### Fix

- Remove PR-Review app

## v3.4.0

03/08/2023

This release bring bug fixes, and enhancements:

### Update

- Now you can check for backend References `Nic.Apis.BackendRest.csproj` if there is any duplicated or not following naming conventions
- **[GenerateIG()]**:
  - **`Reengineering`** the generate IG to support the ability of reading code form TFS
  - Add ability to generate IGs for any specific controller
  - Fix when `property` is `array` instead of `List<>`
  - Add check for Req & Res timestamp naming conventions
  - Support Npv & Gcc root folders
  - Add new common errors such as `ValidateGccNationality()`
  - `[ApiCommonExceptions]` Add the ability to add new common errors in appsettings in future to make it configurable
  - Add new validations for Npv
  - Show warring to the user when there are `ConnectionStrings`(s) to add them to `CM` tab, such as `IgapeOdsConnectionString` or `NotifyOdsConnectionString`, etc..
  - Now `Api_Swagger` table will be filled programmatically
- Check for available updates each 12h
- Stop app when session end. The duration in configured value. By this we can ensure to avoiding unexpected error if the app was opened for long time and force user to restart the app.
- Kill Word process based on process `StartTime`
- **[UtilitiesOps]**: Add ability get `AuthServer` comment for `Npv` contract

### Fix

- Fix one case of "`Index was outside the bounds of the array`" error in `GetClasses()`

## v3.3.3

13/02/2023

This release bring bug fixes, and enhancements:

### Add

- **[SqlToCode & CodeToSql]** Add Sql convert options (Sql option was implemented from v1 in code only, now I added to UI)
  - Remove white spaces
  - Remove comments

### Update

- Now the app will not stop if session has been expired, if it's expired it will check for update only.
  If there is an available update the app will stop in this case

### Fix

- **[Staff]** Fix bugs
- Fix & enhance logging
- Fix when reading class by remove `\t` (Some classes have prefix `tab` (`\t`)).
- Fix `CleanSql`, only use `.Trim()` if `RemoveWhiteSpace` is `true`
- Solve the bug in `v3.1.1` (see bellow) by adding new method `PreparePropertiesDto` to get all props and all subProp (objects & lists) no limit for sub level.
- Fix `BirthDate` validation in Req (`Date property must end with suffix **DateG ..... [1424]`) since no need to add suffix for `BirthDate` in Request

## v3.3.2

25/01/2023

This release bring bug fixes, and enhancements:

### Fix

- **[Staff]** Fix some bugs and enhancements

## v3.3.1

23/01/2023

This release bring bug fixes, and enhancements:

### Update

- **[UtilitiesOps]** Add `MV201` table in `GetPDSTable` if `ValidateVehiclePlateDetails()` called

### Fix

- **[Staff]** Fix some bugs

## v3.3.0

22/01/2023

This release bring new features, bug fixes, and enhancements:

### New

- **[Staff]** New app added to `DevAssistant` it's include all 3452 employees contacts information such as Ext, username, fullname, manager name, etc.

## v3.2.2

10/01/2023

This release bring bug fixes and enhancements:

### Fix

- **[UtilitiesOps]** Fix when geeting `GetPDSTable` from query that written as `@` (Not a `StringBuilder` or `+=`) and only contains one or two lines. Happened while getting tables from `MicroLKIFR250AirlineName`
- `CloseAndFlush` when the session expired.

## v3.2.1

15/12/2022

This release bring bug fixes and enhancements:

### Fix

- **[CheckCapitalLetters()]**:

  - When check for three capital letters for classes [At L1566], `isClass` arg was sent as `false`. Fixed to `true`.
  - Now check by using `char.IsUpper()` (accepts only English letters) instead of `letter == char.ToUpper(letter)` (numbers was pass)

- Fix when `replace` the path from `Models` to `BusinessLayer`. Issue was when trying to replace `**\MicroLKMV209VehicleModels` it will be `**\MicroLKMV209VehicleBusinessLayer`! Fixed by adding `\` as prefix to make sure it's a path.
- Fix `SplitByCapitalLetter()` when name have numbers. For example `MicroLKAD208WasilDistrict` was `Micro LKAD2 0 8 Wasil District`. After fix: `Micro LKAD208 Wasil District`

## v3.2.0

13/12/2022

This release bring bug fixes and enhancements.  
Find the most changes:

### New

- **[GenerateNdbJson]**: Add the ability to generate Ndb json

### Update

- **[GenerateIG()]** Get all tables for irregular status check when ID validation use `IDTypeOptions.None` type.

&nbsp;

## v3.1.1

05/12/2022

This release bring bug fixes and enhancements.  
Find the most changes:

### Update

- **[GenerateIG() and Check Spelling and Rules]**:
  - In `V3.1.0`, exceptions & remarks displayed in Word file as remarks.
    Now if there is any remarks related to `Api Rules` it will appear at the top of the page called `Remarks (rules) Summary`.
  - Add the new error `4210` for `ValidateWantedDepartment()` in `ApiCommonCode` .

### Fix

- **[GenerateIG()]** Fix when get `property`'s class details for third level. (Scenario was `List` > `ObjectA` > `ObjectB` > `ObjectC`. In `ObjectB` there is a `property` when trying to get the `property`'s class details (`ObjectB`) it was return the `ObjectC` details instead of `ObjectB` :)~  
  See `Csmarc.EsbTgaGetLiveLocations` the `VehiclePlate` was returning the `LiveLocationVehicleRes` instead of `VehiclePlateRes`)

&nbsp;

## v3.1.0

16/11/2022

This release bring bug fixes, enhancements and new features.  
Find the most changes:

### New

- **[GenerateIG()]**:
  - Add the ability to generate the IG regarding to errors by stop throwing `exceptions` related to rules checking. Now all `exceptions` will be displayed in Word file as remarks.
  - Add the new errors for `ValidateVehiclePlateDetails()` such as `7205` and `7204` exceptions
- **[UtilitiesOps]**: The ability to get all PDS tables for `MicroService` by typing `AllMicro`
- General bug fixes :)

&nbsp;

## v3.0

28/10/2022

This release bring bug fixes, enhancements and new features.  
Find the most changes:

### New

- New app **PR-Review**:

  It is an app for reviews only. With it you can browse all active Pull Requests (PR) that you joined to it as a Reviewer.  
  So that you can choose the comments you need from the PR to create a new Review task with the selected comments (To-do).
  The Review task will be linked to the CR, dev task, review task, and the PR

&nbsp;

- Converter features:

  Now the all conversation feature will be under **Converter** option
  more options added:

  - Xml to Json
  - Json to Xml
  - Json to C# class
  - Decrypt Jwt Token to instance of JwtSecurityToken
  - Base64 to image
  - Blob to image

&nbsp;

- Get PackageReferences:

  Get all package references (NuGet) from .csproj to check if there is a duplicate reference (Merge issues)

&nbsp;

- **[GenerateIG()]**:
  - Add new check for model's `namespace`
  - Add new check for method's argement if it's `camelCase`.
  - Add new validation for Date property must end with suffix `**DateG` or `**DateH`

### Update

- Stop the app directly after the app session has expired instead of waiting for the next action and then stop the app while the user using the app
- Support the new changes of IDTypeOptions.None in ValidateIDType()
- Add the new common errors such as ValidateNpvFineOwnership() #4208 for IG Generation
- **[CheckRules()]** now show the output in Word file
- **[Reviewme]** now remember your last selected service (option) for the next run
- **[GenerateIG()]** if there are misspelling, a word file we appear to the user to resolve the misspelling
- Responsive UI
- **[UtilitiesForms]** new CM message for Micro's CR. Under `DB Name` options select `Micro Project`
- Ignore `OperatorID` check for GCC (such as `BahVio`, `BahReports` and `BahTravelersInfo`) contracts, also throw an `exception` if the OperatorID has `Required` annotation in GCC contracts

### Fix

- **[MyWork]** Fix when searching for IG link's comment in the CR. was `comment == "integrationguide"` now-> `comment.Contains("integration") or comment.Contains("guide")`
- **[GetAllMethod()]**: if url supplied it well read method from the Interface if there is any.
- **[UtilitiesForms]** when the table already in list it will be `continued` and ignore the validation. if it is continued it will not check for `dboNotFound` (`.dbo`), so if there more the one table with the name it will be check for the first one only! Now it check for alll tables .
- **[UtilitiesForms]** fix when add model path to the list. before the app couldn't go to BusinessLayer.
- **[GenerateIG()]** fix when same class use in both Req & Res (before: was adding the class into req table only)
- **[GenerateIG()]** fix when there is nested class in the Req it will show the props after the main prop,
  then in the end it will show all nested props again under main Req model in the table
- **[GenerateIG()]** fix error `Length cannot be less than zero. (Parameter 'length')` when there is `public` word in controller XML
- Word process now disposed

  > **Warning**: The word process sometimes is still not disposed :( trying to solve this issue.  
  >  End the task if the `Microsoft Word` appear. You can check Task Manager under `Background processes`

- Ignore error #1417 "`[1417] There are more than 2 capital letter next to each other`" when prop contains `**_YYYYMMDD` such as `ExitDateH_YYYYMMDD` or `InDate_YYYYMMDD`
- fix issues releated to **[Get Db length from Sql]**
- **[GenerateIG()]** remove duplicated `error` from IG Error Table.

&nbsp;

---

&nbsp;
