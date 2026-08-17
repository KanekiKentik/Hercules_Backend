public abstract class ServiceBase
{
    protected ICurrentUser _user;
    public ServiceBase(ICurrentUser user) => _user = user;
}