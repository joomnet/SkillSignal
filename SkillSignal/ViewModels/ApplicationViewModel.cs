using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

using SkillSignal.Repository;
using SkillSignal.Commands;
using SkillSignal.ViewModels.Project;
using SkillSignal.ViewModels.Users;

namespace SkillSignal.ViewModels
{
    using SkillSignal.Common;
    using SkillSignal.DependencyResolution;
    using SkillSignal.IBusinessLayer;
    using SkillSignal.ServiceClients;

    public class DesignTimeApplicationViewModel : ApplicationViewModel
    {
        private static readonly INavigationService PageNavigationService = new PageNavigationService(new UserServiceClient(), new ViewModelFactory(null));

        public DesignTimeApplicationViewModel()
            : base(new CreateProjectViewModel(new ProjectServiceClient(), PageNavigationService), PageNavigationService, new UserServiceClient())
        {
            this.ApplicationDialog = null;

            this.LeftNavigationButtonCollection = new List<DisplayableNavigationCommand>()
            {
                new DisplayableNavigationCommand("Purchase ledger", () => TaskEx.Run(() => base.PageNavigationService.CurrentPage = new PurchaseLedgerViewModel(null)), () => true),
                new DisplayableNavigationCommand("VAT", () => TaskEx.Run(() => base.PageNavigationService.CurrentPage = new PurchaseLedgerViewModel(null)), () => true),
                new DisplayableNavigationCommand("Deposits", () => TaskEx.Run(() => base.PageNavigationService.CurrentPage = new PurchaseLedgerViewModel(null)), () => true)
            };
        }
    }

    public class ApplicationViewModel : ViewModel, IApplicationViewModel
    {
        private object _applicationDialog;

        public ProjectViewModel CurrentProject { get; set; }

        private void _GoForward()
        {
            this.PageNavigationService.CurrentPage = new ProjectDashBoardViewModel(this.PageNavigationService)
                {
                    Title = "hello"
                };
        }

        private void _GoBack()
        {
            this.PageNavigationService.CurrentPage = new QuestionViewModel(this.PageNavigationService)
            {
                Title = "hello"
            };
        }

        public ICommand Back { get; set; }
        public ICommand Forward { get; set; }
        public ICommand Close { get; set; }

        public ICommand Resize { get; set; }

        public object ApplicationDialog
        {
            get { return this._applicationDialog; }
            set { this.SetProperty(ref this._applicationDialog, value, () => this.ApplicationDialog); }
        }

        public ICreateProjectViewModel CreateProjectViewModel { get; set; }
        public INavigationService PageNavigationService { get; set; }

        public List<DisplayableNavigationCommand> LeftNavigationButtonCollection { get; set; }

        public ApplicationViewModel(ICreateProjectViewModel createProjectViewModel, INavigationService pageNavigationService, IUserService userService)
        {
            ObjectValidator.IfNullThrowException(createProjectViewModel, "createProjectViewModel");
            ObjectValidator.IfNullThrowException(pageNavigationService, "pageNavigationService");
            ObjectValidator.IfNullThrowException(userService,  "userService");

            this.CreateProjectViewModel = createProjectViewModel;
            this.PageNavigationService = pageNavigationService;
            //this.CurrentProject = new ProjectViewModel(pageNavigationService)
            //{
            //    Title = "My Test Project",
            //    CurrentPage = new ProjectDashBoardViewModel(pageNavigationService),
            //};

            this.Back = new RelayCommand(o => this._GoBack(), o => true);
            this.Forward = new RelayCommand(o => this._GoForward(), o => true);
            this.Close = new RelayCommand(o => this._Close(), o => true);

            this.LeftNavigationButtonCollection = new List<DisplayableNavigationCommand>();


            PageNavigationService.GetPagesByNames().Each(page => this.LeftNavigationButtonCollection.Add(
                                                                    new DisplayableNavigationCommand(
                                                                     page.Key,
                                                                     async () => await TaskEx.Run(() => this.PageNavigationService.CurrentPage = page.Value()),
                                                                     () => true)));
        }

        public Task DisplayPage(IPageViewModel pageViewModel)  
        {
            return TaskEx.Run(() => this.PageNavigationService.CurrentPage = pageViewModel);
        }

        void _Close()
        {
            Application.Current.MainWindow.Close();
        }

        void _HandleUnhandledExceptions()
        {
            Application.Current.DispatcherUnhandledException += (o, e) =>
            {
          
                e.Handled = true;
            };
            TaskScheduler.UnobservedTaskException += (o, e) =>
            {
            
                e.SetObserved();
            };
            AppDomain.CurrentDomain.UnhandledException += (o, e) =>
            {
          
                Thread.Sleep(99999999);
            };
        }
    }
}