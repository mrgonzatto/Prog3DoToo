using Autofac;
using System.Linq;
using System.Reflection;
using Xamarin.Forms;
using DoToo.Views;
using DoToo.Repositories;
using DoToo.ViewModels;

namespace DoToo
{
    public abstract class Bootstrapper
    {

        protected ContainerBuilder ContainerBuilder 
        { get; private set; }

        protected Bootstrapper()
        {
            Initialize();
            FinishInitialization();
        }

        protected virtual void Initialize()
        { 
            var currentAssembly = Assembly.GetExecutingAssembly();
            ContainerBuilder = new ContainerBuilder();

            var assemblyList =
                currentAssembly.DefinedTypes
                .Where( e => e.IsSubclassOf(typeof(Page)) || 
                             e.IsSubclassOf(typeof(ViewModel)) );

            foreach ( var type in assemblyList )
            {
                ContainerBuilder.RegisterType( type.AsType() );
            }

            ContainerBuilder.RegisterType<TodoItemRepository>()
                .SingleInstance();
            //Esta operação será executada para todo e qualquer 
            //classe de objeto que seja necessária a inclusão 
            //para consumo via injeção de depêndencia

        }

        private void FinishInitialization()
        { 
            var container = ContainerBuilder.Build();
            Resolver.Initialize( container );
        }
    }
}
