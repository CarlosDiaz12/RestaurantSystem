using RestaurantSystem.Core.CustomEntities;
using RestaurantSystem.Core.Entities;
using RestaurantSystem.Core.Entities.Base;

namespace RestaurantSystem.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        void SaveChanges();
        Task<int> SaveChangesAsync();
        IRepository<Order> OrderRepository { get; }
        IRepository<OrderDetail> OrderDetailRepository { get; }
        IRepository<Menu> MenuRepository { get; }
        IRepository<MenuItem> MenuItemRepository { get; }
        IRepository<MenuItemCategory> MenuItemCategoryRepository { get; }
        IRepository<Table> TableRepository { get; }
        IRepository<Invoice> InvoiceRepository { get; }
        IRepository<InvoiceDetail> InvoiceDetailRepository { get; }
        IRepository<T> GetRepository<T>() where T : BaseEntity;
    }
}
