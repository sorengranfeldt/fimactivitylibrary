﻿If you add one of the Helper workflow to an existing workflow and you get 
the error 'Cannot use custom activities - failed to load toolbox item', it may 
be because you have the target assembly Granfeldt.FIM.ActivityLibrary.dll 
in the GAC already. Try removing it from C:\Windows\Assembly or add
a version with a working activity compiled into it.
