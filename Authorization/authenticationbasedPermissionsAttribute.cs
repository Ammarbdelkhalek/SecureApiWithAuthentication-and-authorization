using SecureApiWithAuthentication.Authorization;

namespace SecureApiWithAuthentication.Autentication
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class authenticationbasedPermissionsAttribute(PermissionEnum permission) : Attribute
    {
        public PermissionEnum Permission { get; } = permission;
    }
}
