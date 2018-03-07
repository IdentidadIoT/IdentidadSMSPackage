## Installation

The easiest way to install the library is using Package Manager Console from Visual Studio, a package manager console for .NET. Simply run this in the terminal:

    Install-Package YellowPushSMS -Version 1.3.0

YellowPush API needs your YellowPush credentials. You can either pass these directly to the constructor (see the code below).

### Send an SMS

```csharp

using YellowPushSMSPackage;
using YellowPushSMSPackage.Models;

YellowPushSMS sms = new YellowPushSMS("username", "password");
YellowPushSMSResponse response = sms.SendSMS("from", "message", "mobileNumberOne, mobileNumberTwo");

```

```csharp

using YellowPushSMSPackage;
using YellowPushSMSPackage.Models;

YellowPushSMS sms = new YellowPushSMS("username", "password");
YellowPushSMSResponse response = sms.SendSMS("from", "message", "mobileNumberOne", "mobileNumberTwo");

```

### Send Bulk SMS

```csharp

using System.Collections.Generic;
using YellowPushSMSPackage;
using YellowPushSMSPackage.Models;

YellowPushSMS sms = new YellowPushSMS("username", "password");

List<BulkSMS> lstMessages = new List<BulkSMS>()
{
    new BulkSMS() { From = "from", Message = "Message One", MobileNumber = "xxxxxxxxxxxx"},
    new BulkSMS() { From = "from", Message = "Message Two", MobileNumber = "xxxxxxxxxxxx"}
};

YellowPushSMSResponse response = sms.BulkSendSMS(lstMessages);

```

### Gets message status

```csharp

using YellowPushSMSPackage;
using YellowPushSMSPackage.Models;

YellowPushSMS sms = new YellowPushSMS("username", "password");
YellowPushSMSResponse response = sms.GetMessageStatus("5a5600d4-e8fb-6db2-0815", new System.DateTime(2018, 3, 1));

```