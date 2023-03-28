using DoToo.Repositories;
using DoToo.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace DoToo.ViewModels
{
    public class MainViewModel : ViewModel
    {
        private readonly TodoItemRepository repository;

        public MainViewModel(TodoItemRepository repository)
        {
            this.repository = repository;
            Task.Run( async () => await LoadData() );
        }

        private async Task LoadData()
        { 

        }

        // Evento de clique no botão para adicionar item
        public ICommand AddItem => new Command( async () => {

            var itemView = Resolver.Resolve<ItemView>();
            await Navigation.PushAsync( itemView );

        } );
    }
}
