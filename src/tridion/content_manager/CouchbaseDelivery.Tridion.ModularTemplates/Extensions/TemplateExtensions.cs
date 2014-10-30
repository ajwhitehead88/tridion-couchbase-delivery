using Tridion.ContentManager.CommunicationManagement;
using Tridion.ContentManager.ContentManagement.Fields;

namespace CouchbaseDelivery.Tridion.ModularTemplates.Extensions
{
    public static class TemplateExtensions
    {
        private const int DefaultLinkLevel = 3;

        public static int GetLinkLevels(this Template template)
        {
            var templateMeta = new ItemFields(template.Metadata, template.MetadataSchema);
            return templateMeta.Contains("link_levels")
                       ? (int) ((NumberField) templateMeta["link_levels"]).Value
                       : DefaultLinkLevel;
        }
    }
}
