using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication4.Models;
using System.Collections.Generic;
using System.Linq;
using System;

namespace WebApplication4.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        // Static list to store claims in memory
        private static List<Claim> claims = new List<Claim>();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
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

        [HttpPost]
        public IActionResult Claim(Claim claim)
        {
            if (ModelState.IsValid)
            {
                // Generate a unique claim number
                var claimNumber = GenerateClaimNumber();

                // Store the claim number in TempData for use in JavaScript
                TempData["ClaimNumber"] = claimNumber;

                // Simulate saving the claim data (since there's no database)
                // For now, just redirect to the same view to show the claim number
                return RedirectToAction("SubmitClaim");
            }
            return View(claim);
        }

        private string GenerateClaimNumber()
        {
            // Generate a unique claim number
            return "CLM-" + Guid.NewGuid().ToString().Substring(0, 8).ToUpper();
        }



        public IActionResult Verify(string claimNumber)
        {
            var claim = claims.FirstOrDefault(c => c.ClaimNumber == claimNumber);

            if (claim == null)
            {
                return RedirectToAction("Track"); // Or show an error view
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
            var claim = claims.FirstOrDefault(c => c.ClaimNumber == claimNumber);
            if (claim == null)
            {
                ViewBag.Message = "Claim not found.";
                return View();
            }

            return View("Status", claim);
        }

        public IActionResult Manage()
        {
            // Return all claims with a "Pending" status
            var pendingClaims = claims.Where(c => c.Status == "Pending").ToList();
            _logger.LogInformation($"Number of pending claims: {pendingClaims.Count}");
            return View(pendingClaims);
        }
        
        public IActionResult Status()
        {
            return View();
        }

        [HttpPost]
        [HttpPost]
        public IActionResult UpdateClaimStatus(string claimNumber, string status)
        {
            var claim = claims.FirstOrDefault(c => c.ClaimNumber == claimNumber);
            if (claim != null && (status == "Approved" || status == "Declined"))
            {
                claim.Status = status;
            }

            return RedirectToAction("Manage");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
