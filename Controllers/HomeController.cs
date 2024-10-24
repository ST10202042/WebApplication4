using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApplication4.Models;
using WebApplication4.Data;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;

namespace WebApplication4.Controllers
{
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private readonly ClaimContext _context;

        public HomeController(ILogger<HomeController> logger, ClaimContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Claim()
        {
            ModelState.Clear();
            return View();
        }
        private string GenerateClaimNumber()
        {
            return "CLM-" + Guid.NewGuid().ToString().Substring(0, 8).ToUpper();
        }


        [HttpPost]
        public async Task<IActionResult> Claim(Claim claim, IFormFile SupportingDocuments)
        {
            if (ModelState.IsValid)
            {
                // Generate a unique claim number before adding the claim to the database
                claim.ClaimNumber = GenerateClaimNumber();
                claim.Status = "Pending";
                claim.DateOfClaim = DateTime.Now;

                // Handle file upload (if any)
                if (SupportingDocuments != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await SupportingDocuments.CopyToAsync(memoryStream);
                        claim.SupportingDocuments = memoryStream.ToArray(); // Store the file as byte array
                    }
                }
                if (claim.Modules == null)
                {
                    claim.Modules = new List<Module>();
                }

                // Add modules to the claim (this is assuming that claim.Modules is populated via the form)
                foreach (var module in claim.Modules)
                {
                    module.ClaimNumber = claim.ClaimNumber;  // Ensure each module is associated with the correct claim
                    module.DateOfClaim = DateTime.Now;
                }

                // Save the claim and modules to the database
                _context.Claims.Add(claim);
                await _context.SaveChangesAsync();  // Ensure the claim and modules are saved

                // Store the claim number in TempData to display it later
                TempData["ClaimNumber"] = claim.ClaimNumber;

                return RedirectToAction("SubmitClaim");
            }
            return View(claim);
        }


        public IActionResult SubmitClaim()
        {
            // Check if ClaimNumber exists in TempData
            ViewBag.ClaimNumber = TempData["ClaimNumber"];
            return View();
        }


        public IActionResult Verify(string claimNumber)
        {
            var claim = _context.Claims.FirstOrDefault(c => c.ClaimNumber == claimNumber);

            if (claim == null)
            {
                return RedirectToAction("Track");
            }

            return View(claim);
        }

        [HttpGet]
        public IActionResult Track()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Track(string claimNumber)
        {
            var claim = _context.Claims.FirstOrDefault(c => c.ClaimNumber == claimNumber);
            if (claim == null)
            {
                ViewBag.Message = "Claim not found.";
                return View();
            }

            return View("Status", claim); // Redirect to Status view if claim is found
        }

        public IActionResult Manage()
        {
            // Retrieve all claims with a "Pending" status from the database
            var pendingClaims = _context.Claims.Where(c => c.Status == "Pending").ToList();

            _logger.LogInformation($"Number of pending claims: {pendingClaims.Count}");

            return View(pendingClaims);
        }

        public IActionResult Status()
        {
            return View();
        }

      [HttpPost]
public IActionResult UpdateClaimStatus(string claimNumber, string status)
{
    // Find the claim by ClaimNumber
    var claim = _context.Claims.FirstOrDefault(c => c.ClaimNumber == claimNumber);
    
    if (claim == null)
    {
        return NotFound();
    }

    // Update the status
    claim.Status = status;

    // Save changes to the database
    _context.SaveChanges();

    return Ok();
}

        public IActionResult register()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
       

[Authorize(Roles = "Manager")]
    public IActionResult ManageClaims()
    {
        // Code for managers to manage claims
        return View();
    }

    [Authorize(Roles = "Lecturer")]
    public IActionResult ViewClaim()
    {
        // Code for lecturers to view their claims
        return View();
    }


}
}
