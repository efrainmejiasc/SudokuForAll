[assembly: WebActivator.PostApplicationStartMethod(typeof(SudokuDeTodos.App_Start.SimpleInjectorInitializer), "Initialize")]

namespace SudokuDeTodos.App_Start
{
    using System.Reflection;
    using System.Web.Mvc;

    using SimpleInjector;
    using SimpleInjector.Integration.Web;
    using SimpleInjector.Integration.Web.Mvc;
    using SudokuDeTodos.Engine;
    using SudokuDeTodos.Engine.Interfaces;

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
            container.Register<IEngineDb, EngineDb>(Lifestyle.Transient);
            container.Register<IEngineProyect, EngineProyect>(Lifestyle.Transient);
            container.Register<IEngineGameProcess, EngineGameProcess>(Lifestyle.Transient);
            container.Register<IEngineNotificacion, EngineNotificacion>(Lifestyle.Transient);
            container.Register<EngineContext, EngineContext>(Lifestyle.Scoped);
            container.Register<IEngineGameChild, EngineGameChild>(Lifestyle.Transient);
        }
    }
}