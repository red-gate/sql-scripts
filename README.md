#SQL Scripts#
SQL Scripts provides access to [SQL Server Central's community SQL scripts](http://www.sqlservercentral.com/Scripts/) from within SQL Server Management Studio (SSMS) 2005-2014.

SQL Scripts is written as an addin to the [SSMS Ecosystem project](http://documentation.red-gate.com/display/MA/SSMS+ecosystem+project) to integrate with SSMS.

##How to build and run SQL Scripts##
Based on the [instructions for built SSMS Ecosystem's sample addin](http://documentation.red-gate.com/display/MA/Building+the+sample+add-in).

###Step 1: Install SIPFramework###
SSMS Ecosystem uses SIPFramework to load addins into SSMS. [Download and run the EXE installer](http://documentation.red-gate.com/display/MA/Redistributing+the+framework). SIPFramework hooks into SSMS and is launched whenever SSMS starts. It is responsible for starting your add-in and providing it a clean interface to SSMS.

###Step 2: Compile SQL Scripts###
Once the NuGet package dependencies have been restored the solution should build. If it doesn't please get in touch.

###Step 3: Register the compiled sample with SIPFramework###
You need to make an entry in the Registry to tell our SIPframework where to load SQL SCripts from. You should create the registry entry in either:
* 32-bit machines: HKEY_LOCAL_MACHINE\SOFTWARE\Red Gate\SIPFramework\Plugins
* 64-bit machines: HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Red Gate\SIPFramework\Plugins

Create a new String Value (REG_SZ) with a unique name and set the value to the path of the sample's RedGate.SSC.Windows.Host.dll.

For example: D:\CurrentProjects\SQLScripts\Build\Debug\SampleSsmsEcosystemAddin.dll

###Step 4: Start SSMS###
You should see SQL Scripts in the toolbar. If not see trouble-shooting an add-in.