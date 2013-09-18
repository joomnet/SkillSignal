namespace SkillSignal.Repository
{
    using System;
    using System.Data;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Security.Cryptography;

    using LinqKit;

    using SkillSignal.IRepository;

    public class Repository<TObject> : IRepository<TObject>

        where TObject : class
    {
        protected DataAccessContext Context;
        private bool shareContext = false;

        public Repository()
        {
            this.Context = new DataAccessContext();
        }

        public Repository(DataAccessContext context)
        {
            this.Context = context;
            this.shareContext = true;
        }

        protected DbSet<TObject> DbSet
        {
            get
            {
                return this.Context.Set<TObject>();
            }
        }

        public void Dispose()
        {
            if (this.shareContext && (this.Context != null))
                this.Context.Dispose();
        }

        public virtual IQueryable<TObject> All()
        {
            return this.DbSet.AsQueryable();
        }

        public virtual IQueryable<TObject>
        Filter(Expression<Func<TObject, bool>> predicate)
        {
            return DbSet.AsExpandable().Where(predicate.Compile()).AsQueryable();
        }

        public IQueryable<TObject> Filter<Key>(Expression<Func<TObject, bool>> filter, out int total, int index = 0, int size = 50)
        {
            throw new NotImplementedException();
        }

        public virtual IQueryable<TObject> Filter(Expression<Func<TObject, bool>> filter,
         out int total, int index = 0, int size = 50)
        {
            int skipCount = index * size;
            var _resetSet = filter != null ? this.DbSet.Where(filter).AsQueryable() :
                this.DbSet.AsQueryable();
            _resetSet = skipCount == 0 ? _resetSet.Take(size) :
                _resetSet.Skip(skipCount).Take(size);
            total = _resetSet.Count();
            return _resetSet.AsQueryable();
        }

        public bool Contains(Expression<Func<TObject, bool>> predicate)
        {
            return this.DbSet.Count(predicate) > 0;
        }

        public virtual TObject Find(params object[] keys)
        {
            return this.DbSet.Find(keys);
        }

        public virtual TObject Find(Expression<Func<TObject, bool>> predicate)
        {
            return this.DbSet.FirstOrDefault(predicate);
        }

        public virtual TObject Create(TObject TObject)
        {
            var newEntry = this.DbSet.Add(TObject);
            if (!this.shareContext)
                this.Context.SaveChanges();
            return newEntry;
        }

        void IRepository<TObject>.Delete(TObject t)
        {
            throw new NotImplementedException();
        }

        public virtual int Count
        {
            get
            {
                return this.DbSet.Count();
            }
        }

        public virtual int Delete(TObject TObject)
        {
            this.DbSet.Remove(TObject);
            if (!this.shareContext)
                return this.Context.SaveChanges();
            return 0;
        }

        public virtual int Update(TObject TObject)
        {
            var entry = this.Context.Entry(TObject);
            this.DbSet.Attach(TObject);
            entry.State = EntityState.Modified;
            if (!this.shareContext)
                return this.Context.SaveChanges();
            return 0;
        }

        public virtual int Delete(Expression<Func<TObject, bool>> predicate)
        {
            var objects = this.Filter(predicate);
            foreach (var obj in objects)
                this.DbSet.Remove(obj);
            if (!this.shareContext)
                return this.Context.SaveChanges();
            return 0;
        }
    }
}

//if (firstRun)
//{
//    var users = new EnumerableQuery<TObject>((IEnumerable<TObject>)new List<UserAccount>
//    {
//        new UserAccount(Guid.NewGuid().ToString(), "Segun","Meduoye", AccessLevel.Admin),
//        new UserAccount(Guid.NewGuid().ToString(),"Wumi","Meduoye", AccessLevel.Admin),
//        new UserAccount(Guid.NewGuid().ToString(),"Jishua","Meduoye", AccessLevel.Admin)

//    });


//    {
//        users.Each(user => _context.Users.Add(user as UserAccount));
//        _context.SaveChanges();
//        firstRun = false;
//        List<TObject> list = users.ToList();
//        return list.AsQueryable();
//    }
//}