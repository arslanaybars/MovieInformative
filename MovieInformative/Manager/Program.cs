using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Api;
using Api.Models;

namespace Manager
{
    class Program
    {
        static void Main(string[] args)
        {
            var omdbApi = new OmdbApi();
            var twitterApi = new TwitterApi();

            DateTime date = DateTime.Now.AddMinutes(-3);
            var sampleFilm = new TwitterApi().GetMentionedTweet(date);

            foreach (var item in sampleFilm)
            {
                var movieTitle = item.MovieTitle.Remove(0, 14);

                var info = omdbApi.GetByQuery(movieTitle);

                var tweet = StringBuilder(info, movieTitle, item);

                //var tweetToPublishParts = string.Format("@{0}* {1}", item.ScreenName, tweet).Split('*');
                var tweetToPublishParts = string.Format("* {0}" , tweet).Split('*');

                StringBuilder tweetToPublish = new StringBuilder();
                for (int i = 0; i < tweetToPublishParts.Length; i++)
                {
                    tweetToPublish.Append(tweetToPublishParts[i]);
                    if (tweetToPublish.Length > 140 - item.ScreenName.Length)
                    {
                        tweetToPublish.Replace(tweetToPublishParts[i], string.Empty);
                        if (item.TweetId.HasValue)
                        {
                            var send = twitterApi.SendTweetInReply($"@{item.ScreenName} {tweetToPublish}", item.TweetId.Value);
                        }
                        i = i - 1;
                        tweetToPublish.Clear();
                    }

                    if (i + 1 >= tweetToPublishParts.Length)
                    {
                        if (item.TweetId.HasValue)
                        {
                            var send = twitterApi.SendTweetInReply($"@{item.ScreenName} {tweetToPublish}", item.TweetId.Value);
                        }
                    }
                }

             

            }

            Console.ReadKey();

        }

        private static StringBuilder StringBuilder(OmdbModel info, string movieTitle, TweetResponseModel item)
        {
            var genres = info.Genre.Replace(" ", string.Empty).Split(',');

            var tweet = new StringBuilder();
            tweet.Append("#" + movieTitle.Replace(" ", string.Empty));
            tweet.Append("* Year : " + info.Year);
            tweet.Append("* ImdbRating : " + info.imdbRating);
            if (genres != null && genres.Any())
            {
                tweet.Append("* Genres :");
                foreach (var genre in genres)
                {
                    tweet.Append(" #" + genre);
                }
            }
            tweet.Append("* ImdbLink : " + "imdb.com/title/" + info.imdbId);

            string plot = "*Summary : " + info.Plot;
            if (info.Plot.Length > 126 - item.ScreenName.Length)
            {
                plot = "* Plot : " + info.Plot.Substring(0, 126 - item.ScreenName.Length) + "..";
            }
            tweet.Append(plot);
            return tweet;
        }
    }
}
