namespace Shopping.BLL.Interfaces
{
    public interface IRepositoryWrapper
    {
        public IAccountRepository Account { get; }
        public ICategoryRepository Category { get; }
        public ICustomerRepository Customer { get; }
        public IAdminRepository Admin { get; }
        public ITraderRepository Trader { get; }
        public IContactRepository Contact { get; }
        public IProductRepository Product { get; }
        public IImageRepository Image { get; }
        public ICountryRepository Country { get; }
        public ICardRepository Card { get; }
        public ICartRepository Cart { get; }
        public ICartItemRepository CartItem { get; }
        public Task Save();
    }
}
