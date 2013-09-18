namespace SkillSignal.ViewModels
{
    using System.Collections.ObjectModel;

    public class QuestionViewModel : PageViewModel
    {
        public QuestionViewModel(INavigationService navigationService) : base(navigationService)
        {
           
        }

        public string Text { get; protected set; }

        public ObservableCollection<AnswerViewModel> AllAnswers { get; set; }
    }

    public class DesignTimeQuestionViewModel : QuestionViewModel
    {
        public DesignTimeQuestionViewModel()
            : base(null)
        {
            AllAnswers = new ObservableCollection<AnswerViewModel>
                {
                    new AnswerViewModel(null, "Segun", false, true) ,
                    new AnswerViewModel(null, "Wumi", false, true) ,
                    new AnswerViewModel(null, "Joshua", false, true) ,
                    new AnswerViewModel(null, "Rooney", false, true) ,
                };

            Text = "What is my name?";
        }


    }
}