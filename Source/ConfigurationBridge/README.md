# Dot Net Configuration Is Easy ... Right?

Repository for slide deck and demo of my talk ".NET Configuration Is Easy ... Right"

The following needs to be set up on your machine before the demo will work
    
(1) Create a secrets.json file in the root of the C: drive and copy the following into 

<pre>{
  "MyAppSettings": {
    "Secrets": {
      "u+jYf+jVn92kMstZGaJW8hF7ihkRPlAMeuXcREHafFCNbxIAXloOJ2+Mlk90pXrf": "jxM4YAqLdSNEqholAgQaCWHbU/F2WOUxS3400zIZCH36loQcpfW2MojpGJ0wcG4y5ugvl7ON0iKBFqRvIx623g==",
      "CJAsyKTsC6Qi9xh4QvmmK+lFl6Jv+A+CaXgKlgy8a6mKOlkeqn0kuKNtyQXg20fH": "jxM4YAqLdSNEqholAgQaCYQOWnxbLQkm9XA+2jjpMlgb3nhnxsCJy782u0NftnEuohj44x73aTGrBjyKBL/RSlNMzR2Z+dR3nSxIq+koFvo="
    }
  }
}</pre>
 
If you do not have access to the root of C:, change line 24 of Program.cs to use a location that you do have access to

<pre>
private static void AddCustomConfiguration(WebHostBuilderContext ctx, IConfigurationBuilder builder)
{
	builder.AddJsonFile(
	new PhysicalFileProvider(
	@"C:\"),
	@"secrets.json",
	true,
	true);
}
</pre>

(2) In Visual Studio, right click the ConfigurationBridge.Web project and click Manage Secrets to open the secrets.json file in your user profile

Copy the following into the file

<pre>{
	"CryptoSalt": "Salt",
	"CryptoPwd": "Password"
}</pre>