using FakeItEasy;
using FluentAssertions;
using InvoicesManagerWebApp.Data;
using InvoicesManagerWebApp.Models;
using InvoicesManagerWebApp.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace InvoicesManagerWebApp.Tests.Repository
{
    public class InvoiceRepositoryTests
    {
        private IHttpContextAccessor _httpContextAccessor;
        public InvoiceRepositoryTests()
        {
            _httpContextAccessor = A.Fake<IHttpContextAccessor>();
        }
        private async Task<ApplicationDbContext> GetDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new ApplicationDbContext(options);
            databaseContext.Database.EnsureCreated();
            if (await databaseContext.Invoices.CountAsync() == 0)
            {
                for (int i = 0; i < 10; i++)
                {
                    databaseContext.Invoices.Add(
                        new Invoice()
                        {
                            InvoiceDate = DateTime.Now,
                            InvoiceCode = i.ToString(),
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
                                    InvoiceId = i,
                                }
                            },
                            UserId = Guid.NewGuid().ToString(),
                        });
                    await databaseContext.SaveChangesAsync();
                }
            }
            return databaseContext;
        }

        [Fact]
        public async Task invoiceRepository_invoke_GetById_should_return_invoice()
        {
            //arrange
            var id = 1;
            var dbContext = await GetDbContext();
            var invoiceRepository = new InvoiceRepository(dbContext, _httpContextAccessor);
            //act
            var result = await invoiceRepository.GetById(id);
            //assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Invoice>();
        }

        [Fact]
        public async Task invoiceRepository_invoke_GetAll_should_return_invoices_list()
        {
            //arrange
            var dbContext = await GetDbContext();
            var invoiceRepository = new InvoiceRepository(dbContext, _httpContextAccessor);
            //act
            var result = await invoiceRepository.GetAll();
            //assert
            result.Should().NotBeNull();
            result.Should().BeOfType<List<Invoice>>();
        }

        [Fact]
        public async Task invoiceRepository_invoke_Add_invoice_should_be_added()
        {
            //arrange
            var dbContext = await GetDbContext();
            var initialInvoiceCount = dbContext.Invoices.Count();   
            var invoiceRepository = new InvoiceRepository(dbContext, _httpContextAccessor);
            var invoice = new Invoice()
            {
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
            //act
            await invoiceRepository.Add(invoice);
            var afterAddInvoiceCount = await dbContext.Invoices.CountAsync();
            //assert
            afterAddInvoiceCount.Should().Be(initialInvoiceCount + 1);
        }

        [Fact]
        public async Task invoiceRepository_invoke_delete_invoice_should_be_deleted()
        {
            //arrange
            var dbContext = await GetDbContext();
            var initialInvoiceCount = dbContext.Invoices.Count();
            var invoice = dbContext.Invoices.First();
            var invoiceRepository = new InvoiceRepository(dbContext, _httpContextAccessor);
            //act
            await invoiceRepository.Delete(invoice);
            var afterDeleteInvoiceCount = dbContext.Invoices.Count();
            //assert
            afterDeleteInvoiceCount.Should().Be(initialInvoiceCount - 1);
        }

        [Fact]
        public async Task invoiceRepository_invoke_update_invoice_should_be_updated()
        {
            //arrange
            var dbContext = await GetDbContext();
            var initialInvoiceCount = dbContext.Invoices.Count();
            var invoice = dbContext.Invoices.First();
            var oldInvoiceTotal = invoice.Total;
            var invoiceRepository = new InvoiceRepository(dbContext, _httpContextAccessor);
            //act
            invoice.Total = invoice.Total + 1;
            await invoiceRepository.Update(invoice);
            var invoiceAfterUpdate = dbContext.Invoices.First();
            //assert
            invoiceAfterUpdate.Total.Should().Be(oldInvoiceTotal + 1);
        }

    }
}
