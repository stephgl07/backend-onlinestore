using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tranzact.OnlineStore.Domain.FileStorage
{
    public interface IFileStorage
    {
        Task<string> EditFile(byte[] contenido, string extension,
          string nombreContenedor, string rutaArchivo);
        Task DeleteFile(string ruta, string nombreContenedor);
        Task<string> SaveFile(byte[] contenido, string extension,
            string nombreContenedor);
        Task<string> SaveTextPlain(string contenido, string fileName,
            string nombreContenedor);
        Task<string> SavePdf(byte[] contenido, string extension,
            string nombreContenedor);
    }
}
