using DIRS_Ketering.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DIRS_Ketering.Service
{
    public class SessionService
    {
        private static SessionService? _instance;
        private static readonly object _lock = new object();

        private SessionService() { }

        public static SessionService Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                            _instance = new SessionService();
                    }
                }
                return _instance;
            }
        }

        public Korisnik? TrenutniKorisnik { get; set; }
        public bool ulogovanKorisnik()
        {
            if (TrenutniKorisnik!=null) return true;
            return false;
        }
        public bool korisnikAdmin()
        {
            if (TrenutniKorisnik != null && TrenutniKorisnik.Rola == Rola.Admin) return true;
            return false;
        }
        public void prijavi(Korisnik korisnik)
        {
            TrenutniKorisnik = korisnik;
        }

        public void odjavi()
        {
            TrenutniKorisnik = null;
        }
    }
}
