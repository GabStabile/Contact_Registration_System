using ContactsControl.Controllers;
using ContactsControl.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Tests.ControllersTests
{
    public class HomeControllerTests
    {
        /// <summary>
        /// Testa se o método `Index` retorna a view correta.
        /// </summary>
        [Fact(DisplayName = "HomeController: Index should return a view")]
        public void Index_ReturnsViewResult()
        {
            // Arrange
            var controller = new HomeController();

            // Act
            var result = controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result); // Verifica se o resultado é uma View
            Assert.Null(viewResult.ViewName); // Verifica se o nome da view é nulo (porque a view padrão é usada)
        }

        /// <summary>
        /// Testa se o método `Privacy` retorna a view correta.
        /// </summary>
        [Fact(DisplayName = "HomeController: Privacy should return a view")]
        public void Privacy_ReturnsViewResult()
        {
            // Arrange
            var controller = new HomeController();

            // Act
            var result = controller.Privacy();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result); // Verifica se o resultado é uma View
            Assert.Null(viewResult.ViewName); // Verifica se o nome da view é nulo (porque a view padrão é usada)
        }

        /// <summary>
        /// Testa se o método `Error` retorna a view correta com um modelo `ErrorViewModel`
        /// </summary>
        [Fact(DisplayName = "HomeController: Error should return ErrorViewModel")]
        public void Error_ReturnsErrorViewModel()
        {
            // Arrange
            var controller = new HomeController();

            // Mock para garantir que HttpContext.TraceIdentifier não seja nulo
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };

            // Act
            var result = controller.Error();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result); // Verifica se o resultado é uma View
            var model = Assert.IsType<ErrorViewModel>(viewResult.Model); // Verifica se o modelo passado para a view é do tipo ErrorViewModel
            Assert.NotNull(model.RequestId); // Verifica se o RequestId não é nulo
        }
    }
}