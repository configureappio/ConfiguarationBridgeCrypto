using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using ConfigurationBridge.Configuration.Crypto;
using System.Runtime.CompilerServices;
using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using System.Windows;

namespace Encryptor.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {            
            AddNewCommand = new RelayCommand(AddNew);
            CopyToClipboardCommand = new RelayCommand(CopyToClipboard);
            DataValues = new ItemsChangeObservableCollection<EncryptedKeyPair>(new List<EncryptedKeyPair>());
            DataValues.CollectionChanged += DataValues_CollectionChanged;
        }

        private void DataValues_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(JsonDictionary));
        }

        private void CopyToClipboard()
        {
            Clipboard.SetData(DataFormats.UnicodeText, JsonDictionary);
        }

        private void AddNew()
        {
            const string defaultKeyName = "New Key";

            var keyName = defaultKeyName;

            if (DataValues.Any(k => k.RawKey == keyName))
            {



                var existingSameKey = DataValues.Where(k => k.RawKey.StartsWith(defaultKeyName))
                                          .Select(k => k.RawKey.Replace(defaultKeyName, String.Empty)).Select(
                                              k =>
                                              {
                                                  if (int.TryParse(k, out var value))
                                                  {
                                                      return value;
                                                  }

                                                  return 0;
                                              }
                                          ).Max() + 1;

                keyName = defaultKeyName + existingSameKey.ToString();
            }

            DataValues.Add(new EncryptedKeyPair(Algorithm) {RawKey = keyName, RawValue = "New Value"});
        }

        public Dictionary<string, Type> SymetricAlgorithms { get; } = SymetricAlgorithmTypes.GetTypes().ToDictionary(t => t.Name, t => t);

        public string LongestSymetricAlgorithmName => SymetricAlgorithms.Select(k => k.Key).Aggregate("", (max, cur) => max.Length > cur.Length ? max : cur);

        private string _password = "Password";

        public string Password  
        {
            get { return _password; }
            set
            {
                if (value != _password)
                {
                    _password = value;
                    OnPropertyChanged();
                    RefreshAlgorithm();
                }
            }
        }

        private string _salt = "Salt";

        public string Salt
        {
            get { return _salt; }
            set
            {
                if (value != _salt)
                {
                    _salt = value;
                    OnPropertyChanged();
                    RefreshAlgorithm();
                }
            }
        }

        private void RefreshAlgorithm()
        {
            Algorithm = new CryptoFactory().Create(SelectedAlgorithm, Password, Salt);
        }

        public ICryptoAlgorithm Algorithm
        {
            get { return algorithm;}
            set {
                if (value != algorithm)
                {
                    algorithm = value;
                    OnPropertyChanged();
                    foreach (var item in DataValues)
                    {
                        item.CryptoAlgorithm = Algorithm;
                    }
                }
            }
        }

        public Type SelectedAlgorithm
        {
            get
            {
                if (selectedAlgorithmType is null)
                {
                    SelectedAlgorithm = SymetricAlgorithms.First( sa => sa.Value.Equals(typeof(AesManaged))).Value;
                }

                return selectedAlgorithmType;
            }
            set
            {
                if (value != selectedAlgorithmType)
                {
                    selectedAlgorithmType = value;
                    OnPropertyChanged();
                    Algorithm = new CryptoFactory().Create(value, Password, Salt);
                    
                }
            }
        }
        
        public ObservableCollection<EncryptedKeyPair> DataValues { get; } = new ObservableCollection<EncryptedKeyPair>(new List<EncryptedKeyPair>());

        private ICryptoAlgorithm algorithm;

        private Type selectedAlgorithmType;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            RaisePropertyChanged(propertyName);
        }

        public RelayCommand AddNewCommand { get; private set; }

        public RelayCommand CopyToClipboardCommand { get; private set; }

        public string JsonDictionary => JsonConvert.SerializeObject(DataValues.ToDictionary(dv => dv.HashedKey, dv => dv.EncryptedValue),Formatting.Indented);


    }
}