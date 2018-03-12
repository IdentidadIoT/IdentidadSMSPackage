![YellowPush](https://www.identidadsms.net/yellowpush/wp-content/uploads/2018/02/logo-Yellow-Push.png)

# YellowPush SMS API - .NET

## Installation

The easiest way to install the library is using Package Manager Console from Visual Studio, a package manager console for .NET. Simply run this in the terminal:

    Install-Package YellowPushSMS -Version 2.5.0

### YellowPushSMS Reference

YellowPush API needs your YellowPush credentials. You can either pass these directly to the constructor (see the code below).

```csharp

YellowPushSMS sms = new YellowPushSMS("username", "password");

```

**NOTE:** For better performance you can pass credentials and the account Identifier directly to the constructor (see the code below)

```csharp

YellowPushSMS sms = new YellowPushSMS("username", "password", "accountId");

```
### YellowPushSMS parameters:	

- username: Your account user
- password: Your account password 
- accountId: Your account identifier (optional)

### Send an SMS

Sends a text message to one or more mobile numbers

```csharp

using YellowPushSMSPackage;
using YellowPushSMSPackage.Models;

YellowPushSMS sms = new YellowPushSMS("username", "password", "accountId");
YellowPushSMSResponse response = sms.SendSMS("from", "message", "mobileNumberOne, mobileNumberTwo");

```

```csharp

using YellowPushSMSPackage;
using YellowPushSMSPackage.Models;

YellowPushSMS sms = new YellowPushSMS("username", "password", "accountId");
YellowPushSMSResponse response = sms.SendSMS("from", "message", "mobileNumberOne", "mobileNumberTwo");

```

### Send Bulk SMS

Sends single, bulk text messages

```csharp

using System.Collections.Generic;
using YellowPushSMSPackage;
using YellowPushSMSPackage.Models;

YellowPushSMS sms = new YellowPushSMS("username", "password", "accountId");

List<BulkSMS> lstMessages = new List<BulkSMS>()
{
    new BulkSMS() { From = "from", Message = "Message One", MobileNumber = "mobileNumberOne"},
    new BulkSMS() { From = "from", Message = "Message Two", MobileNumber = "mobileNumberTwo"}
};

YellowPushSMSResponse response = sms.BulkSendSMS(lstMessages);

```

### Gets message status

Gets the messages satatus

```csharp

using YellowPushSMSPackage;
using YellowPushSMSPackage.Models;

YellowPushSMS sms = new YellowPushSMS("username", "password", "accountId");
YellowPushSMSResponse response = sms.GetMessageStatus("messsageId", new System.DateTime(2018, 3, 1));

```