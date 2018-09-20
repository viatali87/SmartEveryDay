// Code generated by Microsoft (R) AutoRest Code Generator 0.17.0.0
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.

namespace Remoni.API.DataAccess.Client.Models
{
    using System.Linq;

    public partial class AlarmSubtypeNoRefsDto
    {
        /// <summary>
        /// Initializes a new instance of the AlarmSubtypeNoRefsDto class.
        /// </summary>
        public AlarmSubtypeNoRefsDto() { }

        /// <summary>
        /// Initializes a new instance of the AlarmSubtypeNoRefsDto class.
        /// </summary>
        /// <param name="severity">Possible values include: 'Information',
        /// 'Trivial', 'Minor', 'Medium', 'Major', 'Critical'</param>
        public AlarmSubtypeNoRefsDto(int alarmSubtypeId, string name, string description, string severity)
        {
            AlarmSubtypeId = alarmSubtypeId;
            Name = name;
            Description = description;
            Severity = severity;
        }

        /// <summary>
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "AlarmSubtypeId")]
        public int AlarmSubtypeId { get; set; }

        /// <summary>
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "Name")]
        public string Name { get; set; }

        /// <summary>
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "Description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets possible values include: 'Information', 'Trivial',
        /// 'Minor', 'Medium', 'Major', 'Critical'
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "Severity")]
        public string Severity { get; set; }

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
            if (Severity == null)
            {
                throw new Microsoft.Rest.ValidationException(Microsoft.Rest.ValidationRules.CannotBeNull, "Severity");
            }
        }
    }
}
