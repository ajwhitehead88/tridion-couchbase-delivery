using Tridion.ContentManager.CommunicationManagement;
using Tridion.ContentManager.ContentManagement.Fields;

namespace CouchbaseDelivery.Tridion.ModularTemplates.Extensions
{
    /// <summary>
    /// Extensions to the Template class
    /// </summary>
    public static class TemplateExtensions
    {
        private const int DefaultLinkLevel = 3;

        /// <summary>
        /// Get the link levels from the template's metadata
        /// </summary>
        /// <param name="template"></param>
        /// <returns></returns>
        public static int GetLinkLevels(this Template template)
        {
            var templateMeta = new ItemFields(template.Metadata, template.MetadataSchema);
            return templateMeta.Contains("link_levels")
                       ? (int) ((NumberField) templateMeta["link_levels"]).Value
                       : DefaultLinkLevel;
        }
    }
}
