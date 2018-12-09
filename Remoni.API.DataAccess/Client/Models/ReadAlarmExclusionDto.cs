// Code generated by Microsoft (R) AutoRest Code Generator 0.17.0.0
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.

namespace Remoni.API.DataAccess.Client.Models
{
    using System.Linq;

    public partial class ReadAlarmExclusionDto
    {
        /// <summary>
        /// Initializes a new instance of the ReadAlarmExclusionDto class.
        /// </summary>
        public ReadAlarmExclusionDto() { }

        /// <summary>
        /// Initializes a new instance of the ReadAlarmExclusionDto class.
        /// </summary>
        public ReadAlarmExclusionDto(int alarmExclusionId, System.DateTimeOffset excluded, AlarmTypeNoRefsDto alarmType)
        {
            AlarmExclusionId = alarmExclusionId;
            Excluded = excluded;
            AlarmType = alarmType;
        }

        /// <summary>
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "AlarmExclusionId")]
        public int AlarmExclusionId { get; set; }

        /// <summary>
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "Excluded")]
        public System.DateTimeOffset Excluded { get; set; }

        /// <summary>
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "AlarmType")]
        public AlarmTypeNoRefsDto AlarmType { get; set; }

        /// <summary>
        /// Validate the object.
        /// </summary>
        /// <exception cref="Microsoft.Rest.ValidationException">
        /// Thrown if validation fails
        /// </exception>
        public virtual void Validate()
        {
            if (AlarmType == null)
            {
                throw new Microsoft.Rest.ValidationException(Microsoft.Rest.ValidationRules.CannotBeNull, "AlarmType");
            }
            if (this.AlarmType != null)
            {
                this.AlarmType.Validate();
            }
        }
    }
}
