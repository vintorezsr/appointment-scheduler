namespace AppointmentScheduler.Models
{
    /// <summary>
    /// View model class for login request.
    /// </summary>
    public class LoginViewModel
    {
        /// <summary>
        /// Gets or sets the login username.
        /// </summary>
        public string Username { get; set; } = default!;

        /// <summary>
        /// Gets or sets the login password.
        /// </summary>
        public string Password { get; set; } = default!;

        /// <summary>
        /// Determine whether the user session should be persisted or not.
        /// </summary>
        public bool RememberMe { get; set; }
    }
}
