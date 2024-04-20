using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreProject.BLL.Dtos.Token
{
    public class RefreshTokenRequest
    {
        public string OldToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
