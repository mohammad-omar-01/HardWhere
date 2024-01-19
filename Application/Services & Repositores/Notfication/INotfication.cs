using Domain.NotficationNS;

namespace HardWherePresenter
{
    public interface IClientNotificationHub
    {
        Task ClientReceiveNotification(Notfication notification);
    }
}
