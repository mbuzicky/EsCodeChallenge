# The Challenge #
An imaginary company stores a list of software products they use. They have stored the name and version of each product. They have asked us to create a simple website where users can type in a version number and receive a list of software products that are greater than the version they entered.

The software versions are stored as a string in the format [major version].[minor version].[patch]. You may see versions like “2”, “1.5”, or “2.12.4” (these are all valid inputs from the user as well). The period is only used as a separator and does not represent a decimal point – 1.5 does not mean one and a half. 

"2" == "2.0" == "2.0.0"
"2" < "2.0.1"
"2" < "2.1"
"2.0.1" < "2.1.0"

Lucky for you, they stored the software list as a C# object (provided below) that you can simply drop into your code – no need to call a database or REST service.

This site will be publicly available, so user authentication will not be required.

# Software Product List #

    public class Software
    {
        public string Name { get; set; }
        public string Version { get; set; }
    }

    public static class SoftwareManager
    {
        public static IEnumerable<Software> GetAllSoftware()
        {
            return new List<Software>
            {
                new Software
                {
                    Name = "MS Word",
                    Version = "13.2.1."
                },
                new Software
                {
                    Name = "AngularJS",
                    Version = "1.7.1"
                },
                new Software
                {
                    Name = "Angular",
                    Version = "8.1.13"
                },
                new Software
                {
                    Name = "React",
                    Version = "0.0.5"
                },
                new Software
                {
                    Name = "Vue.js",
                    Version = "2.6"
                },
                new Software
                {
                    Name = "Visual Studio",
                    Version = "2017.0.1"
                },
                new Software
                {
                    Name = "Visual Studio",
                    Version = "2019.1"
                },
                new Software
                {
                    Name = "Visual Studio Code",
                    Version = "1.35"
                },
                new Software
                {
                    Name = "Blazor",
                    Version = "0.7"
                }
            };
        }
    }

# The Solution #
The working solution is deployed to https://escodechallenge.azurewebsites.net/.  

To perform the filtering and sorting of the results, I took advantage of the built in System.Version class (https://docs.microsoft.com/en-us/dotnet/api/system.version?view=net-5.0).  The beauty of this is that it implements IComparable & IEquatable and allows easy filtering and sorting of lists.  (And no messy business logic to maintain to perform string splits and parsing of integers, etc.).  The drawback of this approach is that it expects the version string to conform to having version segments that are Int32 and also does not like version that do not have at least one period so additional code was added to handle these scenarios.  For larger datasets, we would want to perform further analysis on performance impact.

The Web App was written in .NETCore 3.1 ASPNet with Razor pages.

# Unit Testing #
A few basic unit tests were written in the EsCodeChallengeTest Repo.
