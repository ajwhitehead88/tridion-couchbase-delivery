using Tridion.ContentManager;
using Tridion.ContentManager.CommunicationManagement;
using Tridion.ContentManager.ContentManagement;
using Tridion.ContentManager.Templating;
using Tridion.ContentManager.Templating.Assembly;

namespace CouchbaseDelivery.Tridion.ModularTemplates.Tridion.Templates
{
    /// <summary>
    /// Base template calls the Transform() method in implementing classes
    /// </summary>
    public abstract class BaseTemplate : ITemplate
    {
        private Publication _publication;
        private Page _page;
        private StructureGroup _parent;
        private Component _component;

        protected TemplatingLogger Log;

        protected Engine Engine;

        protected Package Package;

        protected Session Session;

        /// <summary>
        /// Get the component
        /// </summary>
        protected Component Component
        {
            get
            {
                if (_component == null)
                {
                    var packageItem = Package.GetByName(Package.ComponentName);
                    if (packageItem == null)
                    {
                        return null;
                    }

                    _component = (Component) Engine.GetObject(packageItem.GetAsSource().GetValue("ID"));
                }

                return _component;
            }
        }

        /// <summary>
        /// Get the parent of the current page
        /// </summary>
        protected StructureGroup Parent
        {
            get
            {
                if (_parent == null && Page != null)
                {
                    _parent = (StructureGroup) Page.OrganizationalItem;
                }

                return _parent;
            }
        }

        /// <summary>
        /// Get the current page
        /// </summary>
        protected Page Page
        {
            get
            {
                if (_page == null)
                {
                    var packageItem = Package.GetByName(Package.PageName);
                    if (packageItem == null)
                    {
                        return null;
                    }

                    _page = (Page) Engine.GetObject(packageItem.GetAsSource().GetValue("ID"));
                }

                return _page;
            }
        }

        /// <summary>
        /// Get the publication
        /// </summary>
        protected Publication Publication
        {
            get { return _publication ?? (_publication = Engine.PublishingContext.ResolvedItem.Template.ContextRepository as Publication); }
        }

        /// <summary>
        /// Main transform method called by Tridion
        /// </summary>
        /// <param name="engine"></param>
        /// <param name="package"></param>
        public void Transform(Engine engine, Package package)
        {
            Log = TemplatingLogger.GetLogger(GetType());

            Engine = engine;
            Package = package;
            Session = engine.GetSession();

            Transform();
        }

        /// <summary>
        /// Transform method performed after the initial items have all been set up
        /// </summary>
        protected abstract void Transform();
    }
}
