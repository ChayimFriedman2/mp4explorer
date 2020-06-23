//===============================================================================
// Copyright © 2009 CM Streaming Technologies.
// All rights reserved.
// http://www.cmstream.net
//===============================================================================

using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using Unity;
using Unity.Lifetime;

namespace Mp4Explorer
{
    public class MainModule : IModule
    {
        private readonly IUnityContainer _Container;
        private readonly IRegionManager _RegionManager;

        public MainModule(IUnityContainer container, IRegionManager regionManager)
        {
            _Container = container;
            _RegionManager = regionManager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            IMainTreePresenter presenter = _Container.Resolve<IMainTreePresenter>();
            IRegion region = _RegionManager.Regions[RegionNames.LeftRegion];
            region.Add(presenter.View);
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            _Container.RegisterType<IMainController, MainController>();
            _Container.RegisterType<IMainService, MainService>(new ContainerControlledLifetimeManager());
            _Container.RegisterType<IMainTreeView, MainTreeView>();
            _Container.RegisterType<IMainTreePresenter, MainTreePresenter>();
            _Container.RegisterType<IMainView, MainView>();
        }
    }
}
