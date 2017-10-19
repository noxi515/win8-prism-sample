using App2.Config;
using App2.Repository;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Unity;
using System;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;

namespace App2
{
    sealed partial class App : MvvmAppBase
    {
        // UnityContainerはDIコンテナの一種。DI = Dependency Injection（依存性注入）
        // Angularだとサービスとかファクトリーの名前を引数名で指定して取得するけれど
        // UnityコンテナはC#のインターフェースに対して実装クラスを注入する
        private readonly IUnityContainer _container = new UnityContainer();

        protected override Task OnLaunchApplicationAsync(LaunchActivatedEventArgs args)
        {
            // Called when application launched

            // First navigation
            // PrismはナビゲーションをURLっぽく行う。Main = Views/MainPage
            // Views/MainPageに対してViewModels/MainPageViewModelが対応するVMクラス。名前空間とクラスの命名規則で引っ張る。
            // XAMLに mvvm:ViewModelLocator.AutoWireViewModel="True" って書いてあるのが重要。
            NavigationService.Navigate("List", args.Arguments);

            return Task.FromResult(0);
        }

        protected override Task OnInitializeAsync(IActivatedEventArgs args)
        {
            // Called when application started


            // Register DI components
            // ViewModelでINavigationServiceがコンストラクタで取れるのは
            // DIコンテナにここでNavigationServiceを設定しているから
            _container.RegisterInstance(NavigationService);
            _container.RegisterType<IGlobalConfig, GlobalConfig>(new ContainerControlledLifetimeManager());
            _container.RegisterType<IMemoRepository, MemoRepository>(new ContainerControlledLifetimeManager());

            return Task.FromResult(0);
        }

        protected override object Resolve(Type type)
        {
            // DIの型解決はこちら
            return _container.Resolve(type);
        }

    }
}
