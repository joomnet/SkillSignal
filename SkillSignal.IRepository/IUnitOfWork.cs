using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SkillSignal.IRepository;

namespace SkillSignal.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        int SaveChanges();
    }

    public interface IDALContext : IUnitOfWork
    {
        IProjectRepository Projects { get; }
        IUserAccountRepository Users { get; }
    }


}
