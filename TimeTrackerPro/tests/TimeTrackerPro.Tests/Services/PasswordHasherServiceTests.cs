using TimeTrackerPro.Infrastructure.Services;

namespace TimeTrackerPro.Tests.Services
{
    public class PasswordHasherServiceTests
    {
        private readonly IPasswordHasherService _passwordHasher;

        public PasswordHasherServiceTests()
        {
            _passwordHasher = new PasswordHasherService();
        }

        [Fact]
        public void HashPassword_WithValidPassword_ReturnsHash()
        {
            // Arrange
            var password = "SecurePassword123";

            // Act
            var hash = _passwordHasher.HashPassword(password);

            // Assert
            Assert.NotNull(hash);
            Assert.NotEmpty(hash);
            Assert.NotEqual(password, hash);
        }

        [Fact]
        public void HashPassword_WithSamePassword_ReturnsDifferentHashes()
        {
            // Arrange
            var password = "SecurePassword123";

            // Act
            var hash1 = _passwordHasher.HashPassword(password);
            var hash2 = _passwordHasher.HashPassword(password);

            // Assert
            Assert.NotEqual(hash1, hash2);
        }

        [Fact]
        public void VerifyPassword_WithCorrectPassword_ReturnsTrue()
        {
            // Arrange
            var password = "SecurePassword123";
            var hash = _passwordHasher.HashPassword(password);

            // Act
            var result = _passwordHasher.VerifyPassword(password, hash);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void VerifyPassword_WithIncorrectPassword_ReturnsFalse()
        {
            // Arrange
            var password = "SecurePassword123";
            var wrongPassword = "WrongPassword456";
            var hash = _passwordHasher.HashPassword(password);

            // Act
            var result = _passwordHasher.VerifyPassword(wrongPassword, hash);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void VerifyPassword_WithEmptyPassword_ReturnsFalse()
        {
            // Arrange
            var password = "SecurePassword123";
            var hash = _passwordHasher.HashPassword(password);

            // Act
            var result = _passwordHasher.VerifyPassword(string.Empty, hash);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void VerifyPassword_WithEmptyHash_ReturnsFalse()
        {
            // Arrange
            var password = "SecurePassword123";

            // Act
            var result = _passwordHasher.VerifyPassword(password, string.Empty);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void VerifyPassword_WithNullPassword_ReturnsFalse()
        {
            // Arrange
            var password = "SecurePassword123";
            var hash = _passwordHasher.HashPassword(password);

            // Act
            var result = _passwordHasher.VerifyPassword(null, hash);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void VerifyPassword_WithNullHash_ReturnsFalse()
        {
            // Arrange
            var password = "SecurePassword123";

            // Act
            var result = _passwordHasher.VerifyPassword(password, null);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void HashPassword_WithEmptyPassword_ThrowsException()
        {
            // Arrange
            var password = string.Empty;

            // Act & Assert
            Assert.Throws<ArgumentException>(() => _passwordHasher.HashPassword(password));
        }

        [Fact]
        public void HashPassword_WithNullPassword_ThrowsException()
        {
            // Arrange
            string password = null;

            // Act & Assert
            Assert.Throws<ArgumentException>(() => _passwordHasher.HashPassword(password));
        }
    }
}
