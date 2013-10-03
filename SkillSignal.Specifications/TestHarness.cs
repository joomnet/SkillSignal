using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    using System.Threading;
    using Machine.Specifications;
    using SkillSignal.Domain;
    using SkillSignal.Repository;
namespace SkillSignal.Specifications
{

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
                //Task t1 = new Task(() =>
                //    {
                //        Thread.Sleep(10);
                //        Console.WriteLine("t1 done");
                //    });
                //Task t2 = new Task(() =>
                //{
                //    Thread.Sleep(5);
                //    Console.WriteLine("t2 done");
                //});
                //AutoResetEvent waithandle3 = new AutoResetEvent(false);
                //Task t3 = new Task(() =>
                //    {
                //    var ctr = 0;
                   
                //        while (ctr < 5)
                //        {
                //            ctr++;
                //            Thread.Sleep(2);
                //        }
                //        waithandle3.Set();
                //    Console.WriteLine("t3 done");
                //});
                //t1.Start();
                //t2.Start();
                //t3.Start();
                //waithandle3.WaitOne();

                //Task.WhenAll(t1, t2, t3).ContinueWith(x =>
                //    {
                //        Console.WriteLine("All done written from new task");
                //    });
                
            };

        It should_load_3_users_into_the_database = () => dataAccessContext.Users.Count().ShouldEqual(3);  

        static DataAccessContext dataAccessContext;
    }
}
