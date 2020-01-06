# quick start with NServiceBus

# Update NServiceBus license
 - copy License.xml to root folder
 - run command "mkdir -p ${XDG_DATA_HOME:-$HOME/.local/share}/ParticularSoftware"
 - run command "cp license.xml ${XDG_DATA_HOME:-$HOME/.local/share}/ParticularSoftware"

# Reconfigure the DB 
 - remove DB "NServiceBusHost" if needed.
 - Create DataBase ([InitSqlDB.sql](https://github.com/polux2016/qs-nservicebus/blob/add-nsb-transport-in-docker/Shared/InitSqlDB.sql)).
 - Set GlobalConfiguration.IsReconfigureMode = true.
 - Rerun all projects.
 - Set GlobalConfiguration.IsReconfigureMode = false.