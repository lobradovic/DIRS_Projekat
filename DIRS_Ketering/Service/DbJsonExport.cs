using DIRS_Ketering.Data;
using DIRS_Ketering.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DIRS_Ketering.Service
{
    public class DbJsonExport
    {
        public void Export()
        {
            using var db = new AppDbContext();

            var podaci = new
            {
                jela = db.Jela.Select(j => new {
                    j.Id,
                    j.Naziv,
                    j.Opis,
                    j.Cena
                }).ToList(),

                korisnici = db.Korisnici.Select(k => new
                {
                    k.Id,
                    k.Ime,
                    k.Email,
                    k.Lozinka,
                    k.Rola
                }).ToList(),

                porudzbine=db.Porudzbine.Select(p => new
                {
                    p.Id,
                    p.VremeKreiranja,
                    p.DatumIsporuke,
                    p.AdresaIsporuke,
                    p.KorisnikId,
                    p.Status,

                    stavke = p.Stavke.Select(s => new
                    {
                        s.Id,
                        s.Kolicina,
                        s.JeloId,
                        s.TrenutnaCena
                    }).ToList()
                }).ToList(),
            };

            var opcije = new JsonSerializerOptions
            {
                WriteIndented = true,
            };
            string json = JsonSerializer.Serialize(podaci, opcije);
            var dialog = new SaveFileDialog
            {
                Filter = "JSON files (*.json)|*.json",
                FileName = "baza.json"
            };

            if (dialog.ShowDialog() == true)
            {
                File.WriteAllText(dialog.FileName, json);
            }

        }
    }
}
