using Microsoft.Extensions.Localization;
using InertiaDemo.Localization;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace InertiaDemo;

[Dependency(ReplaceServices = true)]
public class InertiaDemoBrandingProvider : DefaultBrandingProvider
{
    private IStringLocalizer<InertiaDemoResource> _localizer;

    public InertiaDemoBrandingProvider(IStringLocalizer<InertiaDemoResource> localizer)
    {
        _localizer = localizer;
    }

    public override string AppName => _localizer["AppName"];
}