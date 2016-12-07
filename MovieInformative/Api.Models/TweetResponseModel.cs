using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Models
{
    public class TweetResponseModel
    {
        public string MovieTitle { get; set; }

        public string To { get; set; }

        public bool status { get; set; } = false;
    }
}
