using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using System.Linq;

namespace DoToo.ViewModels
{
    public abstract class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<TodoItemViewModel> Items { get; set; }

        public void RaisePropertyChanged(params string[] propertyNames)
        {
            foreach (var propertyName in propertyNames)
            {
                PropertyChanged?.Invoke( 
                    this, 
                    new PropertyChangedEventArgs(propertyName)
                );
            }
        }

        public INavigation Navigation { get; set; }
    }
}
