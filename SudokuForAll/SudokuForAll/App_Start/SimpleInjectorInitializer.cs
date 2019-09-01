[assembly: WebActivator.PostApplicationStartMethod(typeof(SudokuForAll.App_Start.SimpleInjectorInitializer), "Initialize")]

namespace SudokuForAll.App_Start
{
    using System.Reflection;
    using System.Web.Mvc;

    using SimpleInjector;
    using SimpleInjector.Integration.Web;
    using SimpleInjector.Integration.Web.Mvc;
    using SudokuForAll.Engine;
    using SudokuForAll.Models;

    public static class SimpleInjectorInitializer
    {
        /// <summary>Initialize the container and register it as MVC3 Dependency Resolver.</summary>
        public static void Initialize()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();
            
            InitializeContainer(container);

            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());
            
            container.Verify();
            
            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }
     
        private static void InitializeContainer(Container container)
        {
            container.Register<SudokuContext, SudokuContext>(Lifestyle.Scoped);
            container.Register<IEngineDb, EngineDb>(Lifestyle.Scoped);
            container.Register<IEngineProyect, EngineProyect>(Lifestyle.Scoped);
            container.Register<IEngineNotificacion, EngineNotificacion>(Lifestyle.Scoped);
            container.Register<IEnginePaypal, EnginePaypal>(Lifestyle.Scoped);
        }

        //1.Instalar SimpleInjector.MVC3
        //2.Configurar la clase  SimpleInjectorInitializer  dentro del metodo  InitializeContainer
    }
}