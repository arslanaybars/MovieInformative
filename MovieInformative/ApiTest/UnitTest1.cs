using System;
using Api;
using Api.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ApiTEst
{
    [TestClass]
    public class ApiTest
    {
        [TestMethod]
        public void OmdbApi_GetByTitle()
        {
            var sampleFilm = new OmdbApi().GetByQuery("Amélie");
            var imdbRating = sampleFilm.imdbRating;
        }


        [TestMethod]
        public void SendTweet()
        {
            var sampleFilm = new TwitterApi().SendTweetInReply("Amélie");
            var imdbRating = "";
        }

        [TestMethod]
        public void GetMentionedTweet()
        {
            DateTime date = DateTime.Now.AddMinutes(-45);
            var sampleFilm = new TwitterApi().GetMentionedTweet(date);
            
        }
    }
}
