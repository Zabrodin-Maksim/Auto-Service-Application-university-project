using Auto_Service_Application_university_project.Models;
using Auto_Service_Application_university_project.Services;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;


namespace Auto_Service_Application_university_project.ViewModels
{
    public class PaymentViewModel : ViewModelBase
    {

        #region Private Fields
        private readonly MainViewModel _mainViewModel;

        // Visibilities
        private Visibility _visibleNumberCard;
        private Visibility _visibilityCashTaken;

        // List
        private ObservableCollection<Bill> _bills;
        private Bill _selectedBill;

        //Combo Box
        private ObservableCollection<PaymentType> _paymentTypes;
        private PaymentType _selectedPaymentType;

        // Fields
        private string _numberCard;
        private string _cashTaken;

        // Error message
        private string _errorMessage;
        #endregion

        #region Properties
        public Visibility VisibleNumberCard { get => _visibleNumberCard; set => SetProperty(ref _visibleNumberCard, value, nameof(VisibleNumberCard)); }
        public Visibility VisibilityCashTaken { get => _visibilityCashTaken; set => SetProperty(ref _visibilityCashTaken, value, nameof(VisibilityCashTaken)); }

        public ObservableCollection<Bill> Bills { get => _bills; set => SetProperty(ref _bills, value, nameof(Bills)); }
        public Bill SelectedBills { get => _selectedBill; set => SetProperty(ref _selectedBill, value, nameof(SelectedBills)); }

        public ObservableCollection<PaymentType> PaymentTypes { get => _paymentTypes; set => SetProperty(ref _paymentTypes, value, nameof(PaymentTypes)); }
        public PaymentType PaymentTypeSelected { get => _selectedPaymentType; 
            set
            {
                SetProperty(ref _selectedPaymentType, value, nameof(PaymentTypeSelected));
                if (_selectedPaymentType != null)
                {
                    if (_selectedPaymentType.TypeName == "card")
                    {
                        VisibilityCashTaken = Visibility.Collapsed;
                        VisibleNumberCard = Visibility.Visible;
                    }
                    else
                    {
                        VisibleNumberCard = Visibility.Collapsed;
                        VisibilityCashTaken = Visibility.Visible;
                    }
                }
            }
        }

        public string NumberCard
        {
            get => _numberCard;
            set
            {
                if (value.All(char.IsDigit))
                {
                    if (value.Length <= 10)
                    {
                        SetProperty(ref _numberCard, value, nameof(NumberCard));
                    }
                    else
                    {
                        ErrorMessage = "Maximum of 10 digits!";
                    }
                }
                else
                {
                    ErrorMessage = "Enter numbers only!";
                }
            }
        }
        public string CashTaken
        {
            get => _cashTaken;
            set
            {
                if (value.All(char.IsDigit))
                {
                    if (value.Length <= 10)
                    {
                        SetProperty(ref _cashTaken, value, nameof(CashTaken));
                    }
                    else
                    {
                        ErrorMessage = "Maximum of 10 digits!";
                    }
                }
                else
                {
                    ErrorMessage = "Enter numbers only!";
                }
            }
        }

        public string ErrorMessage { get => _errorMessage; set => SetProperty(ref _errorMessage, value, nameof(ErrorMessage)); }
        #endregion

        #region Commands
        public ICommand PaidCommand { get; }
        public ICommand ClearCommand { get; }
        #endregion

        public PaymentViewModel(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;

            FillinOutAllLists();

            #region Init Commands
            PaidCommand = new MyICommand<object>(async parametr => await OnPaid(parametr));
            ClearCommand = new MyICommand(OnClear);
            #endregion
        }

        private async Task FillinOutAllLists()
        {
            Bills = await _mainViewModel.GetAllBills();
            PaymentTypes = await _mainViewModel.GetAllPaymentTypes();

            if (PaymentTypes != null && PaymentTypes.Count != 0)
            {
                PaymentTypeSelected = PaymentTypes[0];
            }
        }

