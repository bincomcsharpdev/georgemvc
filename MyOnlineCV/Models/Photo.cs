namespace MyOnlineCV.Models
{
    public class Photo
    {
        public int Id { get; set; }
        public string? UserId { get; set; }  // Optional: Track which user uploaded the photo
        public string? FileName { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime UploadedAt { get; set; } = DateTime.Now;
    }
}
