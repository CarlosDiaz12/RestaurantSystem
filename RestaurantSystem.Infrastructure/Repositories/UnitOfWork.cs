using Duende.IdentityServer.Models;
using Microsoft.VisualBasic.FileIO;
using RestaurantSystem.Core.Entities;
using RestaurantSystem.Core.Entities.Base;
using RestaurantSystem.Core.Interfaces;
using RestaurantSystem.Infrastructure.Data;
using RestaurantSystem.Infrastructure.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSystem.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public IRepository<Order> OrderRepository { get; }
        public IRepository<OrderDetail> OrderDetailRepository { get; }
        public IRepository<Menu> MenuRepository { get; }
        public IRepository<MenuItem> MenuItemRepository { get; }
        public IRepository<MenuItemCategory> MenuItemCategoryRepository { get; }
        public IRepository<Table> TableRepository { get; }
        public IRepository<Invoice> InvoiceRepository { get; }
        public IRepository<InvoiceDetail> InvoiceDetailRepository { get; }

        public UnitOfWork(
            AppDbContext context,
                        IRepository<Order> orderRepository,
        IRepository<OrderDetail> orderDetailRepository,
        IRepository<Menu> menuRepository,
        IRepository<MenuItem> menuItemRepository,
        IRepository<MenuItemCategory> menuItemCategoryRepository,
        IRepository<Table> tableRepository,
        IRepository<Invoice> invoiceRepository,
        IRepository<InvoiceDetail> invoiceDetailRepository)
        {
            _context = context;
            OrderRepository = orderRepository;
            OrderDetailRepository = orderDetailRepository;
            MenuRepository = menuRepository;
            MenuItemRepository = menuItemRepository;
            MenuItemCategoryRepository = menuItemCategoryRepository;
            TableRepository = tableRepository;
            InvoiceRepository = invoiceRepository;
            InvoiceDetailRepository = invoiceDetailRepository;
        }
        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
                GC.SuppressFinalize(this);
            }
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }

        public IRepository<T> GetRepository<T>() where T : BaseEntity
        {
            var repositoryProperty = this.GetType().GetProperties().FirstOrDefault(x => x.PropertyType.GenericTypeArguments.FirstOrDefault() == typeof(T));
            if (repositoryProperty == null) return new BaseRepository<T>(this._context);
            return repositoryProperty.GetValue(this) as IRepository<T>;
        }
    }
}
