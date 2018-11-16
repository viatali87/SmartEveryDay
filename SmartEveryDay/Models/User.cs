using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartEveryDay.Models
{
    public class User: IUser
    {
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Guid HouseId { get; set; }
        public string PhoneNo { get; set; }
        public string Email { get; set; }
        public bool IsAdmin { get; set; }

        public User(Guid userId, string username, string firstName, string lastName, Guid houseId, string phoneNo, string email, bool isAdmin)
        {
            UserId = userId;
            Username = username;
            FirstName = firstName;
            LastName = lastName;
            HouseId = houseId;
            PhoneNo = phoneNo;
            Email = email;
            IsAdmin = isAdmin;
        }

        public User()
        {
            Username = "";
            FirstName = "";
            LastName = "";
            PhoneNo = "";
            Email = "";
        }
 
        public string toString()
        {
            return "UserId: " + UserId + ", Username: " + Username + " = " + FirstName + " " + LastName + ". Phoneno: " + PhoneNo + ", Email: " + Email;

        }

        public static implicit operator List<object>(User v)
        {
            throw new NotImplementedException();
        }
    }
}