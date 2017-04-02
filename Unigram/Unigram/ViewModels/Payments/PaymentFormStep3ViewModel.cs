﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Api.Aggregator;
using Telegram.Api.Services;
using Telegram.Api.Services.Cache;
using Telegram.Api.TL;
using Unigram.Common;
using Unigram.Core.Models;
using Windows.Data.Json;
using Windows.UI.Xaml.Navigation;

namespace Unigram.ViewModels.Payments
{
    public class PaymentFormStep3ViewModel : PaymentFormViewModelBase
    {
        private string _publishableKey;

        public PaymentFormStep3ViewModel(IMTProtoService protoService, ICacheService cacheService, ITelegramEventAggregator aggregator) 
            : base(protoService, cacheService, aggregator)
        {
        }

        public override Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            var buffer = parameter as byte[];
            if (buffer != null)
            {
                using (var from = new TLBinaryReader(buffer))
                {
                    var tuple = new TLTuple<TLMessage, TLPaymentsPaymentForm, TLPaymentsValidatedRequestedInfo>(from);

                    Message = tuple.Item1;
                    Invoice = tuple.Item1.Media as TLMessageMediaInvoice;
                    PaymentForm = tuple.Item2;

                    if (_paymentForm.HasNativeProvider && _paymentForm.HasNativeParams && _paymentForm.NativeProvider.Equals("stripe"))
                    {
                        var json = JsonObject.Parse(_paymentForm.NativeParams.Data);

                        NeedCountry = json.GetNamedBoolean("need_country", false);
                        NeedZip = json.GetNamedBoolean("need_zip", false);
                        NeedCardholderName = json.GetNamedBoolean("need_cardholder_name", false);

                        _publishableKey = json.GetNamedString("publishable_key", string.Empty);
                    }
                    else
                    {
                        // TODO: webview
                    }

                    //var info = PaymentForm.HasSavedInfo ? PaymentForm.SavedInfo : new TLPaymentRequestedInfo();
                    //if (info.ShippingAddress == null)
                    //{
                    //    info.ShippingAddress = new TLPostAddress();
                    //}

                    //Info = info;
                    //SelectedCountry = null;
                }
            }

            return Task.CompletedTask;
        }

        public bool NeedCountry { get; private set; }

        public bool NeedZip { get; private set; }

        public bool NeedCardholderName { get; private set; }

        public bool NeedZipOrCountry
        {
            get
            {
                return NeedZip || NeedCountry;
            }
        }

        public List<KeyedList<string, Country>> Countries { get; } = Country.GroupedCountries;

        private Country _selectedCountry = Country.Countries[0];
        public Country SelectedCountry
        {
            get
            {
                return _selectedCountry;
            }
            set
            {
                Set(ref _selectedCountry, value);
            }
        }

        private bool? _isSave = true;
        public bool? IsSave
        {
            get
            {
                return _isSave;
            }
            set
            {
                Set(ref _isSave, value);
            }
        }

        private RelayCommand _sendCommand;
        public RelayCommand SendCommand => _sendCommand = _sendCommand ?? new RelayCommand(SendExecute, () => !IsLoading);
        private async void SendExecute()
        {
            IsLoading = true;

            //var myToken = new StripeTokenCreateOptions();

            //// if you need this...
            //myToken.Card = new StripeCreditCardOptions()
            //{
            //    // set these properties if passing full card details (do not
            //    // set these properties if you set TokenId)
            //    Number = "4242424242424242",
            //    ExpirationYear = "2022",
            //    ExpirationMonth = "10",
            //    AddressCountry = "US",                // optional
            //    AddressLine1 = "24 Beef Flank St",    // optional
            //    AddressLine2 = "Apt 24",              // optional
            //    AddressCity = "Biggie Smalls",        // optional
            //    AddressState = "NC",                  // optional
            //    AddressZip = "27617",                 // optional
            //    Name = "Joe Meatballs",               // optional
            //    Cvc = "1223"                          // optional
            //};

            //var tokenService = new StripeTokenService(_publishableKey);
            //var stripeToken = await tokenService.CreateAsync(myToken);

            Debugger.Break();

            //var save = _isSave ?? false;
            //var info = new TLPaymentRequestedInfo();
            //if (_paymentForm.Invoice.IsNameRequested)
            //{
            //    info.Name = _info.Name;
            //}
            //if (_paymentForm.Invoice.IsEmailRequested)
            //{
            //    info.Email = _info.Email;
            //}
            //if (_paymentForm.Invoice.IsPhoneRequested)
            //{
            //    info.Phone = _info.Phone;
            //}
            //if (_paymentForm.Invoice.IsShippingAddressRequested)
            //{
            //    info.ShippingAddress = _info.ShippingAddress;
            //    info.ShippingAddress.CountryIso2 = _selectedCountry?.Code;
            //}

            //var response = await ProtoService.ValidateRequestedInfoAsync(save, _message.Id, info);
            //if (response.IsSucceeded)
            //{
            //    IsLoading = false;

            //    if (_paymentForm.HasSavedInfo && !save)
            //    {
            //        ProtoService.ClearSavedInfoAsync(true, false, null, null);
            //    }

            //    if (_paymentForm.Invoice.IsFlexible)
            //    {
            //        NavigationService.Navigate(typeof(PaymentFormStep2Page), TLTuple.Create(_message, _paymentForm, response.Result));
            //    }
            //    else if (_paymentForm.HasSavedCredentials)
            //    {
            //        // TODO: Is password expired?
            //        var expired = true;
            //        if (expired)
            //        {
            //            NavigationService.Navigate(typeof(PaymentFormStep4Page));
            //        }
            //        else
            //        {
            //            NavigationService.Navigate(typeof(PaymentFormStep5Page));
            //        }
            //    }
            //    else
            //    {
            //        NavigationService.Navigate(typeof(PaymentFormStep3Page));
            //    }
            //}
        }
    }
}
