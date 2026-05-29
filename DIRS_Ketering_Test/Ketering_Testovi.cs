using DIRS_Ketering.Models;
using DIRS_Ketering.Service;
using DIRS_Ketering.ViewModels;
using Moq;

namespace DIRS_Ketering_Test
{
    public class Tests
    {
        private KorpaViewModel korpa;
        [SetUp]
        public void Setup()
        {
           korpa = KorpaViewModel.Instance;
        }

        [Test]
        public void povecajKolicinu()
        {
            var jelo = new Jelo { Id = 1002, Naziv = "Sladoled", Cena = 100 };

            korpa.dodajStavku(jelo);
            korpa.dodajStavku(jelo);

            Assert.That(2, Is.EqualTo(korpa.Stavke[0].Kolicina));
        }

        [Test]
        public void testRacunajUkupno()
        {
            korpa.Stavke.Clear();
            decimal c1 = 550;
            decimal c2 = 450;
            decimal ocekujemo = 1000;
            var jelo1 = new Jelo {Id=1000, Naziv = "Jelo T1", Cena = c1 };
            var jelo2 = new Jelo {Id=1001, Naziv = "Jelo T2", Cena = c2 };

            korpa.dodajStavku(jelo1);
            korpa.dodajStavku(jelo2);
            decimal korpaRacuna = korpa.Ukupno;


            Assert.That(korpaRacuna, Is.EqualTo(ocekujemo));
        }

        [Test]
        public void loginTest()
        {
            var mock=new Mock<IAuthService>();
            mock.Setup(k => k.prijava("test@test.com", "password"))
                .Returns(new Korisnik { Id = 3, Ime = "Test", Email = "test@test.com" });

            var korisnik = mock.Object.prijava("test@test.com", "password");

            Assert.That(korisnik, Is.Not.Null);
            Assert.That(korisnik.Email,Is.EqualTo("test@test.com"));
        }

    }
}
