using Tridion.ContentManager.ContentManagement;
using Tridion.ContentManager.Templating;

namespace CouchbaseDelivery.Tridion.ModularTemplates.Extensions
{
    /// <summary>
    /// Extensions for components
    /// </summary>
    internal static class ComponentExtensions
    {
        /// <summary>
        /// Publish a binary component and return the URL
        /// </summary>
        /// <param name="component"></param>
        /// <param name="engine"></param>
        /// <param name="package"></param>
        /// <returns></returns>
        internal static string PublishBinary(this Component component, Engine engine, Package package)
        {
            if (component == null)
            {
                return null;
            }

            var mmItem = package.CreateMultimediaItem(component);
            package.PushItem("PublishMultimedia_" + component.Id, mmItem);

            if (!mmItem.Properties.ContainsKey(Item.ItemPropertyPublishedPath))
            {
                var binary = engine.PublishingContext.RenderedItem.AddBinary(component);
                mmItem.Properties[Item.ItemPropertyPublishedPath] = binary.Url;
            }

            return mmItem.Properties[Item.ItemPropertyPublishedPath];
        }
    }
}
