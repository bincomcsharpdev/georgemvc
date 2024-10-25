using Dropbox.Api;
using Dropbox.Api.Files;
using Microsoft.AspNetCore.Mvc;
using MyOnlineCV.Models;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MyOnlineCV.Controllers
{
    public class PhotoController : Controller
    {
        // Action to display the photo gallery
        public IActionResult Index()
        {
            return View(PhotoStore.Photos);
        }

        // Action to handle photo uploads
        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                using var stream = file.OpenReadStream();
                string dropboxPath = $"/{file.FileName}";
                var imageUrl = await UploadToDropbox(stream, dropboxPath);

                // Store metadata in memory
                PhotoStore.Photos.Add(new Photo { FileName = file.FileName, ImageUrl = imageUrl });
            }

            // Redirect to Index to show the uploaded photos
            return RedirectToAction("Index");
        }

        // Action to handle photo deletions
        [HttpPost]
        public IActionResult Delete(string fileName)
        {
            var photo = PhotoStore.Photos.FirstOrDefault(p => p.FileName == fileName);
            if (photo != null)
            {
                PhotoStore.Photos.Remove(photo);
            }

            return RedirectToAction("Index");
        }

        // Method to upload the photo to Dropbox
        private async Task<string> UploadToDropbox(Stream fileStream, string dropboxPath)
        {
            var dbx = new DropboxClient("sl.B_PJPgELpC4Ugbc7msXa0AtqmF8FUxvD2gC9ss8K-mwLbKGtm_d9wgaxqgzoySYCDToo8-lGalKOo4LnEWjloybUQ9TkPbrcuKR6rLLj3WpdU507CtsPNu_k__RDjiCGF23YYlKpUVzpihg"); // Replace with your actual access token
            var uploadResponse = await dbx.Files.UploadAsync(dropboxPath, WriteMode.Overwrite.Instance, body: fileStream);
            var sharedLink = await dbx.Sharing.CreateSharedLinkWithSettingsAsync(dropboxPath);
            return sharedLink.Url;
        }
    }
}
