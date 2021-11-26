using GoTSkillZ.Models.Membership.Data;
using GoTSkillZ.Models.Membership.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;

namespace GoTSkillZ.Models.Membership.Providers
{
    public class UserProvider : IUserProvider
    {
        private string _connectionString
        {
            get
            {
                var entityBuilder = new EntityConnectionStringBuilder
                {
                    Provider = "System.Data.SqlClient",
                    Metadata = @"res://*/Data.UserModel.csdl|res://*/Data.UserModel.ssdl|res://*/Data.UserModel.msl",
                    ProviderConnectionString =
                        ConfigurationManager.ConnectionStrings["GoTSkillZConnectionString"].ConnectionString
                };

                return entityBuilder.ToString();
            }
        }

        public IEnumerable<Users> GetAll()
        {
            using (var context = new GoTSkillZUserEntities(_connectionString))
            {
                return context.Users.AsEnumerable().ToList();
            }
        }

        public IEnumerable<Users> FindBy(Expression<Func<Users, bool>> predicate)
        {
            using (var context = new GoTSkillZUserEntities(_connectionString))
            {
                return context.Users.Where(predicate).ToList();
            }
        }

        public Users Get(int entityId)
        {
            using (var context = new GoTSkillZUserEntities(_connectionString))
            {
                return context.Users.FirstOrDefault(x => x.UserId == entityId);
            }
        }

        public Users Add(Users user)
        {
            try
            {
                if (user == null) return null;

                using (var context = new GoTSkillZUserEntities(_connectionString))
                {
                    context.Users.Add(user);
                    context.SaveChanges();
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}",
                                                validationError.PropertyName,
                                                validationError.ErrorMessage);
                    }
                }
            }


            return user;
        }

        public Users Update(Users user)
        {
            if (user == null) return null;

            using (var context = new GoTSkillZUserEntities(_connectionString))
            {
                var existingUser = context.Users.FirstOrDefault(x => x.UserId == user.UserId);

                //TODO:This can be made smarter with context attachments
                if (existingUser == null) return user;

                existingUser.GooglePublicId = user.GooglePublicId;
                existingUser.GoogleToken = user.GoogleToken;
                existingUser.FirstName = user.FirstName;
                existingUser.LastName = user.LastName;
                existingUser.Email = user.Email;
                existingUser.TelNo = user.TelNo;
                existingUser.Age = user.Age;
                existingUser.DOB = user.DOB;
                existingUser.Country = user.Country;
                existingUser.State = user.State;
                existingUser.City = user.City;
                existingUser.PinCode = user.PinCode;
                existingUser.Address = user.Address;
                existingUser.ShowPersonalInfo = user.ShowPersonalInfo;
                existingUser.IsActive = user.IsActive;
                existingUser.IsDeleted = user.IsDeleted;
                existingUser.CreatedDate = user.CreatedDate;
                existingUser.ModifiedDate = user.ModifiedDate;
                existingUser.IsRegistered = user.IsRegistered;
                existingUser.MaritalStatus = user.MaritalStatus;
                existingUser.Gender = user.Gender;
                existingUser.Occupation = user.Occupation;
                existingUser.GoogleOAuthToken = user.GoogleOAuthToken;


                context.SaveChanges();
            }

            return user;
        }

        public void Delete(int entityId)
        {
            if (entityId < 1) return;

            using (var context = new GoTSkillZUserEntities(_connectionString))
            {
                var existingUser = context.Users.FirstOrDefault(x => x.UserId == entityId);

                if (existingUser == null) return;

                existingUser.IsDeleted = true;
                existingUser.IsActive = false;
                context.SaveChanges();
            }
        }
    }
}
