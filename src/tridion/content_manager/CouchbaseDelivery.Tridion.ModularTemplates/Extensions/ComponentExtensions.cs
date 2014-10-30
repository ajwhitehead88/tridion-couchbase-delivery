using Tridion.ContentManager.ContentManagement;

namespace CouchbaseDelivery.Tridion.ModularTemplates.Extensions
{
    public static class ComponentExtensions
    {
        public static string GetUniqueFilename(this Component component)
        {
            return string.Format("{0}_{1}-{2}",
                                 component.BinaryContent.Filename,
                                 component.Id.PublicationId,
                                 component.Id.ItemId);
        }
    }
}
