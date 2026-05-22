using DIRS_Ketering.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DIRS_Ketering.Service
{
    public interface IAuthService
    {
        Korisnik? prijava(string username, string password);
        void registracija(string username,string email, string password);
    }
}
