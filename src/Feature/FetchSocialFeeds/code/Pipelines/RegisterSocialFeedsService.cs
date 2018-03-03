using Hackathon.SocialWall.Feature.FetchSocialFeeds.Repositories;
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
            serviceCollection.AddTransient<ISocialFeedsRepository,SocialFeedsRepository>();
            
        }
    }
}