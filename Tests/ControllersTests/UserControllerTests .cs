using ContactsControl.Controllers;
using ContactsControl.Models;
using ContactsControl.Repositorie;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace Tests.ControllersTests
{
    public class UserControllerTests
    {
        private readonly Mock<IUserRepositorie> _mockUserRepositorie;

        private readonly UserController _controller;

        public UserControllerTests()
        {
            // Inicializando o mock do repositório
            _mockUserRepositorie = new Mock<IUserRepositorie>();

            // Criando o controlador com o repositório mockado
            _controller = new UserController(_mockUserRepositorie.Object);
        }

        /// <summary>
        /// Testa se o método `Index` retorna uma lista de usuários com a View
        /// </summary>
        [Fact(DisplayName = "UserController: Index should return a list of users")]
        public void Index_ReturnsViewWithUsers()
        {
            // Arrange
            var users = new List<UsersModel>
            {
                new UsersModel { Id = 1, Name = "User1" },
                new UsersModel { Id = 2, Name = "User2" }
            };

            _mockUserRepositorie.Setup(repo => repo.AllSearch()).Returns(users);

            // Act
            var result = _controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result); // Verifica se é uma View
            var model = Assert.IsAssignableFrom<List<UsersModel>>(viewResult.Model); // Verifica o tipo de modelo
            Assert.Equal(2, model.Count); // Verifica se a lista tem 2 usuários
        }

        /// <summary>
        /// Testa se o método `Create` retorna a view correta
        /// </summary>
        [Fact(DisplayName = "UserController: Create should return the create view")]
        public void Create_ReturnsView()
        {
            // Act
            var result = _controller.Create();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result); // Verifica se o resultado é uma View
        }

        /// <summary>
        /// Testa se o método `Edit` retorna um usuário para edição
        /// </summary>
        [Fact(DisplayName = "UserController: Edit should return the user to edit")]
        public void Edit_ReturnsUserForEdit()
        {
            // Arrange
            var user = new UsersModel { Id = 1, Name = "User1" };
            _mockUserRepositorie.Setup(repo => repo.ListForId(1)).Returns(user);

            // Act
            var result = _controller.Edit(1);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result); // Verifica se o resultado é uma View
            var model = Assert.IsAssignableFrom<UsersModel>(viewResult.Model); // Verifica o tipo do modelo
            Assert.Equal(user.Id, model.Id); // Verifica se o ID do usuário é o mesmo
        }

        /// <summary>
        /// Testa o método `Delete` para excluir um usuário com sucesso
        /// </summary>
        [Fact(DisplayName = "UserController: Delete should return success message")]
        public void Delete_ReturnsSuccessMessage_OnSuccessfulDeletion()
        {
            // Arrange
            _mockUserRepositorie.Setup(repo => repo.Delete(1)).Returns(true);

            // Mock do TempData para evitar NullReferenceException
            var tempData = new Mock<ITempDataDictionary>();
            _controller.TempData = tempData.Object;

            // Act
            var result = _controller.Delete(1);

            // Assert
            var redirectResult = Assert.IsType<RedirectToActionResult>(result); // Verifica se é um redirecionamento
            Assert.Equal("Index", redirectResult.ActionName); // Verifica se redireciona para a ação Index

            // Verifica se o TempData contém a mensagem de sucesso
            tempData.VerifySet(td => td["SuccessMessage"] = "User deleted sucessfully", Times.Once);
        }


        /// <summary>
        /// Testa o método `Create` para criar um novo usuário com sucesso
        /// </summary>
        [Fact(DisplayName = "UserController: Create should return success message on successful creation")]
        public void Create_Post_ReturnsSuccessMessage_OnSuccessfulCreation()
        {
            // Arrange
            var user = new UsersModel { Name = "User1", Email = "user1@example.com" };

            // Mock do repositório para simular a criação do usuário
            _mockUserRepositorie.Setup(repo => repo.ToAdd(user)).Returns(user);

            // Mock do TempData para evitar o NullReferenceException
            var tempData = new Mock<ITempDataDictionary>();
            _controller.TempData = tempData.Object;

            // Act
            var result = _controller.Create(user);

            // Assert
            var redirectResult = Assert.IsType<RedirectToActionResult>(result); // Verifica se redireciona para a ação Index
            Assert.Equal("Index", redirectResult.ActionName); // Verifica se o redirecionamento é para a página Index
        }


        /// <summary>
        /// Testa o método `Edit` para editar um usuário com sucesso
        /// </summary>
        [Fact(DisplayName = "UserController: Edit should return success message on successful edit")]
        public void Edit_Post_ReturnsSuccessMessage_OnSuccessfulEdit()
        {
            // Arrange
            var user = new UsersWithoutPasswordModel { Id = 1, Name = "User1", Email = "user1@example.com" };
            
            var updatedUser = new UsersModel { Id = 1, Name = "Updated User", Email = "updated@example.com" };
            
            _mockUserRepositorie.Setup(repo => repo.Edit(It.IsAny<UsersModel>())).Returns(updatedUser);

            // Criando o mock do TempData
            var tempData = new Mock<ITempDataDictionary>();
            _controller.TempData = tempData.Object;

            // Act
            var result = _controller.Edit(user);

            // Assert
            var redirectResult = Assert.IsType<RedirectToActionResult>(result); // Verifica se redireciona para a ação Index
            
            Assert.Equal("Index", redirectResult.ActionName); // Verifica se o redirecionamento é para a página Index

            // Verifica se o TempData contém a mensagem de sucesso
            tempData.VerifySet(td => td["SuccessMessage"] = "User edited successfully", Times.Once());
        }
    }
}