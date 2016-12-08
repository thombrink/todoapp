namespace Todoo.Shared.Models {
    public class Todo : Base {
        public string Note { get; set; }

        /// <summary>
        /// Gets or sets the OS of the user
        /// </summary>
        /// <value>The OS</value>
        public string OS { get; set; }
    }
}
