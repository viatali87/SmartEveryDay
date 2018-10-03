// Code generated by Microsoft (R) AutoRest Code Generator 0.17.0.0
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.

namespace Remoni.API.DataAccess.Client.Models
{
    using System.Linq;

    /// <summary>
    /// Defines headers for PostAsyncByunit operation.
    /// </summary>
    public partial class UnitsPostAsyncByunitHeaders
    {
        /// <summary>
        /// Initializes a new instance of the UnitsPostAsyncByunitHeaders
        /// class.
        /// </summary>
        public UnitsPostAsyncByunitHeaders() { }

        /// <summary>
        /// Initializes a new instance of the UnitsPostAsyncByunitHeaders
        /// class.
        /// </summary>
        /// <param name="location">Provides location for the added
        /// entity</param>
        public UnitsPostAsyncByunitHeaders(string location = default(string))
        {
            Location = location;
        }

        /// <summary>
        /// Gets or sets provides location for the added entity
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "Location")]
        public string Location { get; set; }

    }
}