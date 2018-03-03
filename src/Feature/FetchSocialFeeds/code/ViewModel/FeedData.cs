using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hackathon.SocialWall.Feature.FetchSocialFeeds.ViewModel
{
    public class FeedData
    {
        public int instaVdoPgNo { get; set; }
        public int instaImgsPgNo { get; set; }
        public int twtrImgsPgNo { get; set; }
        public int twtrTwtsPgNo { get; set; }
        public string LimitRecordMonth
        {
            get;
            set;
        }

        List<RedundantPost> _instaVdos;
        public List<RedundantPost> InstaVdos
        {
            get { return _instaVdos; }
            set { _instaVdos = value; }
        }

        List<RedundantPost> _instaImages;
        public List<RedundantPost> InstaImages
        {
            get { return _instaImages; }
            set { _instaImages = value; }
        }

        List<RedundantPost> _twittrImages;
        public List<RedundantPost> TwittrImages
        {
            get { return _twittrImages; }
            set { _twittrImages = value; }
        }

        List<RedundantPost> _twittertweets;
        public List<RedundantPost> twitterTweets
        {
            get { return _twittertweets; }
            set { _twittertweets = value; }

        }

        public FeedData(List<RedundantPost> instaVdos,
            List<RedundantPost> instaImages,
            List<RedundantPost> twittrImages,
            List<RedundantPost> twitterTweets,
            int _vipTwtrPgNo,
            int _vipInstaPgNo,
            int _instaVdoPgNo,
            int _instaImgsPgNo,
            int _twtrImgsPgNo,
            int _twtrTwtsPgNo)
        {
            _instaVdos = instaVdos;
            _instaImages = instaImages;
            _twittrImages = twittrImages;
            _twittertweets = twitterTweets;
            instaVdoPgNo = _instaVdoPgNo;
            instaImgsPgNo = _instaImgsPgNo;
            twtrImgsPgNo = _twtrImgsPgNo; ;
            twtrTwtsPgNo = _twtrTwtsPgNo;
            //LimitRecordMonth = Settings.PostDateLimit.Month.ToString();
        }
    }
}