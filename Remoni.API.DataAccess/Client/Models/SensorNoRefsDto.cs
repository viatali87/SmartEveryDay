// Code generated by Microsoft (R) AutoRest Code Generator 0.17.0.0
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.

namespace Remoni.API.DataAccess.Client.Models
{
    using System.Linq;

    public partial class SensorNoRefsDto
    {
        /// <summary>
        /// Initializes a new instance of the SensorNoRefsDto class.
        /// </summary>
        public SensorNoRefsDto() { }

        /// <summary>
        /// Initializes a new instance of the SensorNoRefsDto class.
        /// </summary>
        /// <param name="sensorType">Possible values include: 'AccessPoint',
        /// 'PowerMoniSpot', 'PowerMoniProC', 'FlowMoniSpot', 'FlowMoniPro',
        /// 'HeatMoniSpot', 'HeatMoniPro', 'TempMoniPro', 'RoomMoniSpot',
        /// 'RoomMoniPro', 'DataMoni'</param>
        /// <param name="calibrationType">Possible values include: 'Power',
        /// 'Current', 'AutoCurrent'</param>
        public SensorNoRefsDto(int sensorId, bool isConfigured, string sensorType, System.DateTimeOffset? dataLastRecorded = default(System.DateTimeOffset?), double? calibrationConstant = default(double?), string calibrationType = default(string))
        {
            SensorId = sensorId;
            DataLastRecorded = dataLastRecorded;
            IsConfigured = isConfigured;
            SensorType = sensorType;
            CalibrationConstant = calibrationConstant;
            CalibrationType = calibrationType;
        }

        /// <summary>
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "SensorId")]
        public int SensorId { get; set; }

        /// <summary>
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "DataLastRecorded")]
        public System.DateTimeOffset? DataLastRecorded { get; set; }

        /// <summary>
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "IsConfigured")]
        public bool IsConfigured { get; set; }

        /// <summary>
        /// Gets or sets possible values include: 'AccessPoint',
        /// 'PowerMoniSpot', 'PowerMoniProC', 'FlowMoniSpot', 'FlowMoniPro',
        /// 'HeatMoniSpot', 'HeatMoniPro', 'TempMoniPro', 'RoomMoniSpot',
        /// 'RoomMoniPro', 'DataMoni'
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "SensorType")]
        public string SensorType { get; set; }

        /// <summary>
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "CalibrationConstant")]
        public double? CalibrationConstant { get; set; }

        /// <summary>
        /// Gets or sets possible values include: 'Power', 'Current',
        /// 'AutoCurrent'
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "CalibrationType")]
        public string CalibrationType { get; set; }

        /// <summary>
        /// Validate the object.
        /// </summary>
        /// <exception cref="Microsoft.Rest.ValidationException">
        /// Thrown if validation fails
        /// </exception>
        public virtual void Validate()
        {
            if (SensorType == null)
            {
                throw new Microsoft.Rest.ValidationException(Microsoft.Rest.ValidationRules.CannotBeNull, "SensorType");
            }
        }
    }
}