using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

using SkillSignal.Commands;
using Microsoft.Practices.Unity;
using SkillSignal.Common;
using SkillSignal.DependencyResolution;
using SkillSignal.IBusinessLayer;
using SkillSignal.ServiceClients;

namespace SkillSignal.ViewModels
{

    public class DesignTimeApplicationViewModel : ApplicationViewModel
    {
        private static new readonly INavigationService PageNavigationService = new PageNavigationService(new ViewModelFactory(null));

        public DesignTimeApplicationViewModel()
            : base(PageNavigationService, new UserServiceClient(), new ViewModelFactory(new UnityContainer()))
        {
            this.ApplicationDialog = null;

            this.LeftNavigationButtonCollection = new List<DisplayableNavigationCommand>()
            {
                new DisplayableNavigationCommand("Purchase ledger", () => TaskEx.Run(() => ViewNavigationService.CurrentPage = new PurchaseLedgerViewModel(null)), () => true),
                new DisplayableNavigationCommand("VAT", () => TaskEx.Run(() => ViewNavigationService.CurrentPage = new PurchaseLedgerViewModel(null)), () => true),
                new DisplayableNavigationCommand("Deposits", () => TaskEx.Run(() => ViewNavigationService.CurrentPage = new PurchaseLedgerViewModel(null)), () => true)
            };
        }
    }

    public class ApplicationViewModel : ViewModel, IApplicationViewModel
    {
        readonly IViewModelFactory viewModelFactory;

        private object _applicationDialog;

        private void _GoForward()
        {

        }

        private void _GoBack()
        {
            this.ViewNavigationService.CurrentPage = new QuestionViewModel(this.ViewNavigationService)
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

        public new INavigationService ViewNavigationService { get; set; }

        public List<DisplayableNavigationCommand> LeftNavigationButtonCollection { get; set; }

        public ApplicationViewModel(INavigationService viewNavigationService, IUserService userService, IViewModelFactory viewModelFactory)
        {
            ObjectValidator.IfNullThrowException(viewNavigationService, "viewNavigationService");
            ObjectValidator.IfNullThrowException(userService,  "userService");
            ObjectValidator.IfNullThrowException(viewModelFactory, "viewModelFactory");

            this.viewModelFactory = viewModelFactory;
            this.ViewNavigationService = viewNavigationService;

            this.Back = new RelayCommand(o => this._GoBack(), o => true);
            this.Forward = new RelayCommand(o => this._GoForward(), o => true);
            this.Close = new RelayCommand(o => this._Close(), o => true);

            this.LeftNavigationButtonCollection = new List<DisplayableNavigationCommand>();

            /*All this behaviour needs to be moved to a loaded command*/
            this.ViewNavigationService.CurrentPage = viewModelFactory.Get<StartPageViewModel>();
            ViewNavigationService.GetDefaultPagesByNames().Each(page => this.LeftNavigationButtonCollection.Add(
                                                                    new DisplayableNavigationCommand(
                                                                     page.Key,
                                                                     async () => await TaskEx.Run(() => ViewNavigationService.CurrentPage = page.Value()),
                                                                     () => true)));

            ViewNavigationService.PageChanged += async (sender, args) =>
            {
                if ((ViewNavigationService.CurrentPage as ProjectDashBoardViewModel) != null)
                {
                    await this._LoadNonDefaultPages();
                }
            };
        }

        async Task _LoadNonDefaultPages()
        {
            this.LeftNavigationButtonCollection.Clear();
            this.ViewNavigationService.GetPagesByNames()
                .Each(
                    page =>
                        this.LeftNavigationButtonCollection.Add(
                            new DisplayableNavigationCommand(
                                page.Key,
                                async () => await TaskEx.Run(() => this.ViewNavigationService.CurrentPage = page.Value()),
                                () => true)));
        }

        public Task DisplayPage(IPageViewModel pageViewModel)  
        {
            return TaskEx.Run(() => ViewNavigationService.CurrentPage = pageViewModel);
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