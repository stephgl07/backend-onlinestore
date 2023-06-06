using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tranzact.OnlineStore.Domain.Api.AppConfiguration;
using Tranzact.OnlineStore.Domain.FileStorage;

namespace Tranzact.OnlineStore.Infrastructure.FileStorage
{
    public class AzureBlobStorage : IFileStorage
    {
        private readonly string connectionString;
        public AzureBlobStorage(IAppConfiguration appConfiguration)
        {
            connectionString = appConfiguration.GetBlobStorageConnectionSring();
        }
        public async Task DeleteFile(string ruta, string nombreContenedor)
        {
            var account = CloudStorageAccount.Parse(connectionString);
            var clientService = account.CreateCloudBlobClient();
            var contenedor = clientService.GetContainerReference(nombreContenedor);

            var blobName = Path.GetFileName(ruta);
            var blob = contenedor.GetBlobReference(blobName);
            await blob.DeleteIfExistsAsync();
        }

        public async Task<string> EditFile(byte[] contenido, string extension, string nombreContenedor, string rutaArchivo)
        {
            if (!string.IsNullOrWhiteSpace(rutaArchivo))
            {
                await DeleteFile(rutaArchivo, nombreContenedor);
            }
            return await SaveFile(contenido, extension, nombreContenedor);
        }

        public async Task<string> SaveTextPlain(string contenido, string fileName, string nombreContenedor)
        {
            var account = CloudStorageAccount.Parse(connectionString);
            var clientService = account.CreateCloudBlobClient();
            var contenedor = clientService.GetContainerReference(nombreContenedor);
            await contenedor.CreateIfNotExistsAsync();
            await contenedor.SetPermissionsAsync(new BlobContainerPermissions
            {
                PublicAccess = BlobContainerPublicAccessType.Container
            });
            var blob = contenedor.GetBlockBlobReference(fileName);

            byte[] contenidoBytes = Encoding.UTF8.GetBytes(contenido);

            await blob.UploadFromByteArrayAsync(contenidoBytes, 0, contenidoBytes.Length);

            blob.Properties.ContentType = "text/plain";
            await blob.SetPropertiesAsync();

            return blob.Uri.ToString();
        }

        public async Task<string> SavePdf(byte[] contenido, string extension, string nombreContenedor)
        {
            var account = CloudStorageAccount.Parse(connectionString);
            var clientService = account.CreateCloudBlobClient();
            var contenedor = clientService.GetContainerReference(nombreContenedor);
            await contenedor.CreateIfNotExistsAsync();
            await contenedor.SetPermissionsAsync(new BlobContainerPermissions
            {
                PublicAccess = BlobContainerPublicAccessType.Container
            });
            var filename = $"{Guid.NewGuid()}.{extension}";
            var blob = contenedor.GetBlockBlobReference(filename);
            await blob.UploadFromByteArrayAsync(contenido, 0, contenido.Length);
            blob.Properties.ContentType = "application/pdf";
            await blob.SetPropertiesAsync();
            return blob.Uri.ToString();
        }
        public async Task<string> SaveFile(byte[] contenido, string extension, string nombreContenedor)
        {
            var account = CloudStorageAccount.Parse(connectionString);
            var clientService = account.CreateCloudBlobClient();
            var contenedor = clientService.GetContainerReference(nombreContenedor);
            await contenedor.CreateIfNotExistsAsync();
            await contenedor.SetPermissionsAsync(new BlobContainerPermissions
            {
                PublicAccess = BlobContainerPublicAccessType.Blob
            });
            var filename = $"{Guid.NewGuid()}.{extension}";
            var blob = contenedor.GetBlockBlobReference(filename);
            await blob.UploadFromByteArrayAsync(contenido, 0, contenido.Length);
            blob.Properties.ContentType = "image/jpg";
            await blob.SetPropertiesAsync();
            return blob.Uri.ToString();
        }
    }
}
