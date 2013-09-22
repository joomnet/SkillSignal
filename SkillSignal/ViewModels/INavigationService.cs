namespace SkillSignal.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Windows.Documents;

    public interface INavigationService
    {
        Task DisplayPage(IPageViewModel pageViewModel);

        IPageViewModel CurrentPage { set; get; }

        Dictionary<string, Func<IPageViewModel>> GetPagesByNames();

        Dictionary<string, Func<IPageViewModel>> GetDefaultPagesByNames();

        event EventHandler PageChanged;
    }
}