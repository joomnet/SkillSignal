namespace SkillSignal.Domain
{
    using System.Runtime.Serialization;

    [DataContract]
    public class UserAccount
    {
        //protected bool Equals(UserAccount other)
        //{
        //    return this.IsActive.Equals(other.IsActive) 
        //        && string.Equals(this.Id, other.Id) 
        //        && string.Equals(this.FirstName, other.FirstName) 
        //        && string.Equals(this.LastName, other.LastName) 
        //        && this.AccessLevel == other.AccessLevel;
        //}

        //public override bool Equals(object obj)
        //{
        //    if (ReferenceEquals(null, obj))
        //    {
        //        return false;
        //    }
        //    if (ReferenceEquals(this, obj))
        //    {
        //        return true;
        //    }
        //    if (obj.GetType() != this.GetType())
        //    {
        //        return false;
        //    }
        //    return Equals((UserAccount)obj);
        //}

        //public override int GetHashCode()
        //{
        //    unchecked
        //    {
        //        int hashCode = this.IsActive.GetHashCode();
        //        hashCode = (hashCode * 397) ^ (this.Id != null ? this.Id.GetHashCode() : 0);
        //        hashCode = (hashCode * 397) ^ (this.FirstName != null ? this.FirstName.GetHashCode() : 0);
        //        hashCode = (hashCode * 397) ^ (this.LastName != null ? this.LastName.GetHashCode() : 0);
        //        hashCode = (hashCode * 397) ^ (int)this.AccessLevel;
        //        return hashCode;
        //    }
        //}

        //public static bool operator ==(UserAccount left, UserAccount right)
        //{
        //    return Equals(left, right);
        //}

        //public static bool operator !=(UserAccount left, UserAccount right)
        //{
        //    return !Equals(left, right);
        //}

        public UserAccount(string id, string firstName, string lastName, AccessLevel accessLevel)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            AccessLevel = accessLevel;
        }

        public UserAccount()
        {
            
        }

        [DataMember]
        public bool IsActive { get;  set; }

        [DataMember]
        public string Id { get; set; }

        [DataMember]
        public string FirstName { get;  set; }

        [DataMember]
        public string LastName { get;  set; }

        [DataMember]
        public AccessLevel AccessLevel { get;  set; }
    }
}