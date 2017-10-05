using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Unity;
using System;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;

namespace App1
{
    sealed partial class App : MvvmAppBase
    {
        private readonly IUnityContainer _container = new UnityContainer();

        protected override Task OnLaunchApplicationAsync(LaunchActivatedEventArgs args)
        {
            // Called when application launched

            // First navigation
            NavigationService.Navigate("Main", args.Arguments);

            return Task.FromResult(0);
        }

        protected override Task OnInitializeAsync(IActivatedEventArgs args)
        {
            // Called when application started


            // Register DI components
            _container.RegisterInstance(NavigationService);

            return Task.FromResult(0);
        }

        protected override object Resolve(Type type)
        {
            return _container.Resolve(type);
        }


    }
}
