namespace SkillSignal.ViewModels.Login
{
    using System.Threading.Tasks;
    using System.Windows.Input;

    using SkillSignal.UserServices;


    public class LoginViewModel : ViewModel
    {
        private string _password;
        private string _userName;

        public string UserName
        {
            get { return this._userName; }
            set { this.SetProperty(ref this._userName, value, () => this.UserName); }
        }

        public string Password
        {
            get { return this._password; }
            set { this.SetProperty(ref this._password, value, () => this.Password); }
        }

        public ICommand SignInCommand{ get; set; }

        public LoginViewModel()
        {
            this.SignInCommand = new AsyncRelayCommand(async () => await Task.Factory.StartNew(() =>
            {
                this.LoggedIn = this.UserService.CanAuthenticate(this.UserName, this.Password);
            }), () => true);
        }

        public bool LoggedIn
        {
            get { return this._loggedIn; }
            set { this.SetProperty(ref this._loggedIn, value, () => this.LoggedIn); }
        }

        private bool _loggedIn;

        public IUserService UserService { get; set; }
    }

    public class DesignTimeLoginViewModel : LoginViewModel
    {
    }
}