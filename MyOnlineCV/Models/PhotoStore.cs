using System.Collections.Generic;
using System.Linq;

namespace MyOnlineCV.Models
{
    public static class PhotoStore
    {
        // In-memory list to store photo data temporarily
        public static List<Photo> Photos = new List<Photo>();

        public static bool RemovePhoto(string? fileName)
        {
            if (string.IsNullOrEmpty(fileName)) return false; // Prevent removing a null or empty file name

            var photoToRemove = Photos.FirstOrDefault(p => p.FileName == fileName);
            if (photoToRemove != null)
            {
                Photos.Remove(photoToRemove);
                return true;
            }
            return false;
        }
    }
}
