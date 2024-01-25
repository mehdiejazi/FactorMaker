using Data.Repositories;
using Data.Repositories.RepositoryInterfaces;

namespace Data
{
    public class UnitOfWork : Base.UnitOfWork, IUnitOfWork
    {
        public UnitOfWork(Tools.Options options) : base(options)
        {
        }

        private IImageAssetRepository _imageAssetRepository;
        public IImageAssetRepository ImageAssetRepository
        {
            get
            {
                if (_imageAssetRepository == null)
                {
                    _imageAssetRepository =
                        new ImageAssetRepository(DatabaseContext);
                }

                return _imageAssetRepository;
            }
        }

        private IStoreRepository _storeRepository;
        public IStoreRepository StoreRepository
        {
            get
            {
                if (_storeRepository == null)
                {
                    _storeRepository =
                        new StoreRepository(DatabaseContext);
                }

                return _storeRepository;
            }
        }

        private ICategoryRepository _categoryRepository;
        public ICategoryRepository CategoryRepository
        {
            get
            {
                if (_categoryRepository == null)
                {
                    _categoryRepository =
                        new CategoryRepository(DatabaseContext);
                }

                return _categoryRepository;
            }
        }

        private ICustomerRepository _customerRepository;
        public ICustomerRepository CustomerRepository
        {
            get
            {
                if (_customerRepository == null)
                {
                    _customerRepository =
                        new CustomerRepository(DatabaseContext);
                }

                return _customerRepository;
            }
        }

        private IFactorItemRepository _factorItemRepository;
        public IFactorItemRepository FactorItemRepository
        {
            get
            {
                if (_factorItemRepository == null)
                {
                    _factorItemRepository =
                        new FactorItemRepository(DatabaseContext);
                }

                return _factorItemRepository;
            }
        }

        private IFactorRepository _factorRepository;
        public IFactorRepository FactorRepository
        {
            get
            {
                if (_factorRepository == null)
                {
                    _factorRepository =
                        new FactorRepository(DatabaseContext);
                }

                return _factorRepository;
            }
        }

        private IProductRepository _productRepository;
        public IProductRepository ProductRepository
        {
            get
            {
                if (_productRepository == null)
                {
                    _productRepository =
                        new ProductRepository(DatabaseContext);
                }

                return _productRepository;
            }
        }

        private IUserRepository _userRepository;
        public IUserRepository UserRepository
        {
            get
            {
                if (_userRepository == null)
                {
                    _userRepository =
                        new UserRepository(DatabaseContext);
                }

                return _userRepository;
            }
        }

        private IRoleRepository _roleRepository;
        public IRoleRepository RoleRepository
        {
            get
            {
                if (_roleRepository == null)
                {
                    _roleRepository =
                        new RoleRepository(DatabaseContext);
                }

                return _roleRepository;
            }
        }

        private IActionPermissionRepository _actionPermissionRepository;
        public IActionPermissionRepository ActionPermissionRepository
        {
            get
            {
                if (_actionPermissionRepository == null)
                {
                    _actionPermissionRepository =
                        new ActionPermissionRepository(DatabaseContext);
                }

                return _actionPermissionRepository;
            }
        }

        public IBlogPostRepository _blogPostRepository;
        public IBlogPostRepository BlogPostRepository
        {
            get
            {
                if (_blogPostRepository == null)
                {
                    _blogPostRepository =
                        new BlogPostRepository(DatabaseContext);
                }

                return _blogPostRepository;
            }
        }
        public IPostCategoryRepository _postCategoryRepository;
        public IPostCategoryRepository PostCategoryRepository
        {
            get
            {
                if (_postCategoryRepository == null)
                {
                    _postCategoryRepository =
                        new PostCategoryRepository(DatabaseContext);
                }

                return _postCategoryRepository;
            }
        }

    }
}
