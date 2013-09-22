using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using SkillSignal.Domain;

namespace SkillSignal.ViewModels.Users
{
    public class UserAccountViewModel: PageViewModel
    {
        readonly INavigationService _navigationService;

        public event EventHandler AccountDeleted;

        public string ID { get; private set; }

        string _firstName;

        string _lastName;

        bool isEdittable;

        bool _isDeleted;

        AccessLevel _selectedAccessLevel;

        public bool IsDeleted
        {
            get
            {
                return _isDeleted; 
            } 
            set
            {
                this.SetProperty(ref _isDeleted, value, () => IsDeleted);
            }
        }

        public string FirstName
        {
            get { return this._firstName; }
            set { this.SetProperty(ref this._firstName, value, () => this.FirstName); }
        }

        public string LastName
        {
            get { return this._lastName; }
            set { this.SetProperty(ref this._lastName, value, () => this.LastName); }
        }

        public AccessLevel[] AccessLevels
        {
            get
            {
                return Enum.GetValues(typeof(AccessLevel)).Cast<AccessLevel>().ToArray();
            }
        }

        public bool IsActive { get; private set; }

        public AsyncRelayCommand Edit { get; private set; }

        public AsyncRelayCommand Delete { set; get; }

        public bool IsEdittable
        {
            get
            {
                return this.isEdittable;
            }
            set
            {
                this.SetProperty(ref this.isEdittable, value, () => this.IsEdittable);
            }
        }

        public AccessLevel SelectedAccessLevel
        {
            get
            {
                return _selectedAccessLevel;
            } 
            set
            {
                this.SetProperty(ref _selectedAccessLevel, value, () => SelectedAccessLevel);
            }
        }

        public UserAccountViewModel(string id, INavigationService navigationService, bool isActive)
            : base(navigationService)
        {
            this._navigationService = navigationService;
            this.ID = id;
            this.IsActive = isActive;
            this.Edit = new AsyncRelayCommand(async () => IsEdittable = true, () => true);
            this.Delete = new AsyncRelayCommand(async () => IsDeleted = true, () => true);
        }
    }
}