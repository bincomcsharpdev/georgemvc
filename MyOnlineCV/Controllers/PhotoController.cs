using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyOnlineCV.Data;
using MyOnlineCV.Models;
using System.Security.Claims;

namespace MyOnlineCV.Controllers
{
    public class PhotoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PhotoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Display the Upload View with Existing Photos
        public async Task<IActionResult> Upload()
        {
            var photos = await _context.Photos.ToListAsync();  // Load photos from DB
            return View(photos ?? new List<Photo>());  // Pass photos to the view, ensuring it's not null
        }

        // Handle Photo Upload (POST)
        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                var photo = new Photo
                {
                    FileName = file.FileName,
                    ImageUrl = "/uploads/" + file.FileName  // Example path
                };

                // Save the file to the server's root directory
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", file.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                _context.Add(photo);
                await _context.SaveChangesAsync();

                // Redirect back to the Upload view to display the updated list
                return RedirectToAction(nameof(Upload));
            }

            return View();
        }

        // Handle Photo Deletion
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var photo = await _context.Photos.FindAsync(id);
            if (photo != null)
            {
                // Check if FileName is not null or empty
                if (!string.IsNullOrEmpty(photo.FileName))
                {
                    // Optionally delete the file from the server
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", photo.FileName);
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }
                }

                _context.Photos.Remove(photo);
                await _context.SaveChangesAsync();
            }

            // Redirect back to the Upload view after deletion
            return RedirectToAction(nameof(Upload));
        }
    }
}
