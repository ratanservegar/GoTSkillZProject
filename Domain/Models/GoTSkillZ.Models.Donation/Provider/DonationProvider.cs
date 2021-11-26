using GoTSkillZ.Models.Donation.Data;
using GoTSkillZ.Models.Donation.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Core.EntityClient;
using System.Linq;
using System.Linq.Expressions;

namespace GoTSkillZ.Models.Donation.Provider
{
    public class DonationProvider : IDonationProvider
    {
        private string _connectionString
        {
            get
            {
                var entityBuilder = new EntityConnectionStringBuilder
                {
                    Provider = "System.Data.SqlClient",
                    Metadata =
                        @"res://*/Data.GoTSkillZDonationModel.csdl|res://*/Data.GoTSkillZDonationModel.ssdl|res://*/Data.GoTSkillZDonationModel.msl",
                    ProviderConnectionString = ConfigurationManager.ConnectionStrings["GoTSkillZConnectionString"]
                        .ConnectionString
                };

                return entityBuilder.ToString();
            }
        }

        public IEnumerable<Data.Donation> GetAll()
        {
            using (var context = new GoTSkillZDonationEntities(_connectionString))
            {
                return context.Donations.ToList();
            }
        }

        public IEnumerable<Data.Donation> FindBy(Expression<Func<Data.Donation, bool>> predicate)
        {
            using (var context = new GoTSkillZDonationEntities(_connectionString))
            {
                return context.Donations.Where(predicate).ToList();
            }
        }

        public Data.Donation Get(int entityId)
        {
            using (var context = new GoTSkillZDonationEntities(_connectionString))
            {
                return context.Donations.FirstOrDefault(x => x.Id == entityId);
            }
        }

        public Data.Donation Add(Data.Donation donationObj)
        {
            if (donationObj == null) return null;

            using (var context = new GoTSkillZDonationEntities(_connectionString))
            {
                context.Donations.Add(donationObj);
                context.SaveChanges();
            }

            return donationObj;
        }

        public Data.Donation Update(Data.Donation donationObj)
        {
            if (donationObj == null) return null;

            using (var context = new GoTSkillZDonationEntities(_connectionString))
            {
                var existingDonation = context.Donations.FirstOrDefault(x => x.Id == donationObj.Id);

                if (existingDonation == null) return donationObj;

                existingDonation.UserId = donationObj.UserId;
                existingDonation.Amount = donationObj.Amount;
                existingDonation.DonationTitle = donationObj.DonationTitle;
                existingDonation.DonationType = donationObj.DonationType;
                existingDonation.DonationDescription = donationObj.DonationDescription;
                existingDonation.DonationDate = donationObj.DonationDate;
                existingDonation.CustomImgUrl = donationObj.CustomImgUrl;
                existingDonation.Name = donationObj.Name;
                existingDonation.IsActive = donationObj.IsActive;
                context.SaveChanges();
            }

            return donationObj;
        }

        public void Delete(int entityId)
        {
            throw new NotImplementedException();
        }
    }
}