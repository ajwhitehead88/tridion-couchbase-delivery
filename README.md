# Tridion Couchbase Delivery
A very early stages broker replacement using couchbase

Ensure the Tridion JARs are added to a local Maven repository using the following commands (in a folder with the JARs from %TRIDION_HOME%\lib)
```
mvn install:install-file -Dfile="cd_broker.jar" -DgroupId=com.tridion -DartifactId=cd_broker -Dversion=7.1.0 -Dpackaging=jar
mvn install:install-file -Dfile="cd_cache" -DgroupId=com.tridion -DartifactId=cd_broker -Dversion=7.1.0 -Dpackaging=jar
mvn install:install-file -Dfile="cd_core" -DgroupId=com.tridion -DartifactId=cd_broker -Dversion=7.1.0 -Dpackaging=jar
mvn install:install-file -Dfile="cd_datalayer" -DgroupId=com.tridion -DartifactId=cd_broker -Dversion=7.1.0 -Dpackaging=jar
mvn install:install-file -Dfile="cd_linking" -DgroupId=com.tridion -DartifactId=cd_broker -Dversion=7.1.0 -Dpackaging=jar
mvn install:install-file -Dfile="cd_model" -DgroupId=com.tridion -DartifactId=cd_broker -Dversion=7.1.0 -Dpackaging=jar
mvn install:install-file -Dfile="cd_tcdl" -DgroupId=com.tridion -DartifactId=cd_broker -Dversion=7.1.0 -Dpackaging=jar
mvn install:install-file -Dfile="cd_undo" -DgroupId=com.tridion -DartifactId=cd_broker -Dversion=7.1.0 -Dpackaging=jar
mvn install:install-file -Dfile="cd_wrapper" -DgroupId=com.tridion -DartifactId=cd_broker -Dversion=7.1.0 -Dpackaging=jar
```
