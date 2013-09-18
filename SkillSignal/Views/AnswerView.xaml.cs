using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SkillSignal.Views
{
    /// <summary>
    /// Interaction logic for QuestionView.xaml
    /// </summary>
    public partial class AnswerView : UserControl
    {
        public static readonly DependencyProperty IndexProperty = DependencyProperty.Register("Index", typeof (int), typeof (AnswerView), new PropertyMetadata(default(int)));

        public AnswerView()
        {
            InitializeComponent();
        }

        public int Index
        {
            get { return (int) GetValue(IndexProperty); }
            set { SetValue(IndexProperty, value); }
        }
    }
}
