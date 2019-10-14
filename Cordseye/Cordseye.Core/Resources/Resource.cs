using System;
using System.Collections.Generic;
using Cordseye.Core.Events;

namespace Cordseye.Core.Resources
{
    public abstract class Resource
    {
        public struct LoadedResource
        {
            public readonly Type ResourceType;
            public readonly Resource Resource;

            public LoadedResource(Type resourceType, Resource resource)
            {
                ResourceType = resourceType;
                Resource = resource;
            }
        }

        private static Dictionary<string, LoadedResource> _LoadedResources;

        public static Dictionary<string, LoadedResource> LoadedResources => _LoadedResources ?? (_LoadedResources = new Dictionary<string, LoadedResource>());

        public abstract string GetKey();

        protected Resource()
        {
            EventsManager.Unload += OnDispose;
        }

        ~Resource()
        {
            EventsManager.Unload += OnDispose;
        }

        public static T GetResource<T>(string key) where T : Resource
        {
            if (LoadedResources.ContainsKey(key))
            {
                if (LoadedResources[key].ResourceType == typeof(T))
                {
                    return (T) LoadedResources[key].Resource;
                }
            }

            return null;
        }

        protected void Init(Type resourceType)
        {
            LoadedResources.Add(GetKey(), new LoadedResource(resourceType, this));
        }

        private void Dispose()
        {
            OnDispose();
            LoadedResources.Remove(GetKey());
        }

        protected virtual void OnDispose() { }
    }
}