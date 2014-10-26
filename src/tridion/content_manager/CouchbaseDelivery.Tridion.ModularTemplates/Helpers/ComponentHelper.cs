using Tridion.ContentManager.ContentManagement;

namespace CouchbaseDelivery.Tridion.ModularTemplates.Helpers
{
    public static class ComponentHelper
    {
        public static string GetUniqueFilename(Component component)
        {
            return string.Format("{0}_{1}-{2}",
                                 component.BinaryContent.Filename,
                                 component.Id.PublicationId,
                                 component.Id.ItemId);
        }
    }
}
