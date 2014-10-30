echo Merging

ILMerge.exe /t:dll /targetplatform:v4,C:\Windows\Microsoft.NET\Framework\v4.0.30319 /out:CouchbaseDelivery.Tridion.ModularTemplates.merged.dll ..\src\tridion\content_manager\CouchbaseDelivery.Tridion.ModularTemplates\bin\Debug\CouchbaseDelivery.Tridion.ModularTemplates.dll ..\src\tridion\content_manager\CouchbaseDelivery.Tridion.ModularTemplates\bin\Debug\Newtonsoft.Json.dll

echo Uploading

TcmUploadAssembly /targeturl:http://willytridion.cloudapp.net /folder:tcm:3-20-2 /username:WILLYTRIDION\tridion /password:tr1d1on_ CouchbaseDelivery.Tridion.ModularTemplates.merged.dll

echo Removing files
del CouchbaseDelivery.Tridion.ModularTemplates.merged.dll
del CouchbaseDelivery.Tridion.ModularTemplates.merged.pdb

echo Complete