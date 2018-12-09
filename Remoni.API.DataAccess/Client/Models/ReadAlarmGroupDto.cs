// Code generated by Microsoft (R) AutoRest Code Generator 0.17.0.0
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.

namespace Remoni.API.DataAccess.Client.Models
{
    using System.Linq;

    public partial class ReadAlarmGroupDto
    {
        /// <summary>
        /// Initializes a new instance of the ReadAlarmGroupDto class.
        /// </summary>
        public ReadAlarmGroupDto() { }

        /// <summary>
        /// Initializes a new instance of the ReadAlarmGroupDto class.
        /// </summary>
        public ReadAlarmGroupDto(int alarmGroupId, string name, AccountNoRefsDto account, System.Collections.Generic.IList<UnitNoRefsDto> units = default(System.Collections.Generic.IList<UnitNoRefsDto>), System.Collections.Generic.IList<ReadAlarmSubscriptionDto> alarmSubscriptions = default(System.Collections.Generic.IList<ReadAlarmSubscriptionDto>), System.Collections.Generic.IList<ReadAlarmExclusionDto> alarmExclusions = default(System.Collections.Generic.IList<ReadAlarmExclusionDto>))
        {
            AlarmGroupId = alarmGroupId;
            Name = name;
            Account = account;
            Units = units;
            AlarmSubscriptions = alarmSubscriptions;
            AlarmExclusions = alarmExclusions;
        }

        /// <summary>
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "AlarmGroupId")]
        public int AlarmGroupId { get; set; }

        /// <summary>
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "Name")]
        public string Name { get; set; }

        /// <summary>
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "Account")]
        public AccountNoRefsDto Account { get; set; }

        /// <summary>
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "Units")]
        public System.Collections.Generic.IList<UnitNoRefsDto> Units { get; set; }

        /// <summary>
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "AlarmSubscriptions")]
        public System.Collections.Generic.IList<ReadAlarmSubscriptionDto> AlarmSubscriptions { get; set; }

        /// <summary>
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "AlarmExclusions")]
        public System.Collections.Generic.IList<ReadAlarmExclusionDto> AlarmExclusions { get; set; }

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
            if (Account == null)
            {
                throw new Microsoft.Rest.ValidationException(Microsoft.Rest.ValidationRules.CannotBeNull, "Account");
            }
            if (this.Account != null)
            {
                this.Account.Validate();
            }
            if (this.Units != null)
            {
                foreach (var element in this.Units)
                {
                    if (element != null)
                    {
                        element.Validate();
                    }
                }
            }
            if (this.AlarmSubscriptions != null)
            {
                foreach (var element1 in this.AlarmSubscriptions)
                {
                    if (element1 != null)
                    {
                        element1.Validate();
                    }
                }
            }
            if (this.AlarmExclusions != null)
            {
                foreach (var element2 in this.AlarmExclusions)
                {
                    if (element2 != null)
                    {
                        element2.Validate();
                    }
                }
            }
        }
    }
}
