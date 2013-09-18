using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using SkillSignal.Common;

namespace SkillSignal.ViewModels
{

    public class ViewModel : INotifyPropertyChanged
    {
        protected INavigationService ViewNavigationService { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void OnPropertyChanged<T>(Expression<Func<T>> property)
        {
            OnPropertyChanged(property.GetProperty().Name);
        }

        protected bool SetProperty<T>(ref T member, T value, Expression<Func<T>> property)
        {
            bool changed = !EqualityComparer<T>.Default.Equals(member, value);

            if (changed)
            {
                member = value;
                OnPropertyChanged(property.Name);
            }
            return changed;
        }
    }
}