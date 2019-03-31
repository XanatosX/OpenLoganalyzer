# Open Loganalyzer
[![Codacy Badge](https://api.codacy.com/project/badge/Grade/4582dd295e6c475e9b89ccede7a9060f)](https://app.codacy.com/app/simonaberle/OpenLoganalyzer?utm_source=github.com&utm_medium=referral&utm_content=XanatosX/OpenLoganalyzer&utm_campaign=Badge_Grade_Dashboard)
[![Build status](https://ci.appveyor.com/api/projects/status/92919rwl3k8q0ah1/branch/master?svg=true)](https://ci.appveyor.com/project/XanatosX/openloganalyzer/branch/master)

## General information
### Project folder structure
This repository will contain multiple projects. You will find the following sub projects:

* **OpenLoganalyzerForm** => This is not done yet, but you will find the source for the form application here 
* **OpenLoganalyzerLib** => In this folder you can find the source for the library.
* **OpenLoganalyzerTest** => This folder contains the tests for the library project.

## The sub projects
### Open Loganalyzer Library
This part of the project is the source code of the library itself. This will be the hearth of the planned form application. 

The library is responsible for loading the log-files which should be analyzed save/load different configurations for different log-file types. Parse the files to a fixed structure defined by the library ready to use for any application
### Getting started for developers

#### prerequisites
You will need the following software to help with the development

##### Libraries

* .NET Framework 4.6.1

##### Programs to help 
* A program to edit the code. As example [Visual Studio Community](https://visualstudio.microsoft.com/de/downloads/) which is free for students and open source developer.
* The nuget package manager. This project depends on [Newtonsoft.Json](https://www.newtonsoft.com/json)

## Contributing
Feel free to add an [issue](https://github.com/XanatosX/OpenLoganalyzer/issues) or create a [pull request](https://github.com/XanatosX/OpenLoganalyzer/pulls) with your changes.

## Versioning
We use SemVer for versioning. For the versions available, see the [tags on this repository](https://github.com/XanatosX/OpenLoganalyzer/tags)

## License
This project is licensed under the GNU v3  License - For more information check the [License file](LICENSE) on this repository