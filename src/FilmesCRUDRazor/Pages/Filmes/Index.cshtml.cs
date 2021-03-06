using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FilmesCRUDRazor.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FilmesCRUDRazor.Pages.Filmes
{
    public class IndexModel : PageModel
    {
        private readonly FilmesCRUDRazor.Models.FilmeContext _context;

        public IndexModel(FilmesCRUDRazor.Models.FilmeContext context)
        {
            _context = context;
        }

        public IList<Filme> Filme { get; set; }

        public SelectList Generos;
        public string FilmePorGenero { get; set; }

        public async Task OnGetAsync(string buscaPorGeneroNome, string filmePorGenero)
        {
            #region Inicio Logica do input

            // select genero from filme where genero = genero orderby genero

            IQueryable<string> queryGenero = from f in _context.Filme orderby f.Genero select f.Genero;

            var filmes = from f in _context.Filme
                         select f;
            if (String.IsNullOrEmpty(buscaPorGeneroNome))
            {
                filmes = filmes.Where(
                    b => b.Titulo.Contains(buscaPorGeneroNome));
            }

            #endregion Fim da logica do input 

            #region Inicio da logica do Select

            if(!String.IsNullOrEmpty(filmePorGenero)){
                filmes = filmes.Where(b => b.Genero == filmePorGenero);
            }

            #endregion Fim da logica do Select

            Generos = new SelectList(await queryGenero.Distinct().ToListAsync());
            Filme = await filmes.ToListAsync();

        }
    }
}
