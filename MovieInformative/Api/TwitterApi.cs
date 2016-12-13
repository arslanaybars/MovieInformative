using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Api.Models;
using Newtonsoft.Json;
using Tweetinvi;
using Tweetinvi.Logic;
using Tweet = Tweetinvi.Tweet;
using User = Tweetinvi.User;

namespace Api
{
    public class TwitterApi
    {
        public TwitterApi()
        {
            Auth.SetUserCredentials("1gpahx05eubCNpLfcBakVctXB", "LkXhlKjXslWzMoF5EsbY59RKyA4M1Pxts0r8m4yWl6hHB9hTnK",
                "806602434986516480-THb7IiUGFPWUtoxw62AfkHA5OKsjK90", "vNpfBnVntMP6IXDanVzN6SJDK9tHsfc9l1Nh0IiYFlMHC");

            //var userTwitterInfos = JsonConvert.SerializeObject(user);

        }

        public List<TweetResponseModel> GetMentionedTweet(DateTime sch)
        {
            var user = User.GetAuthenticatedUser();

            List<TweetResponseModel> response = new List<TweetResponseModel>();


            var mentionedTweets = Timeline.GetMentionsTimeline().Where(t => t.CreatedAt > sch).ToList();

            foreach (var tweet in mentionedTweets)
            {
                tweet.Text.Replace(',', '.');

                if (tweet.Text.Contains('.'))
                {
                    //var t = tweet.Text.Trim().Replace('@' + user.ScreenName, string.Empty);
                    var title = tweet.Text.Trim().Split('.')[0];

                    response.Add(new TweetResponseModel
                    {
                        MovieTitle = title,
                        TweetId = tweet.Id,
                        Status = true,
                        ScreenName = tweet.CreatedBy.ScreenName
                    });

                }
            }

            return response;
        }

        public bool SendTweet(string tweet)
        {
            var t = Tweet.PublishTweet(tweet);

            return true;
        }

        public bool SendTweetInReply(string tweet, long id)
        {

            var t = Tweet.PublishTweetInReplyTo(tweet, id);
            // todo t'nin durumuna göre döndür
            return true;
        }

        
    }
}
