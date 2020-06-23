//===============================================================================
// Copyright © 2009 CM Streaming Technologies.
// All rights reserved.
// http://www.cmstream.net
//===============================================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CMStream.Mp4;

namespace Mp4Explorer
{
    public class MainService : IMainService
    {
        private readonly Dictionary<Type, Type> _ViewsMap;

        public MainService()
        {
            _ViewsMap = new Dictionary<Type, Type>(
                from viewType in Assembly.GetExecutingAssembly().GetTypes()
                let boxViewType = viewType.GetCustomAttribute<BoxViewTypeAttribute>(false)
                where boxViewType != null && typeof(IBoxView).IsAssignableFrom(viewType)
                select new KeyValuePair<Type, Type>(boxViewType.BoxType, viewType)
            );
        }

        public IBoxView GetView(Mp4Box box)
        {
            if (_ViewsMap.TryGetValue(box.GetType(), out Type viewType))
            {
                var view = (IBoxView)Activator.CreateInstance(viewType);
                view.Box = box;
                return view;
            }
            else
            {
                return null;
            }
        }
    }
}