        private async Task OnPaid(object parameter)
        {
            if (CheckInputs())
            {
                if (PaymentTypeSelected.TypeName.Equals("card", StringComparison.OrdinalIgnoreCase) ||
                    (PaymentTypeSelected.TypeName.Equals("cash", StringComparison.OrdinalIgnoreCase) && decimal.TryParse(CashTaken, out decimal cashAmount) && cashAmount >= SelectedBills.Price))
                {
                    // Проверка на null для всех связанных объектов
                    if (SelectedBills?.ServiceOffer?.Car?.Reservation?.Client == null)
                    {
                        ErrorMessage = "Не удалось загрузить связанные данные клиента.";
                        return;
                    }

                    // Загрузка клиента
                    var client = await _mainViewModel.GetClientById(SelectedBills.ServiceOffer.Car.Reservation.Client.ClientId);
                    if (client == null)
                    {
                        ErrorMessage = "Клиент не найден.";
                        return;
                    }

                    var payment = new Payment
                    {
                        Bill = SelectedBills,
                        PaymentType = PaymentTypeSelected,
                        Client = client
                    };

                    // Fill By Type Payment
                    if (PaymentTypeSelected.TypeName.Equals("card", StringComparison.OrdinalIgnoreCase))
                    {
                        if (!int.TryParse(NumberCard, out int numberCard))
                        {
                            ErrorMessage = "Неверный номер карты.";
                            return;
                        }

                        payment.Card = new Card
                        {
                            NumberCard = numberCard
                        };
                    }
                    else if (PaymentTypeSelected.TypeName.Equals("cash", StringComparison.OrdinalIgnoreCase))
                    {
                        if (!decimal.TryParse(CashTaken, out decimal cashTaken))
                        {
                            ErrorMessage = "Неверная сумма наличных.";
                            return;
                        }

                        payment.Cash = new Cash
                        {
                            Taken = cashTaken
                        };
                    }

                    if (payment.Card != null)
                        Debug.WriteLine($"[INFO]NumberCard: {payment.Card.NumberCard}");

                    if (payment.Cash != null)
                        Debug.WriteLine($"[INFO]CashTaken: {payment.Cash.Taken}");

                    
                    await _mainViewModel.AddPayment(payment);

                    if (PaymentTypeSelected.TypeName.Equals("cash", StringComparison.OrdinalIgnoreCase))
                    {
                        MessageBox.Show($"Must give change = {decimal.Parse(CashTaken) - SelectedBills.Price}.",
                                        "Change", MessageBoxButton.OK, MessageBoxImage.Information);
                    }


                    CreateBillDocument(payment.ToString() + "\n Total Price: " + SelectedBills.Price + "\n");

                    OnClear();
                }
                else
                {
                    ErrorMessage = "Need more money!!!";
                }
            }
        }


        private void OnClear() 
        {
            SelectedBills = null;

            NumberCard = "";
            CashTaken = "";

            ErrorMessage = "";
        }

        private bool CheckInputs()
        {
            if (SelectedBills != null)
            {
                if (PaymentTypeSelected.TypeName == "card" && !string.IsNullOrEmpty(NumberCard) || PaymentTypeSelected.TypeName == "cash" && !string.IsNullOrEmpty(CashTaken))
                {
                    return true;
                }
                else
                {
                    ErrorMessage = "Fill field!";
                }
            }
            else
            {
                ErrorMessage = "Select Bill!";
            }

            return false;
        }

        public void CreateBillDocument(string content)
        {
            string filePath = "Bill.docx";

            try
            {
                if (File.Exists(filePath))
                {
                    // If the file exists, open and append content
                    using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(filePath, true))
                    {
                        var body = wordDoc.MainDocumentPart.Document.Body;
                        body.AppendChild(new Paragraph(new Run(new Text(content))));
                        wordDoc.MainDocumentPart.Document.Save();
                    }

                    Console.WriteLine($"Text has been appended to the existing file: {filePath}");
                }
                else
                {
                    // If the file does not exist, create a new one
                    using (WordprocessingDocument wordDoc = WordprocessingDocument.Create(filePath, DocumentFormat.OpenXml.WordprocessingDocumentType.Document))
                    {
                        var mainPart = wordDoc.AddMainDocumentPart();
                        mainPart.Document = new Document();
                        var body = new Body();
                        body.AppendChild(new Paragraph(new Run(new Text(content))));
                        mainPart.Document.AppendChild(body);
                        mainPart.Document.Save();
                    }

                    Console.WriteLine($"A new document has been created: {filePath}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while working with the document: {ex.Message}");
            }
        }
    }
}
