using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XemYoutubeThoi.Models
{
    public class Video
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Img { get; set; }
        public string Description { get; set; }
        public DateTime? PudDate { get; set; }
    }
}
