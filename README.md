# CarDealershipAPI

Run instructions:
For API solution, restore nuget packages. All are available in primary nuget sources, key packages are Microsoft.AspNetCore.CORS, Microsoft.AspNetCore.MVC.NewtonsoftJson, and Newtonsoft.Json
Requires .NET Core 3.1, I built using VS2019

For front end solution, restore npm and nuget packages, npm packages include axios, bootstrap, react-router-dom, rimraf
Requires .NET Core 3.1, I also encountered some odd issues with rimraf npm package until I made sure .NET core 2.x was also available. I built using VS2019

Start the API solution, then start the front end solution. It is already configured to look for the proper port in localhost to interact with the API.
