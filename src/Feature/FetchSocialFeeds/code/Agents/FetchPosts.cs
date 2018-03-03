using Sitecore.DependencyInjection;
using Sitecore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hackathon.SocialWall.Feature.FetchSocialFeeds.Agents
{
    public class FetchPosts
    {
        public void Run(Sitecore.Data.Items.Item[] items, Sitecore.Tasks.CommandItem command,
      Sitecore.Tasks.ScheduleItem schedule)
        {
            try
            {

                Log.Info("PostFetching-Scheduler: Event called! at " + DateTime.Now.ToString(), new object());

                ServiceLocator.ServiceProvider.GetService<>();

                List<Models.HashTag> hashtags = HashTags.GetHashTagCollection(SocialNetwork.Instagram);
                Int32 videosFound = 0;

                try
                {
                    foreach (var hashtag in hashtags)
                    {
                        Posts.AddFromInstagram(hashtag.Hashtag, hashtag.InstagramMaxId);
                    }
                }
                catch (Exception ex)
                {
                    writer.WriteToEventLog(source, ex);
                }

            }
            catch (Exception ex)
            {
                Log.Info("Exception:1 Thrown in Form Schedule, Error: " + ex.Message, new object());

            }
            finally
            {

            }
        }
    }
}