using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRUD_Basico.Models;

namespace CRUD_Basico.Services.Interfaces
{
    public interface ITokenServices
    {
        string CreateToken (Usuario usuario);
    }
}