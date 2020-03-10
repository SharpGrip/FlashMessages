# SharpGrip FlashMessages [![NuGet](https://img.shields.io/nuget/v/SharpGrip.FlashMessages)](https://www.nuget.org/packages/SharpGrip.FlashMessages)

## Introduction
SharpGrip FlashMessages is a messaging system for adding and reading flash messages.

## Installation
Reference NuGet package `SharpGrip.FlashMessages` (https://www.nuget.org/packages/SharpGrip.FlashMessages).

Add the FlashMessages messaging system to your service collection via the extension method.

```
public void ConfigureServices(IServiceCollection services)
{
    services.AddFlashMessages();
}
```

## Configuration

### Properties
| Property      | Default value                              | Description                                                                      |
| ------------- | ------------------------------------------ | -------------------------------------------------------------------------------  |
| StorageKey    | `"__FlashMessages__"`                      | The key used to store message data in the `ITempDataDictionary` session storage. |

### Via the `services.AddFlashMessages()` extension method
```
using SharpGrip.FlashMessages.Extensions;

public void ConfigureServices(IServiceCollection services)
{
    services.AddFlashMessages(options =>
    {
        options.StorageKey = "SomeOtherStorageKey";
    });
}
```

### Via the `services.Configure()` method
```
using SharpGrip.FlashMessages.Extensions;

public void ConfigureServices(IServiceCollection services)
{
    services.AddFlashMessages();

    services.Configure<FlashMessagesOptions>(options =>
    {
        options.StorageKey = "SomeOtherStorageKey";
    });
}
```

## Usage

### Adding messages
Inject the `IFlasher` service and call the `IFlasher.Add(messageType, message)` method.

```
flasher.Add("success", "My very first success message!");
flasher.Add("info", "My very first info message!");
flasher.Add("warning", "My very first warning message!");
flasher.Add("danger", "My very first danger message!");
```

### Reading messages
Inject the `IFlasher` service and call the `IFlasher.GetMessages()` method.

```
@using SharpGrip.FlashMessages
@inject IFlasher Flasher

<div class="alerts">
    @foreach (var message in Flasher.GetMessages())
    {
        <div class="row alert alert-@message.Type alert-dismissible fade show" role="alert">
            @message.Text
            <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                <span aria-hidden="true">&times;</span>
            </button>
        </div>
    }
</div>
```

### Peeking at messages
Inject the `IFlasher` service and call the `IFlasher.PeekMessages()` method. Peeking at messages will not clear the messages present in the message store.

```
var messages = flasher.PeekMessages();
```

### Clearing messages
Inject the `IFlasher` service and call the `IFlasher.Clear()` method.

```
flasher.Clear();
```

**Note:** Clearing the message store is not necessary when reading messages via the `IFlasher.GetMessages()` method.