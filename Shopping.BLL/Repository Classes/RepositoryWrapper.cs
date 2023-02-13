using AutoMapper;
using Shopping.BLL.Interfaces;
using Shopping.DAL;

namespace Shopping.BLL.Repository_Classes
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        protected readonly ShoppingDBContext _context;
        private readonly IMapper _mapper;
        private IAccountRepository _account = null!;
        private ICategoryRepository _category = null!;
        private IAdminRepository _admin = null!;
        private ICustomerRepository _customer = null!;
        private ITraderRepository _trader = null!;
        private IContactRepository _contact = null!;
        private IProductRepository _product = null!;
        private IImageRepository _image = null!;
        private ICountryRepository _country = null!;
        private ICardRepository _card = null!;
        private ICartRepository _cart = null!;
        private ICartItemRepository _cartItem = null!;

        public RepositoryWrapper(ShoppingDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IAccountRepository Account { get
            {
                if (_account == null)
                    _account = new AccountRepository(_context, this, _mapper);
                return _account;
            } }

        public ICategoryRepository Category { get
            {
                if (_category == null)
                    _category = new CategoryRepository(_context, this, _mapper);
                return _category;
            } }

        public IAdminRepository Admin { get
            {
                if (_admin == null)
                    _admin = new AdminRepository(_context, this, _mapper);
                return _admin;
            } }

        public ICustomerRepository Customer { get
            {
                if (_customer == null)
                    _customer = new CustomerRepository(_context, this, _mapper);
                return _customer;
            } }

        public ITraderRepository Trader { get
            {
                if (_trader == null)
                    _trader = new TraderRepository(_context, this, _mapper);
                return _trader;
            } }

        public IContactRepository Contact { get
            {
                if (_contact == null)
                    _contact = new ContactRepository(_context, this, _mapper);
                return _contact;
            } }

        public IProductRepository Product { get
            {
                if (_product == null)
                    _product = new ProductRepository(_context, this, _mapper);
                return _product;
            } }

        public IImageRepository Image { get
            {
                if (_image == null)
                    _image = new ImageRepository(_context, this, _mapper);
                return _image;
            } }

        public ICountryRepository Country { get
            {
                if (_country == null)
                    _country = new CountryRepository(_context, this, _mapper);
                return _country;
            } }

        public ICardRepository Card { get
            {
                if (_card == null)
                    _card = new CardRepository(_context, this, _mapper);
                return _card;
            } }

        public ICartRepository Cart { get
            {
                if (_cart == null)
                    _cart = new CartRepository(_context, this, _mapper);
                return _cart;
            } }

        public ICartItemRepository CartItem { get
            {
                if (_cartItem == null)
                    _cartItem = new CartItemRepository(_context, this, _mapper);
                return _cartItem;
            } }
        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
