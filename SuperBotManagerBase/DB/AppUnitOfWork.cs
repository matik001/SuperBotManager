using SuperBotManagerBase.DB.Repositories;

namespace SuperBotManagerBase.DB
{
    public interface IAppUnitOfWork : IDisposable
    {
        IUserRepository UserRepository { get; }
        IUserPasswordRepository UserPasswordRepository { get; }
        IRoleRepository RoleRepository { get; }
        IUserRoleRepository UserRoleRepository { get; }
        IRefreshTokenRepository RefreshTokenRepository { get; }
        IRevokedTokenRepository RevokedTokenRepository { get; }

        IActionDefinitionRepository ActionDefinitionRepository { get; }
        IActionExecutorRepository ActionExecutorRepository { get; }
        IActionScheduleRepository ActionScheduleRepository { get; }
        IActionRepository ActionRepository { get; }
        ISecretRepository SecretRepository { get; }
        IVaultItemRepository VaultItemRepository { get; }
        Task<int> SaveChangesAsync();
    }
    public class AppUnitOfWork : IAppUnitOfWork
    {
        private readonly AppDBContext _dbContext;
        public AppUnitOfWork(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        private IUserRepository _userRepository;
        public IUserRepository UserRepository
        {
            get
            {
                if (_userRepository == null)
                {
                    _userRepository = new UserRepository(_dbContext);
                }
                return _userRepository;
            }
        }
        private IUserPasswordRepository _userPasswordRepository;
        public IUserPasswordRepository UserPasswordRepository
        {
            get
            {
                if (_userPasswordRepository == null)
                {
                    _userPasswordRepository = new UserPasswordRepository(_dbContext);
                }
                return _userPasswordRepository;
            }
        }

        private IRoleRepository _roleRepository;
        public IRoleRepository RoleRepository
        {
            get
            {
                if (_roleRepository == null)
                {
                    _roleRepository = new RoleRepository(_dbContext);
                }
                return _roleRepository;
            }
        }
        private IUserRoleRepository _userRoleRepository;
        public IUserRoleRepository UserRoleRepository
        {
            get
            {
                if (_userRoleRepository == null)
                {
                    _userRoleRepository = new UserRoleRepository(_dbContext);
                }
                return _userRoleRepository;
            }
        } 
        private IRefreshTokenRepository _refreshTokenRepository;

        public IRefreshTokenRepository RefreshTokenRepository
        {
            get
            {
                if (_refreshTokenRepository == null)
                {
                    _refreshTokenRepository = new RefreshTokenRepository(_dbContext);
                }
                return _refreshTokenRepository;
            }
        }
        private IRevokedTokenRepository _revokedTokenRepository;

        public IRevokedTokenRepository RevokedTokenRepository
        {
            get
            {
                if (_revokedTokenRepository == null)
                {
                    _revokedTokenRepository = new RevokedTokenRepository(_dbContext);
                }
                return _revokedTokenRepository;
            }
        }
        IActionRepository _actionRepository;
        public IActionRepository ActionRepository
        {
            get
            {
                if (_actionRepository == null)
                {
                    _actionRepository = new ActionRepository(_dbContext);
                }
                return _actionRepository;
            }
        }
        IActionExecutorRepository _actionExecutorRepository;
        public IActionExecutorRepository ActionExecutorRepository
        {
            get
            {
                if (_actionExecutorRepository == null)
                {
                    _actionExecutorRepository = new ActionExecutorRepository(_dbContext);
                }
                return _actionExecutorRepository;
            }
        }
        IActionDefinitionRepository _actionDefinitionRepository;
        public IActionDefinitionRepository ActionDefinitionRepository
        {
            get
            {
                if (_actionDefinitionRepository == null)
                {
                    _actionDefinitionRepository = new ActionDefinitionRepository(_dbContext);
                }
                return _actionDefinitionRepository;
            }
        }
        ISecretRepository _secretRepository;
        public ISecretRepository SecretRepository
        {
            get
            {
                if (_secretRepository == null)
                {
                    _secretRepository = new SecretRepository(_dbContext);
                }
                return _secretRepository;
            }
        }

        IActionScheduleRepository _actionScheduleRepository;
        public IActionScheduleRepository ActionScheduleRepository
        {
            get
            {
                if (_actionScheduleRepository == null)
                {
                    _actionScheduleRepository = new ActionScheduleRepository(_dbContext);
                }
                return _actionScheduleRepository;
            }
        }

        IVaultItemRepository _vaultItemRepository;
        public IVaultItemRepository VaultItemRepository
        {
            get
            {
                if (_vaultItemRepository == null)
                {
                    _vaultItemRepository = new VaultItemRepository(_dbContext);
                }
                return _vaultItemRepository;
            }
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }  
}
