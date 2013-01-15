<#
.SYNOPSIS 
	Installing Assemblies to Global Assembly Cache (GAC)
.DESCRIPTION 
	This script is an alternative to the GACUTIL available in 
	the .NET Framework SDK. It will put the specified assembly
	in the GAC.
.EXAMPLE
    .\Add-AssemblyToGlobalAssemblyCache.ps1 -AssemblyName C:\Temp\MyWorkflow.dll
    
    This command will install the file MyWorkflow.dll from the C:\Temp directory in the GAC.
.EXAMPLE
    Dir C:\MyWorkflowAssemblies | % {$_.Fullname} | .\Add-AssemblyToGlobalAssemblyCache.ps1
    
    You can also pass the assembly filenames through the pipeline making it easy
    to install several assemblies in one run. The command abobe  will install 
    all assemblies from the directory C:\MyWorkflowAssemblies, run this command -
.PARAMETER AssemblyName
	Full path of the assembly file
.NOTES 
	April 18, 2012 | Soren Granfeldt (soren@granfeldt.dk) 
		- initial version
.LINK 
    http://blog.goverco.com
#>

[CmdletBinding()]
PARAM
(
	[Parameter(Mandatory=$true, ValueFromPipeline=$True,ValueFromPipelineByPropertyName=$True)]
	[ValidateNotNullOrEmpty()]
	[string] $AssemblyName = "",

	[switch]$PassThru
)
BEGIN 
{
	$me = $MyInvocation.MyCommand -Replace '\.ps1$', ''
	Write-Debug "BEGIN: $Me"
	if ( $null -eq ([AppDomain]::CurrentDomain.GetAssemblies() |? { $_.FullName -eq "System.EnterpriseServices, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" }) ) {
		[System.Reflection.Assembly]::Load("System.EnterpriseServices, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a") | Out-Null
	}
	$PublishObject = New-Object System.EnterpriseServices.Internal.Publish
}

PROCESS {
	Write-Debug "PROCESS: $me"
          
	foreach ($Assembly in $AssemblyName)
	{
        if ( -not (Test-Path $Assembly -type Leaf) ) 
        {
            throw "The assembly '$Assembly' does not exist."
        }

        $LoadedAssembly = [System.Reflection.Assembly]::LoadFile($Assembly)

        if ($LoadedAssembly.GetName().GetPublicKey().Length -eq 0) 
        {
            throw "The assembly '$Assembly' must be strongly signed."
        }
          
        Write-Verbose "Installing: $Assembly"
        $PublishObject.GacInstall($Assembly)

        if ($PassThru) { $_ }
    }
}

END 
{
    Write-Debug "END: $me"
}
