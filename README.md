# Configuaration Bridge Crypto Demo

This repo contains demo code that shows how to apply the GoF Bridge pattern to take a SOLID approach to configuration in an ASP.Net Core application.

The key features are:

* Abstraction of a settings class to an interface
* Binding configuration to a reader DTO
* Using a bridging class that implements the interface and hides IOptionsSnapshot<T> from consumers
* Injection of other functionality into the bridging class such as decryption

Full details on my blog at  http://stevetalkscode.co.uk/configuration-bridging-part-4

## <u>Dislaimer</u>

This code is for demonstration purposes only to illustrate the concepts in the blog post and is not intended for production release.

Feel free to copy the code and adapt it for your use, but no responsibility is accepted for any consequences of its use.

### A Reminder ...

* The code makes use of hard coded encryption keys - DO NOT DO THIS IN THE REAL WORLD!
* It makes assumptions about where the settings are stored on disk - do not take these verbatim

Known issues

* The WPF applicatation is provided as an example of generating encrypted JSON data. It is far from perfect and is provided simply to give a quick way of testing different algorithms. 
