using Volo.Abp.Application.Services;
using InertiaDemo.Localization;

namespace InertiaDemo.Services;

/* Inherit your application services from this class. */
public abstract class InertiaDemoAppService : ApplicationService
{
    protected InertiaDemoAppService()
    {
        LocalizationResource = typeof(InertiaDemoResource);
    }
}