using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkillSignal.ViewModels
{
    public class ProjectDashBoardViewModel : PageViewModel
    {
        readonly Domain.Project project;

        public ProjectDashBoardViewModel(INavigationService viewNavigationService, Domain.Project project) : base(viewNavigationService)
        {
            this.project = project;
            this.Title = project.Name;
        }
    }
}
