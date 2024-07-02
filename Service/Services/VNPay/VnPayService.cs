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
                var vnpay = new VnPayLibrary();

                vnpay.AddRequestData("vnp_Version", _config["VnPay:Version"]);
                vnpay.AddRequestData("vnp_Command", _config["VnPay:Command"]);
                vnpay.AddRequestData("vnp_TmnCode", _config["VnPay:TmnCode"]);
                vnpay.AddRequestData("vnp_Amount", Convert.ToInt32(model.TotalPrice * 24500).ToString());
                vnpay.AddRequestData("vnp_CreateDate", model.TimeOrder.ToString("yyyyMMddHHmmss"));
                vnpay.AddRequestData("vnp_CurrCode", _config["VnPay:CurrCode"]);
                vnpay.AddRequestData("vnp_IpAddr", Utils.GetIpAddress(context));
                vnpay.AddRequestData("vnp_Locale", _config["VnPay:Local"]);
                vnpay.AddRequestData("vnp_OrderInfo", "Thanh toan don hang:" + model.OrderID);
                vnpay.AddRequestData("vnp_OrderType", "other"); //default value: other
                vnpay.AddRequestData("vnp_ReturnUrl", _config["VnPay:PaymentBackUrl"]);
                vnpay.AddRequestData("vnp_TxnRef", model.OrderID.ToString());

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

                var vnp_orderId = Convert.ToInt32(vnpay.GetResponseData("vnp_TxnRef"));
                var vnp_TransactionId = Convert.ToInt32(vnpay.GetResponseData("vnp_TransactionNo"));
                var vnp_SecureHash = collections.FirstOrDefault(p => p.Key == "vnp_SecureHash").Value;
                var vnp_ResponseCode = vnpay.GetResponseData("vnp_ResponseCode");
                var vnp_OrderInfo = vnpay.GetResponseData("vnp_OrderInfo");
                var vnp_Amount = Convert.ToInt32(vnpay.GetResponseData("vnp_Amount"));

                bool checkSignature = vnpay.ValidateSignature(vnp_SecureHash, _config["VnPay:HashSecret"]);
                if (!checkSignature)
                {
                    _logger.LogWarning("Invalid VNPay signature for OrderID: {OrderID}", vnp_orderId);
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
                    OrderId = (int)vnp_orderId,
                    TransactionId = vnp_TransactionId.ToString(),
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
