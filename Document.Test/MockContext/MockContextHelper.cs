using Document.Data;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document.Test.MockContext
{
    public static class MockContextHelper
    {
           /*
        public static ApplicationDbContext GetMockContext<TRepository>()
        {
            // Context setup
            var dbContextOptions = new DbContextOptionsBuilder<ChronoMateContext>()
                .UseInMemoryDatabase($"{nameof(TRepository)}InMemoryDatabase{Guid.NewGuid()}")
                .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;

            // Mock DateTime Provider
            var dateTimeProviderMock = new Mock<IDateTimeProvider>();

            // Mock Resource Authorization Service
            var authorizationServiceMock = new Mock<IResourceAuthorizationService>();

            // Mock application settings
            var mockApplicationSettings = MockSettingsHelper.GetMockSettings<ApplicationSettings>();

            // Mock context with its dependencies
            var mockDbContext = new ApplicationDbContext(dbContextOptions, dateTimeProviderMock.Object, authorizationServiceMock.Object, mockApplicationSettings);

            return mockDbContext;
        }
           */
    }
}
