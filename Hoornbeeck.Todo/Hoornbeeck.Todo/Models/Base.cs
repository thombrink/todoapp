using System;

namespace Hoornbeeck.Todo.Models {
    public abstract class Base {
        [Newtonsoft.Json.JsonProperty("Id")]
        public string Id { get; set; }

        [Microsoft.WindowsAzure.MobileServices.Version]
        public string AzureVersion { get; set; }

        /// <summary>
        /// Gets or sets the date UTC.
        /// </summary>
        /// <value>The date UTC.</value>
        public DateTime DateUtc { get; set; }


        [Newtonsoft.Json.JsonIgnore]
        public string DateDisplay => DateUtc.ToLocalTime().ToString("d");

        [Newtonsoft.Json.JsonIgnore]
        public string TimeDisplay => DateUtc.ToLocalTime().ToString("t");
    }
}
