using DIRS_Ketering.Data;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Controls;

namespace DIRS_Ketering.Service
{
    public class PDFService
    {
        public void exportPDF()
        {
            using var db = new AppDbContext();

            var porudzbine = db.Porudzbine
                .Include(k => k.Korisnik)
                .Include(p => p.Stavke)
                    .ThenInclude(s => s.Jelo)
                .ToList();

            var dialog = new SaveFileDialog
            {
                Filter = "PDF files (*.pdf)|*.pdf",
                FileName = $"radni-nalog-{DateTime.Now.Day}{DateTime.Now.Month}{DateTime.Now.Year}.pdf"
            };

            if (dialog.ShowDialog() == true)
            {

                var pdf = new Document();
                PdfWriter.GetInstance(pdf, new FileStream(dialog.FileName, FileMode.Create));

                pdf.Open();

                foreach (var p in porudzbine)
                {
                    pdf.Add(new Paragraph($"Porudzbina ID: {p.Id}"));
                    pdf.Add(new Paragraph($"Klijent: {p.Korisnik.Ime}"));
                    pdf.Add(new Paragraph($"Datum kreiranja: {p.VremeKreiranja}"));
                    pdf.Add(new Paragraph($"Datum dostave: {p.DatumIsporuke}"));
                    pdf.Add(new Paragraph($"Adresa: {p.AdresaIsporuke}"));
                    pdf.Add(new Paragraph($"Status: {p.Status}"));
                    pdf.Add(new Paragraph(" "));

                    PdfPTable table = new PdfPTable(3);
                    table.AddCell("Jelo");
                    table.AddCell("Kolicina");
                    table.AddCell("Cena");

                    foreach (var s in p.Stavke)
                    {
                        table.AddCell(s.Jelo.Naziv);
                        table.AddCell(s.Kolicina.ToString());
                        table.AddCell(s.TrenutnaCena.ToString());
                    }

                    pdf.Add(table);
                    pdf.Add(new Paragraph("\n----------------------------\n"));
                }

                pdf.Close();
            }
        }
    }
}
