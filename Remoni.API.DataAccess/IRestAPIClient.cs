using Remoni.API.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Remoni.API.DataAccess
{
    public interface IRestAPIClient : IRemoniAPIClient
    {
        void SetCredentials(string email, string password);
    }
}
