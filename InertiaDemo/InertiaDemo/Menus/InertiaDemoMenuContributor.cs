using InertiaDemo.Permissions;
using InertiaDemo.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Identity.Web.Navigation;
using Volo.Abp.SettingManagement.Web.Navigation;
using Volo.Abp.UI.Navigation;
using Volo.Abp.Identity.Web.Navigation;
using Volo.Abp.TenantManagement.Web.Navigation;

namespace InertiaDemo.Menus;

public class InertiaDemoMenuContributor : IMenuContributor
{
    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }
    }

    private static Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        var l = context.GetLocalizer<InertiaDemoResource>();
        context.Menu.Items.Insert(
            0,
            new ApplicationMenuItem(
                InertiaDemoMenus.Home,
                l["Menu:Home"],
                "~/",
                icon: "fas fa-home",
                order: 0
            )
        );

        context.Menu.AddItem(
            new ApplicationMenuItem(
                "Counter",
                "Counter",
                url: "/counter",
                icon: "fas fa-list",
                order: 1
            )
        );

        context.Menu.AddItem(
            new ApplicationMenuItem(
                "About",
                "About",
                url: "/about",
                icon: "fas fa-info-circle",
                order: 2
            )
        );


        //Administration
        var administration = context.Menu.GetAdministration();
        administration.Order = 5;
        //Administration->Tenant Management
        administration.SetSubItemOrder(TenantManagementMenuNames.GroupName, 2);
        //Administration->Identity
        administration.SetSubItemOrder(IdentityMenuNames.GroupName, 2);

        //Administration->Settings
        administration.SetSubItemOrder(SettingManagementMenuNames.GroupName, 7);
        
        return Task.CompletedTask;
    }
}
