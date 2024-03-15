using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MvcPaginacionExamenMauricio.Data;
using MvcPaginacionExamenMauricio.Models;
using System.Data;
using System.Diagnostics.Metrics;

#region PROCEDURES
//alter procedure SP_PAGINACION_ZAPATILLAS
//(@posicion int, @idzapatilla int, @registros int out)
//as
//	select @registros=COUNT(IDPRODUCTO) FROM IMAGENESZAPASPRACTICA
//	where IDPRODUCTO=@idzapatilla
//	select IDIMAGEN, IDPRODUCTO, IMAGEN from
//		(select IDIMAGEN, IDPRODUCTO, IMAGEN,
//        ROW_NUMBER() over (order by IDIMAGEN) POSICION
//		from IMAGENESZAPASPRACTICA
//		where IDPRODUCTO=@idzapatilla) query
//	where POSICION>=@posicion and POSICION<@posicion+1
//go
#endregion

namespace MvcPaginacionExamenMauricio.Repositories
{
    public class RepositoryZapatillas
    {
        private ZapatillasContext context;

        public RepositoryZapatillas(ZapatillasContext context)
        {
            this.context = context;
        }

        public async Task<List<Zapatilla>> GetZapatillasAsyc()
        {
            return await this.context.Zapatillas.ToListAsync();
        }


        public async Task<Zapatilla> FindZapatillaAsyn(int idzapatilla)
        {
            return await this.context.Zapatillas.FirstOrDefaultAsync(z => z.IdZapatilla == idzapatilla);
        }

        public async Task<List<ImagenZapatilla>> GetImagenesZapatilla(int idzapatilla)
        {
            return await this.context.ImagenesZapatillas.Where(img => img.IdZapatilla == idzapatilla).ToListAsync();
        }


        public async Task<ImagenPaginacion> GetImagenPaginacionAsync(int posicion, int idzapatilla)
        {
            string sql = "SP_PAGINACION_ZAPATILLAS @posicion,@idzapatilla,@registros out";
            SqlParameter paramposicion = new SqlParameter("@posicion", posicion);
            SqlParameter paramidzapatilla = new SqlParameter("@idzapatilla", idzapatilla);
            SqlParameter paramregistros = new SqlParameter("@registros", -1);
            paramregistros.Direction = ParameterDirection.Output;
            var consulta = this.context.ImagenesZapatillas.FromSqlRaw(sql, paramposicion, paramidzapatilla, paramregistros).AsEnumerable();
            ImagenZapatilla imagen = consulta.FirstOrDefault();
            int registros = int.Parse(paramregistros.Value.ToString());
            ImagenPaginacion model = new ImagenPaginacion
            {
                ImagenZapatilla = imagen,
                Registros = registros
            };
            return model;
        }

        public async Task InsertImagen(int idzapatilla, string imagen)
        {
            int nextid = this.context.ImagenesZapatillas.Max(z => z.IdImagen) + 1;
            ImagenZapatilla imagenZapa = new ImagenZapatilla
            {
                IdImagen = nextid,
                IdZapatilla = idzapatilla,
                Imagen = imagen
            };
            await this.context.ImagenesZapatillas.AddAsync(imagenZapa);
            await this.context.SaveChangesAsync();
        }
    }
}
