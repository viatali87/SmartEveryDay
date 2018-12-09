// Code generated by Microsoft (R) AutoRest Code Generator 0.17.0.0
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.

namespace Remoni.API.DataAccess.Client.Models
{
    using System.Linq;

    public partial class AlarmTypeNoRefsDto
    {
        /// <summary>
        /// Initializes a new instance of the AlarmTypeNoRefsDto class.
        /// </summary>
        public AlarmTypeNoRefsDto() { }

        /// <summary>
        /// Initializes a new instance of the AlarmTypeNoRefsDto class.
        /// </summary>
        public AlarmTypeNoRefsDto(int alarmTypeId, string name, string description)
        {
            AlarmTypeId = alarmTypeId;
            Name = name;
            Description = description;
        }

        /// <summary>
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "AlarmTypeId")]
        public int AlarmTypeId { get; set; }

        /// <summary>
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "Name")]
        public string Name { get; set; }

        /// <summary>
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "Description")]
        public string Description { get; set; }

        /// <summary>
        /// Validate the object.
        /// </summary>
        /// <exception cref="Microsoft.Rest.ValidationException">
        /// Thrown if validation fails
        /// </exception>
        public virtual void Validate()
        {
            if (Name == null)
            {
                throw new Microsoft.Rest.ValidationException(Microsoft.Rest.ValidationRules.CannotBeNull, "Name");
            }
            if (Description == null)
            {
                throw new Microsoft.Rest.ValidationException(Microsoft.Rest.ValidationRules.CannotBeNull, "Description");
            }
        }
    }
}
