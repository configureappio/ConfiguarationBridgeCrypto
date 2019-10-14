using ConfigurationBridge.Configuration.Crypto;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Encryptor
{
    public class EncryptedKeyPair : INotifyPropertyChanged
    {
        private string _rawKey;
        private string _rawValue;

        public EncryptedKeyPair(ICryptoAlgorithm cryptoAlgorithm)
        {
            CryptoAlgorithm = cryptoAlgorithm;
        }

        public string RawKey
        {
            get => _rawKey;
            set {
                if (_rawKey == value) return;

                _rawKey = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(HashedKey));
                OnPropertyChanged(nameof(EncryptedValue));
            }
        }

        public string RawValue
        {
            get => _rawValue;
            set
            {
                if (_rawValue == value) return;
                _rawValue = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(HashedKey));
                OnPropertyChanged(nameof(EncryptedValue));
            }
        }

        private ICryptoAlgorithm _cryptoAlgorithm;

        public ICryptoAlgorithm CryptoAlgorithm
        {
            get => _cryptoAlgorithm;
            set
            {
                if (_cryptoAlgorithm == value) return;
                _cryptoAlgorithm = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(HashedKey));
                OnPropertyChanged(nameof(EncryptedValue));
            }
        }

        public string HashedKey => CryptoAlgorithm.HashKey(RawKey);

        public string EncryptedValue => CryptoAlgorithm.Encrypt(RawValue);

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}