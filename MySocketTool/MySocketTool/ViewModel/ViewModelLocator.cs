using Autofac;
using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;

namespace MySocketTool.ViewModel
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<MyServerViewModel>();
            //ContainerBuilder builder = new ContainerBuilder();
            //builder.RegisterType<DebugService>().As<IDebugService>().SingleInstance();

        }

        public MainViewModel Main => ServiceLocator.Current.GetInstance<MainViewModel>();
        public MyServerViewModel MyServer => ServiceLocator.Current.GetInstance<MyServerViewModel>();
        //ÈÝÆ÷½Ó¿Ú
        //private static IContainer Container { get; set; }
        //public MainViewModel Main => new MainViewModel(Container.BeginLifetimeScope().Resolve<MainViewModel>());
        public static void Cleanup()
        {
        }
    }
}