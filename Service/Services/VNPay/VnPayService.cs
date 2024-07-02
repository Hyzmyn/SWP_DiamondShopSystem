using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Repository.Models;
using Service.ViewModel;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Services.VNPay
{
    public class VnPayService : IVnPayService
    {
        private readonly IConfiguration _config;
        private readonly ILogger<VnPayService> _logger;

        public VnPayService(IConfiguration config, ILogger<VnPayService> logger)
        {
            _config = config;
            _logger = logger;
        }

        public string CreatePaymentUrl(HttpContext context, Order model)
        {
            try
            {
                var tick = DateTime.Now.Ticks.ToString();
                var vnpay = new VnPayLibrary();

                vnpay.AddRequestData("vnp_Version", _config["VnPay:Version"]);
                vnpay.AddRequestData("vnp_Command", _config["VnPay:Command"]);
                vnpay.AddRequestData("vnp_TmnCode", _config["VnPay:TmnCode"]);
                vnpay.AddRequestData("vnp_Amount", Convert.ToInt64(model.TotalPrice * 2450000).ToString());
                vnpay.AddRequestData("vnp_CreateDate", model.TimeOrder.ToString("yyyyMMddHHmmss"));
                vnpay.AddRequestData("vnp_CurrCode", _config["VnPay:CurrCode"]);
                vnpay.AddRequestData("vnp_IpAddr", Utils.GetIpAddress(context));
                vnpay.AddRequestData("vnp_Locale", _config["VnPay:Local"]);
                vnpay.AddRequestData("vnp_OrderInfo", "Thanh toan don hang:" + model.OrderID);
                vnpay.AddRequestData("vnp_OrderType", "other"); 
                vnpay.AddRequestData("vnp_ReturnUrl", _config["VnPay:PaymentBackUrl"]);
                vnpay.AddRequestData("vnp_TxnRef", model.OrderID.ToString() + tick);

                var paymentUrl = vnpay.CreateRequestUrl(_config["VnPay:BaseUrl"], _config["VnPay:HashSecret"]);
                return paymentUrl;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating VNPay payment URL");
                throw;
            }
        }

        public VnPaymentResponseModel PaymentExecute(IQueryCollection collections)
        {
            try
            {
                var vnpay = new VnPayLibrary();
                foreach (var (key, value) in collections)
                {
                    if (!string.IsNullOrEmpty(key) && key.StartsWith("vnp_"))
                    {
                        vnpay.AddResponseData(key, value.ToString());
                    }
                }

                
              
                var vnp_SecureHash = collections.FirstOrDefault(p => p.Key == "vnp_SecureHash").Value;
                var vnp_ResponseCode = vnpay.GetResponseData("vnp_ResponseCode");
                var vnp_OrderInfo = vnpay.GetResponseData("vnp_OrderInfo");
                var vnp_Amount = Convert.ToInt64(vnpay.GetResponseData("vnp_Amount"));

                bool checkSignature = vnpay.ValidateSignature(vnp_SecureHash, _config["VnPay:HashSecret"]);
                if (!checkSignature)
                {
                    _logger.LogWarning("Invalid VNPay signature");
                    return new VnPaymentResponseModel()
                    {
                        Success = false
                    };
                }

                return new VnPaymentResponseModel
                {
                    Success = true,
                    PaymentMethod = "VnPay",
                    OrderDescription = vnp_OrderInfo,
                  
                    Token = vnp_SecureHash,
                    VnPayResponseCode = vnp_ResponseCode,
                    Amount = vnp_Amount
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error executing VNPay payment");
                throw;
            }
        }
    }
}
