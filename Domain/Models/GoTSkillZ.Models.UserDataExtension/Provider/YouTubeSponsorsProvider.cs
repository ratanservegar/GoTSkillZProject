using GoTSkillZ.Models.UserDataExtension.Data;
using GoTSkillZ.Models.UserDataExtension.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Core.EntityClient;
using System.Linq;
using System.Linq.Expressions;

namespace GoTSkillZ.Models.UserDataExtension.Provider
{
    public class YouTubeSponsorsProvider : IYouTubeSponsorsProvider
    {
        private string _connectionString
        {
            get
            {
                var entityBuilder = new EntityConnectionStringBuilder
                {
                    Provider = "System.Data.SqlClient",
                    Metadata = @"res://*/Data.YouTubeSponsorsModel.csdl|res://*/Data.YouTubeSponsorsModel.ssdl|res://*/Data.YouTubeSponsorsModel.msl",
                    ProviderConnectionString = ConfigurationManager.ConnectionStrings["GoTSkillZConnectionString"].ConnectionString
                };

                return entityBuilder.ToString();
            }
        }


        public IEnumerable<YouTubeSponsors> GetAll()
        {
            using (var context = new GoTSkillZYouTubeSponsorsEntities(_connectionString))
            {
                return context.YouTubeSponsors.ToList();
            }
        }

        public IEnumerable<YouTubeSponsors> FindBy(Expression<Func<YouTubeSponsors, bool>> predicate)
        {
            using (var context = new GoTSkillZYouTubeSponsorsEntities(_connectionString))
            {
                return context.YouTubeSponsors.Where(predicate).ToList();
            }
        }

        public YouTubeSponsors Get(int entityId)
        {
            using (var context = new GoTSkillZYouTubeSponsorsEntities(_connectionString))
            {
                return context.YouTubeSponsors.FirstOrDefault(x => x.Id == entityId);
            }
        }

        public YouTubeSponsors Add(YouTubeSponsors sponsorObj)
        {
            if (sponsorObj == null) return null;

            using (var context = new GoTSkillZYouTubeSponsorsEntities(_connectionString))
            {
                context.YouTubeSponsors.Add(sponsorObj);
                context.SaveChanges();
            }

            return sponsorObj;
        }

        public YouTubeSponsors Update(YouTubeSponsors sponsorObj)
        {
            if (sponsorObj == null) return null;

            using (var context = new GoTSkillZYouTubeSponsorsEntities(_connectionString))
            {
                var existingSponsor = context.YouTubeSponsors.FirstOrDefault(x => x.Id == sponsorObj.Id);

                if (existingSponsor == null) return sponsorObj;

                existingSponsor.IsSponsor = sponsorObj.IsSponsor;
                existingSponsor.SponsorStartDate = sponsorObj.SponsorStartDate;
                existingSponsor.SponsorEndDate = sponsorObj.SponsorEndDate;
                existingSponsor.TotalMonths = sponsorObj.TotalMonths;
                context.SaveChanges();
            }

            return sponsorObj;
        }

        public void Delete(int entityId)
        {
            throw new NotImplementedException();
        }
    }
}
