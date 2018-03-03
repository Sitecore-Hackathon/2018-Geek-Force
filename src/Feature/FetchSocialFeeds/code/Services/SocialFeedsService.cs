using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using Hackathon.SocialWall.Feature.FetchSocialFeeds.Models;
using Hackathon.SocialWall.Feature.FetchSocialFeeds.Repositories;
using Sitecore.Data;
using TweetSharp;

namespace Hackathon.SocialWall.Feature.FetchSocialFeeds.Services
{
    public class SocialFeedsService : ISocialFeedsService
    {
        private ISocialFeedsRepository _socialFeedsRepository;
        public SocialFeedsService(ISocialFeedsRepository socialFeedsRepository)
        {
            _socialFeedsRepository = socialFeedsRepository;
        }

        public void AddPostsByHashTags()
        {
            IEnumerable<Models.HashTag> hashtags = _socialFeedsRepository.GetHashTags();
            TwitterConfiguration twitterConfig = _socialFeedsRepository.GeTwitterConfiguration();

            foreach (var hashtag in hashtags)
            {
                AddTwitterPosts(hashtag.Hashtag, twitterConfig, _socialFeedsRepository);
            }
        }

        private void AddTwitterPosts(string hashTag, TwitterConfiguration twitterConfig, ISocialFeedsRepository socialFeedRepo)
        {

            // bool isPreviousPost = true;
            int DescriptionLength = 500;

            var service = new TwitterService(twitterConfig.ConsumerKey, twitterConfig.ConsumerSecret);
            service.AuthenticateWith(twitterConfig.Token, twitterConfig.TokenSecret);

            TwitterSearchResult prevResults = null;
            TwitterSearchResult currentResults = null;

            SearchOptions options = new SearchOptions()
            {
                Q = hashTag,
                Count = 100
            };


            long sinceId = socialFeedRepo.GetMaxTwitterPostId(hashTag);
            if (sinceId > 0)
            {
                options.SinceId = sinceId;
                options.Resulttype = TwitterSearchResultType.Recent;
            }
            currentResults = service.Search(options);

            sinceId = socialFeedRepo.GetMinTwitterPostId(hashTag);
            if (sinceId > 0)
            {
                options.SinceId = null;
                options.MaxId = sinceId;

                prevResults = service.Search(options);
            }


            List<Post> posts = new List<Post>();

            var parallerOptions = new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount * 2 };
            if (currentResults != null && currentResults.Statuses != null)
            {
                Parallel.ForEach(currentResults.Statuses, parallerOptions, item =>
                {
                    if (TwitterPostType(item) == PostType.Text && !string.IsNullOrEmpty(item.Id.ToString()))
                    {
                        Post post = new Post();
                        post.PostSource = SocialNetwork.Twitter;
                        post.SocialNetworkPostId = Convert.ToString(item.Id);
                        post.SocialNetworkUserId = item.User.Id;
                        post.SocialNetworkUsername = item.User.ScreenName;
                        post.PostType = TwitterPostType(item);
                        post.PostUrl = TwitterPostUrl(item);
                        post.ThumbnailUrl = TwitterThumbnailUrl(item);
                        post.SocialNetworkUserPictureUrl = item.User.ProfileImageUrlHttps;
                        post.Description = GetTrimmedString(item.Text, DescriptionLength);
                        post.PostDateCreated = item.CreatedDate;
                        post.IsVIPContent = false;
                        post.hashTag = hashTag;
                        post.Status = PostStatus.Pending;
                        //if (post.PostDateCreated >= Settings.PostDateLimit)
                        //{
                           posts.Add(post);
                        //}
                    }
                });
            }

