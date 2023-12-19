using FakeItEasy;
using FakeItEasy.Sdk;
using FluentAssertions;
using InvoicesManagerWebApp.Controllers;
using InvoicesManagerWebApp.Interface;
using InvoicesManagerWebApp.Models;
using InvoicesManagerWebApp.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoicesManagerWebApp.Tests.Controller
{
    public class InvoiceControllerTests
    {
        private InvoicesController _invoiceController;
        private IInvoiceService _invoiceService;
        private IInvoiceRepository _invoiceRepository;
        private IHttpContextAccessor _httpContextAccessor;
        
        public InvoiceControllerTests()
        {
            _invoiceService = A.Fake<IInvoiceService>();
            _invoiceRepository= A.Fake<IInvoiceRepository>();
            _httpContextAccessor = A.Fake<IHttpContextAccessor>();

            _invoiceController = new InvoicesController(_invoiceService, _httpContextAccessor);
        }

        [Fact]
        public void invoiceController_index_returnSuccess()
        {
            //Arrange
            var invoices = A.Fake<IEnumerable<Invoice>>();
            A.CallTo(() => _invoiceService.GetAllUserInvoices()).Returns(invoices);
            //Act
            var result = _invoiceController.Index();
            //Assert
            result.Should().BeOfType<Task<IActionResult>>();
        }

        [Fact]
        public void invoiceController_details_returnSuccess()
        {
            //Arrange
            int id = 1;
            var invoice = A.Fake<Invoice>();
            A.CallTo(() => _invoiceService.GetInvoiceUserById(id)).Returns(invoice);
            //Act
            var result = _invoiceController.Details(id);
            //Assert
            result.Should().BeOfType<Task<IActionResult>>();
        }

        [Fact]
        public void invoiceController_create_returnSuccess()
        {
            //Arrange
            var invoiceVM = A.Fake<CreateInvoiceViewModel>();
            //Act
            var result = _invoiceController.Create(invoiceVM);
            //Assert
            result.Should().BeOfType<Task<IActionResult>>();
        }

        [Fact]
        public async Task invoiceController_create_and_modelStateInvalid_returnView()
        {
            //Arrange
            var invoiceVM = A.Fake<CreateInvoiceViewModel>();
            _invoiceController.ModelState.AddModelError("Items", "The Items field is required.");
            //Act
            var result = _invoiceController.Create(invoiceVM);
            var viewResult = await (result as Task<IActionResult>);
            //Assert
            viewResult.Should().BeOfType<ViewResult>().Which.ViewName.Should().BeNull();
        }

        [Fact]
        public async Task invoiceController_delete_and_invoice_not_exist_should_return_notFound()
        {
            //Arrange
            var id = 1;
            var invoice = A.Fake<Invoice>();
            A.CallTo(() => _invoiceService.GetInvoiceUserById(id)).Returns(Task.FromResult<Invoice>(null));
            //Act
            var result = _invoiceController.Delete(1);
            var viewResult = await (result as Task<IActionResult>);
            //Assert
            viewResult.Should().BeOfType<BadRequestResult>();
        }
    }
}
