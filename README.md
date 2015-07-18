# WmiFileBrowser
A simple .NET library for accessing the filesystem on local or remote computer via WMI. Only requires access to WMI root\cimv2 namespace.

*Minimal requirements: .NET Framework 4.0 Client Profile*

A dynamic class library written in C# (WmiFileBrowser.dll) with a WinForms example of usage. To start using the library construct **WmiFileBrowser.WmiFileBrowser** object and call **ConnectToHost** method providing the address of the host with a valid username and password (if needed).

To 'navigate' the browser use the following methods: **SetInitialPath** (to reset the browser history and set the desired path as the first one in browser history), **GoToPath** (to set the path as the current one - the browser history will be updated), **GoBack** and **GoForward** (to navigate through the browser history).

After setting the path call **GetData** method to get a list of 'WmiFileBrowser.Interfaces.IFileDescriptor' objects. The following properties of the object can be accessed:

 > **Type** - a Drive, a Directory or a File;

 > **ObjectPath** - the ManagementPath of the WMI object;

 > **PropertyNames** - an array of the names of the available properties of the object.

 > The method **GetPropertyValue** provided with the name of the property should be used to retrieve the value of the property.

**WmiFileBrowser** object also has a number of settable properties:

 > **ReturnFullInfo** - if set to 'true' the 'GetData' method will return objects with an extended set of properties (by default only the significant properties are returned);

 > **ShowDirectoriesOnly** - if set to 'true' only logical disks and directories will be returned by the 'GetData' method;

 > **FileExtensions** - if set to an array of strings representing file extensions (without a dot in the beginning) only the files with the specified extensions will be returned by the 'GetData' method.
 
There is also a set of properties and methods provided by the **WmiFileBrowser** object, each of them has self-explanatory C# XML documentation comments.
 
***Please, inform me of any bugs you happen to encounter via email (shown on my profile page). Any feedback will be highly appreciated.***
