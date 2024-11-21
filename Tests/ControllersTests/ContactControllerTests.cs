using ContactsControl.Controllers;
using ContactsControl.Data;
using ContactsControl.Models;
using ContactsControl.Repositorie;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Tests.ControllersTests
{
    public class ContactControllerTests
    {
        private readonly Mock<IContactRepositorie> _mockContactRepositorie;

        private readonly Mock<ITempDataDictionary> _mockTempData; // Mock do TempData

        private readonly ContactController _controller;

        public ContactControllerTests()
        {
            _mockContactRepositorie = new Mock<IContactRepositorie>();
            _mockTempData = new Mock<ITempDataDictionary>(); // Mock do TempData

            _controller = new ContactController(_mockContactRepositorie.Object)
            {
                TempData = _mockTempData.Object // Configura o TempData mockado no controlador
            };
        }

        /// <summary>
        /// Testa o método `Index` para garantir que uma lista de contatos seja retornada.
        /// </summary>
        [Fact(DisplayName = "ContactController: Index should return a list of contacts")]
        public void Index_ReturnsViewWithContacts()
        {
            // Arrange
            var contacts = new List<ContactsModel>
            {
                new ContactsModel { Id = 1, Name = "Contact1", Email = "contact1@example.com" },
                new ContactsModel { Id = 2, Name = "Contact2", Email = "contact2@example.com" }
            };

            _mockContactRepositorie.Setup(repo => repo.AllSearch()).Returns(contacts);

            // Act
            var result = _controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result); // Verifica se é uma View
            var model = Assert.IsAssignableFrom<List<ContactsModel>>(viewResult.Model); // Verifica o tipo de modelo
            Assert.Equal(2, model.Count); // Verifica se a lista tem 2 contatos
        }

        /// <summary>
        /// Testa se o método `Create` retorna a view de criação.
        /// </summary>
        [Fact(DisplayName = "ContactController: Create should return the create view")]
        public void Create_ReturnsView()
        {
            // Act
            var result = _controller.Create();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result); // Verifica se o resultado é uma View
        }

        /// <summary>
        /// Testa o método `Edit` para garantir que o contato correto seja retornado para edição.
        /// </summary>
        [Fact(DisplayName = "ContactController: Edit should return the contact to edit")]
        public void Edit_ReturnsContactForEdit()
        {
            // Arrange
            var contact = new ContactsModel { Id = 1, Name = "Contact1", Email = "contact1@example.com" };
            _mockContactRepositorie.Setup(repo => repo.ListForId(1)).Returns(contact);

            // Act
            var result = _controller.Edit(1);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result); // Verifica se o resultado é uma View
            var model = Assert.IsAssignableFrom<ContactsModel>(viewResult.Model); // Verifica o tipo do modelo
            Assert.Equal(contact.Id, model.Id); // Verifica se o ID do contato é o mesmo
        }

        /// <summary>
        /// Testa o método `Delete` para garantir que a exclusão de um contato seja realizada com sucesso
        /// </summary>
        [Fact(DisplayName = "ContactController: Delete should return success message on successful deletion")]
        public void Delete_ReturnsSuccessMessage_OnSuccessfulDeletion()
        {
            // Arrange
            _mockContactRepositorie.Setup(repo => repo.Delete(1)).Returns(true); // Configura o mock para retornar true

            // Act
            var result = _controller.Delete(1); // Chama o método Delete

            // Assert
            var redirectResult = Assert.IsType<RedirectToActionResult>(result); // Verifica se é um redirecionamento
            Assert.Equal("Index", redirectResult.ActionName); // Verifica se o redirecionamento é para a página Index
        }

        /// <summary>
        /// Testa o método `Create` para garantir que a criação de um contato seja bem-sucedida
        /// </summary>
        [Fact(DisplayName = "ContactController: Create should return success message on successful creation")]
        public void Create_Post_ReturnsSuccessMessage_OnSuccessfulCreation()
        {
            // Arrange
            var contact = new ContactsModel
            {
                Name = "New Contact",
                Email = "newcontact@example.com",
                Phone = "123456789"
            };

            // Mock do IContactRepositorie
            var mockContactRepositorie = new Mock<IContactRepositorie>();
            mockContactRepositorie
                .Setup(repo => repo.ToAdd(It.IsAny<ContactsModel>()))
                .Returns((ContactsModel c) => c); // Simula o retorno do método ToAdd

            // Usando TempData real (não mockado) para garantir que a mensagem de sucesso seja definida
            var tempData = new TempDataDictionary(new DefaultHttpContext(), Mock.Of<ITempDataProvider>());

            // Criando o controlador com o repositório mockado e TempData real
            var controller = new ContactController(mockContactRepositorie.Object)
            {
                TempData = tempData // Usando o TempData real aqui
            };

            // Act
            var result = controller.Create(contact);

            // Assert
            var redirectResult = Assert.IsType<RedirectToActionResult>(result); // Verifica se redireciona para a ação Index
            Assert.Equal("Index", redirectResult.ActionName); // Verifica se o redirecionamento é para a página Index

            // Verifica se a mensagem de sucesso foi armazenada no TempData corretamente
            Assert.Equal("Contact created sucessfully", controller.TempData["SuccessMessage"]);

            // Verifica se o método ToAdd foi chamado uma vez
            mockContactRepositorie.Verify(repo => repo.ToAdd(contact), Times.Once);
        }

        /// <summary>
        /// Testa o método `Edit` para garantir que um contato editado seja salvo corretamente.
        /// </summary>
        [Fact(DisplayName = "ContactController: Edit should return success message on successful edit")]
        public void Edit_Post_ReturnsSuccessMessage_OnSuccessfulEdit()
        {
            // Arrange
            var contact = new ContactsModel
            {
                Id = 1,
                Name = "Updated Contact",
                Email = "updatedcontact@example.com",
                Phone = "987654321"
            };

            // Mock do repositório
            _mockContactRepositorie.Setup(repo => repo.Edit(It.IsAny<ContactsModel>())).Returns(contact);

            // Act
            var result = _controller.Edit(contact); // Passa o modelo atualizado

            // Assert
            var redirectResult = Assert.IsType<RedirectToActionResult>(result); // Verifica se é um redirecionamento
            Assert.Equal("Index", redirectResult.ActionName); // Verifica se o redirecionamento é para a página Index

            // Verifica se o método Edit foi chamado uma vez
            _mockContactRepositorie.Verify(repo => repo.Edit(contact), Times.Once);

        }
    }
}