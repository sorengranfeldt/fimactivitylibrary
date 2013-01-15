<#
.SYNOPSIS 
	Creates Activity Information Configuration (AIC) in FIM 2010
.DESCRIPTION 
	TBD.
.PARAMETER Uri
	The URI where the FIM 2010 Service can be reached. Default credentials are used for
	access, so the user logged in must have permission to access the FIM Service Schema
.PARAMETER DisplayName
	The display name of the AIC as it will appear in the FIM Portal
.PARAMETER Description
	The description of the AIC
.PARAMETER ActivityName
	TBD
.PARAMETER AssemblyName
	TBD
.PARAMETER TypeName
	TBD
.PARAMETER IsActionActivity
	TBD
.PARAMETER IsAuthenticationActivity
	TBD
.PARAMETER IsAuthorizationActivity
	TBD
.PARAMETER IsConfigurationType
	TBD
.NOTES 
	April 18, 2012 | Soren Granfeldt (soren@granfeldt.dk) 
		- initial version
.LINK 
    http://blog.goverco.com
#> 

[CmdletBinding()]
PARAM
(
	[string] $Uri = "http://localhost:5725",

	[Parameter(Mandatory=$true)]
	[ValidateNotNullOrEmpty()]
	[string] $DisplayName = "",
	
	[string] $Description = "",

	[Parameter(Mandatory=$true)]
	[ValidateNotNullOrEmpty()]
	[string] $ActivityName = "",

	[Parameter(Mandatory=$true)]
	[ValidateNotNullOrEmpty()]
	[string] $AssemblyName = "",

	[Parameter(Mandatory=$true)]
	[ValidateNotNullOrEmpty()]
	[string] $TypeName = "",

	[bool] $IsActionActivity = $false,
	[bool] $IsAuthenticationActivity = $false,
	[bool] $IsAuthorizationActivity = $false,
	[bool] $IsConfigurationType = $true
)

BEGIN 
{
	$me = $MyInvocation.MyCommand -replace '\.ps1$', ''
	Write-Debug "BEGIN: $Me"
	Import-Module (Join-Path $PWD "FIM2010-Module.psm1") -Force
	If(@(Get-PSSnapin | Where-Object {$_.Name -eq "FIMAutomation"} ).Count -eq 0) {Add-PSSnapin FIMAutomation}
}

PROCESS
{
	Write-Debug "PROCESS: $me"
	$ExistObject = Export-FIMConfig -Uri $Uri –OnlyBaseResources -CustomConfig "/ActivityInformationConfiguration[ActivityName='$DisplayName']"
	$NewObject = New-FIMImportObject -objectType "ActivityInformationConfiguration"
	If(!$ExistObject) {
		Set-FIMAttribute -object $NewObject -attributeName "DisplayName" -attributeValue $DisplayName
		Set-FIMAttribute -object $NewObject -attributeName "Description" -attributeValue $Description
		Set-FIMAttribute -object $NewObject -attributeName "ActivityName" -attributeValue $ActivityName
		Set-FIMAttribute -object $NewObject -attributeName "AssemblyName" -attributeValue $AssemblyName
		Set-FIMAttribute -object $NewObject -attributeName "TypeName" -attributeValue $TypeName
		Set-FIMAttribute -object $NewObject -attributeName "IsActionActivity" -attributeValue $IsActionActivity
		Set-FIMAttribute -object $NewObject -attributeName "IsAuthenticationActivity" -attributeValue $IsAuthenticationActivity
		Set-FIMAttribute -object $NewObject -attributeName "IsAuthorizationActivity" -attributeValue $IsAuthorizationActivity
		Set-FIMAttribute -object $NewObject -attributeName "IsConfigurationType" -attributeValue $IsConfigurationType
	
		$NewObject | Import-FIMConfig -uri $Uri 
		Write-Debug "$($NewObject.ObjectType.ToString()) '$DisplayName' created successfully"
		
		$CreatedObject = Export-FIMConfig -Uri $Uri –OnlyBaseResources -CustomConfig "/ActivityInformationConfiguration[ActivityName='$ActivityName']"
		$CreatedObject
	}
	else
	{
		Write-Warning "$($NewObject.ObjectType) '$DisplayName' already exists"
		$ExistObject
	}
}

END 
{
	Write-Debug "END: $me"
}

