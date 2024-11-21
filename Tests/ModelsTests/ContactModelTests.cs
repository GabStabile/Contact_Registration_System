using ContactsControl.Models;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Xunit;

namespace Tests.ModelsTests
{
    public class ContactsModelTests
    {
        /// <summary>
        /// Testa se o modelo `ContactsModel` passa na validação quando todos os campos obrigatórios são preenchidos corretamente
        /// </summary>
        [Fact(DisplayName = "ContactsModel: All fields are valid")]
        public void ContactsModel_AllFieldsValid_ShouldPassValidation()
        {
            // Arrange
            var model = new ContactsModel
            {
                Name = "Valid Name",
                Email = "valid.email@example.com",
                Phone = "123456789"
            };

            // Act
            var validationResults = ValidateModel(model);

            // Assert
            Assert.Empty(validationResults); // No validation errors
        }

        /// <summary>
        /// Testa se o modelo `ContactsModel` falha na validação quando os campos obrigatórios não são preenchidos
        /// </summary>
        [Fact(DisplayName = "ContactsModel: Missing required fields")]
        public void ContactsModel_MissingRequiredFields_ShouldFailValidation()
        {
            // Arrange
            var model = new ContactsModel(); // Missing all required fields

            // Act
            var validationResults = ValidateModel(model);

            // Assert
            Assert.NotEmpty(validationResults);
            
            Assert.Contains(validationResults, v => v.ErrorMessage.Contains("Enter name by contact"));
            
            Assert.Contains(validationResults, v => v.ErrorMessage.Contains("Enter e-mail by contact"));
            
            Assert.Contains(validationResults, v => v.ErrorMessage.Contains("Enter contact phone number"));
        }
        /// <summary>
        /// Helper method que valida o modelo usando `ValidationContext` e retorna os erros encontrados
        /// </summary>
        private IList<ValidationResult> ValidateModel(object model)
        {
            var validationResults = new List<ValidationResult>();
            
            var validationContext = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, validationContext, validationResults, true);
            
            return validationResults;
        }
    }
}