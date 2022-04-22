using System.Collections.Generic;

namespace ComplaintService.Provider
{
    public class AuthResult
    {
        public string Token { get; set; }
        public bool Success { get; set; }
        public IEnumerable<string> ErrorMessage { get; set; }
        public string UserId { get; set; }
    }
}