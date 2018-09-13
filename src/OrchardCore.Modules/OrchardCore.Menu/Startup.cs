using Microsoft.Extensions.DependencyInjection;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Display.ContentDisplay;
using OrchardCore.ContentManagement.Handlers;
using OrchardCore.ContentTree.Models;
using OrchardCore.ContentTree.Services;
using OrchardCore.Data.Migration;
using OrchardCore.DisplayManagement.Descriptors;
using OrchardCore.DisplayManagement.Handlers;
using OrchardCore.Environment.Navigation;
using OrchardCore.Lists.Drivers;
using OrchardCore.Menu.Handlers;
using OrchardCore.Menu.Models;
using OrchardCore.Menu.TagHelpers;
using OrchardCore.Menu.Trees;
using OrchardCore.Modules;
using OrchardCore.Security.Permissions;

namespace OrchardCore.Menu
{
    public class Startup : StartupBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IDataMigration, Migrations>();
            services.AddScoped<IShapeTableProvider, MenuShapes>();
            services.AddScoped<IPermissionProvider, Permissions>();

            // MenuPart
            services.AddScoped<IContentHandler, MenuContentHandler>();
            services.AddScoped<IContentPartDisplayDriver, MenuPartDisplayDriver>();
            services.AddSingleton<ContentPart, MenuPart>();

            // LinkMenuItemPart
            services.AddScoped<IContentPartDisplayDriver, LinkMenuItemPartDisplayDriver>();
            services.AddSingleton<ContentPart, LinkMenuItemPart>();

            services.AddTagHelpers(typeof(MenuTagHelper).Assembly);
        }
    }


    [RequireFeatures("OrchardCore.ContentTree")]
    public class ContentTreeStartup : StartupBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ITreeNodeProviderFactory>(new TreeNodeProviderFactory<MenuTreeNode>());
            services.AddScoped<ITreeNodeNavigationBuilder, MenuTreeNodeNavigationBuilder>();
            services.AddScoped<IDisplayDriver<MenuItem>, MenuTreeNodeDriver>();
        }
    }
}
