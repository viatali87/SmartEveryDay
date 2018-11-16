using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartEveryDay.Models
{
    interface IUser
    {
        Guid UserId { get; set; }
        string Username { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        Guid HouseId { get; set; }
        string PhoneNo { get; set; }
        string Email { get; set; }
        bool IsAdmin { get; set; }
    }
}
