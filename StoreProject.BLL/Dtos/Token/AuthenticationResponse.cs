using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreProject.BLL.Dtos.Token
{
    public class AuthenticationResponse
    {
        public string Id { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
