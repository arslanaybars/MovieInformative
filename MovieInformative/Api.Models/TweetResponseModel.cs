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

        public long? TweetId { get; set; }

        public string ScreenName { get; set; }

        public bool Status { get; set; } = false;
    }
}
