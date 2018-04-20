using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DevHubWeb.Domains
{
    public class PhotoForCreationModel
    {
        public IFormFile File { get; set; }
        [Required]
        public int ReferenceID { get; set; }
        public string PhotoURL { get; set; }
        public string PublicID { get; set; }
    }

    public class PhotoForReturnModel
    {
        public int PhotoID { get; set; }
        public int ReferenceID { get; set; }
        public bool IsMain { get; set; }
        public string PhotoURL { get; set; }
        public string PublicID { get; set; }
    }
}
