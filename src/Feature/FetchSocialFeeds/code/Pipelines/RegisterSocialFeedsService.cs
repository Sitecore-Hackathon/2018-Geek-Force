using Hackathon.SocialWall.Feature.FetchSocialFeeds.Repositories;
using Hackathon.SocialWall.Feature.FetchSocialFeeds.Services;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hackathon.SocialWall.Feature.FetchSocialFeeds.Pipelines
{
    public class RegisterSocialFeedsService : IServicesConfigurator
    {
        public void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<ISocialFeedsService, SocialFeedsService>();
            serviceCollection.AddTransient<ISocialFeedsRepository,SocialFeedsRepository>();

        } //: IServicesConfigurator
          //{
          //    public void Configure(IServiceCollection serviceCollection)
          //    {
          //        serviceCollection.AddTransient<ISocialFeedsService, SocialFeedsService>();
          //        serviceCollection.AddTransient<ISocialFeedsRepository,SocialFeedsRepository>();

        //    }
    }
}