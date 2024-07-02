using Microsoft.AspNetCore.Http;
using Repository.Models;
using Service.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.VNPay
{
    public interface IVnPayService
    {
        string CreatePaymentUrl(HttpContext context, Order model);
        VnPaymentResponseModel PaymentExecute(IQueryCollection collections);
    }
}
