using System.Collections.ObjectModel;
using System.Windows;

namespace Encryptor.ViewModel
{
    public class DictionaryKeyValidationRuleBindingWrapper : DependencyObject
    {
        public static readonly DependencyProperty ExistingValuesProperty =
            DependencyProperty.Register("ExistingValues", typeof(ObservableCollection<EncryptedKeyPair>),
                typeof(DictionaryKeyValidationRuleBindingWrapper), new FrameworkPropertyMetadata(new ObservableCollection<EncryptedKeyPair>(),
                    (o, args) =>
                    {

                    }));

        public ObservableCollection<EncryptedKeyPair> ExistingValues
        {
            get { return (ObservableCollection<EncryptedKeyPair>) GetValue(ExistingValuesProperty); }
            set { SetValue(ExistingValuesProperty, value); }
        }
        
    }
}