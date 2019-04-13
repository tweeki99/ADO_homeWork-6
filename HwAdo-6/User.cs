using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HwAdo_6
{
    public class User
    {
        public Guid Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsAdmin { get; set; }
    }
}
//CREATE TABLE[dbo].[Users]
//(
//   [Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
//    [Login] NVARCHAR(50) NOT NULL,
//    [Password] NVARCHAR(50) NOT NULL,
//    [Address] NVARCHAR(50) NOT NULL,
//    [PhoneNumber] NVARCHAR(50) NOT NULL,
//    [IsAdmin] BIT NOT NULL
//)
