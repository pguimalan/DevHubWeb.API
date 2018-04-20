using System;
using System.Collections.Generic;

namespace DevHubWeb.Data.Entities
{
    public partial class Photos
    {
        public int PhotoId { get; set; }
        public int ReferenceId { get; set; }
        public bool IsMain { get; set; }
        public string PhotoUrl { get; set; }
        public string PublicId { get; set; }
    }
}
