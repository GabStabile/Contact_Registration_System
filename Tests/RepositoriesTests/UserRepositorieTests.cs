using ContactsControl.Data;
using ContactsControl.Models;
using ContactsControl.Repositorie;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace Tests.RepositoriesTests
{
    public class UserModelTests
    {
        [Fact(DisplayName = "Given valid user, When ToAdd is called, Then user should be added successfully")]
        public void ToAdd_ValidUser_AddsUser()
        {
            //arrange
            var mockDbSet = new Mock<DbSet<UsersModel>>();

            // create list for simulation to add on DbSet
            var userList = new List<UsersModel>();
            mockDbSet.Setup(m => m.Add(It.IsAny<UsersModel>())).Callback<UsersModel>((users) => userList.Add(users));

            // Create mock for context of DB (BContext)
            var mockContext = new Mock<BContext>();

            mockContext.Setup(d => d.DB_Users).Returns(mockDbSet.Object);

            // instancia o repo UserRepositorie usando o contexto do mock
            var repository = new UserRepositorie(mockContext.Object);

            // user model
            var validUser = new UsersModel
            {
                Name = "Test",
                Login = "test",
                Email = "test@test.com",
                Password = "12345",
                Profile = (ContactsControl.Enums.EnumProfile?)1
            };

            // Act
            var result = repository.ToAdd(validUser);

            // Assert
            Assert.NotNull(result); // Verifica se o resultado não é nulo
            Assert.Equal(validUser.Name, result.Name);
            Assert.Equal(validUser.Login, result.Login);
            Assert.Equal(validUser.Email, result.Email);
            Assert.Equal(validUser.Password, result.Password);
            Assert.Equal(validUser.Profile, result.Profile);

            mockDbSet.Verify(m => m.Add(It.IsAny<UsersModel>()), Times.Once());

            Assert.Contains(result, userList); // Verifica se o usuário foi adicionado à lista de usuários simulada
        }
    }
}