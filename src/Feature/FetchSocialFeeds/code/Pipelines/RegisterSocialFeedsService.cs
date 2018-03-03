using Hackathon.SocialWall.Feature.FetchSocialFeeds.Repositories;
using Hackathon.SocialWall.Feature.FetchSocialFeeds.Services;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.DependencyInjection;
using Sitecore.XA.Foundation.IOC.Pipelines.IOC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hackathon.SocialWall.Feature.FetchSocialFeeds.Pipelines
{
    public class RegisterSocialFeedsService : IocProcessor
    {
        public override void Process(IocArgs args)
        {
            args.ServiceCollection.AddTransient<ISocialFeedsService, SocialFeedsService>();
            args.ServiceCollection.AddTransient<ISocialFeedsRepository,SocialFeedsRepository>();

        } //: IServicesConfigurator
          //{
          //    public void Configure(IServiceCollection serviceCollection)
          //    {
          //        serviceCollection.AddTransient<ISocialFeedsService, SocialFeedsService>();
          //        serviceCollection.AddTransient<ISocialFeedsRepository,SocialFeedsRepository>();

        //    }
    }
}