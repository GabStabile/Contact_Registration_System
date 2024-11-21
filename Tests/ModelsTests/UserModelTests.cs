using ContactsControl.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Xunit;

namespace Tests.ModelsTests
{
    public class UsersModelTests
    {
        /// <summary>
        /// Testa se o modelo `UsersModel` passa na validação quando todos os campos obrigatórios são preenchidos corretamente.
        /// </summary>
        [Fact(DisplayName = "UsersModel: All fields are valid")]
        public void UsersModel_AllFieldsValid_ShouldPassValidation()
        {
            // Arrange
            var model = new UsersModel
            {
                Name = "Valid User",
                Login = "userlogin",
                Password = "securepassword",
                Email = "user@example.com",
                Profile = ContactsControl.Enums.EnumProfile.Admin,
                RegistrationDate = DateTime.Now
            };

            // Act
            var validationResults = ValidateModel(model);

            // Assert
            Assert.Empty(validationResults); // No validation errors
        }

        /// <summary>
        /// Testa se o modelo `UsersModel` falha na validação quando os campos obrigatórios não são preenchidos
        /// </summary>
        [Fact(DisplayName = "UsersModel: Missing required fields")]
        public void UsersModel_MissingRequiredFields_ShouldFailValidation()
        {
            // Arrange
            var model = new UsersModel(); // Missing all required fields

            // Act
            var validationResults = ValidateModel(model);

            // Assert
            Assert.NotEmpty(validationResults);

            Assert.Contains(validationResults, v => v.ErrorMessage.Contains("Enter user name"));

            Assert.Contains(validationResults, v => v.ErrorMessage.Contains("Enter user login"));

            Assert.Contains(validationResults, v => v.ErrorMessage.Contains("Enter user password"));

            Assert.Contains(validationResults, v => v.ErrorMessage.Contains("Enter user e-mail"));

            Assert.Contains(validationResults, v => v.ErrorMessage.Contains("Select user profile"));
        }

        /// <summary>
        /// Testa se o modelo `UsersModel` falha na validação quando o campo `Email` possui um formato inválido
        /// </summary>
        [Fact(DisplayName = "UsersModel: Invalid email format")]
        public void UsersModel_InvalidEmailFormat_ShouldFailValidation()
        {
            // Arrange
            var model = new UsersModel
            {
                Name = "Valid User",
                Login = "userlogin",
                Password = "securepassword",
                Email = "invalid-email",
                Profile = ContactsControl.Enums.EnumProfile.Admin
            };

            // Act
            var validationResults = ValidateModel(model);

            // Assert
            Assert.NotEmpty(validationResults);
            Assert.Contains(validationResults, v => v.ErrorMessage.Contains("E-mail information is not valid"));
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