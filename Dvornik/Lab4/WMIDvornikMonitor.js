objService = GetObject("winmgmts:\\\\.\\root\\CIMV2"); 
colNamespaces = objService.InstancesOf("Win32_DesktopMonitor"); 
P=new Enumerator(colNamespaces);
for (i=1; !P.atEnd(); P.moveNext(),i++)
{ 
    WScript.Echo("------")
    WScript.Echo ("Monitor ",i);
    objItemC = P.item();
    Prop = new Enumerator(objItemC.Properties_);
    for (; !Prop.atEnd(); Prop.moveNext())
    {
        objItemC = Prop.item();
        WScript.Echo(objItemC.Name," - ",objItemC.Value);
    }
    WScript.Echo("------")
}

