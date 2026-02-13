namespace ProductWebApi.Service
{
    public interface IPermissionService
    {
        Task<bool> HasPermission(string role, string permission);
    }
}
