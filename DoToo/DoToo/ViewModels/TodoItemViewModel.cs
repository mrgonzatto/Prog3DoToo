using DoToo.Models;
using System;
using System.Windows.Input;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Text;

namespace DoToo.ViewModels
{
    public class TodoItemViewModel : ViewModel
    {
        public TodoItem Item { get; private set; }

        public TodoItemViewModel(TodoItem item) => Item = item;

        public event EventHandler ItemStatusChanged;

        public string StatusText => 
            (Item.Completed ? "Reativar" : "Completo");

        /* A instrução acima é a mesma coisa que esta
         * Só que está com notação funcional => (LAMBDA)
        public string StatusText()
        {
            if (Item.Completed)
                return "Reativar";
            else
                return "Completo";
        }
        */

        public ICommand ToggleCompleted => new Command(
            (arg) => { 
                Item.Completed = !Item.Completed;
                ItemStatusChanged?.Invoke(this, new EventArgs());
            }
        );
        
    }
}
