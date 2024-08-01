using Microsoft.AspNetCore.Http;

namespace Models
{
    public class File : IFormFile
    {
        public int Id { get; set; }

        public string ContentType { get; set; } = string.Empty;

        public string ContentDisposition { get; set; } = string.Empty;

        public IHeaderDictionary Headers { get; set; } = null!;

        public long Length { get; set; }

        public string Name { get; set; } = string.Empty;

        public string FileName { get; set; } = string.Empty;
        public void CopyTo(Stream target)
        {
            throw new NotImplementedException();
        }

        public Task CopyToAsync(Stream target, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Stream OpenReadStream()
        {
            throw new NotImplementedException();
        }
    }
}
