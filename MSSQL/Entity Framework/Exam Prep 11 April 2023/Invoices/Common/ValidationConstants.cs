using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoices.Common
{
    public static  class ValidationConstants
    {
        //Product
        public const int ProductNameMinLength = 9;
        public const int ProductNameMaxLength = 30;
        public const decimal ProductPriceMin = 5;
        public const decimal ProductPriceMax = 1000;

        //Address
        public const int AddressStreetNameMaxLength = 20;
        public const int AddressStreetNameMinLength = 10;
        public const int AddressCityMaxLength = 15;
        public const int AddressCityMinLength = 5;
        public const int AddressCountryMaxLength = 15;
        public const int AddressCountryMinLength = 5;
        //Client
        public const int ClientNameMaxLength = 25;
        public const int ClientNameMinLength = 10;
        public const int ClientNumberVatMaxLength = 15;
        public const int ClientNumberVatMinLength = 10;
        //Invoice
        public const int InvoiceNumberMinLength = 1_000_000_000;
        public const int InvoiceNumberMaxLength = 1_500_000_000;

    }
}
