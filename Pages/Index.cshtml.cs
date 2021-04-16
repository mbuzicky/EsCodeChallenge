using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace EsCodeChallenge.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        [BindProperty]
        [RegularExpression(@"^(\d+\.)?(\d+\.)?(\d+)$", ErrorMessage = "Version must be in the format of [major version].[minor version].[patch]. For Example - 2, 1.5, or 2.12.4.")]
        [StringLength(32, ErrorMessage = "Version cannot be longer than 32 characters.")]
        [Required]
        public string Version { get; set; }
        [BindProperty]
        public IEnumerable<Software> Results { get; set; }

        public void OnGet()
        {
            this.Version = "";
            this.Results = SoftwareManager.GetAllSoftware();
        }

        public void OnPost()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (!this.Version.Contains("."))
                        this.Version += ".0"; // System.Version does not treat "2" as a valid version so convert to equivalent "2.0"
                    Version ver = new Version(this.Version);

                    var software = SoftwareManager.GetAllSoftware();
                    this.Results = software.Where(s => s.VersionObj > ver).OrderBy(o => o.VersionObj).ToList();
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("Version", e.Message);
                }
            }
        }
    }
}
