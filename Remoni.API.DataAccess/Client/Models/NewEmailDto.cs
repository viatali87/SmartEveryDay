// Code generated by Microsoft (R) AutoRest Code Generator 0.17.0.0
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.

namespace Remoni.API.DataAccess.Client.Models
{
    using System.Linq;

    public partial class NewEmailDto
    {
        /// <summary>
        /// Initializes a new instance of the NewEmailDto class.
        /// </summary>
        public NewEmailDto() { }

        /// <summary>
        /// Initializes a new instance of the NewEmailDto class.
        /// </summary>
        public NewEmailDto(string newEmail = default(string))
        {
            NewEmail = newEmail;
        }

        /// <summary>
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "NewEmail")]
        public string NewEmail { get; set; }

    }
}