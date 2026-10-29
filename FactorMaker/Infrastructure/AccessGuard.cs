using Common;
using Data;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FactorMaker.Infrastructure
{
    /// <summary>
    /// Ownership checks: store owners may only touch data that belongs to stores they own
    /// (or, for blog/post-category/image data, rows they own). Programmer keeps full access.
    /// </summary>
    public class AccessGuard
    {
        public const string AdminRoleName = "Programmer";

        public AccessGuard(DatabaseContext database)
        {
            Database = database;
        }

        private DatabaseContext Database { get; }
        private HashSet<Guid> _ownedStoreIds;
        private Guid _ownedStoreIdsUser;

        public static bool IsAdmin(User user) => user?.Role?.Name == AdminRoleName;

        public static Result Denied()
        {
            var result = new Result { IsSuccessful = false };
            result.AddErrorMessage(Resources.ErrorMessages.UnauthorizedActionAccess);
            return result;
        }

        public async Task<HashSet<Guid>> OwnedStoreIdsAsync(User user)
        {
            if (user == null) return new HashSet<Guid>();
            if (_ownedStoreIds == null || _ownedStoreIdsUser != user.Id)
            {
                var ids = await Database.Stores.AsNoTracking()
                    .Where(s => s.OwnerId == user.Id && !s.IsDeleted)
                    .Select(s => s.Id)
                    .ToListAsync();
                _ownedStoreIds = new HashSet<Guid>(ids);
                _ownedStoreIdsUser = user.Id;
            }
            return _ownedStoreIds;
        }

        public async Task<bool> CanAccessStoreAsync(User user, Guid storeId)
        {
            if (user == null) return false;
            if (IsAdmin(user)) return true;
            return (await OwnedStoreIdsAsync(user)).Contains(storeId);
        }

        private async Task<bool> CanAccessStoreOfAsync(User user, Guid? storeId)
        {
            if (user == null) return false;
            if (IsAdmin(user)) return true;
            // unknown id: let the service report "not found"
            if (storeId == null) return true;
            return await CanAccessStoreAsync(user, storeId.Value);
        }

        public async Task<bool> CanAccessCustomerAsync(User user, Guid id) =>
            await CanAccessStoreOfAsync(user, await Database.Customers.AsNoTracking().Where(x => x.Id == id).Select(x => (Guid?)x.StoreId).FirstOrDefaultAsync());

        public async Task<bool> CanAccessProductAsync(User user, Guid id) =>
            await CanAccessStoreOfAsync(user, await Database.Products.AsNoTracking().Where(x => x.Id == id).Select(x => (Guid?)x.StoreId).FirstOrDefaultAsync());

        public async Task<bool> CanAccessCategoryAsync(User user, Guid id) =>
            await CanAccessStoreOfAsync(user, await Database.Categories.AsNoTracking().Where(x => x.Id == id).Select(x => (Guid?)x.StoreId).FirstOrDefaultAsync());

        public async Task<bool> CanAccessFactorAsync(User user, Guid id) =>
            await CanAccessStoreOfAsync(user, await Database.Factors.AsNoTracking().Where(x => x.Id == id).Select(x => (Guid?)x.StoreId).FirstOrDefaultAsync());

        public async Task<bool> CanAccessFactorItemAsync(User user, Guid id) =>
            await CanAccessStoreOfAsync(user, await Database.FactorItems.AsNoTracking().Where(x => x.Id == id).Select(x => (Guid?)x.Factor.StoreId).FirstOrDefaultAsync());

        public async Task<bool> CanAccessStoreByIdAsync(User user, Guid id) =>
            await CanAccessStoreOfAsync(user, await Database.Stores.AsNoTracking().Where(x => x.Id == id).Select(x => (Guid?)x.Id).FirstOrDefaultAsync());

        private static Task<bool> OwnsAsync(User user, Guid? ownerId)
        {
            if (user == null) return Task.FromResult(false);
            if (IsAdmin(user) || ownerId == null) return Task.FromResult(true);
            return Task.FromResult(ownerId.Value == user.Id);
        }

        public async Task<bool> CanAccessBlogPostAsync(User user, Guid id) =>
            await OwnsAsync(user, await Database.BlogPosts.AsNoTracking().Where(x => x.Id == id).Select(x => (Guid?)x.OwnerId).FirstOrDefaultAsync());

        public async Task<bool> CanAccessPostCategoryAsync(User user, Guid id) =>
            await OwnsAsync(user, await Database.PostCategories.AsNoTracking().Where(x => x.Id == id).Select(x => (Guid?)x.OwnerId).FirstOrDefaultAsync());

        public async Task<bool> CanAccessImageAssetAsync(User user, Guid id) =>
            await OwnsAsync(user, await Database.ImageAssets.AsNoTracking().Where(x => x.Id == id).Select(x => (Guid?)x.OwnerId).FirstOrDefaultAsync());

        /// <summary>Filters a list result down to rows of the user's own stores (no-op for admins).</summary>
        public async Task FilterByStoreAsync<T>(User user, Result<ICollection<T>> result, Func<T, Guid> storeIdOf)
        {
            if (result?.Data == null || IsAdmin(user)) return;
            var owned = await OwnedStoreIdsAsync(user);
            result.Data = result.Data.Where(x => owned.Contains(storeIdOf(x))).ToList();
        }

        /// <summary>Filters a list result down to rows the user owns (no-op for admins).</summary>
        public Task<bool> IsPublicStoreAsync(Guid storeId) =>
            Database.Stores.AsNoTracking().AnyAsync(x => x.Id == storeId && !x.IsDeleted && x.IsPublic);

        public void FilterByOwner<T>(User user, Result<ICollection<T>> result, Func<T, Guid> ownerIdOf)
        {
            if (result?.Data == null || IsAdmin(user)) return;
            result.Data = result.Data.Where(x => ownerIdOf(x) == user.Id).ToList();
        }
    }
}