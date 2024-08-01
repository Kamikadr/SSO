using Microsoft.EntityFrameworkCore;

namespace Test.Base.Fixtures
{
    public static class GlobalDatabaseInitializer
    {
        // thread safety is not a problem because of single threading xunit runner configuration
        //private static bool _isInitialized;

        private static HashSet<string> _initializedContexts = new();
        
        public static async Task InitializeIfNeeded<T>(T dbContext) where T : DbContext
        {
            var key = nameof(T)!;
            if (_initializedContexts.Contains(key))
            {
                return;
            }
            
            await dbContext.Database.MigrateAsync();
            _initializedContexts.Add(key);
        }
    }
}