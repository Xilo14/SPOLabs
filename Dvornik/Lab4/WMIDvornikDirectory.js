argv=WScript.Arguments;
WScript.Echo("Directories in "+argv(0));
if (argv.Length==0) {WScript.Echo("no parameters");}
else{
  a=argv(0);
  Drive = a.slice(0,2)
  Path = a.slice(2)
  objClass = GetObject("winmgmts:\\\\.\\Root\\CIMV2");
  //WScript.Echo(Drive);
  //WScript.Echo(Path);
  Disks=objClass.ExecQuery("Select * from Win32_Directory  Where Path = '"+Path+"' and Drive = '"+Drive+"'");
  P=new Enumerator(Disks);
  for (; !P.atEnd(); P.moveNext())
  { 
    objItemC = P.item();
    s = objItemC.Caption;
    WScript.Echo(s);
  }
}


