# Test

This is the Tecnical Test Project

## Getting Started

Use the Xamarin [5.0.0.2545](https://visualstudio.microsoft.com/es/vs/mac/) or major to run this project.

#### Tools 
 • NetStandar (2.1) 
 • Visual Studio Mac 17.4.4 (build 12)
 • Xamarin 5.0.0.2545 

#### Instalation Process
1. Clone this repository
2. Open with the lastest Visual Studio or Visual Studio Mac
3. Restore Packages.
```bash
 Right Click on Main Project -> Restore Nuget Packages
```
4. Build or Run the project on prefer platform (Android | iOs)

#### Dependencies



## Build Release version

#### Android Platform
1. Build App Bundle or APK format with the same keystore used to publish the first file

#### iOs Platform
1. Build IPA format with a valid App Store certificated and upload to App Store connect
      

### Application Folder Structure

```bash
├── Core
│   ├── Constants "Constants Files"
│   ├── Converters "Data Converter"
│   ├── Helpers "Helper Methods (Usually static methods)"
│   ├── Locator "used to registry DryIoC MVVM Library"
│   ├── Models "Models used to WebApi Response, Local Data Storage"
│   ├── Router "Static string to Routing Navigation"
│   ├── Resources "Image and Files used as Resources"
│   ├── Services "Shared services in all project", "Dependency Injection"         
│   └── UI "CustomViews and Custom Controls"
│
├── Modules
│   ├── Models "Models used to ViewModels"
│   ├── Repositories "Definition of the repositories used by the modules (Interfaces)"
│   ├── UI "Pages"
│   ├──ViewModels "ViewModels"
│ 
├── Properties
│   └── AssemblyInfo
│   
│ 
└── Assets "Assets used on Application"
    └── i18n
``` 


## Authors 

#### [Geral Alexander Torres Guzmán](https://github.com/alextorres50/) 2023
