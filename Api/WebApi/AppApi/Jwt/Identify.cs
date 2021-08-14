using System.ComponentModel.DataAnnotations;

namespace AppApi.Jwt
{
    public  class Identify
    {
        /// <summary>
        /// Just A Static User Pass Identify
        /// For Test!!!
        /// </summary>

        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }

    }
}
