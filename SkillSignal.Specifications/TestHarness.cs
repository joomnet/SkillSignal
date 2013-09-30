using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillSignal.Specifications
{
    using Machine.Specifications;

    using SkillSignal.Domain;
    using SkillSignal.Repository;

    public class TestHarness
    {
        Establish context = () =>
            {
               dataAccessContext = new DataAccessContext();
                dataAccessContext.Users.ToList().ForEach(x => dataAccessContext.Users.Remove(x));
                dataAccessContext.SaveChanges();
            };

        Because of = () =>
            {
                dataAccessContext.Users.Add(
                    new UserAccount
                        {
                            AccessLevel = AccessLevel.Admin,
                            Id = 1,
                            IsActive = true,
                            FirstName = "Wumi",
                            LastName = "Meduoye"
                        });

                dataAccessContext.Users.Add(
                    new UserAccount
                        {
                            AccessLevel = AccessLevel.Admin,
                            Id = 2,
                            IsActive = true,
                            FirstName = "Joshua",
                            LastName = "Meduoye"
                        });

                dataAccessContext.Users.Add(
                    new UserAccount
                        {
                            AccessLevel = AccessLevel.Admin,
                            Id = 3,
                            IsActive = false,
                            FirstName = "Segun",
                            LastName = "Meduoye"
                        });

                dataAccessContext.SaveChanges();

            };

        It should_load_3_users_into_the_database = () => dataAccessContext.Users.Count().ShouldEqual(3);  

        static DataAccessContext dataAccessContext;
    }
}
