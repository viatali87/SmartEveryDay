// Code generated by Microsoft (R) AutoRest Code Generator 0.17.0.0
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.

namespace Remoni.API.DataAccess.Client.Models
{
    using System.Linq;

    public partial class EditUnitDto
    {
        /// <summary>
        /// Initializes a new instance of the EditUnitDto class.
        /// </summary>
        public EditUnitDto() { }

        /// <summary>
        /// Initializes a new instance of the EditUnitDto class.
        /// </summary>
        /// <param name="dataInterval">Possible values include: 'Minute',
        /// 'Minutes5', 'Seconds20Boost'</param>
        public EditUnitDto(string name = default(string), string description = default(string), double? latitude = default(double?), double? longitude = default(double?), double? gpsAccuracy = default(double?), string dataInterval = default(string), System.Collections.Generic.IList<WriteUnitValueDto> unitValues = default(System.Collections.Generic.IList<WriteUnitValueDto>))
        {
            Name = name;
            Description = description;
            Latitude = latitude;
            Longitude = longitude;
            GpsAccuracy = gpsAccuracy;
            DataInterval = dataInterval;
            UnitValues = unitValues;
        }

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
        [Newtonsoft.Json.JsonProperty(PropertyName = "Latitude")]
        public double? Latitude { get; set; }

        /// <summary>
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "Longitude")]
        public double? Longitude { get; set; }

        /// <summary>
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "GpsAccuracy")]
        public double? GpsAccuracy { get; set; }

        /// <summary>
        /// Gets or sets possible values include: 'Minute', 'Minutes5',
        /// 'Seconds20Boost'
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "DataInterval")]
        public string DataInterval { get; set; }

        /// <summary>
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "UnitValues")]
        public System.Collections.Generic.IList<WriteUnitValueDto> UnitValues { get; set; }

    }
}
