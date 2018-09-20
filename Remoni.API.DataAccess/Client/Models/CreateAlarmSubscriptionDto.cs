// Code generated by Microsoft (R) AutoRest Code Generator 0.17.0.0
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.

namespace Remoni.API.DataAccess.Client.Models
{
    using System.Linq;

    public partial class CreateAlarmSubscriptionDto
    {
        /// <summary>
        /// Initializes a new instance of the CreateAlarmSubscriptionDto class.
        /// </summary>
        public CreateAlarmSubscriptionDto() { }

        /// <summary>
        /// Initializes a new instance of the CreateAlarmSubscriptionDto class.
        /// </summary>
        /// <param name="feedSeverity">Possible values include: 'Information',
        /// 'Trivial', 'Minor', 'Medium', 'Major', 'Critical'</param>
        /// <param name="emailSeverity">Possible values include:
        /// 'Information', 'Trivial', 'Minor', 'Medium', 'Major',
        /// 'Critical'</param>
        public CreateAlarmSubscriptionDto(UserIdDto user, string feedSeverity = default(string), string emailSeverity = default(string))
        {
            User = user;
            FeedSeverity = feedSeverity;
            EmailSeverity = emailSeverity;
        }

        /// <summary>
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "User")]
        public UserIdDto User { get; set; }

        /// <summary>
        /// Gets or sets possible values include: 'Information', 'Trivial',
        /// 'Minor', 'Medium', 'Major', 'Critical'
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "FeedSeverity")]
        public string FeedSeverity { get; set; }

        /// <summary>
        /// Gets or sets possible values include: 'Information', 'Trivial',
        /// 'Minor', 'Medium', 'Major', 'Critical'
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "EmailSeverity")]
        public string EmailSeverity { get; set; }

        /// <summary>
        /// Validate the object.
        /// </summary>
        /// <exception cref="Microsoft.Rest.ValidationException">
        /// Thrown if validation fails
        /// </exception>
        public virtual void Validate()
        {
            if (User == null)
            {
                throw new Microsoft.Rest.ValidationException(Microsoft.Rest.ValidationRules.CannotBeNull, "User");
            }
            if (this.User != null)
            {
                this.User.Validate();
            }
        }
    }
}
