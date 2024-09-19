[Download](https://github.com/kulkaGM/PlayerHome/releases/latest/download/PlayerHome.zip)

### Configuration
```xml
<?xml version="1.0" encoding="utf-8"?>
<Config xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
  <cancelOnMove>true</cancelOnMove>
  <delay>5</delay>
</Config>
```
### Translation
```xml
<?xml version="1.0" encoding="utf-8"?>
<Translations xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
  <Translation Id="onDriving" Value="{color=green}[Home]{/color} You cannot be teleported when being in a vehicle" />
  <Translation Id="noBed" Value="{color=green}[Home]{/color} You have no bed" />
  <Translation Id="alreadyTeleporting" Value="{color=green}[Home]{/color} You are already being teleported to your bed" />
  <Translation Id="teleportCancel" Value="{color=green}[Home]{/color} Unable to teleport you to bed, you died" />
  <Translation Id="teleporting" Value="{color=green}[Home]{/color} You will be teleported to your bed in {seconds} seconds" />
  <Translation Id="teleportedToBed" Value="{color=green}[Home]{/color} You've been teleported to your bed" />
  <Translation Id="onMoved" Value="{color=green}[Home]{/color} Teleport canceled, you moved" />
</Translations>
```