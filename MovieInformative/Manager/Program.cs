using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Api;

namespace Manager
{
    class Program
    {
        static void Main(string[] args)
        {
            var omdbApi = new OmdbApi();
            var twitterApi = new TwitterApi();

            DateTime date = DateTime.Now.AddMinutes(-30);
            var sampleFilm = new TwitterApi().GetMentionedTweet(date);

            foreach (var item in sampleFilm)
            {
                var info = omdbApi.GetByQuery(item.MovieTitle);

                var tweet = item.To + ", ImdbRating : " + info.imdbRating + ", Summary : " + info.Plot;

                if (tweet.Length > 140)
                {
                    tweet = tweet.Substring(0, 138) + "..";
                }

                var send = twitterApi.SendTweetInReply(tweet);
            }

        }
    }
}
