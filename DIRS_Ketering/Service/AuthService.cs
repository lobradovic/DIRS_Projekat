using DIRS_Ketering.Models;
using FoodOrderApp.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace DIRS_Ketering.Service
{
    public class AuthService:IAuthService
    {
        public Korisnik? prijava(string email, string password)
        {
            using var db = new AppDbContext();
            var korisnik = db.Korisnici.FirstOrDefault(k => k.Email == email);

            if (korisnik == null) return null;

            if(password!=korisnik.Lozinka) return null;

            return korisnik;
        }

        public void registracija(string ime, string email, string password)
        {
            using var db=new AppDbContext();
            Korisnik korisnik=new Korisnik();
            if(ime!="" && email!="" && password!="")
            {
                korisnik.Ime = ime;
                korisnik.Email = email;
                korisnik.Lozinka = password;
                korisnik.Rola = Rola.Klijent;
            }

            db.Korisnici.Add(korisnik);
            db.SaveChanges();

        }

    }
}