            if (prevResults != null && prevResults.Statuses != null)
            {
                Parallel.ForEach(prevResults.Statuses, parallerOptions, item =>
                {
                    if (TwitterPostType(item) == PostType.Text && !string.IsNullOrEmpty(item.Id.ToString()))
                    {
                        Post post = new Post();
                        post.PostSource = SocialNetwork.Twitter;
                        post.SocialNetworkPostId = Convert.ToString(item.Id);
                        post.SocialNetworkUserId = item.User.Id;
                        post.SocialNetworkUsername = item.User.ScreenName;
                        post.PostType = TwitterPostType(item);
                        post.PostUrl = TwitterPostUrl(item);
                        post.ThumbnailUrl = TwitterThumbnailUrl(item);
                        post.SocialNetworkUserPictureUrl = item.User.ProfileImageUrlHttps;
                        post.Description = GetTrimmedString(item.Text, DescriptionLength);
                        post.PostDateCreated = item.CreatedDate;
                        post.IsVIPContent = false;
                        post.hashTag = hashTag;
                        post.Status = PostStatus.Pending;
                        //if (post.PostDateCreated >= Settings.PostDateLimit)
                        //{
                          posts.Add(post);
                        //}
                        //else
                        //{ isPreviousPost = false; }
                    }
                });
            }

            if (posts.Count > 0)
            {
                socialFeedRepo.SaveFeeds(posts);
                //if (!isPreviousPost)
                //{
                //    socialFeedRepo.UpdateFeed(hashTag);
                //}
            }
            posts.Clear();
        }

        private static string TwitterPostUrl(TwitterStatus item)
        {
            string postUrl = string.Empty;
            if (item.Entities.Media.Count > 0 && PostType.Image == TwitterPostType(item))
            {
                postUrl = item.Entities.Media.First().MediaUrlHttps;
            }
            else if (item.Entities.Urls.Count > 0 && !string.IsNullOrEmpty(GetYouTubeVideoId(item.Entities.Urls.First().ExpandedValue)))
            {
                postUrl = GetYouTubeEmbededVideoUrl((item.Entities.Urls.First().ExpandedValue));
            }
            return postUrl;
        }

        private static string TwitterThumbnailUrl(TwitterStatus item)
        {
            string postUrl = string.Empty;
            if (item.Entities.Media.Count > 0)
            {
                postUrl = item.Entities.Media.First().MediaUrlHttps;
            }
            else if (item.Entities.Urls.Count > 0 && !string.IsNullOrEmpty(GetYouTubeVideoId(item.Entities.Urls.First().ExpandedValue)))
            {
                postUrl = GetYouTubeVideoThumbnailUrl((item.Entities.Urls.First().ExpandedValue));
            }
            return postUrl;
        }

        private static PostType TwitterPostType(TwitterStatus item)
        {
            PostType postType = PostType.Image;
            if (item.Entities.Media.Count > 0 && item.Entities.Media.First().MediaType.ToString().Equals("Photo"))
            {
                postType = PostType.Image;
            }
            else if (item.Entities.Urls.Count > 0 && !string.IsNullOrEmpty(GetYouTubeVideoId(item.Entities.Urls.First().ExpandedValue)))
            {
                postType = PostType.Video;
            }
            else
            {
                postType = PostType.Text;
            }

            return postType;
        }

        private static DateTime TwitterPostDate(TwitterStatus item)
        {
            var sd = item.CreatedDate;
            return sd;
        }

        private static string GetYouTubeEmbededVideoUrl(string url)
        {
            return string.Format("//www.youtube.com/embed/{0}?rel=0", GetYouTubeVideoId(url));
        }

        private static string GetYouTubeVideoThumbnailUrl(string url)
        {
            return string.Format("//img.youtube.com/vi/{0}/0.jpg", GetYouTubeVideoId(url));
        }

        private static string GetTrimmedString(string source, int length)
        {
            return (!string.IsNullOrEmpty(source) && source.Length > length) ? source.ToString().Substring(0, length) : source;
        }

        public static string GetYouTubeVideoId(string url)
        {
            Match regexMatch = Regex.Match(url, "^[^v]+v=(.{11}).*", RegexOptions.IgnoreCase);
            if (regexMatch.Success)
            {
                return regexMatch.Groups[1].Value;
            }
            else
            {
                Match match = Regex.Match(url, @"youtu(?:\.be|be\.com)/(?:(.*)v(/|=)|(.*/)?)([a-zA-Z0-9-_]+)", RegexOptions.IgnoreCase);
                if (match == null)
                    return string.Empty;
                if (match.Groups.Count == 0)
                    return string.Empty;
                return match.Groups[match.Groups.Count - 1].Value;
            }
        }
    }
}
