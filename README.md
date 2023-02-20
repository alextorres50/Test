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
pub get
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
│   ├── Helpers "Helper Methods (Usually static methods)"
│   ├── Services "Shared services in all project",
│            "Dependency Injection"
├── Data
│   ├── Repositories "Implementation of the repositories",
│   │         "They can be grouped by modules"
│   └── DataSource 
│       ├── Local "Model Files used to local information"
│       │   
│       └── Remote "Model Files used to parse remote information"
│  
├── Presentation
│   └── Module "Parent module, Ex. Login, Profile, Home, etc."
│       └── SubModule
│           ├── Views
│           └── ViewModels 
├── Domain
│   ├── Models "Models used by the modules"
│   └── Repositories "Definition of the repositories used by the modules (Interfaces)"
│ 
└── Resources "Assets used on Application"
    ├── Fonts
    ├── Images
    └── Styles
``` 


## Authors 

#### [Geral Alexander Torres Guzmán](https://github.com/alextorres50/) 2023
