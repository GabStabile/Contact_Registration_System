using ContactsControl.Data;
using ContactsControl.Models;
using ContactsControl.Repositorie;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Tests.RepositoriesTests
{
    public class ContactModelTests
    {
        [Fact(DisplayName = "Given valid contact, When ToAdd is called, Then contact should be added successfully")]
        public void ToAdd_ValidContact_AddsContact()
        {
            // Arrange
            var mockDbSet = new Mock<DbSet<ContactsModel>>();

            // Cria uma lista interna para simular a adição de elementos no DbSet.
            var contactList = new List<ContactsModel>();

            // Configura o mockDbSet para adicionar à lista interna quando Add for chamado
            mockDbSet.Setup(m => m.Add(It.IsAny<ContactsModel>())).Callback<ContactsModel>((contact) => contactList.Add(contact));

            // Cria um mock para o context do DB (BContext), simulando conexão com banco
            var mockContext = new Mock<BContext>();

            // Configura o mock do DbSet para quando o contexto for acessado, retorne o mockDbSet
            mockContext.Setup(c => c.DB_Contacts).Returns(mockDbSet.Object);

            // Instancia o repositório ContactRepositorie usando o contexto do mock
            var repository = new ContactRepositorie(mockContext.Object);

            // Contact model válido
            var validContact = new ContactsModel
            {
                Name = "Valid Name",
                Email = "valid.email@example.com",
                Phone = "123456789"
            };

            // Act
            var result = repository.ToAdd(validContact); // Executa o método ToAdd para adicionar o contato

            // Assert
            Assert.NotNull(result); // Verifica se o resultado não é nulo (se o contato foi adicionado corretamente)
            Assert.Equal(validContact.Name, result.Name); // Verifica se o nome do contato no resultado é igual ao nome do contato passado

            // Verifica se o método Add foi chamado uma vez no DbSet
            mockDbSet.Verify(m => m.Add(It.IsAny<ContactsModel>()), Times.Once);

            // Verifica se o método SaveChanges foi chamado uma vez no contexto
            mockContext.Verify(m => m.SaveChanges(), Times.Once);

            // Verifica se a lista interna do DbSet agora contém o contato adicionado
            Assert.Contains(contactList, c => c.Name == validContact.Name && c.Email == validContact.Email);
        }
    }
}
