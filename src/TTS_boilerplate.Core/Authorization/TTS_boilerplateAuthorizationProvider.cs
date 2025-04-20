using Abp.Authorization;
using Abp.Localization;
using Abp.MultiTenancy;

namespace TTS_boilerplate.Authorization
{
    public class TTS_boilerplateAuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            context.CreatePermission(PermissionNames.Pages_Users, L("Users"));
            context.CreatePermission(PermissionNames.Pages_Users_Activation, L("UsersActivation"));
            context.CreatePermission(PermissionNames.Pages_Roles, L("Roles"));
            context.CreatePermission(PermissionNames.Pages_Tenants, L("Tenants"), multiTenancySides: MultiTenancySides.Host);

            var product = context.CreatePermission(PermissionNames.Pages_Products, L("Products"));
            product.CreateChildPermission(PermissionNames.Pages_Products_Create, L("CreateProducts"));
            product.CreateChildPermission(PermissionNames.Products_C, L("Products_C"));
            product.CreateChildPermission(PermissionNames.Products_R, L("Products_R"));
            product.CreateChildPermission(PermissionNames.Products_U, L("Products_U"));
            product.CreateChildPermission(PermissionNames.Products_D, L("Products_D"));

            var category = context.CreatePermission(PermissionNames.Pages_Category, L("Categories"));
            category.CreateChildPermission(PermissionNames.Categories_C, L("Categories_C"));
            category.CreateChildPermission(PermissionNames.Categories_R, L("Categories_R"));
            category.CreateChildPermission(PermissionNames.Categories_U, L("Categories_U"));
            category.CreateChildPermission(PermissionNames.Categories_D, L("Categories_D"));

            
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, TTS_boilerplateConsts.LocalizationSourceName);
        }
    }
}
