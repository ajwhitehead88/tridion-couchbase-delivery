using System;
using Tridion.ContentManager.CommunicationManagement;
using Tridion.ContentManager.Templating;
using Tridion.ContentManager.Templating.Assembly;

namespace CouchbaseDelivery.Tridion.ModularTemplates.Tridion.Templates
{
    public abstract class BaseTemplate : ITemplate
    {
        protected Engine Engine;

        protected Package Package;

        public void Transform(Engine engine, Package package)
        {
            Engine = engine;
            Package = package;
            
            Transform();
        }

        protected abstract void Transform();

        protected Page GetPage()
        {
            // try and get the page directly from the package
            var pageItem = Package.GetByType(ContentType.Page);
            if (pageItem != null)
            {
                return (Page)Engine.GetObject(pageItem.GetAsSource().GetValue("ID"));
            }

            throw new InvalidOperationException("Cannot find page object");
        }
    }
}
