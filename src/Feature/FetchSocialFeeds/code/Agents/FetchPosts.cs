using Hackathon.SocialWall.Feature.FetchSocialFeeds.Models;
using Hackathon.SocialWall.Feature.FetchSocialFeeds.Repositories;
using Sitecore.DependencyInjection;
using Sitecore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using TweetSharp;

namespace Hackathon.SocialWall.Feature.FetchSocialFeeds.Agents
{
    public class FetchPosts
    {
        public void Run(Sitecore.Data.Items.Item[] items, Sitecore.Tasks.CommandItem command,
      Sitecore.Tasks.ScheduleItem schedule)
        {
            try
            {
                SocialFeedsRepository socialFeedRepo = (SocialFeedsRepository)ServiceLocator.ServiceProvider.GetService(typeof(ISocialFeedsRepository));
                IEnumerable<Models.HashTag> hashtags = socialFeedRepo.GetHashTags();
                TwitterConfiguration twitterConfig = socialFeedRepo.GeTwitterConfiguration();

                foreach (var hashtag in hashtags)
                {
                    AddFromTwitter(hashtag.Hashtag, twitterConfig, socialFeedRepo);
                }
                Log.Info("PostFetching-Scheduler: Event called! at " + DateTime.Now.ToString(), new object());

            }
            catch (Exception ex)
            {
                Log.Info("Exception Thrown in Form Feed Fetch Schedule, Error: " + ex.Message, new object());

            }
        }


        private void AddFromTwitter(string hashTag, TwitterConfiguration twitterConfig, SocialFeedsRepository socialFeedRepo)
        {
            bool isPreviousPost = true;

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
                //foreach (var item in results.Statuses)
                {
                    if (TwitterPostType(item) == PostType.Text)
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
                        if (post.PostDateCreated >= Settings.PostDateLimit)
                        {
                            posts.Add(post);
                        }
                    }
                });
            }

            if (prevResults != null && prevResults.Statuses != null)
            {
                Parallel.ForEach(prevResults.Statuses, parallerOptions, item =>
                //foreach (var item in results.Statuses)
                {
                    if (TwitterPostType(item) == PostType.Text)
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
                        if (post.PostDateCreated >= Settings.PostDateLimit)
                        {
                            posts.Add(post);
                        }
                        else
                        { isPreviousPost = false; }
                    }
                });
            }

            if (posts.Count > 0)
            {
                Add(posts);
                if (!isPreviousPost)
                {
                    HashTags.Update(hashTag);
                }
            }
            posts.Clear();
        }
    }
}