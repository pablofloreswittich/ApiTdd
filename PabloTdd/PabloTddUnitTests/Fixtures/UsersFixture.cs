using PabloTdd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PabloTddUnitTests.Fixtures
{
    public static class UsersFixture
    {
        public static List<User> GetTestUsers() => new()
        {
            new User
                    {
                        Id = 1,
                        Name="Pablin",
                        Address =new Address()
                        {
                            City="Tucuman",
                            Region = "noa",
                            PostalCode = "123"
                        },
                        Email = "pablo@gmail.com"
                    },
            new User
                    {
                        Id = 1,
                        Name="Pablin",
                        Address =new Address()
                        {
                            City="Tucuman",
                            Region = "noa",
                            PostalCode = "123"
                        },
                        Email = "pablo@gmail.com"
                    },
            new User
                    {
                        Id = 1,
                        Name="Pablin",
                        Address =new Address()
                        {
                            City="Tucuman",
                            Region = "noa",
                            PostalCode = "123"
                        },
                        Email = "pablo@gmail.com"
                    }
        };
    }
}
