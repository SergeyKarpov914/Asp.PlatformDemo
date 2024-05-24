# ASP Client-Server Platform

The code for ASP Demo platform was written in summer of 2023. It is a simplified demo version of the commercial-grade ASP Client-Server platform solution. The demo version has been put together to showcase the general design and coding style.  

The platform is a collection of .NET assempblies which is used to build custom CRUD-type API servers and Blazor Web UIs. 

The architectural background for the ASP Client-Server platform can be found in couple of documets written at the same period of time:

https://github.com/SergeyKarpov914/Asp.PlatformDemo/blob/master/Docs/CleanArchitectureStudy.md
https://github.com/SergeyKarpov914/Asp.PlatformDemo/blob/master/Docs/CleanArchitectureDemo.md

## Platform Demo vs Production structure

The side by side summary of the main components of the ASP platform shows the difference between (free) Demo and Production versions:

| Component Class    | Subclass  | Demo        | Production    |
| :---        |    :----:   | :----:   |          ---: |
| Abstractions      |        | S   | F   |
| .Net 6 Support  |         | S      |   F      |
| .Net 7 Support  |         | S      |   F      |
| .Net 8 Support  |         |       |   F      |
| Extensions   |         | S      |   F      |
| Gateway   |  Sql          | F      |   F      |
| Gateway   |  Http          | F      |   F      |
| Gateway   |  gRpc          |       |   F      |
| Master   |  Hosting  |  S    |   F      |
| Master   |  Data Access           |   S   |   F      |
| Master   |  other           |      |   F      |
| Azure   | Service Bus           |      |   F      |
| Azure   | Storage           |      |   F      |
| Azure   | Authentication  |      |   F      |
| Azure   | other           |      |   F      |
| Pattern   | Builder           |  S    |   F      |
| Pattern   | Producer-Consumer           |   S   |   F      |
| Pattern   | Cache           |      |   F      |
| Pattern   | Book-of-Work           |      |   F      |
| Pattern   |  other          |   S   |   F      |
| Telemetry   |            |   S   |   F      |
| Test   |            | S   |   F      |

## The platform applications

The ASP Client-Server platform is designed to work as a base for rapid development of high-performance and reliable systems. 

I see the start-up types of operations as main beneficiaries of the ASP Client-Server platform.

The platform can be used for wide variety of aplications types: 

- [ ] API servers
- [ ] Blazor Web pages
- [ ] Backend servers
- [ ] Command-line batch jobs
- [ ] Desktop WPF presentation 

Platform can be used unversally, even though it is somewhat optimized for financial type of applications, because this is my primary area of operations in the past years. 






