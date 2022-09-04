namespace DapperASPNetCore.Dto
{
    public class FileUploadDto
    {
        public string FileName { get; set; }
        public long FileSize { get; set; }
        public byte[] FileContent { get; set; }
        public string ImageUrl { get; set; }
        public string Created { get; set; }
        public string CreatedBy { get; set; }
    }
}
