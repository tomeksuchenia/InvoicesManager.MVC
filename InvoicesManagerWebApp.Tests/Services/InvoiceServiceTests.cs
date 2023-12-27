using FakeItEasy;
using FluentAssertions;
using InvoicesManagerWebApp.Interface;
using InvoicesManagerWebApp.Models;
using InvoicesManagerWebApp.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace InvoicesManagerWebApp.Tests.Services
{
    public class InvoiceServiceTests
    {
        [Fact]
        public async Task when_invoking_getById__this_invoke_get_async_on_invoice_repostory()
        {
            //Arrange
            var invoice = new Invoice()
            {
                Id = 1,
                InvoiceDate = DateTime.Now,
                InvoiceCode = 1.ToString(),
                Notes = null,
                Total = 1230,
                Status = Data.Enum.Status.Paid,
                Currency = Data.Enum.Currency.PLN,
                Customer = null,
                PaymentMethod = Data.Enum.PaymentMethod.Cash,
                Items = new List<Item>()
                    {
                        new Item()
                        {
                            Name = "Bike",
                            Price = 1000,
                            Quantity = 1,
                            Vat = 23,
                            InvoiceId = 1,
                        }
                    },
                UserId = Guid.NewGuid().ToString()
            }; 

            var invoiceRepositoryMock = new Mock<IInvoiceRepository>();
            var invoiceService = new InvoiceService(invoiceRepositoryMock.Object);
            invoiceRepositoryMock.Setup(x => x.GetById(invoice.Id)).ReturnsAsync(invoice);
            //Act
            var returnInvoice = await invoiceService.GetById(invoice.Id);
            //Assert
            invoiceRepositoryMock.Verify(x => x.GetById(invoice.Id), Moq.Times.Once());
            returnInvoice.Should().NotBeNull();
            returnInvoice.Total.Should().Be(invoice.Total);
        }

        [Fact]
        public async Task when_invoking_getAll_this_invoke_getAll_on_invoice_repostory()
        {
            //Arrange
            var invoiceRepositoryMock = new Mock<IInvoiceRepository>();
            var invoiceService = new InvoiceService(invoiceRepositoryMock.Object);
            //Act
            var returnInvoice = await invoiceService.GetAll();
            //Assert
            invoiceRepositoryMock.Verify(x => x.GetAll(), Moq.Times.Once());
        }

        [Fact]
        public async Task when_invoking_add_this_invoke_add_on_invoice_repository()
        {
            var invoice = new Invoice()
            {
                Id = 1,
                InvoiceDate = DateTime.Now,
                InvoiceCode = 1.ToString(),
                Notes = null,
                Total = 1230,
                Status = Data.Enum.Status.Paid,
                Currency = Data.Enum.Currency.PLN,
                Customer = null,
                PaymentMethod = Data.Enum.PaymentMethod.Cash,
                Items = new List<Item>()
                    {
                        new Item()
                        {
                            Name = "Bike",
                            Price = 1000,
                            Quantity = 1,
                            Vat = 23,
                            InvoiceId = 1,
                        }
                    },
                UserId = Guid.NewGuid().ToString()
            };

            //Arrange
            var invoiceRepositoryMock = new Mock<IInvoiceRepository>();
            var invoiceService = new InvoiceService(invoiceRepositoryMock.Object);
            //Act
            await invoiceService.Add(invoice);
            //Assert
            invoiceRepositoryMock.Verify(x => x.Add(invoice), Moq.Times.Once());
        }
    }
}
