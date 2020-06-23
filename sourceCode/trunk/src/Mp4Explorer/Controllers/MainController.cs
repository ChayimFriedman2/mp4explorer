//===============================================================================
// Copyright © 2009 CM Streaming Technologies.
// All rights reserved.
// http://www.cmstream.net
//===============================================================================

using CMStream.Mp4;
using Prism.Regions;
using Unity;

namespace Mp4Explorer
{
    public class MainController : IMainController
    {
        private readonly IUnityContainer _Container;
        private readonly IRegionManager _RegionManager;
        private readonly IMainService _MainService;

        public MainController(IUnityContainer container, IRegionManager regionManager, IMainService mainService)
        {
            _Container = container;
            _RegionManager = regionManager;
            _MainService = mainService;
        }

        public void OnBoxSelected(Mp4Box box)
        {
            RemoveViews();

            if (box != null)
            {
                IBoxView view = _MainService.GetView(box);
                if (view != null)
                {
                    IRegion mainRegion = _RegionManager.Regions[RegionNames.MainRegion];
                    mainRegion.Add(view);
                    mainRegion.Activate(view);
                }
            }
        }

        public void ShowFile(Mp4File file)
        {
            RemoveViews();

            if (file != null)
            {
                IMainView view = _Container.Resolve<IMainView>();
                view.File = file;
                IRegion mainRegion = _RegionManager.Regions[RegionNames.MainRegion];
                mainRegion.Add(view);
                mainRegion.Activate(view);
            }
        }

        public void RemoveViews()
        {
            IRegion mainRegion = _RegionManager.Regions[RegionNames.MainRegion];
            foreach (object view in mainRegion.Views)
            {
                mainRegion.Remove(view);
            }
        }
    }
}
