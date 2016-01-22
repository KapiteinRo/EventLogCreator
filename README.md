EventLogCreator
===============

Simple tool to create Windows Eventlogs for your .NET applications under *'Applications and Services Logs'*.

## Usage
Just open a commandline prompt in Administrator-mode, and punch the following:

```
> EventLogCreator MyLog1 MyLog2
```

And then you can use it in your code:

### C# example
Now you can simply write entries in your log:

```C#
using (EventLog log = new EventLog { Source = "MyLog1" })
{
    log.WriteEntry("My first log-entry!", EventLogEntryType.Information, 1234);
}
```

**Note:** EventId's are a short integer type, so maximum value is 65535.

## Contributing
Yes, I would love me some pull requests. If you do, please update the AUTHORS file as well.

#### Features I would love to add

 * An list-option to list previously created eventlogs.
 * Maybe some fancier error handling?
 * Rollbacking.
 * Documentation would be nice.
 * Any other great ideas.

#### Features I would rather not add

 * GUI-version. If you want one, just copypasta my code and create one yourself.

## License
Copyright (c) 2016 Royi Eltink
License: MIT