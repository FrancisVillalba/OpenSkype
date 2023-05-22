using OpenSky.Clases;
using OpenSky.Models;
using OpenSky.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace OpenSky.Controllers
{
    public class SalesController : Controller
    {
        SalesBLL sales = new SalesBLL();
        CustomersBLL customers = new CustomersBLL();
        ArticlesBLL articles = new ArticlesBLL();
        TimbradoBLL timbrado = new TimbradoBLL();
        BranchBLL branch = new BranchBLL();
        CurrencyTypeBLL currency = new CurrencyTypeBLL();
        ThousandsSeparatorBLL thousands = new ThousandsSeparatorBLL();
        PaymentTypeBLL paymentType = new PaymentTypeBLL();
        PaymentsBLL payments = new PaymentsBLL();

        public ActionResult Sales()
        {
            if (Session["user_date"] == null)
            {
                return Redirect("/Home/Index");
            }

            var list = new List<SalesListDto>();
            var dt = sales.get_list_sales("limp", "");

            list = dt.AsEnumerable()
                        .Select(row => new SalesListDto
                        {
                            Id = row.Field<Guid>("Id").ToString(),
                            Process_number = row.Field<string>("ProcessNumber"),
                            Invoices_number = row.Field<string>("InvoicesNumber"),
                            Timbrado = row.Field<string>("Timbrado"),
                            DateInvoices = row.Field<string>("DateInvoices"),
                            AmountInvoices = thousands.ThousandsSeparator(row.Field<double>("TotalInvoiceAmount"), row.Field<string>("CurrencyTypeId")),
                            Customer = row.Field<string>("Customer"),
                            Branch = row.Field<string>("Branch"),
                            User = row.Field<string>("UserName"),
                            Status = row.Field<string>("Status"),
                            DateCreated = row.Field<string>("DateCreated")
                        }).ToList();

            return View(list);
        }
        public async Task<ActionResult> Sales_creation()
        {
            if (Session["user_date"] == null)
            {
                return Redirect("/Home/Index");
            }

            var listCustomer = await customers.list_custommer();
            ViewBag.listCustomers = listCustomer;

            var listArticles = await articles.list_articles();
            ViewBag.listArticles = listArticles;

            var listSalesConditions = await sales.list_sales_conditions();
            ViewBag.listSalesConditions = listSalesConditions;

            var listCurrencyType = await currency.list_currency_type();
            ViewBag.listCurrencyType = listCurrencyType;

            var listPaymentType = await paymentType.list_payment_type();
            ViewBag.listPaymentType = listPaymentType;

            return View();
        }
        public async Task<ActionResult> Sales_update(string Id)
        {
            if (Session["user_date"] == null)
            {
                return Redirect("/Home/Index");
            }
            var list = new List<SalesDetailListDto>();
            var v_currencyTypeId = "";

            //datos de la cabecera de facturas
            var responseDtoHeader = await sales.get_invoices_header_id(Guid.Parse(Id));
            foreach (var dto in responseDtoHeader)
            {
                v_currencyTypeId = dto.CurrencyTypeId;  
                ViewData["dtoHeader"] = new InvoicesUpdateRequestDto
                {
                    CurrencyTypeId = dto.CurrencyTypeId,
                    CustomerId = dto.CustomerId.ToString(),
                    DateInvoices = dto.DateInvoices.ToString(),
                    InvoicesHeaderId = dto.Id,
                    InvoicesNumber = dto.InvoicesNumber,
                    ProcessNumber = dto.ProcessNumber,
                    SalesConditionId = dto.SalesConditionId.ToString(),
                    TotalInvoiceAmount = dto.TotalInvoiceAmount.ToString()
                };
            };


            //datos del detalle de las facturas
            var dt = sales.get_data_invoices_details_id(Guid.Parse(Id));
            list = dt.AsEnumerable()
                 .Select(row => new SalesDetailListDto
                 {
                     Item = row.Field<int>("NumberItems").ToString(),
                     Article = row.Field<string>("nameArticle"),
                     AmountArticles = thousands.ThousandsSeparator(row.Field<double>("AmountArticles"), v_currencyTypeId),
                     Impuesto = thousands.ThousandsSeparator(row.Field<double>("PriceUnit"), v_currencyTypeId),
                     PriceUnit = row.Field<string>("nameImpuesto"),
                     TotalPrice = thousands.ThousandsSeparator(row.Field<double>("PriceTotal"), v_currencyTypeId)
                 }).ToList();
            //ViewBag.dtoDetails = list;
            ViewData["dtoDetails"] = list;


            //Datos del pago 
            var responseDtoPayment = await payments.get_payment_invoices_id(Guid.Parse(Id));
            foreach (var payment in responseDtoPayment)
            {
                ViewData["dtoPayment"] = new PaymentsRequestDto
                {
                    InvoiceHeaderId = (Guid)payment.InvoiceHeaderId,
                    PaymentTypeId = payment.PaymentTypeId,
                    AmountPaid = (Double)payment.AmountPaid,
                    Change = (Double)payment.Change,
                    VoucherNumber = payment.VoucherNumber
                };

                //ViewBag.dtoPayment = dtoPayment;
            };


            var listCustomer = await customers.list_custommer();
            ViewBag.listCustomers = listCustomer;

            var listArticles = await articles.list_articles();
            ViewBag.listArticles = listArticles;

            var listSalesConditions = await sales.list_sales_conditions();
            ViewBag.listSalesConditions = listSalesConditions;

            var listCurrencyType = await currency.list_currency_type();
            ViewBag.listCurrencyType = listCurrencyType;

            var listPaymentType = await paymentType.list_payment_type();
            ViewBag.listPaymentType = listPaymentType;

            return View();
        }
        public async Task<ActionResult> Insert_data_invoices_header(InvoicesRequestDto dto)
        {
            if (Session["user_date"] == null)
            {
                return Redirect("/Home/Index");
            }

            var executionStatus = new ExecutionStatusInvoices();

            try
            {
                var invoicesHeaderId = dto.InvoicesCreated == null ? Guid.NewGuid() : dto.InvoicesHeaderId;

                if (dto.InvoicesCreated == null)
                {
                    User user = (User)Session["user_date"];
                    var timbradoData = await timbrado.get_timbrado_branch_id(user.BranchCompanyId);
                    var branchCompany = await branch.get_id(user.BranchCompanyId);
                    var InvoicesNumberRange = sales.get_max_invoices_id(user.BranchCompanyId);
                    int numberRange = InvoicesNumberRange + 1;
                    var processNumber = branchCompany.BranchCompanyNumber + numberRange.ToString("D7");
                    var invoiceNumber = numberRange.ToString("D7");


                    var invoicesHeader = new os_invoices_header
                    {
                        Id = invoicesHeaderId,
                        ProcessNumber = processNumber,
                        InvoicesNumber = timbradoData.InvoicesPuntoExpedicion + "-" + timbradoData.InvoicesPuntoEstablecimiento + "-" + invoiceNumber,
                        InvoicesNumberRange = numberRange,
                        DateInvoices = Convert.ToDateTime(dto.InvoicesDate),
                        SalesConditionId = dto.SalesConditionsId,
                        CustomerId = dto.CustomerId,
                        BranchCompanyId = user.BranchCompanyId,
                        TimbradoDataId = timbradoData.Id,
                        UserCreatedId = user.UserId,
                        DateCreated = DateTime.Now,
                        CurrencyTypeId = dto.CurrencyTypeId,
                        Status = "A"
                    };

                    var responseDdto = await sales.insert_invoices_header_data(invoicesHeader);
                }

                executionStatus.status = true;
                executionStatus.InvoicesHeaderId = invoicesHeaderId;

                /***********************************************************************/
            }
            catch (Exception ex)
            {
                executionStatus.status = false;
                executionStatus.message = ex.Message;
            }

            return Json(executionStatus);
        }
        public async Task<ActionResult> Insert_data_invoices_detatail(InvoicesDetailRequestDto dto)
        {
            if (Session["user_date"] == null)
            {
                return Redirect("/Home/Index");
            }

            var executionStatus = new ExecutionStatusInvoices();

            try
            {
                User user = (User)Session["user_date"];

                /********************************************************************/

                var articlesDto = await articles.get_by_id(dto.ArticlesId);
                var invoicesDetais = sales.get_max_invoices_details_id(dto.InvoicesHeaderId);
                var invocesDetails = new os_invoices_detail
                {
                    Id = Guid.NewGuid(),
                    ArticlesId = dto.ArticlesId,
                    InvoicesHeaderId = dto.InvoicesHeaderId,
                    IvaId = articlesDto.IvaId,
                    NumberItems = invoicesDetais + 1,
                    AmountArticles = dto.ArticlesAmount,
                    PriceUnit = Convert.ToDouble(dto.ArticlesPrice),
                    PriceTotal = Convert.ToDouble(dto.ArticlesPrice) * dto.ArticlesAmount,
                    UserCreated = user.UserId,
                    DateCreated = DateTime.Now
                };

                var responseInsert = await sales.insert_invoices_detail_data(invocesDetails);

                /***********************************************************************/

                var price = await sales.get_total_invoices_price(dto.InvoicesHeaderId);

                executionStatus.status = true;
                executionStatus.InvoicesHeaderId = dto.InvoicesHeaderId;
                executionStatus.TotalInvoiceAmount = thousands.ThousandsSeparator((double)price, dto.CurrencyTypeId);
            }
            catch (Exception ex)
            {
                executionStatus.status = false;
                executionStatus.message = ex.Message;
            }

            return Json(executionStatus);
        }
        public async Task<JsonResult> Get_detail_invoices_id(InvoicesDetailTableRequestDto dto)
        {
            var list = new List<SalesDetailListDto>();
            try
            {
                var dt = sales.get_data_invoices_details_id(dto.InvoicesHeaderId);

                list = dt.AsEnumerable()
                     .Select(row => new SalesDetailListDto
                     {
                         Item = row.Field<int>("NumberItems").ToString(),
                         Article = row.Field<string>("nameArticle"),
                         AmountArticles = thousands.ThousandsSeparator(row.Field<double>("AmountArticles"), dto.CurrencyTypeId),
                         Impuesto = thousands.ThousandsSeparator(row.Field<double>("PriceUnit"), dto.CurrencyTypeId),
                         PriceUnit = row.Field<string>("nameImpuesto"),
                         TotalPrice = thousands.ThousandsSeparator(row.Field<double>("PriceTotal"), dto.CurrencyTypeId)
                     }).ToList();


                var data = await sales.update_total_invoice_amount(dto.InvoicesHeaderId, float.Parse(dto.TotalInvoiceAmount.ToString().Replace(".", "")));


                return Json(list, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }
        public async Task<ActionResult> Delete_detail_invoices(DeleteDetailInvoicesRequest dto)
        {
            if (Session["user_date"] == null)
            {
                return Redirect("/Home/Index");
            }

            var executionStatus = new ExecutionStatusInvoices();

            try
            {
                var responseDto = await sales.delete_detail_invoicet(dto);

                executionStatus.status = true;
            }
            catch (Exception ex)
            {
                executionStatus.status = false;
                executionStatus.message = ex.Message;
            }

            return Json(executionStatus);
        }
        public JsonResult SalesFilter(string filter, string data)
        {
            var list = new List<SalesListDto>();
            var dt = sales.get_list_sales(filter, data);

            list = dt.AsEnumerable()
                        .Select(row => new SalesListDto
                        {
                            Process_number = row.Field<string>("ProcessNumber"),
                            Invoices_number = row.Field<string>("InvoicesNumber"),
                            Timbrado = row.Field<string>("Timbrado"),
                            DateInvoices = row.Field<string>("DateInvoices"),
                            AmountInvoices = thousands.ThousandsSeparator(row.Field<double>("TotalInvoiceAmount"), row.Field<string>("CurrencyTypeId")),
                            Customer = row.Field<string>("Customer"),
                            Branch = row.Field<string>("Branch"),
                            User = row.Field<string>("UserName"),
                            Status = row.Field<string>("Status"),
                            DateCreated = row.Field<string>("DateCreated")
                        }).ToList();

            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}