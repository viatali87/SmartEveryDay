// Code generated by Microsoft (R) AutoRest Code Generator 0.17.0.0
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.

namespace Remoni.API.DataAccess.Client.Models
{
    using System.Linq;

    public partial class ReadAlarmTypeDto
    {
        /// <summary>
        /// Initializes a new instance of the ReadAlarmTypeDto class.
        /// </summary>
        public ReadAlarmTypeDto() { }

        /// <summary>
        /// Initializes a new instance of the ReadAlarmTypeDto class.
        /// </summary>
        public ReadAlarmTypeDto(int alarmTypeId, string name, string description, System.Collections.Generic.IList<ReadAlarmSubtypeDto> alarmSubtypes = default(System.Collections.Generic.IList<ReadAlarmSubtypeDto>), System.Collections.Generic.IList<UnitTypeNoRefsDto> unitTypes = default(System.Collections.Generic.IList<UnitTypeNoRefsDto>))
        {
            AlarmTypeId = alarmTypeId;
            Name = name;
            Description = description;
            AlarmSubtypes = alarmSubtypes;
            UnitTypes = unitTypes;
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
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "AlarmSubtypes")]
        public System.Collections.Generic.IList<ReadAlarmSubtypeDto> AlarmSubtypes { get; set; }

        /// <summary>
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "UnitTypes")]
        public System.Collections.Generic.IList<UnitTypeNoRefsDto> UnitTypes { get; set; }

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
            if (this.AlarmSubtypes != null)
            {
                foreach (var element in this.AlarmSubtypes)
                {
                    if (element != null)
                    {
                        element.Validate();
                    }
                }
            }
            if (this.UnitTypes != null)
            {
                foreach (var element1 in this.UnitTypes)
                {
                    if (element1 != null)
                    {
                        element1.Validate();
                    }
                }
            }
        }
    }
}
