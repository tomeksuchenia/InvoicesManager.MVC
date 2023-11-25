﻿using InvoicesManagerWebApp.Models;

namespace InvoicesManagerWebApp.Interface
{
    public interface IInvoiceRepository
    {
        Task<IEnumerable<Invoice>> GetAll();
        Task<Invoice> GetById(int id);
        Task Add(Invoice invoice);

        Task Update(Invoice invoice);

        Task Delete(Invoice invoice);
    }
}